using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace app6
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Watch : ContentPage
    {
        public Watch()
        {
            InitializeComponent();
            time.Text = DateTime.Now.ToString("HH:mm:ss");
            date.Text = DateTime.Now.ToString("D");
            Device.StartTimer(TimeSpan.FromSeconds(1), OnTimerTick);
        }
        private bool timerStart = true;
        private bool OnTimerTick()
        {
            time.Text = DateTime.Now.ToString("HH:mm:ss");
            date.Text = DateTime.Now.ToString("D");
            return timerStart;
        }
    }
}