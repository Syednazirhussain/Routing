using System;
using System.Linq;
using RoutingWinApp;
using System.Collections;
using System.Windows.Forms;


namespace RoutingWinApp
{

    class CFRoutingWinAppMain
    {

        [STAThread]
        public static int Main(string[] args)
        {
            var intReturnMode = 0;

            GlobalVars.NewMethodCallQueue = new Queue();
            GlobalVars.QueueretryCount = 0;
            GlobalVars.QueuemaxCount = 10;
            GlobalVars.AS400ActiveConnection = "POSDev";
            GlobalVars.ServiceURL = "";
            GlobalVars.ServiceCode = "";
            GlobalVars.ServiceAPIKey = "";

            //GlobalVars.DtsMain = new AS400GetRteDtaMain.GetRoutingDataServicesClient();
            //GlobalVars.DtsDetail = new AS400GetRteDtaDetail.GetRoutingDetailDataServicesClient();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CFRoutingWinAppMainToolBar());

            return intReturnMode;

        }
    }
}
