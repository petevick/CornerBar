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
    public class TranslateExtension : IMarkupExtension
    {
        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return null;

            try
            {
                Debug.WriteLine(AppResources.ResourceManager);
                if (AppResources.ResourceManager.GetString(Text, App.Ci) == null)
                {
                    return Text;
                }
                return AppResources.ResourceManager.GetString(Text, App.Ci).Replace("!CR", Environment.NewLine);
            }
            catch (Exception)
            {
                return Text;
            }

        }

        public static class TranslationManager
        {
            public static string Translate(string key)
            {
                try
                {
                    Debug.WriteLine(AppResources.ResourceManager);
                    return AppResources.ResourceManager.GetString(key, App.Ci)
                        .Replace("!CR", Environment.NewLine);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Debug: " + "No Resource entry for: " + key + ":" + ex);
                    Debug.WriteLine(App.Ci);
                    return key;
                }
            }
        }
    }
}