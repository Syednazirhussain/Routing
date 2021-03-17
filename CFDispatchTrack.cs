using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using System.Xml.Linq;
using System.Data;
using System.Net;
using System.IO;
using System.Globalization;
using System.Xml.Serialization;
using System.Xml;

namespace RoutingWinApp
{
    public class CFDispatchTrack
    {
        private readonly IMyLogger myLogger;

        public CFDispatchTrack() : this(new ConsoleLogger())
        {
        }

        public CFDispatchTrack(IMyLogger log)
        {
            myLogger = log;
        }

        public interface IMyLogger
        {
            void ProcessLog(string sMsg);
            void ProcessLog(int nFormID, string sMsg);
            void ProcessLog(string sPathName, string sMsg);
        }
        //public XDocument Build_DT_XML_AS400BookedStops(string strDate, string strRoutingZone, List<WaveFiltersValues> filtersList, List<int> intWavesList, string warehouseID)
        //{
        //    var clsMSWindowsLog = new MSWindowsLogFunctions();
        //    var appLogFile = new AppLogFile();
        //    int currentCustomerRecord = 0;
        //    string batchID = "";
        //    string uniqueOrderID = "";
        //    string orderDateMainOnly = "";
        //    string ordersList = "";
        //    string orderClass = "";
        //    List<string> orderClassDescription = new List<string>();
        //    int itemsCounter = 0;
        //    DateTime ordersDate = Convert.ToDateTime(strDate);
        //    DataAccess as400Connection = new DataAccess();

        //    XDocument xDoc = new XDocument();
        //    xDoc.Declaration = new XDeclaration("1.0", "UTF-8", null);
        //    XElement ServiceOrders = new XElement("service_orders");

        //    foreach (int listitem in intWavesList)
        //    {
        //        DateTime dtRouteDate;
        //        string strSessionName = "";
        //        string strSessionDescription = "";

        //        DataAccess roadNetWaves = new DataAccess();
        //        DataTable daSessionMasterChilds = new DataTable();
        //        DataTable daSessionPreferedRoutes = new DataTable();

        //        daSessionMasterChilds = roadNetWaves.GetSessionsMasterBySessionID(listitem, 3);

        //        if (daSessionMasterChilds.Rows.Count == 0)
        //            daSessionMasterChilds = roadNetWaves.GetSessionsMasterBySessionID(listitem, 1);

        //        if (daSessionMasterChilds.Rows.Count > 0)
        //        {
        //            foreach (DataRow dtrow in daSessionMasterChilds.Rows)
        //            {
        //                if ((int)dtrow["as400sessionid_fk"] > 0)
        //                {
        //                    if ((int)dtrow["adddates"] > 0)
        //                    {
        //                        dtRouteDate = ordersDate.AddDays((int)dtrow["adddates"]);
        //                    }
        //                    else
        //                    {
        //                        dtRouteDate = ordersDate;
        //                    }

        //                    strSessionName = "TAMARAC-" + strRoutingZone.Replace(' ', '-');
        //                    strSessionDescription = (string)dtrow["sessionname"];
        //                    daSessionPreferedRoutes = roadNetWaves.GetPreferedRouteBySessionID((int)dtrow["sessionid_pk"], 2);

        //                    List<dynamic> dynlist = new List<dynamic>();

        //                    foreach (DataRow dtrow2 in daSessionPreferedRoutes.Rows)
        //                    {
        //                        dynlist.Add(dtrow2);
        //                    }


        //                    AS400GetRteDtaMain.getrtedtaResult iEBookedStops = new AS400GetRteDtaMain.getrtedtaResult();
        //                    iEBookedStops = as400Connection.GetRoutingDataFromAS400APIRouteiMaster(dtRouteDate, Convert.ToInt32(dtrow["as400sessionid_fk"]), strRoutingZone, filtersList, warehouseID);
        //                    if (iEBookedStops.P_RESPONSE.SUCCESSFUL == "Y")
        //                    {
        //                        batchID = iEBookedStops.P_RESPONSE.BATCHID;
        //                        if (iEBookedStops.P_RESPONSE.CUSTOMERS.Length > 0)
        //                        {
        //                            myLogger.ProcessLog(1, string.Format("Building DispatchTrack XML for Delivery Date: {0} Orders: {1}", dtRouteDate.ToString("MM/dd/yyyy"), iEBookedStops.P_RESPONSE.CUSTOMERS.Length));

