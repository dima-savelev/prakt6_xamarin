using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.CommunityToolkit.Extensions;

namespace app6
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Timer : ContentPage
    {
        public Timer()
        {
            InitializeComponent();
            Number = Enumerable.Range(0, 60).ToList();
            Hours = Enumerable.Range(0, 24).ToList();
            this.BindingContext = this;
        }
        public List<int> Number { get; set; }
        public List<int> Hours { get; set; }
        private bool timerStart = false;
        TimeSpan _time;

        private void Start_Clicked(object sender, EventArgs e)
        {
            int hours = 0;
            int minutes = 0;
            int seconds = 0;
            hours = Convert.ToInt32(carousel1.CurrentItem);
            minutes = Convert.ToInt32(carousel2.CurrentItem);
            seconds = Convert.ToInt32(carousel3.CurrentItem);
            if (hours == 0 && seconds == 0 && minutes == 0)
            {
                return;
            }
            _time = new TimeSpan(hours, minutes, seconds);
            timer.Text = String.Format("{0,2:00}:{1,2:00}:{2,2:00}", _time.Hours, _time.Minutes, _time.Seconds);
            Device.StartTimer(TimeSpan.FromSeconds(1), OnTimerTick);
            timerStart = true;
            timer.IsEnabled = true;
            timer.IsVisible = true;
            startGrid.IsEnabled = false;
            startGrid.IsVisible = false;
            pause.IsVisible = true;
            pause.IsEnabled = true;
            stop.IsVisible = true;
            stop.IsEnabled = true;
        }
        private bool OnTimerTick()
        {
            if (!timerStart) return timerStart;
            var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            _time = _time.Add(new TimeSpan(0, 0, -1));
            timer.Text = String.Format("{0,2:00}:{1,2:00}:{2,2:00}", _time.Hours, _time.Minutes, _time.Seconds);
            if (_time.CompareTo(new TimeSpan(0, 0, 0)) == 0)
            {
                Popup popup = new Popup();
                App.Current.MainPage.Navigation.ShowPopup(popup);
                player.Load("waves.mp3");
                player.Play();
                player.Loop = true;
                popup.Dismissed += (s, e) => player.Stop();
                timer.IsEnabled = false;
                timer.IsVisible = false;
                startGrid.IsEnabled = true;
                startGrid.IsVisible = true;
                stop.IsVisible = false;
                stop.IsEnabled = false;
                continueButton.IsVisible = false;
                continueButton.IsEnabled = false;
                pause.IsVisible = false;
                pause.IsEnabled = false;
                return timerStart = false;
            }
            return timerStart;
        }

        private void Pause_Clicked(object sender, EventArgs e)
        {
            stop.IsVisible = true;
            stop.IsEnabled = true;
            continueButton.IsVisible = true;
            continueButton.IsEnabled = true;
            pause.IsVisible = false;
            pause.IsEnabled = false;
            timerStart = false;
        }

        private void Stop_Clicked(object sender, EventArgs e)
        {
            timer.IsEnabled = false;
            timer.IsVisible = false;
            startGrid.IsEnabled = true;
            startGrid.IsVisible = true;
            timerStart = false;
            stop.IsVisible = false;
            stop.IsEnabled = false;
            continueButton.IsVisible = false;
            continueButton.IsEnabled = false;
            pause.IsVisible = false;
            pause.IsEnabled = false;
        }

        private void Continue_Clicked(object sender, EventArgs e)
        {
            pause.IsVisible = true;
            pause.IsEnabled = true;
            continueButton.IsVisible = false;
            continueButton.IsEnabled = false;
            timerStart = true;
            Device.StartTimer(TimeSpan.FromSeconds(1), OnTimerTick);
        }
    }
}