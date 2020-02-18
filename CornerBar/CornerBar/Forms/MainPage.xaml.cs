using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CarouselPageNavigation;
using CornerBar.Classes;
using CornerBar.Helpers;
using CornerBar.Forms;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Plugin.Settings;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CornerBar.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private static int carouselCt = 1;
        bool firstActive = true;
        private static string carousel_image = "Carousel";
        private bool isActive = false;

        public MainPage()
        {



            InitializeComponent();
            Utilities.open_close_page("Open", this.GetType().Name);
            //if (Device.OS == TargetPlatform.iOS)
            //{
            //    NavigationPage.SetHasNavigationBar(this, true);
            //}
            //FlagsVis
            switch (Device.RuntimePlatform)
            {
                case Device.UWP:
                    break;
                case Device.Android:
                    //this.Padding = new Thickness(0, 0, 0, 80);
                    imgES.IsVisible = true;
                    imgUK.IsVisible = true;
                    break;

                case Device.iOS:
                    this.Padding = new Thickness(0, 0, 0, 0);
                    break;
            }
            Debug.WriteLine(Plugin.DeviceInfo.CrossDeviceInfo.Current.Id);
            Debug.WriteLine(Plugin.DeviceInfo.CrossDeviceInfo.Current.Model);
            show_flags();
            refresh_labels();
            start_carousel_timer();
            
        }

        private void BtnMenu_OnClicked(object sender, EventArgs e)
        {
            if (App.pressed)
            {
                return;
            }
            App.pressed = true;
            Analytics.TrackEvent("Menu Button Pressed");
            // ...
            // use for debugging, not in released app code!
            //var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            //foreach (var res in assembly.GetManifestResourceNames())
            //    System.Diagnostics.Debug.WriteLine("found resource: " + res);
            try
            {
                Stream xmlStream = typeof(MainPage).GetTypeInfo().Assembly.GetManifestResourceStream(String.Format("CornerBar.{0}", App.menuFilename));
                PdfLoadedDocument document = new PdfLoadedDocument(xmlStream);
                PdfDocument doc = new PdfDocument();
                doc.ImportPageRange(document, 0, document.PageCount - 1);

                doc.Pages.Add();

                //Saves the document.
                MemoryStream stream = new MemoryStream();
                doc.Save(stream);
                doc.Close();
                document.Close(true);
                Xamarin.Forms.DependencyService.Get<ISave>().SaveTextAsync(App.menuFilename, "application/pdf", stream);
            }
            catch (Exception)
            {

                Navigation.PushAsync(new MenuPage());
            }
            App.pressed = false;
        }

        private void BtnBook_OnClicked(object sender, EventArgs e)
        {
            if (App.pressed)
            {
                return;
            }
            App.pressed = true;
            Analytics.TrackEvent("Book Button Pressed");
            Navigation.PushAsync(new BookATable(""));

        }

        private void BtnContact_OnClicked(object sender, EventArgs e)
        {
            if (App.pressed)
            {
                return;
            }
            App.pressed = true;
            Analytics.TrackEvent("Contact Button Pressed");
            Navigation.PushAsync(new ContactUs());
 
        }

        private void BtnNews_OnClicked(object sender, EventArgs e)
        {
            if (App.pressed)
            {
                return;
            }
            App.pressed = true;
            Analytics.TrackEvent("News Button Pressed");
            Navigation.PushAsync(new News());

        }

        protected override void OnAppearing()
        {
            isActive = true;
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            isActive = false;
            Utilities.open_close_page("Close", this.GetType().Name);
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


            carouselCt += 1;
            if (carouselCt >= 15)
            {
                carouselCt = 1;
            }
            Debug.WriteLine(String.Format("{0}{1}.jpg", carousel_image, carouselCt));
            if (firstActive)
            {
                imgCarousel2.Source = String.Format("{0}{1}.jpg", carousel_image, carouselCt);
                imgCarousel2.FadeTo(1, 1000, Easing.SinIn);
                imgCarousel1.FadeTo(0, 1000, Easing.SinOut);
            }
            else
            {
                imgCarousel1.Source = String.Format("{0}{1}.jpg", carousel_image, carouselCt);
                imgCarousel1.FadeTo(1, 1000, Easing.SinIn);
                imgCarousel2.FadeTo(0, 1000, Easing.SinOut);
            }

            firstActive = !firstActive;

            return true;

        }

        private void BtnAbout_OnClicked(object sender, EventArgs e)
        {
            if (App.pressed)
            {
                return;
            }
            App.pressed = true;
            Analytics.TrackEvent("Menu Button Pressed");
            Navigation.PushAsync(new AboutUs());
           
        }

        private void English_OnTapped(object sender, EventArgs e)
        {
            App.Set_Language("en-GB");
            refresh_labels();
        }
        private void Spanish_OnTapped(object sender, EventArgs e)
        {
            App.Set_Language("es-ES");
            refresh_labels();
            
        }

        private void refresh_labels()
        {
            btnMenu.Text = TranslateExtension.TranslationManager.Translate("OurMenu");
            btnBook.Text = TranslateExtension.TranslationManager.Translate("Book");
            btnContact.Text = TranslateExtension.TranslationManager.Translate("ContactUs");
            btnAbout.Text = TranslateExtension.TranslationManager.Translate("About");
            btnNews.Text = TranslateExtension.TranslationManager.Translate("News");
            btnWhoAreWe.Text = TranslateExtension.TranslationManager.Translate("WhoAreWe");
            lblOpening.Text = TranslateExtension.TranslationManager.Translate("OpeningTimes");
            show_flags();

        }

        private void show_flags()
        {
            string lang = CrossSettings.Current.GetValueOrDefault("Language", "en-GB");
            //Flags or 2Flags



            grdFlags.IsVisible = false;
            switch (lang)
            {
                case "en-GB":
                    imgES.Source = "es_flag.jpg";
                    imgES.Opacity = .7;
                    imgUK.Source = "en_flagF.jpg";
                    imgUK.Opacity = 1;
                    break;
                case "es-ES":
                    imgES.Source = "es_flagF.jpg";
                    imgES.Opacity = 1;
                    imgUK.Source = "en_flag.jpg";
                    break;
            }
            //grdFlags.IsVisible = true;
            //switch (lang)
            //{
            //    case "en-GB":
            //        imgAR.Source = "ar_flag.jpg";
            //        imgAR.Opacity = .7;
            //        imgES.Source = "es_flag.jpg";
            //        imgES.Opacity = .7;
            //        imgDE.Source = "de_flag.jpg";
            //        imgDE.Opacity = .7;
            //        imgFR.Source = "fr_flag.jpg";
            //        imgFR.Opacity = .7;
            //        imgUK.Source = "en_flagF.jpg";
            //        imgUK.Opacity = 1;
            //        break;
            //    case "es-ES":
            //        imgAR.Source = "ar_flag.jpg";
            //        imgAR.Opacity = .7;
            //        imgES.Source = "es_flagF.jpg";
            //        imgES.Opacity = 1;
            //        imgDE.Source = "de_flag.jpg";
            //        imgDE.Opacity = .7;
            //        imgFR.Source = "fr_flag.jpg";
            //        imgFR.Opacity = .7;
            //        imgUK.Source = "en_flag.jpg";
            //        imgUK.Opacity = .7;
            //        break;
            //    case "de-DE":
            //        imgAR.Source = "ar_flag.jpg";
            //        imgAR.Opacity = .7;
            //        imgES.Source = "es_flag.jpg";
            //        imgES.Opacity = .7;
            //        imgDE.Source = "de_flagF.jpg";
            //        imgDE.Opacity = 1;
            //        imgFR.Source = "fr_flag.jpg";
            //        imgFR.Opacity = .7;
            //        imgUK.Source = "en_flag.jpg";
            //        imgUK.Opacity = .7;
            //        break;
            //    case "fr-FR":
            //        imgAR.Source = "ar_flag.jpg";
            //        imgAR.Opacity = .7;
            //        imgES.Source = "es_flag.jpg";
            //        imgES.Opacity = .7;
            //        imgDE.Source = "de_flag.jpg";
            //        imgDE.Opacity = .7;
            //        imgFR.Source = "fr_flagF.jpg";
            //        imgFR.Opacity = 1;
            //        imgUK.Source = "en_flag.jpg";
            //        imgUK.Opacity = .7;
            //        break;
            //    case "ar-AR":
            //        imgAR.Source = "ar_flag.jpg";
            //        imgAR.Opacity = 1;
            //        imgES.Source = "es_flag.jpg";
            //        imgES.Opacity = .7;
            //        imgDE.Source = "de_flag.jpg";
            //        imgDE.Opacity = .7;
            //        imgFR.Source = "fr_flagF.jpg";
            //        imgFR.Opacity = .7;
            //        imgUK.Source = "en_flag.jpg";
            //        imgUK.Opacity = .7;
            //        break;
            //}
            
        }

        private void BtnWhoAreWe_OnClicked(object sender, EventArgs e)
        {
            if (App.pressed)
            {
                return;
            }
            App.pressed = true;
            Analytics.TrackEvent("Who Are We Button Pressed");
            Navigation.PushAsync(new WhoAreWe());

        }

        private void German_OnTapped(object sender, EventArgs e)
        {
            App.Set_Language("de-DE");
            refresh_labels();
        }

        private void French_OnTapped(object sender, EventArgs e)
        {
            App.Set_Language("fr-FR");
            refresh_labels();
        }
        private void Arabic_OnTapped(object sender, EventArgs e)
        {
            App.Set_Language("ar-AE");
            refresh_labels();
        }

        private void Generate_Error(object sender, EventArgs e)
        {

          
        }
    }
}
