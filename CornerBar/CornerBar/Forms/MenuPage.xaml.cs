using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CornerBar.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CornerBar.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : CarouselPage
    {

        public MenuPage()
        {
            InitializeComponent();

            if (Device.OS == TargetPlatform.iOS)
            {
                NavigationPage.SetHasNavigationBar(this, true);
            }

        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            Page1.Source = null;
            Page1.Source = AppResources.Page1;
            Page2.Source = null;
            Page2.Source = AppResources.Page2;
            Page3.Source = null;
            Page3.Source = AppResources.Page3;
            Page4.Source = null;
            Page4.Source = AppResources.Page4;
            Page5.Source = null;
            Page5.Source = AppResources.Page5;
            Page6.Source = null;
            Page6.Source = AppResources.Page6;
        }
    }
}