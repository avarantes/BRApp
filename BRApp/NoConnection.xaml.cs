using System;
using Windows.Networking.Connectivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BRApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NoConnection : Page
    {
        private DispatcherTimer timer;
        private int basetime;

        public NoConnection()
        {
            InitializeComponent();

           

            TimerGo();
        }
        void timer_Tick(object sender, object e)
        {
            basetime = basetime - 1;
            txtTimer.Text = basetime.ToString();
            if (basetime == 0)
            {
               
                timer.Stop();

                if (NetworkInformation.GetInternetConnectionProfile() != null)
                {
                    if (this.Frame.CanGoBack)
                    {
                        this.Frame.GoBack();
                    }
                }
                else
                {
                    
                    TimerGo();
                }
            }

        }

        private void TimerGo()
        {
            timer = new DispatcherTimer();
            basetime = 10;
            txtTimer.Text = basetime.ToString();
            timer.Start();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += timer_Tick;
        }
        private void btnStart_Click_1(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
