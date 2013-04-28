using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;

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
                timeSpan.Value = ((Microsoft.Phone.Scheduler.ScheduledAction) (note)).ExpirationTime -
                                      DateTime.Now;

                if (note.ExpirationTime.Date == DateTime.Now.Date)
                {
                    BeginTime.Text = "Today at " + note.BeginTime.TimeOfDay.ToString();
                    ExpirationTime.Text = "Today at " + note.ExpirationTime.TimeOfDay.ToString();
                }
                else
                {
                    BeginTime.Text = String.Format("{0:f}", note.BeginTime);
                    ExpirationTime.Text = String.Format("{0:f}", note.ExpirationTime);
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
                SetIconicTile(true, note.ExpirationTime.ToString());
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
            if (MessageBox.Show("Are you sure?", "Delete Item", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
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
                    SetIconicTile(false, null);
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
                oIcontile.WideContent1 = "Parking meter expire at ";
                if (expirationTime != null) oIcontile.WideContent2 = expirationTime;

                oIcontile.BackgroundColor = Color.FromArgb(255, 56, 149, 253);

                // find the tile object for the application tile that using "Iconic" contains string in it.
                ShellTile TileToFind =
                    ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("Iconic".ToString()));

                if (TileToFind != null && TileToFind.NavigationUri.ToString().Contains("Iconic"))
                {
                    TileToFind.Delete();
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
                ShellTile TileToFind =
                    ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("Iconic".ToString()));

                if (TileToFind != null && TileToFind.NavigationUri.ToString().Contains("Iconic"))
                {
                    TileToFind.Delete();
                }
            }

        }
    }
}