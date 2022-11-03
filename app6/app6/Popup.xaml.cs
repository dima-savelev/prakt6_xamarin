using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace app6
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Popup
    {
        public Popup() => InitializeComponent();

        private void Button_Clicked(object sender, EventArgs e)
        {
            Dismiss(null);
        }
    }
}