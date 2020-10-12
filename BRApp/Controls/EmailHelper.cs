using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.System;

namespace BRApp.Controls
{
    /// <summary>
    /// Emailing tools
    /// </summary>
    public static class EmailHelper
    {
        /// <summary>
        /// Methods to trigger the default mail sender with a pre composed message
        /// </summary>
        /// <param name="subject">Subject for the e-mail</param>
        /// <param name="body">Body messagem for the e-mail</param>
        /// <param name="recipient">Destination e-mail</param>
        /// <returns></returns>
        internal static async Task SendEmail(string subject, string body, string recipient)
        {
            string encodedSubject = WebUtility.UrlEncode(subject).Replace("+", " ");
            string encodedBody = WebUtility.UrlEncode(body).Replace("+", " ");

            Uri mailtoUri = new Uri("mailto:" + recipient + "?subject=" + encodedSubject + "&body=" + encodedBody);

            await Launcher.LaunchUriAsync(mailtoUri);
        }

        /// <summary>
        /// Method that verifies if the inserted e-mail has a valid format
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsValidEmailAddress(this string s)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(s);
        }
    }
}