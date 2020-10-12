using BRApp.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace BRApp
{
    /// <summary>
    /// List screen to display the list of question obtained from the API request
    /// </summary>
    public sealed partial class ListScreen : Page
    {
        #region Vars

        public List<Questions> Questions { get; } = new List<Questions>();

        #endregion Vars

        #region Ctor

        public ListScreen()
        {
            InitializeComponent();

            Questions = HttpHandling.GetAllQuestions();

            questionsList.ItemsSource = Questions;

            NetworkChange.NetworkAvailabilityChanged += AvailabilityChanged;
        }

        #endregion Ctor

        #region Network methods

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

        #endregion Network methods

        #region Navigation methods

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

        #endregion Navigation methods

        #region Search events

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<Questions> search = Questions.Where(q => q.QuestionListNames.ToLower().Contains(sbar.Text.ToLower()));

            questionsList.ItemsSource = search;
        }

        private void sbar_TextChanged(object sender, TextChangedEventArgs e)
        {
            questionsList.ItemsSource = Questions;
        }

        #endregion Search events

        #region Selection events

        private void questionsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Questions question = (Questions)e.AddedItems[0];
            string idToSearch = question.Id;

            Frame.Navigate(typeof(DetailScreen), idToSearch);
        }

        #endregion Selection events
    }
}