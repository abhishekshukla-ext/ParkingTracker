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

            if (expirationTimePicker.Value != null)
                time = (DateTime) expirationTimePicker.Value;
            DateTime expirationTime = time;
            //TODO : Replace the begin time with ExpirationTime-15
            DateTime beginTime = expirationTime.Subtract(new TimeSpan(0, 15, 0));
            //DateTime beginTime = expirationTime;

            if (DateTime.Now > beginTime)
            {
                MessageBox.Show("Parking meter already expired or about to expire within next 15 minutes.");
                return;
            }

            // Create a URI for the page that will be launched if the user
            // taps on the reminder. Use query string parameters to pass 
            // content to the page that is launched.
            
            var navigationUri = new Uri("/MainPage.xaml",UriKind.Relative);


            Reminder reminder = new Reminder(name);
            reminder.Title = "Parking meter expiring title";
            reminder.Content = "Parking meter expiring content";
            reminder.BeginTime = beginTime;
            reminder.ExpirationTime = expirationTime;
            reminder.RecurrenceType = RecurrenceInterval.None;
            reminder.NavigationUri = navigationUri;

            // Register the reminder with the system.
            ScheduledActionService.Add(reminder);

            NavigationService.GoBack();
        }
    }
}