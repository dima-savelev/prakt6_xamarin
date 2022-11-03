using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Plugin.LocalNotification;
using Plugin.LocalNotification.EventArgs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ForegroundServiceDemo.Services;
using Xamarin.CommunityToolkit.Extensions;

namespace app6
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Alarm : ContentPage
    {
        public Alarm()
        {
            LocalNotificationCenter.Current.NotificationReceived += OnNotificationReceived;
            LocalNotificationCenter.Current.NotificationActionTapped += OnNotificationTapped;
            InitializeComponent();
            try {
             
            foreach (var item in group1.Children)
            {
                var rb = item as RadioButton;
                if (rb.Content.ToString() == App.Current.Properties["Data"].ToString())
                {
                    rb.IsChecked = true;
                    break;
                }
            }
                timePicker.Time = (TimeSpan)App.Current.Properties["Time"];
                timePicker.IsEnabled = Convert.ToBoolean(App.Current.Properties["Picker"]);
                group1.IsEnabled = Convert.ToBoolean(App.Current.Properties["Group"]);
                switchToggle.Toggled -= Switch_Toggled;
                switchToggle.IsToggled = Convert.ToBoolean(App.Current.Properties["Toggle"]);
                switchToggle.Toggled += Switch_Toggled;
            }
            catch { }
        }
        TimeSpan timeSpan;
        DateTime dateTime;
        private string _week = null;
        private Plugin.SimpleAudioPlayer.ISimpleAudioPlayer player { get; set; }
        private void OnNotificationReceived(NotificationEventArgs e)
        {
            player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            player.Load("waves.mp3");
            player.Loop = true;
            player.Play();
        }
        private void OnNotificationTapped(NotificationActionEventArgs e)
        {
            player.Stop();
        }
        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            DependencyService.Resolve<IForegroundService>().StartMyForegroundService();
            App.Current.Properties["Toggle"] = switchToggle.IsToggled;
            if (switchToggle.IsToggled == true)
            {
                group1.IsEnabled = false;
                timePicker.IsEnabled = false;
                App.Current.Properties["Group"] = group1.IsEnabled;
                App.Current.Properties["Picker"] = timePicker.IsEnabled;
                RadioButton radioButton = default;
                foreach (var item in group1.Children)
                {
                    if ((item as RadioButton).IsChecked == true)
                    {
                        radioButton = item as RadioButton;
                        break;
                    }
                }

                if (radioButton == null)
                {
                    switchToggle.IsToggled = false;
                    App.Current.Properties["Toggle"] = switchToggle.IsToggled;
                    return;
                }

                _week = Convert.ToString(radioButton.Content);
                dateTime = DateTime.Now.Date;
                dateTime = dateTime.Add(timeSpan);
                do
                {
                    if (_week == Convert.ToString(dateTime.DayOfWeek))
                    {
                        break;
                    }
                    else
                    {
                       dateTime = dateTime.AddDays(1);
                    }
                } while (_week != Convert.ToString(dateTime.DayOfWeek));
                var notification = new NotificationRequest
                {
                    NotificationId = 100,
                    Title = "АЛО",
                    Description = "Вставай дурак",
                    CategoryType = NotificationCategoryType.Alarm,
                    Schedule =
                    {
                    NotifyTime = dateTime,
                    RepeatType = NotificationRepeat.Weekly,
                    }
                };
                LocalNotificationCenter.Current.Show(notification);

            }
            else
            {
                group1.IsEnabled = true;
                timePicker.IsEnabled = true;
                App.Current.Properties["Group"] = group1.IsEnabled;
                App.Current.Properties["Picker"] = timePicker.IsEnabled;
                DependencyService.Resolve<IForegroundService>().StopMyForegroundService();
            }
        }

        private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton radioButton = default;
            foreach (var item in group1.Children)
            {
                if ((item as RadioButton).IsChecked == true)
                {
                    radioButton = item as RadioButton;
                    break;
                }
            }

            if (radioButton == null)
            {
                switchToggle.IsToggled = false;
                App.Current.Properties["Toggle"] = switchToggle.IsToggled;
                return;
            }
            _week = Convert.ToString(radioButton.Content);
            App.Current.Properties["Data"] = _week;
        }

        private void timePicker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Time")
            {
                timeSpan = timePicker.Time;
                App.Current.Properties["Time"] = timeSpan;
            }
        }
    }
}