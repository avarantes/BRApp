using BRApp.Controls;
using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace BRApp
{
    /// <summary>
    /// Main page that check health status
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region Ctor

        public MainPage()
        {
            InitializeComponent();

            Loaded += (sender, args) => GetApiStatus();

            NetworkChange.NetworkAvailabilityChanged += AvailabilityChanged;
        }

        #endregion Ctor

        #region Network availability

        private async void AvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            if (!e.IsAvailable)
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
                {
                    Frame.Navigate(typeof(NoConnection));
                });
            }

            NetworkChange.NetworkAvailabilityChanged -= AvailabilityChanged;
        }

        #endregion Network availability

        #region Methods

        /// <summary>
        /// Method that calls the funtion that checks the API health
        /// </summary>
        private async void GetApiStatus()
        {
            ProgressBarcontrol.IsIndeterminate = true;

            if (HttpHandling.GetAPIHealth())
            {
                await Task.Delay(TimeSpan.FromSeconds(2));

                Frame.Navigate(typeof(ListScreen));
            }
            else
            {
                MessageDialog message = new MessageDialog("Server health is not OK! Try again later.");

                UICommand taButton = new UICommand("Try again")
                {
                    Invoked = taButtonClick
                };
                message.Commands.Add(taButton);

                Windows.Foundation.IAsyncOperation<IUICommand> messageExploder = message.ShowAsync();
            }

            ProgressBarcontrol.IsIndeterminate = false;
        }

        #endregion Methods

        #region Try again button event

        private void taButtonClick(IUICommand command)
        {
            GetApiStatus();
        }

        #endregion Try again button event

        
    }
}