using System;
using System.Linq;
using System.Diagnostics;
using System.Net.Mail;

namespace RoutingWinApp
{
    class MSWindowsLogFunctions
    {
        public void WriteEventToWindowsLog(string strMyApp, string strEvent, bool bolEmail, int intEntryType)
        {
            try
            {
                if (!System.Diagnostics.EventLog.SourceExists(strMyApp))
                    System.Diagnostics.EventLog.CreateEventSource(strMyApp, "Application");

                EventLog myEventLog = new EventLog();
                myEventLog.Source = strMyApp;
                switch (intEntryType)
                {
                    case 1:
                        myEventLog.WriteEntry(strEvent, EventLogEntryType.Warning);
                        break;
                    case 2:
                        myEventLog.WriteEntry(strEvent, EventLogEntryType.SuccessAudit);
                        break;
                    case 3:
                        myEventLog.WriteEntry(strEvent, EventLogEntryType.Information);
                        break;
                    case 4:
                        myEventLog.WriteEntry(strEvent, EventLogEntryType.FailureAudit);
                        break;
                    case 5:
                        myEventLog.WriteEntry(strEvent, EventLogEntryType.Error);
                        break;
                }

            }
            catch 
            {
                //ex.Message.ToString();
            }
            if (bolEmail == true)
            {
                SendEmailMessage(strEvent);
            }
        }

        public void SendEmailMessage(string strMessage)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient();
                CFDispatchTrackApplicationSettings loAppSettings = new CFDispatchTrackApplicationSettings();

                smtpServer.Host = loAppSettings.Get("SMTPServerAddress");
                smtpServer.Port = Convert.ToInt32(loAppSettings.Get("SMTPServerPort"));
                smtpServer.Credentials = new System.Net.NetworkCredential("barcode1", "Barcode1");
                mail.From = new MailAddress(loAppSettings.Get("NotificationEmailAddressFrom"));
                mail.To.Add(loAppSettings.Get("NotificationEmailAddressTo"));
                mail.Subject = "RoadNetWebService Interface Program";
                mail.Body = strMessage;

                //smtpServer.EnableSsl = true;

                smtpServer.Send(mail);
                smtpServer.Dispose();
            }
            catch (Exception ex)
            {
                WriteEventToWindowsLog("RoadNetWebServiceInterface", ex.ToString(), false, 5);
            }
        }
    }
}