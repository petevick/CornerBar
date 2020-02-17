
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Threading;
using CornerBar.Classes;
using CornerBar.Forms;
using Xamarin.Forms.Xaml;

namespace CornerBar.Forms{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashForm: ContentPage
  {

      public SplashForm()
    {
          InitializeComponent();
          
          Debug.WriteLine("Debug: " +"Screen size: {0}x{1}", App.ScreenSize.Width, App.ScreenSize.Height);
          Utilities.open_close_page("Open", this.GetType().Name);

        }

      protected override void OnAppearing()
      {
          base.OnAppearing();
            //this.ForceLayout();


            Show_Logo();

        }

        private async void Show_Logo()
        {

            await imgLogo.ScaleTo(1.0,2000, Easing.CubicInOut);

            await Task.Delay(2000);

            await Navigation.PushAsync(new MainPage());
            Navigation.RemovePage(this);
            
           
        }
        protected override void OnDisappearing()
        {
            Utilities.open_close_page("Close", this.GetType().Name);
            base.OnDisappearing();
        }

    }
}