        //                            currentCustomerRecord = 0;
        //                            XElement Invoice;
        //                            XElement Customer;
        //                            XElement Notes;
        //                            XElement Extra;
        //                            XElement CustomFields;
        //                            XElement Items;
        //                            XNamespace empNM = "urn:lst-emp:emp";

        //                            foreach (AS400GetRteDtaMain.customerT customer in iEBookedStops.P_RESPONSE.CUSTOMERS)
        //                            {
        //                                if (iEBookedStops.P_RESPONSE.CUSTOMERS.Length > currentCustomerRecord) { 

        //                                    Customer = new XElement("customer",
        //                                    new XElement("customer_id", customer.CUSTOMERID),
        //                                    new XElement("first_name", customer.FIRSTNAME),
        //                                    new XElement("last_name", customer.LASTNAME),
        //                                    new XElement("email", customer.EMAIL),
        //                                    new XElement("phone1", Convert.ToInt64(customer.PHONE1)!=0? customer.PHONE1: ""),
        //                                    new XElement("phone2", Convert.ToInt64(customer.PHONE2) != 0 ? customer.PHONE2 : ""),
        //                                    new XElement("phone3", Convert.ToInt64(customer.PHONE3) != 0 ? customer.PHONE3 : ""),
        //                                    new XElement("address1", customer.ADDRESS1),
        //                                    new XElement("address2", customer.ADDRESS2),
        //                                    new XElement("city", customer.CITY),
        //                                    new XElement("state", customer.STATE),
        //                                    new XElement("zip", customer.ZIP5.Trim() + (customer.ZIP4.Trim() != ""? ("-"+ customer.ZIP4.Trim()) : "")),
        //                                    new XElement("latitude", customer.GEOCODEX),
        //                                    new XElement("longitute", customer.GEOCODEY)
        //                                    );

        //                                Notes = new XElement("notes");

        //                                XElement NotesItems;
        //                                var notescnt = 0;


        //                                if (customer.COMMENTS.COMMENT1.Trim().Length > 1)
        //                                {
        //                                    NotesItems = new XElement("note",
        //                                        new XAttribute("created_at", ""),
        //                                        new XAttribute("author", ""),
        //                                        customer.COMMENTS.COMMENT1
        //                                    );
        //                                    Notes.Add(NotesItems);
        //                                    notescnt++;
        //                                }

        //                                if (customer.COMMENTS.COMMENT2.Trim().Length > 1)
        //                                {
        //                                    NotesItems = new XElement("note",
        //                                        new XAttribute("created_at", ""),
        //                                        new XAttribute("author", ""),
        //                                        customer.COMMENTS.COMMENT2
        //                                    );
        //                                    Notes.Add(NotesItems);
        //                                    notescnt++;
        //                                }

        //                                if (customer.COMMENTS.COMMENT3.Trim().Length > 1)
        //                                {
        //                                    NotesItems = new XElement("note",
        //                                        new XAttribute("created_at", ""),
        //                                        new XAttribute("author", ""),
        //                                        customer.COMMENTS.COMMENT3
        //                                    );
        //                                    Notes.Add(NotesItems);
        //                                    notescnt++;
        //                                }

        //                                if (customer.COMMENTS.COMMENT4.Trim().Length > 1)
        //                                {
        //                                    NotesItems = new XElement("note",
        //                                        new XAttribute("created_at", ""),
        //                                        new XAttribute("author", ""),
        //                                        customer.COMMENTS.COMMENT4
        //                                    );
        //                                    Notes.Add(NotesItems);
        //                                    notescnt++;
        //                                }

        //                                Notes.Add(new XAttribute("count", notescnt));

        //                                CustomFields = new XElement("custom_fields");

        //                                Items = new XElement("items");

        //                                XElement ItemElement;

        //                                ordersList = "";
        //                                orderClass = "";
        //                                orderClassDescription.Clear();
        //                                orderDateMainOnly = "";

        //                                    AS400GetRteDtaMain.orderT order = new AS400GetRteDtaMain.orderT();

