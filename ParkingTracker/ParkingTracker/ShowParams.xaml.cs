using System.Windows.Navigation;

namespace ParkingTracker
{
    public partial class ShowParams
    {
        public ShowParams()
        {
            InitializeComponent();
        }

        // Implement the OnNavigatedTo method and use NavigationContext.QueryString
        // to get the parameter values passed by the reminder.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string param1Value;
            string param2Value;

            NavigationContext.QueryString.TryGetValue("param1", out param1Value);
            NavigationContext.QueryString.TryGetValue("param2", out param2Value);

            param1TextBlock.Text = param1Value;
            param2TextBlock.Text = param2Value;
        }
    }
}