using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingWinApp
{
    public class AS400Routeo
    {
        public string RoZone { get; set; }
        public int RoDate { get; set; }
        public int RoPos { get; set; }
        public int RoInv { get; set; }
        public string RoRouT { get; set; }
        public string RoRTyp { get; set; }
        public int RoDist { get; set; }
        public string RoXGeo { get; set; }
        public string RoYGeo { get; set; }
        public int RoSMin { get; set; }
        public int RoDMin { get; set; }
    }


    public class SQLRouteoHist : AS400Routeo
    {

        public DateTime CreateDT { get; set; }
        public int RNSessionID { get; set; }
        public DateTime RNSessionDate { get; set; }
        public int RNStopID { get; set; }
        public int RNStopPos { get; set; }
        public string RNZipCode { get; set; }
        public string RNTWStart { get; set; }
        public string RNTWEnd { get; set; }
        public int RNCubes { get; set; }
        public string RNFullName { get; set; }
        public string RNHasRestrictions { get; set; }
        public string RNHasBackOrders { get; set; }
        public string RNIsStore { get; set; }
        public string RNSessionName { get; set; }
        public string RNIsFreeShip { get; set; }
    }

    public class AS400DLVWave
    {
        public decimal WVWave { get; set; }
        public string WVName { get; set; }
        public string WVMonF { get; set; }
        public string WVTueF { get; set; }
        public string WVWedF { get; set; }
        public string WVThuF { get; set; }
        public string WVFriF { get; set; }
        public string WVSatF { get; set; }
        public string WVSunF { get; set; }
        public string WVActF { get; set; }
        public int ActiveDate { get; set; }
    }

    public class AS400DLVWaveRte
    {
        public string WRWave { get; set; }
        public string WRRout { get; set; }
        public string WRActF { get; set; }
    }

    public class RouteiMaster
    {
            public int INVOIC { get; set; }
            public string FNAME { get; set; }
            public string ADDRESS { get; set; }
            public string CITY { get; set; }
            public string STATE { get; set; }
            public int ZIP { get; set; }
            public int TIMEWS { get; set; }
            public int TIMEWF { get; set; }
            public int SERTIM { get; set; }
            public decimal WEIGHT { get; set; }
            public decimal CUBES { get; set; }
            public decimal WEIGHTP { get; set; }
            public decimal CUBESP { get; set; }
            public int MINSTP { get; set; }
            public string ZONE { get; set; }
            public int ZONEDAT { get; set; }
            public int ZONEPOS { get; set; }
            public string ZFOLDR { get; set; }
            public int PHONE { get; set; }
            public string STRETX { get; set; }
            public int RSTORE { get; set; }
            public int SLM { get; set; }
            public string GEOCDX { get; set; }
            public string GEOCDY { get; set; }
            public string XZONE { get; set; }
            public int XPOS { get; set; }
            public decimal XTRTIM { get; set; }
            public int PHONE2 { get; set; }
            public int PHONE3 { get; set; }
            public string COMENTS1 { get; set; }
            public string COMENTS2 { get; set; }
            public string COMENTS3 { get; set; }
            public string COMENTS4 { get; set; }
            public int INVOIC1 { get; set; }
            public decimal WEIGHT1 { get; set; }
            public decimal CUBES1 { get; set; }
            public decimal WEIGHT1P { get; set; }
            public decimal CUBES1P { get; set; }
            public int INVOIC2 { get; set; }
            public decimal WEIGHT2 { get; set; }
            public decimal CUBES2 { get; set; }
            public decimal WEIGHT2P { get; set; }
            public decimal CUBES2P { get; set; }
            public string DLVRST { get; set; }
            public string SBOFLG { get; set; }
            public string STKDSP { get; set; }
            public string STKDSP2 { get; set; }
            public string STKDSP3 { get; set; }
            public string CUFNAM { get; set; }
            public string CULNAM { get; set; }
            public int CUMCID { get; set; }
            public string CUEML1 { get; set; }
            public string UNIQORDID { get; set; }
            public string BATCHID { get; set; }
        }

        public class RouteiDetail
    {
            public int INVOICE { get; set; }
            public int ILINE { get; set; }
            public int IITEM { get; set; }
            public string ISERIAL { get; set; }
            public string IDESCRIPTION { get; set; }
            public int IQUANTITY { get; set; }
            public string ILOCATION { get; set; }
            public string ICUBES { get; set; }
            public decimal IWEIGHT { get; set; }
            public decimal IUNITPRICE { get; set; }
            public int ICOUNTABLE { get; set; }
            public string IPICKUPSTORE { get; set; }
            public int IMASTINV { get; set; }
            public string IUNIQORDID { get; set; }
            public int ISTORE { get; set; }
            public string IUNIQLINE { get; set; }
            public string IBATCHID { get; set; }
        }

        public class RouteIMasterArgs
        {
            public string WarehouseID { get; set; }
            public int DeliveryDate { get; set; }
            public string WaveID { get; set; }
        }

        public class DTResponse
        {
            public string success { get; set; }
        }
}
