using System;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using NLog;


namespace RoutingWinApp
{

    public class AppLogFile
    {

       public delegate void EventHandler1(object sender, EventArgs e);

       public event EventHandler1 FormProcessLogBoxHook;


       public void ProcessLog(string sPathName, string sErrMsg)
       {
            
           Logger log = LogManager.GetCurrentClassLogger();
           LogEventInfo theLogMsg = new LogEventInfo(LogLevel.Debug, "", sErrMsg);
           theLogMsg.Properties["mymessage"] = sErrMsg;
           log.Log(theLogMsg);

           UpdateFormProcessLogBoxHook(EventArgs.Empty);
       }


        //public void ProcessLog(string sPathName, string sErrMsg)
        //{
                       
        //    //sLogFormat used to create Log files format :
        //    // dd/mm/yyyy hh:mm:ss AM/PM ==> Log Message
        //    string sLogFormat = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " ==> ";

        //    //this variable used to create Log filename format "
        //    //for example filename : ErrorLogYYYYMMDD
        //    string sYear = DateTime.Now.Year.ToString();
        //    string sMonth = DateTime.Now.Month.ToString();
        //    string sDay = DateTime.Now.Day.ToString();
        //    var sErrorTime = sYear + sMonth + sDay;
        
        //    StreamWriter sw = new StreamWriter(sPathName + "." + sErrorTime + ".Log",true);
        //    sw.WriteLine(sLogFormat + sErrMsg);
        //    sw.Flush();
        //    sw.Close();

        //    UpdateFormProcessLogBoxHook(EventArgs.Empty);
        //}


        protected virtual void UpdateFormProcessLogBoxHook(EventArgs e)
        {
            if (FormProcessLogBoxHook != null)
                FormProcessLogBoxHook(this, e);
        }
    }
}