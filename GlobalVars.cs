using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace RoutingWinApp
{

    public class WaveFiltersValues
    {
        public string FilterName { get; set; }
        public bool FilterValue { get; set; }
    }

    public static class GlobalVars
    {
        //static AS400GetRteDtaMain.GetRoutingDataServicesClient dtsMain = new AS400GetRteDtaMain.GetRoutingDataServicesClient();
        //static AS400GetRteDtaDetail.GetRoutingDetailDataServicesClient dtsDetail = new AS400GetRteDtaDetail.GetRoutingDetailDataServicesClient();
        public static Queue NewMethodCallQueue = new Queue();

        public static string LastErrorMessage = "";
        public static bool LastErrorFlag = false;
        public static string LogPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Logs\\CFRouting.log";
        public static string AS400ActiveConnection = "";
        public static string ServiceURL = "";
        public static string ServiceCode = "";
        public static string ServiceAPIKey = "";

        //public static AS400GetRteDtaMain.GetRoutingDataServicesClient DtsMain
        //{
        //    get { return dtsMain; }
        //    set { dtsMain = value; }
        //}

        //public static AS400GetRteDtaDetail.GetRoutingDetailDataServicesClient DtsDetail
        //{
        //    get { return dtsDetail; }
        //    set { dtsDetail = value; }
        //}

        static int queueretryCount = 5;

        public static int QueueretryCount
        {
            get { return queueretryCount; }
            set { queueretryCount = value; }
        }

        static int queuemaxCount = 0;

        public static int QueuemaxCount
        {
            get { return queuemaxCount; }
            set { queuemaxCount = value; }
        }
    }
}
