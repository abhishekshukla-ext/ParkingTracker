using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Phone.Scheduler;

namespace ParkingTracker
{
    public partial class MainPage
    {
        IEnumerable<ScheduledNotification> _notifications;
        private DispatcherTimer _dispatcherTimer;
        private DateTime EndTime { get; set; }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            EndTime = DateTime.MinValue;
            _dispatcherTimer = null;
            ResetItemsList();
        }

        public MainPage()
        {
            InitializeComponent();
        }

        private void ApplicationBarAddButtonClick(object sender, EventArgs e)
        {
            if (_notifications != null && _notifications.Count<ScheduledNotification>() > 0)
            {
                MessageBox.Show("Parking time tracker already exists. Please delete this one to add a new one");
            }
            else
            {
                NavigationService.Navigate(new Uri("/AddNotification.xaml", UriKind.RelativeOrAbsolute));
            }
        }

        private void ResetItemsList()
        {
            _notifications = ScheduledActionService.GetActions<ScheduledNotification>();

            if (_notifications.Count<ScheduledNotification>() > 0)
            {
                var note = _notifications.First<ScheduledNotification>();
                timeSpan.Value = ((Microsoft.Phone.Scheduler.ScheduledAction) (note)).ExpirationTime.TimeOfDay -
                                      DateTime.Now.TimeOfDay;
                BeginTime.Text = ((Microsoft.Phone.Scheduler.ScheduledAction) (note)).BeginTime.TimeOfDay.ToString();
                ExpirationTime.Text = ((Microsoft.Phone.Scheduler.ScheduledAction)(note)).ExpirationTime.TimeOfDay.ToString();

                if (_dispatcherTimer == null)
                {
                    _dispatcherTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
                    _dispatcherTimer.Tick += DispatcherTimerTick;
                }

                if (EndTime == DateTime.MinValue)
                {
                    if (timeSpan.Value != null) EndTime = DateTime.Now + (TimeSpan)timeSpan.Value;
                }

                _dispatcherTimer.Start();

                EmptyTextBlock.Visibility = Visibility.Collapsed;
                timespanPanel.Visibility = Visibility.Visible;
            }
            else
            {
                EmptyTextBlock.Visibility = Visibility.Visible;
            }
        }

        void DispatcherTimerTick(object sender, EventArgs e)
        {
            var remaining = EndTime - DateTime.Now;
            var remainingSeconds = (int)remaining.TotalSeconds;
            timeSpan.Value = TimeSpan.FromSeconds(remainingSeconds);

            if (remaining.TotalSeconds <= 0)
            {
                _dispatcherTimer.Stop();
                ScheduledActionService.Remove(_notifications.First<ScheduledNotification>().Name);
                EndTime = DateTime.MinValue;
                timeSpan.Value = new TimeSpan();
                timespanPanel.Visibility = Visibility.Collapsed;
                EmptyTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void ApplicationBarDeleteButtonClick(object sender, EventArgs e)
        {
            if (_notifications.Count<ScheduledNotification>() > 0)
            {
                ScheduledActionService.Remove(_notifications.First<ScheduledNotification>().Name);
                _notifications = null;
                EndTime = DateTime.MinValue;
                timeSpan.Value = new TimeSpan();
                timespanPanel.Visibility = Visibility.Collapsed;
                EmptyTextBlock.Visibility = Visibility.Visible;
                if (_dispatcherTimer != null)
                {
                    _dispatcherTimer.Stop();
                }
            }
        }
    }
}