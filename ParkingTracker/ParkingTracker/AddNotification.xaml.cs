using System;
using System.Windows;
using Microsoft.Phone.Scheduler;

namespace ParkingTracker
{
    public partial class AddNotification
    {
        public AddNotification()
        {
            InitializeComponent();
            expirationTimeLabel1.TextDecorations = TextDecorations.Underline;
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

            time = date.Date + time.TimeOfDay;

            DateTime expirationTime = time;

            DateTime beginTime = expirationTime.Subtract(new TimeSpan(0, 15, 0));

            if (DateTime.Now > beginTime)
            {
                MessageBox.Show("Parking meter already expired or about to expire within next 15 minutes.");
                return;
            }
            
            var navigationUri = new Uri("/MainPage.xaml",UriKind.Relative);
            
            var reminder = new Reminder(name)
                               {
                                   Title = "Parking Meter",
                                   Content = "Your parking meter time is expiring in 15 minutes",
                                   BeginTime = beginTime,
                                   ExpirationTime = expirationTime,
                                   RecurrenceType = RecurrenceInterval.None,
                                   NavigationUri = navigationUri
                               };

            // Register the reminder with the system.
            ScheduledActionService.Add(reminder);
            App.UsageCount++;
            if (App.UsageCount > 7)
            {
                (Application.Current as App).IsTrialUsageOver = true;
            }
            NavigationService.GoBack();
        }

        private void ApplicationBarBackButtonClick(object sender, EventArgs e)
        {
            if(NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void ExpirationDatePickerValueChanged(object sender, Microsoft.Phone.Controls.DateTimeValueChangedEventArgs e)
        {
            if(expirationDatePicker.Value != null && expirationDatePicker.Value.Value.Date > DateTime.Now.Date.AddDays(1))
            {
                expirationDatePicker.Value = DateTime.Now.Date;
                MessageBox.Show("The parking meter expiry date cannot be after tomorrow.");
            }
        }
    }
}