        //                                    if (order.INVOICE != 0)
        //                                    {
        //                                        string uniqueOrderToPull = "";
        //                                        uniqueOrderID = order.UNIQUEORDERID.ToString().Trim();
        //                                            if (uniqueOrderID.Length > 7)
        //                                                uniqueOrderToPull = uniqueOrderID;
        //                                            else
        //                                            {
        //                                                uniqueOrderToPull = uniqueOrderID.Substring(0, 2) + order.INVOICE.ToString().Trim().PadLeft(7,'0') + uniqueOrderID.Substring(uniqueOrderID.Length - 7, 7);
        //                                            }
        //                                        if (orderDateMainOnly == "")
        //                                                orderDateMainOnly = DateTime.ParseExact(order.INVOICEDATE.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyy-MM-dd");
        //                                        ordersList += (ordersList.Trim().Length > 0 ? "," : "") + order.INVOICE.ToString().Trim();
        //                                        orderClass += order.INVOICETYPE.ToString().Trim();
        //                                        orderClassDescription.Add(CFBusinessLogic.GetOrderClassDescription(order.INVOICETYPE.ToString().Trim()));

        //                                        AS400GetRteDtaDetail.getrtedetResult iEBookedStopsDetail = new AS400GetRteDtaDetail.getrtedetResult();

        //                                        iEBookedStopsDetail = as400Connection.GetRoutingDataFromAS400APIRouteiDetail(uniqueOrderToPull, batchID);
        //                                        if (iEBookedStopsDetail.P_RESPONSE.SUCCESSFUL == "Y")
        //                                        {
        //                                            if (iEBookedStopsDetail.P_RESPONSE.RECORDCOUNT > 0)
        //                                            {
        //                                                itemsCounter = 0;
        //                                                foreach (AS400GetRteDtaDetail.itemT item in iEBookedStopsDetail.P_RESPONSE.ITEMS)
        //                                                {
        //                                                    if (iEBookedStopsDetail.P_RESPONSE.RECORDCOUNT > itemsCounter)
        //                                                    {
        //                                                        ItemElement = new XElement("item",
        //                                                            new XElement("sale_sequence", item.INVOICELINE),
        //                                                            new XElement("number", order.INVOICE),
        //                                                            new XElement("item_id", item.ITEM),
        //                                                            new XElement("serial_number", item.SERIAL),
        //                                                            new XElement("description", item.DESCRIPTION),
        //                                                            new XElement("quantity", item.QUANTITY),
        //                                                            new XElement("location", item.WAREHOUSE),
        //                                                            new XElement("cube", item.CUBES),
        //                                                            new XElement("setup_time", item.SKUASSEMBLYTIME),
        //                                                            new XElement("weight", item.WEIGHT),
        //                                                            new XElement("price", item.UNITPRICE),
        //                                                            new XElement("countable", 1),
        //                                                            new XElement("store_code", order.STORE)
        //                                                            );
        //                                                        Items.Add(ItemElement);
        //                                                        itemsCounter++;
        //                                                    }
        //                                                    else
        //                                                        break;
        //                                                }
        //                                            }
        //                                        }
        //                                    }
        //                                    else
        //                                    {
        //                                        orderClass += "0";
        //                                    }

        //                                Extra = new XElement("extra",
        //                                   new XElement("RoutePos", customer.CUSTOM.ZONEPOSITION),
        //                                   new XElement("AS400Routes", customer.CUSTOM.ZONE),
        //                                   new XElement("WaveID", strRoutingZone),
        //                                   new XElement("oClass", orderClass),
        //                                   new XElement("Invoice1_OrderClass", (orderClassDescription.Count > 0 ? orderClassDescription[0].ToString() : "")),
        //                                   new XElement("Invoice2_OrderClass", orderClassDescription.Count > 1 ? orderClassDescription[1].ToString() : ""),
        //                                   new XElement("Invoice3_OrderClass", orderClassDescription.Count > 2 ? orderClassDescription[2].ToString() : "")
        //                               );
        //                                //Extra.Add(Extra2);

        //                                DateTime deliveryDate = DateTime.ParseExact(customer.CUSTOM.ZONEDATE.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);

