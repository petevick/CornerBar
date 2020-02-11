using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CornerBar.Classes;
using CornerBar.Helpers;
using CornerBar.Resources;
using Plugin.ExternalMaps;
using Plugin.Messaging;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CornerBar.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactUs : ContentPage
    {
        //https://www.google.com/maps/dir//Carrer+Hort+des+Lle%C3%B3,+11,+07740+Puerto+Luz,+Illes+Balears,+Spain/@40.006973,4.192685,181m/data=!3m1!1e3!4m9!4m8!1m0!1m5!1m1!1s0x12be25caca0bdf5b:0xb242babbbae83fe3!2m2!1d4.1925983!2d40.0068972!3e0?hl=en-GB

        public ContactUs()
        {
            InitializeComponent();
            if (Device.OS == TargetPlatform.iOS)
            {
                NavigationPage.SetHasNavigationBar(this, true);
            }
            
        }
        protected override void OnAppearing()
        {
            
            base.OnAppearing();
            App.pressed = false;
        }
        private void BtnPhone_OnClicked(object sender, EventArgs e)
        {
            // Make Phone Call
            Utilities.Enable_Button((Button)sender, false);
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
            Utilities.Enable_Button((Button)sender, true);
        }

        private async void BtnEmail_OnClicked(object sender, EventArgs e)
        {
            Utilities.Enable_Button((Button)sender, false);
            try
            {
                List<string> recips = new List<string>();
                recips.Add(DetailsExtension.DetailsManager.Details("email").ToString());
                var message = new EmailMessage
                {
                    Subject = "Enquiry",
                    Body = "",
                    To = recips
                };
                await Email.ComposeAsync(message);
               }
            catch (FeatureNotSupportedException fbsEx)
            {
                // Email is not supported on this device
            }
            catch (Exception ex)
            {
                // Some other exception occurred
            }
            Utilities.Enable_Button((Button)sender, true);
        }

        private void BtnWeb_OnClicked(object sender, EventArgs e)
        {
            Utilities.Enable_Button((Button)sender, false);
            string web = DetailsExtension.DetailsManager.Details("www");
            if (web.Substring(0, 6) != "http://")
            {
                web = "http://" + web;
            }
            Uri website = new Uri(web);
            Device.OpenUri(website);
            Utilities.Enable_Button((Button)sender, true);
        }

        private void BtnDirections_OnClicked(object sender, EventArgs e)
        {
            Utilities.Enable_Button((Button)sender, false);
            Uri googledirections = new Uri(DetailsExtension.DetailsManager.Details("directions"));
            Device.OpenUri(googledirections);
            Utilities.Enable_Button((Button)sender, true);
        }

        private void facebook_OnTapped(object sender, EventArgs e)
        {
            Utilities.Enable_Image((Image)sender, false);
            Uri facebookUri = new Uri(DetailsExtension.DetailsManager.Details("facebook"));
            Device.OpenUri(facebookUri);
            Utilities.Enable_Image((Image)sender, true);
        }
        private void twitter_OnTapped(object sender, EventArgs e)
        {
            Utilities.Enable_Image((Image)sender, false);
            Uri twitterUri = new Uri(DetailsExtension.DetailsManager.Details("twitter"));
            Device.OpenUri(twitterUri);
            Utilities.Enable_Image((Image)sender, true);
        }
        private void tripadvisor_OnTapped(object sender, EventArgs e)
        {
            Utilities.Enable_Image((Image)sender, false);
            Uri tripadvisorUri = new Uri(DetailsExtension.DetailsManager.Details("tripadvisor"));
            Device.OpenUri(tripadvisorUri);
            Utilities.Enable_Image((Image)sender, true);
        }

        private void map_OnTapped(object sender, EventArgs e)
        {
            Utilities.Enable_Image((Image)sender, false);
            Uri mapUri = new Uri(DetailsExtension.DetailsManager.Details("googlemap"));
            Device.OpenUri(mapUri);
            Utilities.Enable_Image((Image)sender, true);
        }
    }
}
