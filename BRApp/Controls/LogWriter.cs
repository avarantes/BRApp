using System;
using System.Diagnostics;
using System.Text;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace BRApp.Controls
{
    public static class LogWriter
    {
        /// <summary>
        /// Output to catch the exceptions and write them to the console
        /// </summary>
        public static void WriteLog(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("-----------------------------------------------------------------------------");
            sb.Append("Date : " + DateTime.Now.ToString());
            sb.Append(ex.GetType().FullName);
            sb.Append("Message : " + ex.Message);
            sb.Append("StackTrace : " + ex.StackTrace);

            Console.Write(sb.ToString());
            Debug.Write(sb.ToString());
        }

        /// <summary>
        /// Trigger notifications to alter user
        /// </summary>
        /// <param name="message">Message in the notification</param>
        /// <param name="title">Notification title</param>
        public static void ShowToast(string message, string title)
        {
            XmlDocument notificationXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);
            XmlNodeList toastElement = notificationXml.GetElementsByTagName("text");
            toastElement[0].AppendChild(notificationXml.CreateTextNode(title));
            toastElement[1].AppendChild(notificationXml.CreateTextNode(message));

            ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(notificationXml));
        }
    }
}