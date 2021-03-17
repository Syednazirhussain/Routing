using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using System.Windows.Forms;

namespace RoutingWinApp
{
    public class ExportParams
    {
        public ExportParams(string date, string warehouse, string waveid, string processId, string[] routes)
        {
            this.date = date;
            this.warehouse = warehouse;
            this.waveid = waveid;
            this.processId = processId;
            this.routes = routes;
        }

        [JsonProperty("date")]
        public string date { get; set; }

        [JsonProperty("warehouse")]
        public string warehouse { get; set; }

        [JsonProperty("waveid")]
        public string waveid { get; set; }

        [JsonProperty("processId")]
        public string processId { get; set; }

        [JsonProperty("routes")]
        public string[] routes { get; set; }
    }

    public class NodeAPI
    {
        public static bool UpdateDriversInformation(string url)
        {
            CFDispatchTrackApplicationSettings loAppSettings = new CFDispatchTrackApplicationSettings();

            try
            {
                int connectionTimeOut = int.Parse(loAppSettings.Get("DispatchTrackAPIPortTimeOutMinutes"));
                HttpClient client = new HttpClient();
                client.Timeout = new TimeSpan(0, connectionTimeOut, 0);
                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    dynamic json = JsonConvert.DeserializeObject(result);
                    string error = json.error;
                    return error.Trim().Length == 0;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool UploadOrdersData(string url, out HttpResponseMessage aSyncResponse)
        {
            CFDispatchTrackApplicationSettings loAppSettings = new CFDispatchTrackApplicationSettings();

            try
            {
                int connectionTimeOut = int.Parse(loAppSettings.Get("DispatchTrackAPIPortTimeOutMinutes"));    //value represents the time in minutes
                HttpClient client = new HttpClient();
                client.Timeout = new TimeSpan(0, connectionTimeOut, 0);   
                //var result =  client.GetAsync(url).Result;
                //return true;
                HttpResponseMessage response = client.GetAsync(url).Result;
                aSyncResponse = response;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    dynamic json = JsonConvert.DeserializeObject(result);
                    string error = json.error;
                    if (error.Trim().Length == 0)
                        return true;
                    else
                        throw new Exception(error);
                    //return error.Trim().Length == 0;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool ExportOrdersDataByRoute(string url, string date, string warehouse, string waveid, string processId, List<string> routes)
        {
            CFDispatchTrackApplicationSettings loAppSettings = new CFDispatchTrackApplicationSettings();
            
            try
            {
                ExportParams postParams = new ExportParams(date, warehouse, waveid, processId, routes.ToArray<string>());
                string strPostParams = JsonConvert.SerializeObject(postParams);

                int connectionTimeOut = int.Parse(loAppSettings.Get("DispatchTrackAPIPortTimeOutMinutes"));    //value represents the time in minutes
                HttpClient client = new HttpClient();
                client.Timeout = new TimeSpan(0, connectionTimeOut, 0);     //1 hour

                var content = new StringContent(strPostParams, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(url, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    dynamic jsonRes = JsonConvert.DeserializeObject(result);
                    string error = jsonRes.error;
                    if (error.Trim().Length > 0)
                    {
                        MessageBox.Show(error.Trim());
                        return false;
                    }
                    else
                        return true;
                }
                else
                {
                    MessageBox.Show(response.ReasonPhrase);
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool ExportOrdersData(string url)
        {
            CFDispatchTrackApplicationSettings loAppSettings = new CFDispatchTrackApplicationSettings();

            try
            {
                int connectionTimeOut = int.Parse(loAppSettings.Get("DispatchTrackAPIPortTimeOutMinutes"));   //value represents the time in minutes
                HttpClient client = new HttpClient();
                client.Timeout = new TimeSpan(0, connectionTimeOut, 0);   //1 hour
                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    dynamic json = JsonConvert.DeserializeObject(result);
                    string error = json.error;
                    if (error.Trim().Length > 0)
                    {
                        MessageBox.Show(error.Trim());
                        return false;
                    }
                    else
                        return true;
                }
                else
                {
                    MessageBox.Show(response.ReasonPhrase);
                    return false;
                }
                //dynamic json = JsonConvert.DeserializeObject(result);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool UpdateRouteOHistByRoute(string url, string date, string warehouse, string waveid, string processId, List<string> routes)
        {
            CFDispatchTrackApplicationSettings loAppSettings = new CFDispatchTrackApplicationSettings();

            try
            {
                ExportParams postParams = new ExportParams(date, warehouse, waveid, processId, routes.ToArray<string>());
                string strPostParams = JsonConvert.SerializeObject(postParams);

                int connectionTimeOut = int.Parse(loAppSettings.Get("DispatchTrackAPIPortTimeOutMinutes"));    //value represents the time in minutes
                HttpClient client = new HttpClient();
                client.Timeout = new TimeSpan(0, connectionTimeOut, 0);     //1 hour

                var content = new StringContent(strPostParams, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(url, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    dynamic json = JsonConvert.DeserializeObject(result);
                    string error = json.error;
                    return error.Trim().Length == 0;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool UpdateRouteOHist(string url)
        {
            CFDispatchTrackApplicationSettings loAppSettings = new CFDispatchTrackApplicationSettings();

            try
            {
                int connectionTimeOut = int.Parse(loAppSettings.Get("DispatchTrackAPIPortTimeOutMinutes"));   //value represents the time in minutes
                HttpClient client = new HttpClient();
                client.Timeout = new TimeSpan(0, connectionTimeOut, 0);   //1 hour
                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    dynamic json = JsonConvert.DeserializeObject(result);
                    string error = json.error;
                    return error.Trim().Length == 0;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Get full orders information from dispatch track using the route
        /// http://host:port/dispatchtrack/get/routeinfo/warehouse_id/date/session_id
        /// </summary>
        /// <returns></returns>
        public static object GetFullOrdersDataFromDispatchTrack(string url)
        {
            try
            {
                CFDispatchTrackApplicationSettings loAppSettings = new CFDispatchTrackApplicationSettings();
                int connectionTimeOutMinutes = int.Parse(loAppSettings.Get("DispatchTrackAPIPortTimeOutMinutes"));

                HttpClient client = new HttpClient();
                client.Timeout = new TimeSpan(0, connectionTimeOutMinutes, 0);
                HttpResponseMessage response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    dynamic json = JsonConvert.DeserializeObject(result);
                    return json;
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Return dictionary with route and order info (no item info)
        /// </summary>
        /// <param name="url">API endpoint url</param>
        /// <param name="dictRes">This is an out parameter that brings back routes and orders info, no item info</param>
        /// <returns>true: no errors occurred. false: error occurred</returns>
        public static bool GetOrdersDataFromDispatchTrack(string url, out Dictionary<string, List<string>> dictRes)
        {
            dictRes = new Dictionary<string, List<string>>();
            try
            {
                HttpClient client = new HttpClient();
                client.Timeout = new TimeSpan(1, 0, 0);   //1 hour
                HttpResponseMessage response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    dynamic json = JsonConvert.DeserializeObject(result);
                    
                    string currentRoute = "";
                    string currentOrder = "";
                    foreach (var item in json.data)
                    {
                        currentRoute = item.routeID;
                        currentOrder = item.uniqueOrderID;
                        if (!dictRes.ContainsKey(currentRoute))
                            dictRes.Add(currentRoute, new List<string>());
                        dictRes[currentRoute].Add(currentOrder);
                    }
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool CheckDataInDTBeforeExporting(string url,
                                                        out List<string> notInAS400,
                                                        out List<string> zoneChanged,
                                                        out List<string> positionChanged,
                                                        out List<string> notComing,
                                                        out List<string> importedNotComing)
        {
            CFDispatchTrackApplicationSettings loAppSettings = new CFDispatchTrackApplicationSettings();
            int connectionTimeOutMinutes = int.Parse(loAppSettings.Get("DispatchTrackAPIPortTimeOutMinutes"));
            notInAS400 = new List<string>();
            zoneChanged = new List<string>();
            positionChanged = new List<string>();
            notComing = new List<string>();
            importedNotComing = new List<string>();
            string currentInvoice = "";

            try
            {
                HttpClient client = new HttpClient();
                client.Timeout = new TimeSpan(0, connectionTimeOutMinutes, 0);    //1 hour
                HttpResponseMessage response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    dynamic json = JsonConvert.DeserializeObject(result);

                    string error = json.error;
                    if (error.Trim().Length == 0)
                    {
                        if (json.data != null)
                        {
                            if(json.data.noAS400 != null)
                                foreach (var currentInv in json.data.noAS400)
                                {
                                    currentInvoice = currentInv;
                                    notInAS400.Add(currentInvoice);
                                }
                            if(json.data.zoneChanged != null)
                                foreach (var currentInv in json.data.zoneChanged)
                                {
                                    currentInvoice = currentInv;
                                    zoneChanged.Add(currentInvoice);
                                }
                            if(json.data.posChanged != null)
                                foreach (var currentInv in json.data.posChanged)
                                {
                                    currentInvoice = currentInv;
                                    positionChanged.Add(currentInvoice);
                                }
                            if(json.data.expectedNotComing != null)
                                foreach (var currentInv in json.data.expectedNotComing)
                                {
                                    currentInvoice = currentInv;
                                    notComing.Add(currentInvoice);
                                }
                            if(json.data.importedNotComing != null)
                                foreach (var currentInv in json.data.importedNotComing)
                                {
                                    currentInvoice = currentInv;
                                    importedNotComing.Add(currentInvoice);
                                }
                        }
                        return true;
                    }
                    else
                        throw new Exception(error.Trim());                 
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Return dictionary with route, order and item info
        /// </summary>
        /// <param name="url">API endpoint url</param>
        /// <param name="dictRes">This is an output parameter that brings back routes, orders and items info </param>
        /// <returns></returns>
        public static bool GetOrdersDataFromDispatchTrack(string url,
                                                          out Dictionary<string, Dictionary<Stop, List<string>>> dictRes,
                                                          out List<string> invalidRoutes, 
                                                          out List<string> invalidGeoCodes,
                                                          out Dictionary<Tuple<double, double>, List<Tuple<string, string>>> repeatedGeoCodes,
                                                          out int totalRoutes,
                                                          out int totalStops,
                                                          out int totalOrders)
        {
            CFDispatchTrackApplicationSettings loAppSettings = new CFDispatchTrackApplicationSettings();

            decimal latitudeFrom;
            decimal latitudeTo;
            decimal longitudeFrom;
            decimal longitudeTo;
            decimal.TryParse(loAppSettings.Get("FloridaLatitudeFrom"), out latitudeFrom);
            decimal.TryParse(loAppSettings.Get("FloridaLatitudeTo"), out latitudeTo);
            decimal.TryParse(loAppSettings.Get("FloridaLongitudeFrom"), out longitudeFrom);
            decimal.TryParse(loAppSettings.Get("FloridaLongitudeTo"), out longitudeTo);

            dictRes = new Dictionary<string, Dictionary<Stop, List<string>>>();
            invalidRoutes = new List<string>();
            invalidGeoCodes = new List<string>();
            string[] auxArray;

            repeatedGeoCodes = new Dictionary<Tuple<double, double>, List<Tuple<string, string>>>();
            totalRoutes = 0;
            totalStops = 0;
            totalOrders = 0;
            try
            {
                HttpClient client = new HttpClient();
                client.Timeout = new TimeSpan(1, 0, 0);   //1 hour
                HttpResponseMessage response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    dynamic json = JsonConvert.DeserializeObject(result);

                    string currentRoute = "";
                    string currentOrder = "";
                    string currentItem = "";
                    string currentItemQuantity = "";
                    double currentLatitude;
                    double currentLongitude;
                    int currentStop;
                    Tuple<double, double> currentTuple;
                    Tuple<string, string> currentTupleRteInfo;
                    Stop currentStopStruct;
                    var lastRoute = "";
                    var lastStop = 0;
                    foreach (var route in json.data)
                    {
                        currentRoute = route.routeID;
                        currentRoute = currentRoute.Trim();
                        currentStop = route.stopSeqID;
                        currentOrder = route.uniqueOrderID;

                        if (lastRoute != currentRoute)
                        {
                            lastRoute = currentRoute;
                            totalStops = totalStops + lastStop;
                            lastStop = currentStop;
                        }
                        else
                        {
                            if (lastStop < currentStop)
                            {
                                 lastStop = currentStop;
                            }
                        }

                            //check route value has the right format
                        if (currentRoute.Length != 2 && !invalidRoutes.Contains(currentRoute))
                          invalidRoutes.Add(currentRoute);

                        //get geocodes and routes information
                        currentLatitude = route.orderDetails.customer.First.latitude.First;
                        currentLongitude = route.orderDetails.customer.First.longitude.First;
                        currentTuple = new Tuple<double, double>(currentLatitude, currentLongitude);
                        currentTupleRteInfo = new Tuple<string, string>(currentRoute, currentRoute + "/" + currentStop + "/" + currentOrder);
                        if (repeatedGeoCodes.ContainsKey(currentTuple))
                        {
                            if (repeatedGeoCodes[currentTuple].Where(r => currentTupleRteInfo.Item1 == r.Item1).Count() == 0)
                                repeatedGeoCodes[currentTuple].Add(currentTupleRteInfo);
                        }
                        else
                            repeatedGeoCodes.Add(currentTuple, new List<Tuple<string, string>>(){currentTupleRteInfo});

                        string customerName = route.orderDetails.customer.First.first_name.First;
                        string customerLastname = route.orderDetails.customer.First.last_name.First;

                        if (Math.Abs((decimal)currentLatitude) > Math.Abs(latitudeFrom) || Math.Abs((decimal)currentLatitude) < Math.Abs(latitudeTo) || Math.Abs((decimal)currentLongitude) > Math.Abs(longitudeFrom) || Math.Abs((decimal)currentLongitude) < Math.Abs(longitudeTo))
                        {
                            invalidGeoCodes.Add(currentOrder);
                        }

                        auxArray = currentOrder.Split(new char[] { '-' });
                        currentOrder = auxArray[auxArray.Length - 1].Trim();
                        if (!dictRes.ContainsKey(currentRoute))
                        {
                            dictRes.Add(currentRoute, new Dictionary<Stop, List<string>>());
                            totalRoutes++;
                        }
                        currentStopStruct = new Stop(currentStop, currentOrder, customerName + " " + customerLastname);
                        dictRes[currentRoute].Add(currentStopStruct, new List<string>());
                        totalOrders++;

                        if (route.orderDetails.items != null)
                            try {
                                foreach (var item in route.orderDetails.items[0].item) //this structure looks weird but that's how JSON.Net parses the coming JSON
                                {
                                    currentItem = item.item_id.First;
                                    currentItemQuantity = item.quantity.First;
                                    dictRes[currentRoute][currentStopStruct].Add(currentItem + " (" + currentItemQuantity + ")");
                                }
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show(e.ToString());
                            }
                        else
                            dictRes[currentRoute][currentStopStruct].Add("No Items");
                    }
                    //add laststop for last record in json
                    totalStops = totalStops + lastStop;
                    //filter geocode tuples in more than one route
                    repeatedGeoCodes = repeatedGeoCodes.Where(t => t.Value.Count > 1).ToDictionary(t => t.Key, t => t.Value);
                    
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                //dictRes = new Dictionary<string, Dictionary<Stop, List<string>>>();
                //invalidRoutes = new List<string>();
                //repeatedGeoCodes = new Dictionary<Tuple<double, double>, List<string>>();
                //return false;
                throw e;
            }
        }
    }
}
