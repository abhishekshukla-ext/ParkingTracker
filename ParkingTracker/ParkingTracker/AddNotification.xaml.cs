using System;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Scheduler;

namespace ParkingTracker
{
    public partial class AddNotification
    {
        public AddNotification()
        {
            InitializeComponent();
        }

        private void ApplicationBarSaveButtonClick(object sender, EventArgs e)
        {
            String name = Guid.NewGuid().ToString();

            var time = DateTime.Now;
            var date = DateTime.Now;

            if (expirationDatePicker.Value != null)
                date = (DateTime) expirationDatePicker.Value;

            if (expirationTimePicker.Value != null)
                time = (DateTime) expirationTimePicker.Value;

            //time = date.Date + time.TimeOfDay;

            DateTime expirationTime = time;
            //TODO : Replace the begin time with ExpirationTime-15
            DateTime beginTime = expirationTime.Subtract(new TimeSpan(0, 15, 0));
            //DateTime beginTime = expirationTime;

            if (DateTime.Now > beginTime)
            {
                MessageBox.Show("Parking meter already expired or about to expire within next 15 minutes.");
                return;
            }
            
            var navigationUri = new Uri("/MainPage.xaml",UriKind.Relative);
            
            var reminder = new Reminder(name);
            reminder.Title = "Parking Meter";
            reminder.Content = "Your parking meter time is expiring in 15 minutes";
            reminder.BeginTime = beginTime;
            reminder.ExpirationTime = expirationTime;
            reminder.RecurrenceType = RecurrenceInterval.None;
            reminder.NavigationUri = navigationUri;
            
            // Register the reminder with the system.
            ScheduledActionService.Add(reminder);

            NavigationService.GoBack();
        }

        private void ApplicationBarBackButtonClick(object sender, EventArgs e)
        {
            if(NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}