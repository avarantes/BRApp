using BRApp.Controls;
using System;
using System.Text;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace BRApp
{
    /// <summary>
    /// Share screen where we can share (by email) the questino info
    /// </summary>
    public sealed partial class ShareScreen : Page
    {
        #region Vars

        private Questions question;

        #endregion Vars

        #region Ctor

        public ShareScreen()
        {
            this.InitializeComponent();
            Loaded += (sender, args) => LoadInfo();
        }

        #endregion

        #region Methods

        private void LoadInfo()
        {
            idTxt.Text = question.Id;
            questionTxt.Text = question.Question;
            imgSrc.Source = new BitmapImage(new Uri(question.Image, UriKind.Absolute));
            publishTxt.Text = question.Publish.ToString();
            choicesGrid.ItemsSource = question.Choices;
        }

        #endregion Methods

        #region Navigation methods

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            BackButton.IsEnabled = this.Frame.CanGoBack;
            question = (Questions)e.Parameter;
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

        #region Events

        /// <summary>
        /// Send email button click event
        /// </summary>
        private async void SendEmailBtn_Click(object sender, RoutedEventArgs e)
        {
            emailTxt.BorderBrush = new SolidColorBrush(Colors.Gray);
            if (!EmailHelper.IsValidEmailAddress(emailTxt.Text))
            {
                LogWriter.ShowToast("Invalid e-mail!", "Please insert a valid e-mail!");
                emailTxt.BorderBrush = new SolidColorBrush(Colors.Red);
                emailTxt.Focus(FocusState.Programmatic);
            }
            else
            {
                string subject = $"BRApp Share Screen about question:{question.Id}";
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(question.QuestionListNames);
                sb.AppendLine(question.Publish.ToString());
                sb.AppendLine("Choices:");
                question.Choices.ForEach(n => sb.AppendLine(n.ChoicesQuestionVotes));
                
                await EmailHelper.SendEmail(subject, sb.ToString(), emailTxt.Text);
            }
        }

        #endregion Events
    }
}