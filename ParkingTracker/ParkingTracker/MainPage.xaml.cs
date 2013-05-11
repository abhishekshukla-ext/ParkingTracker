using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

namespace ParkingTracker
{
    public partial class MainPage
    {
        IEnumerable<ScheduledNotification> _notifications;
        private DispatcherTimer _dispatcherTimer;
        private DateTime EndTime { get; set; }
        MarketplaceDetailTask _marketPlaceDetailTask = new MarketplaceDetailTask();

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            EndTime = DateTime.MinValue;
            _dispatcherTimer = null;
            ResetItemsList();
            base.OnNavigatedTo(e);

            if ((Application.Current as App).IsTrial)
            {
                ActivateTrialSettings();
            }
            else
            {
                DeactivateTrialSettings();
            }
        }

        private void ActivateTrialSettings()
        {
            if ((Application.Current as App).IsTrialUsageOver)
            {
                EmptyTextBlock.Visibility = Visibility.Collapsed;
                TrialInfo.Visibility = Visibility.Visible;
            }
            btnBuyApplication.Visibility = Visibility.Visible;
            AppRegistrationStatus.Visibility = Visibility.Visible;
            AppRegistrationStatus.Text = "You have used the App for " + App.UsageCount + " times";
        }

        private void DeactivateTrialSettings()
        {
            btnBuyApplication.Visibility = Visibility.Collapsed;
            TrialInfo.Visibility = Visibility.Collapsed;
            if (_notifications.Count<ScheduledNotification>() <= 0)
            {
                EmptyTextBlock.Visibility = Visibility.Visible;
            }
            AppRegistrationStatus.Visibility = Visibility.Collapsed;
        }

        public MainPage()
        {
            InitializeComponent();
        }

        private void ApplicationBarAddButtonClick(object sender, EventArgs e)
        {
            if ((Application.Current as App).IsTrialUsageOver) return;
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
                timeSpan.Value = note.ExpirationTime -
                                      DateTime.Now;

                if (note.ExpirationTime.Date == DateTime.Now.Date)
                {
                    BeginTime.Text = note.BeginTime.TimeOfDay.ToString() + " Today";
                    ExpirationTime.Text = note.ExpirationTime.TimeOfDay.ToString() + " Today";
                }
                else
                {
                    BeginTime.Text = String.Format("{0:t}", note.BeginTime) + " " + String.Format("{0:m}", note.BeginTime);
                    ExpirationTime.Text = String.Format("{0:t}", note.ExpirationTime) + " " + String.Format("{0:m}", note.ExpirationTime);
                }
                if (_dispatcherTimer == null)
                {
                    _dispatcherTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
                    _dispatcherTimer.Tick += DispatcherTimerTick;
                }

                if (EndTime == DateTime.MinValue)
                {
                    if (timeSpan.Value != null) EndTime = DateTime.Now + (TimeSpan)timeSpan.Value;
                }
                SetIconicTile(true, ExpirationTime.Text);
                _dispatcherTimer.Start();
                
                EmptyTextBlock.Visibility = Visibility.Collapsed;
                if (!(Application.Current as App).IsTrialUsageOver)
                {
                    timespanPanel.Visibility = Visibility.Visible;
                }
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
            currentTimeSpan.Text = DateTime.Now.ToShortTimeString();

            if (remaining.TotalSeconds <= 0)
            {
                if (_dispatcherTimer != null) _dispatcherTimer.Stop();
                if (_notifications != null && _notifications.Count<ScheduledNotification>() > 0)
                {
                    ScheduledActionService.Remove(_notifications.First<ScheduledNotification>().Name);
                }
                EndTime = DateTime.MinValue;
                timeSpan.Value = new TimeSpan();
                timespanPanel.Visibility = Visibility.Collapsed;
                EmptyTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void ApplicationBarDeleteButtonClick(object sender, EventArgs e)
        {
            if (!(Application.Current as App).IsTrialUsageOver)
            {
                if (_notifications != null && _notifications.Count<ScheduledNotification>() > 0)
                {
                    if (MessageBox.Show("Are you sure?", "Delete Item", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
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
                        SetIconicTile(false, null);
                    }
                }
            }
        }

        private void SetIconicTile(bool timeSet, string expirationTime)
        {
            IconicTileData oIcontile = new IconicTileData();
            oIcontile.Title = "Parking Tracker";

            oIcontile.IconImage = new Uri("Assets/Tiles/Iconic/IconicTileMediumLarge.png", UriKind.Relative);
            oIcontile.SmallIconImage = new Uri("Assets/Tiles/Iconic/IconicTileSmall.png", UriKind.Relative);

            if (timeSet)
            {
                if (expirationTime != null) oIcontile.WideContent1 = expirationTime;

                if (expirationTime != null) oIcontile.WideContent2 = "Parking expiry time";

                oIcontile.BackgroundColor = Color.FromArgb(255, 56, 149, 253);

                // find the tile object for the application tile that using "Iconic" contains string in it.
                ShellTile tileToFind =
                    ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("Iconic".ToString()));

                if (tileToFind != null && tileToFind.NavigationUri.ToString().Contains("Iconic"))
                {
                    tileToFind.Delete();
                    try
                    {
                        ShellTile.Create(new Uri("/MainPage.xaml?id=Iconic", UriKind.Relative), oIcontile, true);
                    }
                    catch(Exception){}
                }
                else
                {
                    try
                    {
                        ShellTile.Create(new Uri("/MainPage.xaml?id=Iconic", UriKind.Relative), oIcontile, true);
                    }
                    catch(Exception){}
                }
            }
            else
            {
                ShellTile tileToFind =
                    ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("Iconic".ToString()));

                if (tileToFind != null && tileToFind.NavigationUri.ToString().Contains("Iconic"))
                {
                    tileToFind.Delete();
                }
            }

        }

        private void BtnBuyApplicationClick(object sender, RoutedEventArgs e)
        {
            _marketPlaceDetailTask.Show();
        }
    }
}