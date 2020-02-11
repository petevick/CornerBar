using CornerBar.Resources;
using System;
using System.Diagnostics;
using System.Globalization;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace CornerBar.Helpers
{
    [ContentProperty("Text")]
    public class DetailsExtension : IMarkupExtension
    {
        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return null;

            try
            {
                for (int i = 0; i <= App.detailKey.GetUpperBound(0); i++)
                {
                    if (App.detailKey[i] == Text)
                    {
                        return App.detailData[i].Replace("!CR", Environment.NewLine); ;
                    }

                }
            }
            catch (Exception)
            {
                //Log.Information("Debug: " + "No Resource entry for: " + Text);
                return Text;
            }
            return Text;

        }

        public static class DetailsManager
        {
            public static string Details(string key)
            {
                try
                {
                    for (int i = 0; i <= App.detailKey.GetUpperBound(0); i++)
                    {
                        if (App.detailKey[i] == key)
                        {
                            return App.detailData[i].Replace("!CR", Environment.NewLine); ;
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Debug: " + "No Resource entry for: " + key + ":" + ex);
                    Debug.WriteLine(App.Ci);
                    return key;
                }

                return key;
            }
        }
    }
}