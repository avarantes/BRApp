using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using System.Threading.Tasks;
using BRApp.Controls;
using Windows.UI.Popups;
using Windows.Networking.Connectivity;
using System.Net.NetworkInformation;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BRApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {



        public MainPage()
        {

            InitializeComponent();

            Loaded += (sender, args) => GetApiStatus();
          
            NetworkChange.NetworkAvailabilityChanged += AvailabilityChanged;
         
        }
        private async void AvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
                                  
            if (!e.IsAvailable)
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () => {
                    Frame.Navigate(typeof(NoConnection));
                });
               
            }

            NetworkChange.NetworkAvailabilityChanged -= AvailabilityChanged;
        }

        private async void GetApiStatus()
        {
            ProgressBarcontrol.IsIndeterminate = true;

            if (HttpHandling.GetAPIHealth())
            {
                //MessageDialog message = new MessageDialog("Server cheked successfully!");
                //Windows.Foundation.IAsyncOperation<IUICommand> messageExploder = message.ShowAsync();
                await Task.Delay(TimeSpan.FromSeconds(2));

                Frame.Navigate(typeof(ListScreen));

                //messageExploder.Cancel();

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



        private void taButtonClick(IUICommand command)
        {
            GetApiStatus();
        }

        private void ProgressBarcontrol_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {

        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}