using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingWinApp
{
    public class CFBusinessLogic
    {

        public static string GetOrderClass(string strInvoice, string strInvoice1, string strInvoice2)
        {
            var cInvType = "";
            var cInvoice = "";

            var laList = SortInvoiceList(strInvoice, strInvoice1, strInvoice2);

            for (int i = 0; i < 3; i++)
            {
                cInvoice = laList[i];
                if (cInvoice.Trim().Length > 0)
                {
                    if (cInvoice.ToString().Trim().Length <= 5)
                        if (Convert.ToInt32(cInvoice.Trim()) > 0)
                            cInvType += "2"; //Credit Pickup
                        else
                            cInvType += "0";
                    else
                    {
                        switch (cInvoice.Substring(0, 1))
                        {
                            case "3": //Exchange
                                cInvType += "3";
                                break;
                            case "8": //Driver Service
                                cInvType += "4";
                                break;
                            case "9": // Transfer
                                cInvType += "5";
                                break;
                            default: //Invoice
                                cInvType += "1";
                                break;
                        }
                    }
                }
            }
            return cInvType;
        }

        public static string[] SortInvoiceList(string strInvoice, string strInvoice1, string strInvoice2)
        {
            var clastInvoice = "";
            var nlastPos = 0;
            var bOrdered = false;

            string[] laList = new string[3] { strInvoice, strInvoice1, strInvoice2 };

            while (bOrdered == false)
            {
                bOrdered = true;
                clastInvoice = "";
                nlastPos = 0;

                for (int i = 0; i < 3; i++)
                {
                    if (clastInvoice.Trim().Length == 0)
                    {
                        clastInvoice = laList[i];
                        nlastPos = i;
                    }
                    else
                    {
                        if (laList[i].Trim().Length > clastInvoice.Trim().Length)
                        {
                            laList[nlastPos] = laList[i];
                            laList[i] = clastInvoice;
                            bOrdered = false;
                            break;
                        }
                        else
                        {
                            clastInvoice = laList[i];
                            nlastPos = i;
                        }
                    }

                }
            }
            return laList;
        }

        public static string SortInvoiceList(string strInvoice, string strInvoice1, string strInvoice2, int intReturnInvoice)
        {
            var clastInvoice = "";
            var nlastPos = 0;
            var bOrdered = false;
            var strReturn = "";

            string[] laList = new string[3] { strInvoice, strInvoice1, strInvoice2 };

            while (bOrdered == false)
            {
                bOrdered = true;
                clastInvoice = "";
                nlastPos = 0;

                for (int i = 0; i < 3; i++)
                {
                    if (clastInvoice.Trim().Length == 0)
                    {
                        clastInvoice = laList[i];
                        nlastPos = i;
                    }
                    else
                    {
                        if (laList[i].Trim().Length > clastInvoice.Trim().Length)
                        {
                            laList[nlastPos] = laList[i];
                            laList[i] = clastInvoice;
                            bOrdered = false;
                            break;
                        }
                        else
                        {
                            clastInvoice = laList[i];
                            nlastPos = i;
                        }
                    }

                }
            }

            if (intReturnInvoice > 0)
                strReturn = laList[intReturnInvoice - 1];

            return strReturn;
        }

        public static string GetOrderClassDescription(string strOrderClass)
        {
            var cInvType = "";

            switch (strOrderClass)
            {
                case "1":
                    cInvType = "Invoice";
                    break;
                case "2":
                    cInvType = "Credit Pickup";
                    break;
                case "3":
                    cInvType = "Exchange";
                    break;
                case "4":
                    cInvType = "Driver Service";
                    break;
                case "5":
                    cInvType = "Transfer";
                    break;
            }
            return cInvType;
        }


        public static string GetOrderType(string strInvoice, string strInvoice1, string strInvoice2)
        {
            var cInvType = "";
            var cOrderType = "";
            cInvType = GetOrderClass(strInvoice, strInvoice1, strInvoice2);
            if (cInvType.IndexOf('2') >= 0 || cInvType.IndexOf('3') >= 0)
            {
                cOrderType = "Pickup";
            }
            else
            {
                cOrderType = "Delivery";
            }
            return cOrderType;
        }

        public static string GetOrderTypeDescription(string strInvType, string strStockDisp)
        {
            var cOrderType = "";
            if (strInvType.IndexOf('2') >= 0 || strInvType.IndexOf('3') >= 0)
            {
                if (strStockDisp == "F")
                    cOrderType = "Free Pickup";
                else
                    cOrderType = "Pickup";
            }
            else
            {
                if (strStockDisp == "F")
                    cOrderType = "Free Shipping";
                else
                    cOrderType = "Delivery";
            }
            return cOrderType;
        }

        public static string GetPreferedRouteID(IEnumerable<dynamic> PreferedRoutesList, string strAS400RouteID)
        {
            var cReturnPreferedRouteID = PreferedRoutesList.Where(a => a["as400routeid"] == strAS400RouteID).Select(a => a["preferedrouteid"]).FirstOrDefault();
            return (String.IsNullOrEmpty(cReturnPreferedRouteID) ? "" : cReturnPreferedRouteID);
        }
    }

}