        //                                Invoice = new XElement("service_order",
        //                                    new XElement("number", uniqueOrderID),
        //                                    new XElement("account", strSessionName),
        //                                    new XElement("service_type", CFBusinessLogic.GetOrderTypeDescription(orderClass)),
        //                                    new XElement("description", strSessionDescription),
        //                                    Customer,
        //                                    Notes,
        //                                    Items,
        //                                    new XElement("pre_reqs", ordersList),
        //                                    new XElement("amount", 0.00),
        //                                    new XElement("cod_amount", 0.00),
        //                                    new XElement("service_unit", strRoutingZone),
        //                                    new XElement("delivery_date", deliveryDate.ToString("yyy-MM-dd")),
        //                                    new XElement("request_delivery_date", deliveryDate.ToString("yyy-MM-dd")),
        //                                    new XElement("driver_id", ""),
        //                                    new XElement("truck_id", ""),
        //                                    new XElement("origin", "04"),
        //                                    new XElement("stop_number", 0),
        //                                    new XElement("stop_time", ""),
        //                                    new XElement("service_time", customer.SERVICETIME),
        //                                    new XElement("request_time_window_start", deliveryDate.ToString("yyy-MM-dd") + " " + customer.CUSTOM.TIMEWINDOWSTART.ToString("00:00")),
        //                                    new XElement("request_time_window_end", deliveryDate.ToString("yyy-MM-dd") + " " + customer.CUSTOM.TIMEWINDOWEND.ToString("00:00")),
        //                                    new XElement("delivery_time_window_start", deliveryDate.ToString("yyy-MM-dd") + " " + customer.CUSTOM.TIMEWINDOWSTART.ToString("00:00")),
        //                                    new XElement("delivery_time_window_end", deliveryDate.ToString("yyy-MM-dd") + " " + customer.CUSTOM.TIMEWINDOWEND.ToString("00:00")),
        //                                    new XElement("delivery_charge", 0.00),
        //                                    new XElement("taxes", 0.00),
        //                                    new XElement("store_code", ""),
        //                                    Extra,
        //                                    CustomFields
        //                                );
        //                                ServiceOrders.Add(Invoice);
        //                                currentCustomerRecord++;
        //                                }
        //                            }
        //                        }
        //                        else
        //                        {
        //                            myLogger.ProcessLog(1, string.Format("No order(s) sent to DispatchTrack for delivery date: {0}", dtRouteDate));
        //                        }
        //                    }
        //                    else
        //                    {
        //                        myLogger.ProcessLog(1, string.Format("Error calling AS400 Import Routing Webservice."));
        //                        clsMSWindowsLog.SendEmailMessage(string.Format("Error calling AS400 Import Routing Webservice."));
        //                    }
        //                }
        //            }
                    
        //        }
        //    }
        //    xDoc.Add(ServiceOrders);
        //    return xDoc;
        //}

        //public XDocument Build_DT_XML_AS400BookedStopsSingleOrderMode(string strDate, string strRoutingZone, List<WaveFiltersValues> filtersList, List<int> intWavesList, string warehouseID)
        //{
        //    var clsMSWindowsLog = new MSWindowsLogFunctions();
        //    var appLogFile = new AppLogFile();
        //    int currentCustomerRecord = 0;
        //    string batchID = "";
        //    string uniqueOrderID = "";
        //    string orderMainOrder = "";
        //    string orderMainStore = "";
        //    string ordersList = "";
        //    string orderClass = "";
        //    string orderType = "";
        //    string orderStockDisp = "";
        //    int itemsCounter = 0;
        //    DateTime ordersDate = Convert.ToDateTime(strDate);
        //    DataAccess as400Connection = new DataAccess();

        //    XDocument xDoc = new XDocument();
        //    xDoc.Declaration = new XDeclaration("1.0", "UTF-8", null);
        //    XElement ServiceOrders = new XElement("service_orders");

        //    foreach (int listitem in intWavesList)
        //    {
        //        DateTime dtRouteDate;
        //        string strSessionName = "";
        //        string strSessionDescription = "";

        //        DataAccess roadNetWaves = new DataAccess();
        //        DataTable daSessionMasterChilds = new DataTable();
        //        DataTable daSessionPreferedRoutes = new DataTable();

