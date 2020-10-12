using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.System;

namespace BRApp.Controls
{
    public static class EmailHelper
    {
        internal static async Task SendEmail(string subject, string body, string recipient)
        {
            string encodedSubject = WebUtility.UrlEncode(subject).Replace("+", " ");
            string encodedBody = WebUtility.UrlEncode(body).Replace("+", " ");

            // Create a mailto URI
            Uri mailtoUri = new Uri("mailto:" + recipient + "?subject=" +
               encodedSubject +               
               "&body=" + encodedBody);

            // Execute the default application for the mailto protocol
            await Launcher.LaunchUriAsync(mailtoUri);
        }

        public static bool IsValidEmailAddress(this string s)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(s);
        }
    }
}
