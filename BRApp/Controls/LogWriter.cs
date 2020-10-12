using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Windows.UI.Notifications;

namespace BRApp.Controls
{
    public static class LogWriter
    {
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

        public static void ShowToast(string message, string title)
        {
            Windows.Data.Xml.Dom.XmlDocument notificationXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);
            Windows.Data.Xml.Dom.XmlNodeList toastElement = notificationXml.GetElementsByTagName("text");
            toastElement[0].AppendChild(notificationXml.CreateTextNode(title));
            toastElement[1].AppendChild(notificationXml.CreateTextNode(message));
            ToastNotification toastNotification = new ToastNotification(notificationXml);
            ToastNotificationManager.CreateToastNotifier().Show(toastNotification);
        }
    }
}
