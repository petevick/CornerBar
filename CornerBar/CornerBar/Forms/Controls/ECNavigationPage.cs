using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CornerBar.Forms.Controls
{
    public sealed class ECNavigationPage : NavigationPage
    {
        public ECNavigationPage(Page p) : base(p) { }

        public ECNavigationPage() : base() { }
    }
}