using BRApp.Controls;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BRApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DetailScreen : Page
    {
        string questionIdToLoad = string.Empty;
        Questions question;
        public DetailScreen()
        {
            this.InitializeComponent();

            Loaded += (sender, args) => RetrieveQuestion();
        }

        private void RetrieveQuestion()
        {

            question = HttpHandling.RetrieveQuestion(questionIdToLoad);

            idTxt.Text = question.Id;
            questionTxt.Text = question.Question;
            imgSrc.Source = new BitmapImage(new Uri(question.Image, UriKind.Absolute));
            publishTxt.Text = question.Publish.ToString();
            choicesGrid.ItemsSource = question.Choices;

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            BackButton.IsEnabled = this.Frame.CanGoBack;
            questionIdToLoad = (string)e.Parameter;
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            On_BackRequested();
        }
        private bool On_BackRequested()
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
                return true;
            }
            return false;
        }

        private void GridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void shareBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ShareScreen), question);
        }

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
    }
}
