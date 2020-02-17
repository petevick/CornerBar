using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CornerBar.Helpers;
using CornerBar.Models;
using CornerBar.Resources;
using Plugin.Messaging;
using Plugin.Settings;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Globalization;
using CornerBar.Classes;

namespace CornerBar.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookATable : ContentPage
    {
        private TimeSpan _timeSpan;

        private static string emailSubject = TranslateExtension.TranslationManager.Translate("Book");
        private static DateTime bookingDateTime;
        MainPageViewModel mvp = new MainPageViewModel();

        public  BookATable(string subject)
        {
            InitializeComponent();

            CultureInfo.DefaultThreadCurrentCulture = App.Ci;
            CultureInfo.DefaultThreadCurrentUICulture = App.Ci;

            if (Device.OS == TargetPlatform.iOS)
            {
                NavigationPage.SetHasNavigationBar(this, true);
            }
            Utilities.open_close_page("Open", this.GetType().Name);
        }


       protected async override void OnAppearing()
        {

            base.OnAppearing();

            txtemail.Text = CrossSettings.Current.GetValueOrDefault("email", string.Empty);
            txtphone.Text = CrossSettings.Current.GetValueOrDefault("phone", string.Empty);
            if (string.IsNullOrEmpty(txtemail.Text))
            {
                if (App.language == "en")
                {
                    txtphone.Text = "+44";
                }
            }
            string[] occs = TranslateExtension.TranslationManager.Translate("Occasions").Split('|');

            for (int i = 0; i < occs.GetUpperBound(0); i++)
            {
                pckOccasion.Items.Add(occs[i]);
            }

            pckOccasion.SelectedIndex = 0;
            pckTime.BindingContext = App.mv;

            App.pressed = false;
        }

        private void Stepper_OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            lblPeople.Text = e.NewValue.ToString();
        }

        private void BtnPhone_OnClicked(object sender, EventArgs e)
        {
            // Make Phone Call

            try
            {
                PhoneDialer.Open(DetailsExtension.DetailsManager.Details("phonetodial"));
            }
            catch (ArgumentNullException anEx)
            {
                // Number was null or white space
            }
            catch (FeatureNotSupportedException ex)
            {
                // Phone Dialer is not supported on this device.
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }


        }

        private void BtnEmail_OnClicked(object sender, EventArgs e)
        {
            send_an_email("Enquiry", "");
        }

        private void DatePicker_OnDateSelected(object sender, DateChangedEventArgs e)
        {

        }

        private async static Task<bool> send_an_email(string subject, string body)
        {

            try
            {
                List<string> recips = new List<string>();
                recips.Add(DetailsExtension.DetailsManager.Details("email").ToString());
                var message = new EmailMessage
                {
                    Subject = subject,
                    Body = body,
                    To = recips
                };
                await Email.ComposeAsync(message);
                return true;
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                // Email is not supported on this device
            }
            catch (Exception ex)
            {
                // Some other exception occurred
            }
            
            return false;
        }

        private async void BtnBook_OnClicked(object sender, EventArgs e)
        {
            var result = await validates_ok();

            if (result == false)
            {
                return;
            }

            string emailBody =
                TranslateExtension.TranslationManager.Translate("MailMess")
                    .Replace("!CR", Environment.NewLine)
                    .Replace("!P1", lblPeople.Text)
                    .Replace("!P2", string.Format("{0:dd/MMM/yyyy}", bookingDateTime))
                    .Replace("!P3", string.Format("{0:hh:mm tt}", bookingDateTime))
                    .Replace("!P4", DetailsExtension.DetailsManager.Details("name"));

            emailBody += Environment.NewLine;
            emailBody +=
                TranslateExtension.TranslationManager.Translate("MailMessTel")
                    .Replace("!P1", txtphone.Text)
                    .Replace("!CR", Environment.NewLine);
            emailBody +=
                TranslateExtension.TranslationManager.Translate("MailMessEmail")
                    .Replace("!P1", txtemail.Text)
                    .Replace("!CR", Environment.NewLine);

            if (pckOccasion.SelectedIndex != 0)
            {
                emailBody +=
                    TranslateExtension.TranslationManager.Translate("MailMessOcc")
                        .Replace("!P1", pckOccasion.Items[pckOccasion.SelectedIndex].ToString())
                        .Replace("!CR", Environment.NewLine);
            }

            emailBody += TranslateExtension.TranslationManager.Translate("MailMessThanks")
                .Replace("!CR", Environment.NewLine);


            CrossSettings.Current.AddOrUpdateValue("email", txtemail.Text);
            CrossSettings.Current.AddOrUpdateValue("phone", txtphone.Text);


            Boolean res = await (send_an_email(emailSubject, emailBody));

            if (res)
            {
                await
                    DisplayAlert(TranslateExtension.TranslationManager.Translate("BookingConfCap"),
                        TranslateExtension.TranslationManager.Translate("BookingConf"),
                        TranslateExtension.TranslationManager.Translate("OK"));
            }
            else
            {
                await
                    DisplayAlert(TranslateExtension.TranslationManager.Translate("BookingFailCap"),
                        TranslateExtension.TranslationManager.Translate("BookingFail"),
                        TranslateExtension.TranslationManager.Translate("OK"));
            }



        }

        private async Task<bool> validates_ok()
        {
            if (Convert.ToInt32(pckDate.Date.DayOfWeek) == get_closed_day_number(DetailsExtension.DetailsManager.Details("closedon")))
            {
                await
                    DisplayAlert(TranslateExtension.TranslationManager.Translate("IsClosedCap").Replace("!P1",TranslateExtension.TranslationManager.Translate(DetailsExtension.DetailsManager.Details("closedon"))),
                        TranslateExtension.TranslationManager.Translate("IsClosed").Replace("!P1", TranslateExtension.TranslationManager.Translate(DetailsExtension.DetailsManager.Details("closedon"))),
                        TranslateExtension.TranslationManager.Translate("OK"));
                pckDate.Focus();
                return false;
            }


            if (string.IsNullOrEmpty(txtemail.Text))
            {
                await
                    DisplayAlert(TranslateExtension.TranslationManager.Translate("MissingEmailCap"),
                        TranslateExtension.TranslationManager.Translate("MissingEmail"),
                        TranslateExtension.TranslationManager.Translate("OK"));
                txtemail.Focus();
                return false;
            }
            if (!valid_email())
            {
                var answer =
                await
                    DisplayAlert(TranslateExtension.TranslationManager.Translate("AreYouSure"),
                        TranslateExtension.TranslationManager.Translate("Invalidemail"),
                        TranslateExtension.TranslationManager.Translate("Yes"),
                            TranslateExtension.TranslationManager.Translate("No"));
                if (answer == false)
                {
                    txtemail.Focus();
                    return false;
                }
            }
            if (string.IsNullOrEmpty(txtphone.Text))
            {
                await
                    DisplayAlert(TranslateExtension.TranslationManager.Translate("MissingPhoneCap"),
                        TranslateExtension.TranslationManager.Translate("MissingPhone"),
                        TranslateExtension.TranslationManager.Translate("OK"));
                txtphone.Focus();
                return false;
            }

            string phoneNo = string.Empty;
            if (txtphone.Text.Substring(0, 1) == "+")
            {
                phoneNo = txtphone.Text.Substring(1, txtphone.Text.Length - 1);
            }
            else
            {
                phoneNo = txtphone.Text;
            }

            if (!numeric_phone_number(phoneNo))
            {
                var answer =
                    await
                        DisplayAlert(TranslateExtension.TranslationManager.Translate("AreYouSure"),
                            TranslateExtension.TranslationManager.Translate("InvalidPhone"),
                            TranslateExtension.TranslationManager.Translate("Yes"),
                            TranslateExtension.TranslationManager.Translate("No"));
                if (answer == false)
                {
                    txtphone.Focus();
                    return false;
                }
            }


            string sStart = DetailsExtension.DetailsManager.Details("startbooking");
            string sEnd = DetailsExtension.DetailsManager.Details("endbooking");
            string[] stime = sStart.Split(',');
            string[] etime = sEnd.Split(',');
            TimeSpan startTime = new TimeSpan( Convert.ToInt32(stime[0]), Convert.ToInt32(stime[1]), Convert.ToInt32(stime[2]));
            //10 o'clock
            TimeSpan endTime = new TimeSpan( Convert.ToInt32(etime[0]), Convert.ToInt32(etime[1]), Convert.ToInt32(etime[2])); 
            bookingDateTime = new DateTime(pckDate.Date.Year, pckDate.Date.Month, pckDate.Date.Day, pckTime.Time.Hours,
                pckTime.Time.Minutes, 0);
            TimeSpan bookingTime = new TimeSpan(pckTime.Time.Hours,
                pckTime.Time.Minutes, 0);

            if (bookingDateTime < DateTime.Now)
            {
                await
                    DisplayAlert(TranslateExtension.TranslationManager.Translate("TooEarlyCap"),
                        TranslateExtension.TranslationManager.Translate("TooEarly"),
                        TranslateExtension.TranslationManager.Translate("OK"));
                pckTime.Focus();
                return false;
            }

            if ((bookingTime >= startTime) && (bookingTime <= endTime))
            {
                return true;
            }

            if ((bookingTime <= startTime) || (bookingTime >= endTime))
            {
                await
                    DisplayAlert(TranslateExtension.TranslationManager.Translate("InvalidBookCap"),
                        TranslateExtension.TranslationManager.Translate("InvalidBook"),
                        TranslateExtension.TranslationManager.Translate("OK"));
                pckTime.Focus();
                return false;

            }


            return true;
        }

        private bool numeric_phone_number(string phoneNo)
        {
            int num1;
            int iStart = 0;
            if (phoneNo.Substring(0, 1) == "+")
            {
                iStart = 1;
            }
            for (int i = iStart; i < phoneNo.Length - 1; i++)
            {
                bool res = int.TryParse(phoneNo.Substring(i, 1), out num1);
                if (res == false)
                {
                    return false;
                }
            }

            if (phoneNo.Length < 10)
            {
                return false;
            }

            return true;
        }

        private bool valid_email()
        {
            const string phoneRegex = @"^([a-zA-Z0-9]+(?:[.-]?[a-zA-Z0-9]+)*@[a-zA-Z0-9]+(?:[.-]?[a-zA-Z0-9]+)*\.[a-zA-Z]{2,7})$";
 
            return (Regex.IsMatch(txtemail.Text, phoneRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
        }
        private static int get_closed_day_number(string dayname)
        {
            switch (dayname.ToUpper())
            {
                case "SUNDAY":
                    return 0;
                    break;
                case "MONDAY":
                    return 1;
                    break;
                case "TUESDAY":
                    return 2;
                    break;
                case "WEDNESDAY":
                    return 3;
                    break;
                case "THURSDAY":
                    return 4;
                    break;
                case "FRIDAY":
                    return 5;
                    break;
                case "SATURDAY":
                    return 6;
                    break;

            }
            return -1;
        }
        private void PckTime_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Debug.WriteLine(e.PropertyName);
            if (e.PropertyName == "Time")
            {
                Debug.WriteLine(e.PropertyName);
                Debug.WriteLine(App.mv.BookingTime);
                Debug.WriteLine(pckTime);
            }
        }
        protected override void OnDisappearing()
        {
            Utilities.open_close_page("Close", this.GetType().Name);
            base.OnDisappearing();
        }
    }
    
}
