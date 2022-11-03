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
    public partial class Stopwatch : ContentPage
    {
        public Stopwatch()
        {
            InitializeComponent();
        }
        List<string> _time = new List<string>();
        DateTime date = new DateTime(0,0);
        private bool timerStart = false;
        private bool OnTimerTick()
        {
            if (!timerStart) return timerStart;
            date = date.AddMilliseconds(17);
            sec.Text = date.ToString("mm:ss.ff");
            return timerStart;
        }

        private void Start_Clicked(object sender, EventArgs e)
        {
            if (timerStart)
            {
                return;
            }
            pause.IsVisible = true;
            pause.IsEnabled = true;
            addButton.IsVisible = true;
            addButton.IsEnabled = true;
            start.IsVisible = false;
            start.IsEnabled = false;
            date = new DateTime(0, 0);
            timerStart = true;
            Device.StartTimer(TimeSpan.FromMilliseconds(1), OnTimerTick);
        }

        private void Pause_Clicked(object sender, EventArgs e)
        {
            stop.IsVisible = true;
            stop.IsEnabled = true;
            continueButton.IsVisible = true;
            continueButton.IsEnabled = true;
            pause.IsVisible = false;
            pause.IsEnabled = false;
            start.IsVisible = false;
            start.IsEnabled = false;
            addButton.IsVisible = false;
            addButton.IsEnabled = false;
            timerStart = false;
        }

        private void Add_Clicked(object sender, EventArgs e)
        {
            timerList.ItemsSource = null;
            _time.Add(date.ToString("mm:ss.ff"));
            timerList.ItemsSource = _time;
        }

        private void Stop_Clicked(object sender, EventArgs e)
        {
            timerStart = false;
            sec.Text = "00:00.00";
            stop.IsVisible = false;
            stop.IsEnabled = false;
            continueButton.IsVisible = false;
            continueButton.IsEnabled = false;
            pause.IsVisible = false;
            pause.IsEnabled = false;
            start.IsVisible = true;
            start.IsEnabled = true;
            addButton.IsVisible = false;
            addButton.IsEnabled = false;
            timerList.ItemsSource = null;
            _time.Clear();
        }

        private void Continue_Clicked(object sender, EventArgs e)
        {
            pause.IsVisible = true;
            pause.IsEnabled = true;
            addButton.IsVisible = true;
            addButton.IsEnabled = true;
            continueButton.IsVisible = false;
            continueButton.IsEnabled = false;
            stop.IsVisible = false;
            stop.IsEnabled = false;
            timerStart = true;
            Device.StartTimer(TimeSpan.FromMilliseconds(1), OnTimerTick);
        }
    }
}