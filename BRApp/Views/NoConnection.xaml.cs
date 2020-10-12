using System;
using Windows.Networking.Connectivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BRApp
{
    /// <summary>
    /// No connection screen
    /// </summary>
    public sealed partial class NoConnection : Page
    {
        #region Vars

        private DispatcherTimer timer;
        private int basetime;

        #endregion Vars

        #region Ctor

        public NoConnection()
        {
            InitializeComponent();

            TimerGo();
        }

        #endregion Ctor

        #region Timer ecountdown mechanism

        private void timer_Tick(object sender, object e)
        {
            basetime = basetime - 1;
            txtTimer.Text = basetime.ToString();
            if (basetime == 0)
            {
                timer.Stop();

                if (NetworkInformation.GetInternetConnectionProfile() != null)
                {
                    if (Frame.CanGoBack)
                    {
                        Frame.GoBack();
                    }
                }
                else
                {
                    TimerGo();
                }
            }
        }

        #endregion Timer ecountdown mechanism

        #region Timer start

        private void TimerGo()
        {
            timer = new DispatcherTimer();
            basetime = 10;
            txtTimer.Text = basetime.ToString();
            timer.Start();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += timer_Tick;
        }

        #endregion Timer start
    }
}