        //        daSessionMasterChilds = roadNetWaves.GetSessionsMasterBySessionID(listitem, 3);

        //        if (daSessionMasterChilds.Rows.Count == 0)
        //            daSessionMasterChilds = roadNetWaves.GetSessionsMasterBySessionID(listitem, 1);

        //        if (daSessionMasterChilds.Rows.Count > 0)
        //        {
        //            foreach (DataRow dtrow in daSessionMasterChilds.Rows)
        //            {
        //                if ((int)dtrow["as400sessionid_fk"] > 0)
        //                {
        //                    if ((int)dtrow["adddates"] > 0)
        //                    {
        //                        dtRouteDate = ordersDate.AddDays((int)dtrow["adddates"]);
        //                    }
        //                    else
        //                    {
        //                        dtRouteDate = ordersDate;
        //                    }

        //                    strSessionName = "TAMARAC-" + strRoutingZone.Replace(' ', '-');
        //                    strSessionDescription = (string)dtrow["sessionname"];
        //                    daSessionPreferedRoutes = roadNetWaves.GetPreferedRouteBySessionID((int)dtrow["sessionid_pk"], 2);

        //                    List<dynamic> dynlist = new List<dynamic>();

        //                    foreach (DataRow dtrow2 in daSessionPreferedRoutes.Rows)
        //                    {
        //                        dynlist.Add(dtrow2);
        //                    }


        //                    AS400GetRteDtaMain.getrtedtaResult iEBookedStops = new AS400GetRteDtaMain.getrtedtaResult();
                            
        //                    iEBookedStops = as400Connection.GetRoutingDataFromAS400APIRouteiMaster(dtRouteDate, Convert.ToInt32(dtrow["as400sessionid_fk"]), strRoutingZone, filtersList, warehouseID);
        //                    if (iEBookedStops.P_RESPONSE != null && iEBookedStops.P_RESPONSE.SUCCESSFUL == "Y")
        //                    {
        //                        batchID = iEBookedStops.P_RESPONSE.BATCHID;
        //                        if (iEBookedStops.P_RESPONSE.CUSTOMERS.Length > 0)
        //                        {
        //                            myLogger.ProcessLog(1, string.Format("Building DispatchTrack XML for Session: {0} - Delivery Date: {1} - Orders: {2}", strSessionDescription, dtRouteDate.ToString("MM/dd/yyyy"), iEBookedStops.P_RESPONSE.CUSTOMERS.Length));

        //                            currentCustomerRecord = 0;
        //                            XElement Invoice;
        //                            XElement Customer;
        //                            XElement Notes;
        //                            XElement Extra;
        //                            XElement CustomFields;
        //                            XElement Items;
        //                            XNamespace empNM = "urn:lst-emp:emp";

        //                            foreach (AS400GetRteDtaMain.customerT customer in iEBookedStops.P_RESPONSE.CUSTOMERS)
        //                            {

        //                                if (iEBookedStops.P_RESPONSE.CUSTOMERS.Length > currentCustomerRecord)
        //                                {
        //                                    DateTime deliveryDate = DateTime.ParseExact(customer.CUSTOM.ZONEDATE.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
        //                                    XElement ItemElement;
        //                                    orderClass = "";
        //                                    orderType = "";
        //                                    orderMainOrder = "";
        //                                    orderMainStore = "";


        //                                    AS400GetRteDtaMain.orderT order = customer.ORDER;


        //                                        if (order.INVOICE != 0)
        //                                        {
        //                                            Items = new XElement("items");
        //                                            string uniqueOrderToPull = "";
        //                                            uniqueOrderID = order.INVOICE.ToString().Trim();
        //                                            if (uniqueOrderID.Length > 7)
        //                                                uniqueOrderToPull = uniqueOrderID;
        //                                            else
        //                                            {
        //                                                uniqueOrderToPull = deliveryDate.ToString("yyMMdd") + "-" + order.INVOICE.ToString().Trim().PadLeft(6, '0');
        //                                            }
        //                                            orderMainOrder = order.MASTERORDERID.ToString();
        //                                            orderMainStore = order.MASTERSTORE.ToString();
        //                                            ordersList = order.INVOICE.ToString().Trim();
        //                                            orderClass = order.INVOICETYPE.ToString().Trim();
        //                                            orderStockDisp = order.STOCKDISPOSITION.ToString().Trim();
        //                                            orderType = CFBusinessLogic.GetOrderClassDescription(orderClass);

