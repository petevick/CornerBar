using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AppCenter.Analytics;
using Xamarin.Forms;

namespace CornerBar.Classes
{
    public class Utilities
    {
        public static async void Enable_Button(Button btn, Boolean cond)
        {
            btn.IsEnabled = cond;
            App.pressed = !cond;
            await Task.Yield();
        }

        public static async void Enable_Image(Image img, Boolean cond)
        {
            img.IsEnabled = cond;
            await Task.Yield();
        }

        public static async void open_close_page(string function, string pagename)
        {

            
        }

    }
}
