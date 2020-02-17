using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CornerBar.Classes;
using CornerBar.Helpers;
using CornerBar.Models;
using CornerBar.Resources;
using Syncfusion.Pdf.Interactive;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CornerBar.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutUs : ContentPage
    {
        bool firstActive = true;
        private static string carousel_image = "Food";
        private bool isActive = false;
       
        public AboutUs()
        {
            InitializeComponent();
            if (Device.RuntimePlatform == Device.iOS)
            {
                NavigationPage.SetHasNavigationBar(this, true);
            }
            switch (Device.RuntimePlatform)
            {
                case Device.UWP:
                    //this.Padding = new Thickness(0, 0, 0, 80);
                    break;

                case Device.iOS:
                    this.Padding = new Thickness(0, 0, 0, 0);
                    break;
            }
            Utilities.open_close_page("Open", this.GetType().Name);
            
            App.mv.BlurbCount = 1;
            
            lblHeading.Text = TranslateExtension.TranslationManager.Translate("Head1");
            //lblBlurb.Text = DetailsExtension.DetailsManager.Details(string.Format("blurb{0}", App.language.ToUpper()));
            lblBlurb.Text = TranslateExtension.TranslationManager.Translate("AboutBlurb");
            //lblDetails.Text = TranslateExtension.TranslationManager.Translate("About1");

            //lblBlurb.Text = TranslateExtension.TranslationManager.Translate("FinalAbout").Replace("!CR", Environment.NewLine) + Environment.NewLine + TranslateExtension.TranslationManager.Translate("Blurb2").Replace("!CR", Environment.NewLine) + Environment.NewLine + TranslateExtension.TranslationManager.Translate("ContactBlurb").Replace("!CR", Environment.NewLine);
            switch (Device.RuntimePlatform)
            {
                case Device.UWP:
                case Device.iOS:
                    //lblFinalAbout.Text = TranslateExtension.TranslationManager.Translate("FinalAbout").Replace("!CR", "");
                    lblVersion.Text = TranslateExtension.TranslationManager.Translate("Version").Replace("!CR", "").Replace("!P1", "1.0.10");
                    lblVersion.Text = lblVersion.Text.Replace(Environment.NewLine, "");
                    lblVersion.Text += Environment.NewLine;
                    //lblFinalAbout.Text = lblFinalAbout.Text.Replace(Environment.NewLine, "");
                    //lblFinalAbout.Text += Environment.NewLine;
                    break;

                case Device.Android:

                    //lblFinalAbout.Text = TranslateExtension.TranslationManager.Translate("FinalAbout").Replace("!CR", Environment.NewLine);
                    lblVersion.Text = TranslateExtension.TranslationManager.Translate("Version").Replace("!CR", Environment.NewLine).Replace("!P1", "1.0.10");
                    break;
            }


            start_carousel_timer();

            App.pressed = false;
        }
        protected override void OnAppearing()
        {
            isActive = true;
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            Utilities.open_close_page("Close", this.GetType().Name);
            isActive = false;
            base.OnDisappearing();
        }

        private void start_carousel_timer()
        {

            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                if (isActive)
                {
                    change_carousel_image();
                }
                return true;
            });

        }
        private async Task<bool> change_carousel_image()
        {


            App.mv.BlurbCount += 1;
            if (App.mv.BlurbCount >= 4)
            {
                App.mv.BlurbCount = 1;
            }

            //switch ((int)App.mv.BlurbCount)
            //{
            //    case 1:
            //        lblHeading.Text = TranslateExtension.TranslationManager.Translate("Head1");
            //        lblDetails.Text = TranslateExtension.TranslationManager.Translate("About1");
            //        btnBook.IsVisible = false;
            //        break;
            //    case 2:
            //        lblHeading.Text = TranslateExtension.TranslationManager.Translate("Head2");
            //        lblDetails.Text = TranslateExtension.TranslationManager.Translate("About2");
            //        btnBook.IsVisible = true;
            //        break;
            //    case 3:
            //        lblHeading.Text = TranslateExtension.TranslationManager.Translate("Head3");
            //        lblDetails.Text = TranslateExtension.TranslationManager.Translate("About3");
            //        btnBook.IsVisible = false;
            //        break;
            //}
            //Debug.WriteLine(String.Format("{0}{1}.jpg", carousel_image, App.mv.BlurbCount) + " : " + lblHeading.Text + " : " + lblDetails.Text);
            if (firstActive)
            {
                imgCarousel2.Source = String.Format("{0}{1}.jpg", carousel_image, App.mv.BlurbCount);
                imgCarousel2.FadeTo(1, 1000, Easing.SinIn);
                imgCarousel1.FadeTo(0, 1000, Easing.SinOut);
            }
            else
            {
                imgCarousel1.Source = String.Format("{0}{1}.jpg", carousel_image, App.mv.BlurbCount);
                imgCarousel1.FadeTo(1, 1000, Easing.SinIn);
                imgCarousel2.FadeTo(0, 1000, Easing.SinOut);
            }

            firstActive = !firstActive;

            return true;

        }

        private void BtnBook_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BookATable(""));
        }
        private void BtnBookFC_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BookATable("Fish'N'Chips"));
        }
        private void GUI_OnTapped(object sender, EventArgs e)
        {
            Uri mapUri = new Uri("http://www.gui-innovations.com");
            Device.OpenUri(mapUri);
        }
    }
}