        //                                            AS400GetRteDtaDetail.getrtedetResult iEBookedStopsDetail = new AS400GetRteDtaDetail.getrtedetResult();

        //                                            iEBookedStopsDetail = as400Connection.GetRoutingDataFromAS400APIRouteiDetail(uniqueOrderToPull, batchID);
        //                                            if (iEBookedStopsDetail.P_RESPONSE.SUCCESSFUL == "Y")
        //                                            {
        //                                                if (iEBookedStopsDetail.P_RESPONSE.ITEMS.Length > 0)
        //                                                {
        //                                                    itemsCounter = 0;
        //                                                    foreach (AS400GetRteDtaDetail.itemT item in iEBookedStopsDetail.P_RESPONSE.ITEMS)
        //                                                    {
        //                                                        if (iEBookedStopsDetail.P_RESPONSE.ITEMS.Length > itemsCounter)
        //                                                        {
        //                                                            if (order.INVOICE == iEBookedStopsDetail.P_RESPONSE.INVOICE)
        //                                                            {
        //                                                                ItemElement = new XElement("item",
        //                                                                    new XElement("sale_sequence", item.INVOICELINE),
        //                                                                    new XElement("number", order.INVOICE),
        //                                                                    new XElement("item_id", item.ITEM),
        //                                                                    new XElement("serial_number", item.SERIAL),
        //                                                                    new XElement("description", item.DESCRIPTION),
        //                                                                    new XElement("quantity", item.QUANTITY),
        //                                                                    new XElement("location", item.WAREHOUSE),
        //                                                                    new XElement("cube", item.CUBES),
        //                                                                    new XElement("setup_time", item.SKUASSEMBLYTIME),
        //                                                                    new XElement("weight", item.WEIGHT),
        //                                                                    new XElement("price", item.UNITPRICE),
        //                                                                    new XElement("countable", 1),
        //                                                                    new XElement("store_code", order.STORE)
        //                                                                    );
        //                                                                Items.Add(ItemElement);
        //                                                                itemsCounter++;
        //                                                            }
        //                                                        }
        //                                                        else
        //                                                            break;
        //                                                    }
        //                                                }
        //                                            }

        //                                            Customer = new XElement("customer",
        //                                                new XElement("customer_id", customer.CUSTOMERID),
        //                                                new XElement("first_name", customer.FIRSTNAME),
        //                                                new XElement("last_name", customer.LASTNAME),
        //                                                new XElement("email", customer.EMAIL),
        //                                                new XElement("phone1", customer.PHONE1 != "" ? customer.PHONE1 : ""),
        //                                                new XElement("phone2", customer.PHONE2 != "" ? customer.PHONE2 : ""),
        //                                                new XElement("phone3", customer.PHONE3 != "" ? customer.PHONE3 : ""),
        //                                                new XElement("address1", customer.ADDRESS1),
        //                                                new XElement("address2", customer.ADDRESS2),
        //                                                new XElement("city", customer.CITY),
        //                                                new XElement("state", customer.STATE),
        //                                                new XElement("zip", customer.ZIP5.Trim()), // + (customer.ZIP4.Trim() != "" ? ("-" + customer.ZIP4.Trim()) : "")),
        //                                                new XElement("longitute", customer.GEOCODEX),
        //                                                new XElement("latitude", customer.GEOCODEY)
        //                                            );

        //                                            Notes = new XElement("notes");

        //                                            XElement NotesItems;
        //                                            var notescnt = 0;


        //                                            if (customer.COMMENTS.COMMENT1.Trim().Length > 1)
        //                                            {
        //                                                NotesItems = new XElement("note",
        //                                                    new XAttribute("created_at", ""),
        //                                                    new XAttribute("author", ""),
        //                                                    customer.COMMENTS.COMMENT1
        //                                                );
        //                                                Notes.Add(NotesItems);
        //                                                notescnt++;
        //                                            }

