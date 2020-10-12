using BRApp.Controls;
using System;
using System.Text;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BRApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShareScreen : Page
    {
        private Questions question;
        public ShareScreen()
        {
            this.InitializeComponent();
            Loaded += (sender, args) => LoadInfo();
        }

        private void LoadInfo()
        {
            idTxt.Text = question.Id;
            questionTxt.Text = question.Question;
            imgSrc.Source = new BitmapImage(new Uri(question.Image, UriKind.Absolute));
            publishTxt.Text = question.Publish.ToString();
            choicesGrid.ItemsSource = question.Choices;
        }

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
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
                return true;
            }
            return false;
        }

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
                foreach (Choices choice in question.Choices)
                {
                    sb.AppendLine(choice.ChoicesQuestionVotes);
                }


                await EmailHelper.SendEmail(subject, sb.ToString(), emailTxt.Text);
            }
        }

        
    }
}
