using BRApp.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BRApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListScreen : Page
    {
        public List<Questions> Questions { get; } = new List<Questions>();

        public ListScreen()
        {
            this.InitializeComponent();

           

            Questions = HttpHandling.GetAllQuestions();

            questionsList.ItemsSource = Questions;

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
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            BackButton.IsEnabled = this.Frame.CanGoBack;
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            On_BackRequested();
        }
        private bool On_BackRequested()
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
                return true;
            }
            return false;
        }

       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<Questions> search = from s in Questions where s.QuestionListNames.ToLower().Contains(sbar.Text.ToLower()) select s;  

            questionsList.ItemsSource = search;
        }

        private void sbar_TextChanged(object sender, TextChangedEventArgs e)
        {
            questionsList.ItemsSource = Questions;
        }

        private void questionsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Questions question = (Questions)e.AddedItems[0];
            string idToSearch = question.Id;
            
            Frame.Navigate(typeof(DetailScreen), idToSearch);
        }
    }
}
