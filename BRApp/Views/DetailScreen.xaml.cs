using BRApp.Controls;
using System;
using System.Net.NetworkInformation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace BRApp
{
    /// <summary>
    /// Detail screen pages where we can share and or vote one question
    /// </summary>
    public sealed partial class DetailScreen : Page
    {
        #region Vars

        private string questionIdToLoad = string.Empty;
        private Questions question;

        #endregion Vars

        #region Ctor

        public DetailScreen()
        {
            InitializeComponent();

            Loaded += (sender, args) => RetrieveQuestion();

            NetworkChange.NetworkAvailabilityChanged += AvailabilityChanged;
        }

        #endregion Ctor

        #region Internet verification method

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

        #endregion Internet verification method

        #region Navigation methods

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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            BackButton.IsEnabled = Frame.CanGoBack;
            questionIdToLoad = (string)e.Parameter;
        }

        #endregion Navigation methods

        #region Methods

        /// <summary>
        /// Funtion that connects with API to retrieve a specific question
        /// </summary>
        private void RetrieveQuestion()
        {
            question = HttpHandling.RetrieveQuestion(questionIdToLoad);

            idTxt.Text = question.Id;
            questionTxt.Text = question.Question;
            imgSrc.Source = new BitmapImage(new Uri(question.Image, UriKind.Absolute));
            publishTxt.Text = question.Publish.ToString();
            choicesGrid.ItemsSource = question.Choices;
        }

        #endregion Methods

        #region Events

        /// <summary>
        /// Share button click event
        /// </summary>
        private void shareBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ShareScreen), question);
        }

        /// <summary>
        /// Vote button click event
        /// </summary>
        private async void voteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (await HttpHandling.VoteQuestionAsync(question))
            {
                LogWriter.ShowToast("Question voted!", "Your vote has been successffully received");
            }
            else
            {
                LogWriter.ShowToast("Vote erro!", "Your vote has failed!");
            }
        }

        #endregion Events
    }
}