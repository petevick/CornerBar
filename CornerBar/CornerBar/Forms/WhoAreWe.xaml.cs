using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CornerBar.Classes;
using CornerBar.Helpers;
using CornerBar.Models;
using CornerBar.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CornerBar.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WhoAreWe : ContentPage
    {
        private string[] staffMembers;
        private string[] staffPhotos;
        private string[] staffNotes;
        
        public WhoAreWe()
        {
            InitializeComponent();
            if (Device.RuntimePlatform == Device.iOS)
            {
                NavigationPage.SetHasNavigationBar(this, true);
            }

            Utilities.open_close_page("Open", this.GetType().Name);
            var assembly = typeof(App).GetTypeInfo().Assembly;
            string filename = string.Format("CornerBar.Employees{0}.txt", App.language.ToUpper());
            Stream stream = assembly.GetManifestResourceStream(filename);
            string text = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }
            text = text.Replace("\r\n", "$");
            string[] staff = text.Split('$');
            staffMembers = new string[staff.GetUpperBound(0) + 1];
            staffPhotos = new string[staff.GetUpperBound(0) + 1];
            staffNotes = new string[staff.GetUpperBound(0) + 1];
            for (int i = 0; i <= staff.GetUpperBound(0); i++)
            {
                string[] staffDetails = staff[i].Split('|');
                staffMembers[i] = staffDetails[0];
                staffPhotos[i] = staffDetails[1];
                staffNotes[i] = staffDetails[2];

            }


            populate_staff();
            //start_carousel_timer();

        }

        //private void start_carousel_timer()
        //{

        //    Device.StartTimer(TimeSpan.FromSeconds(5), () =>
        //    {
        //        if (isActive)
        //        {
        //            change_carousel_image();
        //        }
        //        return true;
        //    });

        //}
        //private async Task<bool> change_carousel_image()
        //{


        //    carouselCt += 1;
        //    if (carouselCt >= 3)
        //    {
        //        carouselCt = 1;
        //    }
        //    Debug.WriteLine(String.Format("{0}{1}.jpg", carousel_image, carouselCt));
        //    if (firstActive)
        //    {
        //        img1.Source = String.Format("{0}{1}.jpg", carousel_image, carouselCt);
        //        img2.FadeTo(1, 1000, Easing.SinIn);
        //        img1.FadeTo(0, 1000, Easing.SinOut);
        //    }
        //    else
        //    {
        //        img1.Source = String.Format("{0}{1}.jpg", carousel_image, carouselCt);
        //        img1.FadeTo(1, 1000, Easing.SinIn);
        //        img2.FadeTo(0, 1000, Easing.SinOut);
        //    }

        //    firstActive = !firstActive;

        //    return true;

        //}
        protected override void OnAppearing()
        {

            base.OnAppearing();
            App.pressed = false;
        }

       private void populate_staff()
        {

            List<Staff> staff = new List<Staff>();

            for (int i = 0; i <= staffMembers.GetUpperBound(0); i++)
            {
                staff.Add(new Staff(staffMembers[i], staffPhotos[i], staffNotes[i]));
            }
            lvStaff.ItemsSource = staff;
          
        }
    }
}