        //                                            if (customer.COMMENTS.COMMENT2.Trim().Length > 1)
        //                                            {
        //                                                NotesItems = new XElement("note",
        //                                                    new XAttribute("created_at", ""),
        //                                                    new XAttribute("author", ""),
        //                                                    customer.COMMENTS.COMMENT2
        //                                                );
        //                                                Notes.Add(NotesItems);
        //                                                notescnt++;
        //                                            }

        //                                            if (customer.COMMENTS.COMMENT3.Trim().Length > 1)
        //                                            {
        //                                                NotesItems = new XElement("note",
        //                                                    new XAttribute("created_at", ""),
        //                                                    new XAttribute("author", ""),
        //                                                    customer.COMMENTS.COMMENT3
        //                                                );
        //                                                Notes.Add(NotesItems);
        //                                                notescnt++;
        //                                            }

        //                                            if (customer.COMMENTS.COMMENT4.Trim().Length > 1)
        //                                            {
        //                                                NotesItems = new XElement("note",
        //                                                    new XAttribute("created_at", ""),
        //                                                    new XAttribute("author", ""),
        //                                                    customer.COMMENTS.COMMENT4
        //                                                );
        //                                                Notes.Add(NotesItems);
        //                                                notescnt++;
        //                                            }

        //                                            Notes.Add(new XAttribute("count", notescnt));

        //                                            CustomFields = new XElement("custom_fields");

        //                                            //Items = new XElement("items");

        //                                            Extra = new XElement("extra",
        //                                               new XElement("RoutePos", customer.CUSTOM.ZONEPOSITION),
        //                                               new XElement("AS400Routes", customer.CUSTOM.ZONE),
        //                                               new XElement("WaveID", strRoutingZone),
        //                                               new XElement("OrderType", orderType.ToString()),
        //                                               new XElement("MainStopOrder", orderMainOrder.ToString())
        //                                            );

        //                                            Invoice = new XElement("service_order",
        //                                                new XElement("number", uniqueOrderToPull),
        //                                                new XElement("account", strSessionName),
        //                                                new XElement("service_type", CFBusinessLogic.GetOrderTypeDescription(orderClass, orderStockDisp)),
        //                                                new XElement("description", strSessionDescription),
        //                                                Customer,
        //                                                Notes,
        //                                                Items,
        //                                                new XElement("pre_reqs", ""),
        //                                                new XElement("amount", 0.00),
        //                                                new XElement("cod_amount", 0.00),
        //                                                new XElement("service_unit", ""),
        //                                                new XElement("delivery_date", deliveryDate.ToString("yyy-MM-dd")),
        //                                                new XElement("request_delivery_date", deliveryDate.ToString("yyy-MM-dd")),
        //                                                new XElement("driver_id", ""),
        //                                                new XElement("truck_id", ""),
        //                                                new XElement("origin", "04"),
        //                                                new XElement("stop_number", 0),
        //                                                new XElement("stop_time", ""),
        //                                                new XElement("service_time", customer.CUSTOM.MINUTESATSTOP),
        //                                                new XElement("request_time_window_start", deliveryDate.ToString("yyy-MM-dd") + " " + customer.CUSTOM.TIMEWINDOWSTART.ToString("00:00")),
        //                                                new XElement("request_time_window_end", deliveryDate.ToString("yyy-MM-dd") + " " + customer.CUSTOM.TIMEWINDOWEND.ToString("00:00")),
        //                                                new XElement("delivery_time_window_start", deliveryDate.ToString("yyy-MM-dd") + " " + customer.CUSTOM.TIMEWINDOWSTART.ToString("00:00")),
        //                                                new XElement("delivery_time_window_end", deliveryDate.ToString("yyy-MM-dd") + " " + customer.CUSTOM.TIMEWINDOWEND.ToString("00:00")),
        //                                                new XElement("delivery_charge", 0.00),
        //                                                new XElement("taxes", 0.00),
        //                                                new XElement("store_code", ""),
        //                                                Extra,
        //                                                CustomFields
        //                                            );
        //                                            ServiceOrders.Add(Invoice);
        //                                            currentCustomerRecord++;
        //                                        }
        //                                    }
                                        
