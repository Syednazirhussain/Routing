using System;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using NLog;
using RoutingWinApp;

namespace RoutingWinApp
{

    public class FormLogger : CFDispatchTrack.IMyLogger
    {

        private Form _form;

        public FormLogger(Form form)
        {
            this._form = form;
        }

        public void ProcessLog(string sMsg)
        {
        }

        public void ProcessLog(int nFormID, string sMsg)
        {
            if (nFormID == 1) //"CFRoadNetImportOrders"
            {
                ((CFDispatchTrackImportOrders)_form).txtProcessLog.Text += sMsg.Trim() + Environment.NewLine;
                ((CFDispatchTrackImportOrders)_form).txtProcessLog.Refresh();
                ((CFDispatchTrackImportOrders)_form).JobProgressBar.PerformStep();
            }
            if (nFormID == 2) //"CFRoadNetImportOrders"
            {
                ((CFDispatchTrackExportOrders)_form).txtProcessLog.Text += sMsg.Trim() + Environment.NewLine;
                ((CFDispatchTrackExportOrders)_form).txtProcessLog.Refresh();
                ((CFDispatchTrackExportOrders)_form).JobProgressBar.PerformStep();
            }
            ProcessLog(GlobalVars.LogPath, sMsg);            
        }

        public void ProcessLog(string sPathName, string sErrMsg)
        {

            Logger log = LogManager.GetCurrentClassLogger();
            log.Log(LogLevel.Debug, sErrMsg);

            //string sLogFormat = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " ==> ";

            //string sYear = DateTime.Now.Year.ToString();
            //string sMonth = DateTime.Now.Month.ToString();
            //string sDay = DateTime.Now.Day.ToString();
            //var sErrorTime = sYear + sMonth + sDay;

            //var pathDirectory = Path.GetDirectoryName(sPathName);
                
            //try 
            //{
            //    if (!Directory.Exists(pathDirectory))
            //    {
            //        Directory.CreateDirectory(pathDirectory);
            //    }
            //}
            //catch (Exception)
            //{

            //    pathDirectory = sPathName;
            //}

            //StreamWriter sw = new StreamWriter(sPathName + "." + sErrorTime + ".Log",true);
            //sw.WriteLine(sLogFormat + sErrMsg);
            //sw.Flush();
            //sw.Close();        
        }

    
    }


}
