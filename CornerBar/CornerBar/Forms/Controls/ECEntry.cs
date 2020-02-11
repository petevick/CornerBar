using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CornerBar.Forms.Controls
{
    public sealed class ECEntry : Entry
    {
        public static readonly BindableProperty ValidTextColorProperty =
          BindableProperty.Create<ECEntry, Color>(p => p.ValidTextColor, Color.FromHex("#211F20"));
        public static readonly BindableProperty InvalidTextColorProperty =
          BindableProperty.Create<ECEntry, Color>(p => p.InvalidTextColor, Color.Default);

        public Color ValidTextColor
        {
            get { return (Color)GetValue(ValidTextColorProperty); }
            set { SetValue(ValidTextColorProperty, value); }
        }

        public Color InvalidTextColor
        {
            get { return (Color)GetValue(InvalidTextColorProperty); }
            set { SetValue(InvalidTextColorProperty, value); }
        }
    }
}
