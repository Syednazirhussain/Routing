using System;
using NLog;

namespace RoutingWinApp
{
    public class ConsoleLogger : CFDispatchTrack.IMyLogger
    {

        public void ProcessLog(string sMsg)
        {
            Console.WriteLine(sMsg);
            ProcessLog(GlobalVars.LogPath, sMsg);
        }

        public void ProcessLog(int nFormID, string sMsg)
        {
            Console.WriteLine(sMsg);
            ProcessLog(GlobalVars.LogPath, sMsg);
        }

        public void ProcessLog(string sPathName, string sMsg)
        {

            Logger log = LogManager.GetCurrentClassLogger();
            LogEventInfo theLogMsg = new LogEventInfo(LogLevel.Debug, "", sMsg);
            theLogMsg.Properties["mymessage"] = sMsg;
            log.Log(theLogMsg);

            //string sLogFormat = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " ==> ";

            //string sYear = DateTime.Now.Year.ToString();
            //string sMonth = DateTime.Now.Month.ToString();
            //string sDay = DateTime.Now.Day.ToString();
            //var sErrorTime = sYear + sMonth + sDay;

            //StreamWriter sw = new StreamWriter(sPathName + "." + sErrorTime + ".Log",true);
            //sw.WriteLine(sLogFormat + sMsg);
            //sw.Flush();
            //sw.Close();        
        }


    }
}
