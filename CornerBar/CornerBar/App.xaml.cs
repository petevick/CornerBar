using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using CornerBar.Forms;
using Xamarin.Forms;
using CornerBar.Forms.Controls;
using System.Threading;
using System.Xml.Serialization;
using CornerBar.Classes;
using CornerBar.Helpers;
using CornerBar.Models;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Xamarin.Forms.Xaml;


namespace CornerBar
{
    public partial class App : Application
    {
        
        public static Size ScreenSize;
        public static CultureInfo Ci;
        public static string language = string.Empty;
        public static string menuFilename = "menu2015.pdf";
        public static MainPageViewModel mv = new MainPageViewModel();
        public static Boolean pressed = false;
        public static string[] detailKey;
        public static string[] detailData;
        public static bool MultiLang = false;

        private static ISettings AppSettings =>
            CrossSettings.Current;
        public App()
        {
            InitializeComponent();

            try
            {
                Set_Language(CrossSettings.Current.GetValueOrDefault("Language", "en-GB"));

                var assembly = typeof(App).GetTypeInfo().Assembly;
                Stream stream = assembly.GetManifestResourceStream("CornerBar.Config.txt");
                string text = "";
                using (var reader = new System.IO.StreamReader(stream))
                {
                    text = reader.ReadToEnd();
                }
                text = text.Replace("\r\n", "|");
                string[] resources = text.Split('|');
                for (int i = 0; i <= resources.GetUpperBound(0); i++)
                {
                    string[] nameAndColor = resources[i].Split(',');
                    Debug.WriteLine(nameAndColor[0]);
                    App.Current.Resources[nameAndColor[0]] = Color.FromHex(nameAndColor[1]);
                }

                

                assembly = typeof(App).GetTypeInfo().Assembly;
                stream = assembly.GetManifestResourceStream("CornerBar.Details.txt");
                text = "";
                using (var reader = new System.IO.StreamReader(stream))
                {
                    text = reader.ReadToEnd();
                }
                text = text.Replace("\r\n", "$");
                string[] details = text.Split('$');
                detailKey = new string[details.GetUpperBound(0) + 1];
                detailData = new string[details.GetUpperBound(0) + 1];

                for (int i = 0; i <= details.GetUpperBound(0); i++)
                {
                    string[] fullDetails = details[i].Split('|');
                    Debug.WriteLine(fullDetails[0]);
                    detailKey[i] = fullDetails[0];
                    detailData[i] = fullDetails[1];
                }

                App.MultiLang = DetailsExtension.DetailsManager.Details("multilang") == "Y";

                App.mv.BookingTime = new TimeSpan(20, 0, 0);

                ECNavigationPage np = new ECNavigationPage(new SplashForm());

                np.BackgroundColor = (Color)App.Current.Resources["GlobalBackground"];
                np.BackgroundImage = App.Current.Resources["LogoFile"].ToString();
                np.BarTextColor = (Color)App.Current.Resources["DarkForegroundColor"];



                //if (Device.OS == TargetPlatform.Windows &&
                //            Device.Idiom == TargetIdiom.Phone)
                //{
                //    np = new ECNavigationPage(new MainPage());
                //}


                //np.BackgroundColor = (Color)App.Current.Resources["GlobalBackground"];
                //np.BarTextColor = (Color)App.Current.Resources["DarkForegroundColor"];

                MainPage = np;
                if (Device.RuntimePlatform == Device.UWP)
                {
                    OnStart();
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message) ;
            }
            
        }

        public static void Set_Language(string lang)
        {
            Ci = new CultureInfo(lang);
            CrossSettings.Current.AddOrUpdateValue("Language", lang);
            Set_Menuname(lang);
            App.language = lang.Substring(0, 2);

            
            CultureInfo.DefaultThreadCurrentCulture = Ci;
            CultureInfo.DefaultThreadCurrentUICulture = Ci;
        }

        /// <exclude />
        public static void Set_Menuname(string lang)
        {
            menuFilename = String.Format("menu{0}{1}.pdf", DateTime.Now.Year,lang.Substring(0,2));
            //menuFilename = String.Format("Menu{0}.png", lang.Substring(0, 2).ToUpper());
        }
        protected override async void OnStart()
        {
            
        }

       
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        
    }
}