        //                            }
        //                        }
        //                        else
        //                        {
        //                            myLogger.ProcessLog(1, string.Format("No order(s) sent to DispatchTrack for Session: {0} - Delivery Date: {1}", strSessionDescription, dtRouteDate));
        //                        }
        //                    }
        //                    else
        //                    {
        //                        myLogger.ProcessLog(1, string.Format("Error calling AS400 Import Routing Webservice - Session: {0}", strSessionDescription));
        //                        clsMSWindowsLog.SendEmailMessage(string.Format("Error calling AS400 Import Routing Webservice. - Session: {0}", strSessionDescription));
        //                    }
        //                }
        //            }

        //        }
        //    }
        //    xDoc.Add(ServiceOrders);
        //    return xDoc;
        //}

        public int SendDataTo_DT_API(int sendmode, XDocument xmldocument)
        {

            CFDispatchTrackApplicationSettings loAppSettings = new CFDispatchTrackApplicationSettings();

            int retValue = 0;
            dynamic postURL;
            string credentials = "";

            if (sendmode == 1)
            { //Batch Mode
                postURL = @loAppSettings.Get("DispatchTrackURLImportBatch");
            }
            else
            {
                postURL = @loAppSettings.Get("DispatchTrackURLImportSingle");
            }
            credentials = "code=" + loAppSettings.Get("DispatchTrackCode") + "&api_key=" + loAppSettings.Get("DispatchTrackAPIKey") + "&data=";

            try
            {
                // Create a request using a URL that can receive a post.
                WebRequest request = WebRequest.Create(postURL);
                // Set the Method property of the request to POST.
                request.Method = "POST";
                // A long time out
                request.Timeout = 1000000;
                //read the xml file into a string prior to encoding and converting to byte array
                string datatopost = ToStringWithDeclaration(xmldocument);
                //now urlencode the xml file
                datatopost = System.Web.HttpUtility.UrlEncode(datatopost);
                //now concatenate the urlencoded xml file with the non url encoded credentials string
                credentials = credentials + datatopost;
                // Create POST data and convert it to a byte array.
                byte[] byteArray = Encoding.UTF8.GetBytes(credentials);
                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/x-www-form-urlencoded";
                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteArray.Length;
                // Get the request stream.
                Stream dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);
                // Close the Stream object.
                dataStream.Close();
                // Get the response.
                WebResponse response = request.GetResponse();
                // Display the status.
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Write to the file
                WriteResponseStream(ref responseFromServer);
                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                response.Close();
                Object obj = ObjectToXML(responseFromServer, typeof(DTResponse));
                myLogger.ProcessLog(1, string.Format("DispatchTrack Server Response: {0}", responseFromServer));
                retValue = 1;
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                myLogger.ProcessLog(1, string.Format("Error Exception: {0}", ex.ToString()));
                WriteResponseStream(ref msg);
            }
            return retValue;
        }

        public int UpdateStagingSQLData(string strDate, string strRoutingZone, string strOrderNumber, XElement xOrderElement)
        {
            return 0;
        }

        public void WriteResponseStream(ref string s)
        {
            StreamWriter sw = new StreamWriter(@"the file you want to write the responses to here", true);
            sw.Write(s.ToString());
            sw.Close();
        }

        public string ToStringWithDeclaration(XDocument doc)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc");
            }
            StringWriter builder = new Utf8StringWriter();
            doc.Save(builder, SaveOptions.None);
            //StringBuilder builder = new StringBuilder();
            //using (TextWriter writer = new StringWriter(builder))
            //{
            //    doc.Save(writer);
            //}
            return builder.ToString();
        }

        public static Object ObjectToXML(string xml, Type objectType)
        {
            StringReader strReader = null;
            XmlSerializer serializer = null;
            XmlTextReader xmlReader = null;
            Object obj = null;
            try
            {
                strReader = new StringReader(xml);
                serializer = new XmlSerializer(objectType);
                xmlReader = new XmlTextReader(strReader);
                obj = serializer.Deserialize(xmlReader);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                if (xmlReader != null)
                {
                    xmlReader.Close();
                }
                if (strReader != null)
                {
                    strReader.Close();
                }
            }
            return obj;
        }

        private class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding { get { return Encoding.UTF8; } }
        }
    }
}
