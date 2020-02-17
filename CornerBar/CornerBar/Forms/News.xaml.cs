using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CornerBar.Classes;
using CornerBar.Helpers;
using CornerBar.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CornerBar.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class News : ContentPage
    {
        public News()
        {
            InitializeComponent();
            if (Device.OS == TargetPlatform.iOS)
            {
                NavigationPage.SetHasNavigationBar(this, true);
            }
            lblNews.Source = new Uri(DetailsExtension.DetailsManager.Details("newslink"));
            this.Content = lblNews;
            Utilities.open_close_page("Open", this.GetType().Name);
        }

        private void LblNews_OnNavigating(object sender, WebNavigatingEventArgs e)
        {
            
            Debug.WriteLine(e);
        }

        private void LblNews_OnNavigated(object sender, WebNavigatedEventArgs e)
        {
            Debug.WriteLine(e);
        }

        protected override void OnAppearing()
        {

            base.OnAppearing();
            App.pressed = false;
        }

        protected override void OnDisappearing()
        {
            Utilities.open_close_page("Close", this.GetType().Name);
            base.OnDisappearing();
        }
    }
}
