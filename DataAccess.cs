using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Linq.Expressions;
using IBM.Data.DB2.iSeries;
using System.Data.Common;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using Massive400;
using System.Xml;

namespace RoutingWinApp
{
    public class DataAccess
    {
        public class LambdaCriteries<T> : List<Expression<Func<T, bool>>>
        {
            public Expression<Func<T, bool>> GetFinalLambdaExpression()
            {
                var par = Expression.Parameter(typeof(T));
                var intial = Expression.Invoke(this.First(), par);
                var sec = Expression.Invoke(this.Skip(1).First(), par);
                BinaryExpression binaryExpression = Expression.And(intial, sec);
                if (this.Count > 2)
                {
                    foreach (var ex in this.ToList().Skip(2))
                    {
                        binaryExpression = Expression.And(binaryExpression, Expression.Invoke(ex, par));
                    }
                    return Expression.Lambda<Func<T, bool>>(binaryExpression, par);
                }
                else
                {
                    return Expression.Lambda<Func<T, bool>>(binaryExpression, par);
                }

            }
        }

        [Serializable()]
        public class CFRNCustomException : Exception, ISerializable
        {
            public CFRNCustomException() : base()
            {
            }

            public CFRNCustomException(string message) : base(message)
            {
            }

            public CFRNCustomException(string message, System.Exception inner) : base(message, inner)
            {
            }

            public CFRNCustomException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }

        #region AS400Data Connection

        private dynamic dbconn400;

        private string _connectionStringPlaceHolderAS400 { get; set; }

        public dynamic GetConnectionAS400(string connectionString)
        {
            if (dbconn400 == null)
            {
                this.DataAccessAS400(connectionString);
                //DynamicModel.Open(connectionString);
            }
            return dbconn400;
        }

        public void DataAccessAS400(string connectionString)
        {
            this._connectionStringPlaceHolderAS400 = connectionString;
            dbconn400 = DynamicModel.Open(connectionString);
        }

        #endregion AS400Data Connection

        #region SQLData Connection

        public SqlConnection SqlDataConnection(string connectionname)
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings[connectionname].ConnectionString);
        }

        #endregion SQLData Connection


        public SqlDataReader SqlGetAppConstants(string PropertyName, char PropertyPlatform, bool PropertyIncludeAll)
        {
            SqlDataReader reader;
            var conn = SqlDataConnection("CFRN");
            using (SqlCommand cmd = new SqlCommand("GetAppConstants", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@propertyname", PropertyName);
                cmd.Parameters.AddWithValue("@propertyplatform", PropertyPlatform);
                cmd.Parameters.AddWithValue("@includeall", PropertyIncludeAll);
                conn.Open();
                reader = cmd.ExecuteReader();
                return reader;
            }
        }

        public void ExportRoutingSessionHistoryData(SqlConnection Sqlcon, SQLRouteoHist dtAS400RouteO)
        {
            var OpenLocal = false;
            //SqlDataReader reader;
            if (Sqlcon == null)
            {
                Sqlcon = SqlDataConnection("CFRN");
                Sqlcon.Open();
                OpenLocal = true;
            }
            else
            {
                if (Sqlcon.State == ConnectionState.Closed)
                {
                    Sqlcon.Open();
                    OpenLocal = true;
                }
            }
            using (SqlCommand command = new SqlCommand("InsertRouteOHist", Sqlcon))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = Sqlcon;
                command.Parameters.AddWithValue("@CreateDT", DateTime.Today);
                command.Parameters.AddWithValue("@RNSessionID", dtAS400RouteO.RNSessionID);
                command.Parameters.AddWithValue("@RNSessionDate", dtAS400RouteO.RNSessionDate);
                command.Parameters.AddWithValue("@RNStopID", dtAS400RouteO.RNStopID);
                command.Parameters.AddWithValue("@RNStopPos", dtAS400RouteO.RNStopPos);
                command.Parameters.AddWithValue("@Rozone", dtAS400RouteO.RoZone);
                command.Parameters.AddWithValue("@Rodate", dtAS400RouteO.RoDate);
                command.Parameters.AddWithValue("@Ropos", dtAS400RouteO.RoPos);
                command.Parameters.AddWithValue("@Roinv", dtAS400RouteO.RoInv);
                command.Parameters.AddWithValue("@Rorout", dtAS400RouteO.RoRouT);
                command.Parameters.AddWithValue("@Rortyp", dtAS400RouteO.RoRTyp);
                command.Parameters.AddWithValue("@Rodist", dtAS400RouteO.RoDist);
                command.Parameters.AddWithValue("@Roxgeo", dtAS400RouteO.RoXGeo);
                command.Parameters.AddWithValue("@Roygeo", dtAS400RouteO.RoYGeo);
                command.Parameters.AddWithValue("@RoSMin", dtAS400RouteO.RoSMin);
                command.Parameters.AddWithValue("@RoDMin", dtAS400RouteO.RoDMin);
                command.Parameters.AddWithValue("@RNZipCode", dtAS400RouteO.RNZipCode);
                command.Parameters.AddWithValue("@RNTWStart", dtAS400RouteO.RNTWStart);
                command.Parameters.AddWithValue("@RNTWEnd", dtAS400RouteO.RNTWEnd);
                command.Parameters.AddWithValue("@RNCubes", dtAS400RouteO.RNCubes);
                command.Parameters.AddWithValue("@RNFullName", dtAS400RouteO.RNFullName);
                command.Parameters.AddWithValue("@RNHasRestrictions", dtAS400RouteO.RNHasRestrictions);
                command.Parameters.AddWithValue("@RNHasBackorders", dtAS400RouteO.RNHasBackOrders);
                command.Parameters.AddWithValue("@RNIsStore", dtAS400RouteO.RNIsStore);
                command.Parameters.AddWithValue("@RNSessionName", dtAS400RouteO.RNSessionName);
                command.Parameters.AddWithValue("@RNIsFreeShip", dtAS400RouteO.RNIsFreeShip);
                command.ExecuteNonQuery();
                if (OpenLocal == true)
                    Sqlcon.Close();
            }
        }

        public void InsertRoutingSessionHistoryData(DateTime createDT,                       //currentCreateDT
                                                    DateTime rnSessionDate,                  //currentSessionDate
                                                    int rnStopID,                            //currentStopID
                                                    int rnStopPos,                           //currentStop
                                                    string roZone,                           //currentRouteZone
                                                    int roDate,                              //currentRoDate
                                                    int roPos,                               //currentRoutePos
                                                    int roInv,                               //currentOrder            
                                                    string roRout,                           //currentRoute
                                                    string rorTyp,                           //currentRouteType
                                                    int roDist,                              //currentRouteDistance
                                                    string roXGeo,                           //currentLongitude
                                                    string roYGeo,                           //currentLatitude
                                                    int roSMin,                              //currentRoSMin
                                                    int roDmin,                              //currentRoDMin
                                                    string rnZipCode,                        //currentZipCode
                                                    string rnTwStart,                        //currentTimeWindowStart
                                                    string rnTwEnd,                          //currentTimeWindowEnd
                                                    int rnCubes,                             //currentCubes
                                                    string rnFullName,                       //currentFullName
                                                    string rnHasRestrictions,                //currentHasRestrictions
                                                    string rnHasBackOrders,                  //currentHasBackorders
                                                    string rnIsStore,                        //currentIsStore
                                                    string rnSessionName,                    //currentSessionName
                                                    string rnIsFreeShip,                     //currentIsFreeShip
                                                    string deliveryDate
                                                   )                   
        {
            SqlConnection conn = null;
            try
            {
                conn = SqlDataConnection("CityFurnitureMaster");
                using (SqlCommand cmd = new SqlCommand("Routing.InsertRouteOHist_DispatchTrack", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@CreateDT", createDT);
                    cmd.Parameters.AddWithValue("@RNSessionDate", rnSessionDate);
                    cmd.Parameters.AddWithValue("@RNStopID", rnStopID);
                    cmd.Parameters.AddWithValue("@RNStopPos", rnStopPos);
                    cmd.Parameters.AddWithValue("@Rozone", roZone);
                    cmd.Parameters.AddWithValue("@Rodate", roDate);
                    cmd.Parameters.AddWithValue("@Ropos", roPos);
                    cmd.Parameters.AddWithValue("@Roinv", roInv);
                    cmd.Parameters.AddWithValue("@Rorout", roRout);
                    cmd.Parameters.AddWithValue("@Rortyp", rorTyp);
                    cmd.Parameters.AddWithValue("@Rodist", roDist);
                    cmd.Parameters.AddWithValue("@Roxgeo", roXGeo);
                    cmd.Parameters.AddWithValue("@Roygeo", roYGeo);
                    cmd.Parameters.AddWithValue("@RoSMin", roSMin);
                    cmd.Parameters.AddWithValue("@RoDMin", roDmin);
                    cmd.Parameters.AddWithValue("@RNZipCode", rnZipCode);
                    cmd.Parameters.AddWithValue("@RNTWStart", rnTwStart);
                    cmd.Parameters.AddWithValue("@RNTWEnd", rnTwEnd);
                    cmd.Parameters.AddWithValue("@RNCubes", rnCubes);
                    cmd.Parameters.AddWithValue("@RNFullName", rnFullName);
                    cmd.Parameters.AddWithValue("@RNHasRestrictions", rnHasRestrictions);
                    cmd.Parameters.AddWithValue("@RNHasBackorders", rnHasBackOrders);
                    cmd.Parameters.AddWithValue("@RNIsStore", rnIsStore);
                    cmd.Parameters.AddWithValue("@RNSessionName", rnSessionName);
                    cmd.Parameters.AddWithValue("@RNIsFreeShip", rnIsFreeShip);
                    cmd.Parameters.AddWithValue("@deliveryDate", deliveryDate);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            { }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        public bool DeleteRoutingSessionHistoryData(string ohistroute, int ohistdate)
        {
            SqlConnection conn = null;
            try
            {
                //SqlDataReader reader;
                conn = SqlDataConnection("CityFurnitureMaster");
                using (SqlCommand cmd = new SqlCommand("Routing.DeleteRouteOHist", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@RoRouT", ohistroute);
                    cmd.Parameters.AddWithValue("@RoDate", ohistdate);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        public SqlDataReader GetRoutingSessionHistoryData(string ohistroute, int ohistdate)
        {
            SqlDataReader reader;
            var conn = SqlDataConnection("CFRN");
            using (SqlCommand cmd = new SqlCommand("SelectRouteOHist", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@RoRouT", ohistroute);
                cmd.Parameters.AddWithValue("@RoDate", ohistdate);
                conn.Open();
                reader = cmd.ExecuteReader();
                return reader;
            }
        }

        public SqlDataReader GetRoutingDateSessionHistoryDataOnlyStops(int ohistdatefrom, int ohistdateto, int ohistrnsession)
        {
            SqlDataReader reader;
            var conn = SqlDataConnection("CFRN");
            using (SqlCommand cmd = new SqlCommand("SelectDateRNSessionOHistOnlyStops", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@RoDateFrom", ohistdatefrom);
                cmd.Parameters.AddWithValue("@RoDateTo", ohistdateto);
                cmd.Parameters.AddWithValue("@RnSessionID", ohistrnsession);
                conn.Open();
                reader = cmd.ExecuteReader();
                return reader;
            }
        }

        public DataTable GetSessionsMasterBySessionID(int sessionId, int viewMode)
        {
            SqlDataReader reader;
            var dt = new DataTable();
            //var conn = SqlDataConnection("CFRN");
            var conn = SqlDataConnection("CityFurnitureMaster");
            //using (SqlCommand cmd = new SqlCommand("GetSessionsMasterBySessionID", conn))
            using (SqlCommand cmd = new SqlCommand("Routing.GetSessionsMasterBySessionID", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@tSessionID", sessionId);
                cmd.Parameters.AddWithValue("@tViewMode", viewMode);
                conn.Open();
                reader = cmd.ExecuteReader();
                dt.Load(reader);
                return dt;
            }
        }

        public DataTable GetSessionsMasterByDate(DateTime dateToProcess)
        {
            SqlDataReader reader;
            var dt = new DataTable();
            //var conn = SqlDataConnection("CFRN");
            var conn = SqlDataConnection("CityFurnitureMaster");
            //using (SqlCommand cmd = new SqlCommand("GetSessionsMasterByDate", conn))
            using (SqlCommand cmd = new SqlCommand("Routing.GetSessionsMasterByDate", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@tDateToProcess", dateToProcess);
                conn.Open();
                reader = cmd.ExecuteReader();
                dt.Load(reader);
                conn.Close();
                return dt;
            }
        }

        public bool CheckLockStatus(DateTime targetDate, string accountId)
        {
            SqlConnection conn = null;
            string targetDateStr = targetDate.ToString("yyyy-MM-dd");
            try
            {
                SqlDataReader reader;
                var dt = new DataTable();
                conn = SqlDataConnection("CityFurnitureMaster");
                string query = "EXEC Routing.sp_CheckLockStatus @account_id = " + accountId + ", @target_date = '" + targetDateStr + "'";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    conn.Open();
                    reader = cmd.ExecuteReader();
                    dt.Load(reader);
                    if (dt.Rows.Count == 1)
                        return ((int)dt.Rows[0]["check_lock_status"]) == 1;
                    else
                        return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        public string CheckWaveExportedtoAS400Already(DateTime deliveryDate, string waveName)
        {
            SqlConnection conn = null;
            string targetDateStr = deliveryDate.ToString("yyyy-MM-dd");
            try
            {
                SqlDataReader reader;
                var dt = new DataTable();
                conn = SqlDataConnection("CityFurnitureMaster");
                string query = "EXEC Routing.sp_Get_DT_Get_Export_Orders_Staging_byDeliveryDateAccountID @deliveryDate = '" + targetDateStr + "', @accountId = '" + waveName + "'";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    conn.Open();
                    reader = cmd.ExecuteReader();
                    dt.Load(reader);
                    if (dt.Rows.Count > 0)
                        return "BAD";
                    else
                        return "OK";
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        public bool CreateProcess(string processCode, out int processId)
        {
            SqlConnection conn = null;
            try
            {
                SqlDataReader reader;
                var dt = new DataTable();
                conn = SqlDataConnection("CityFurnitureMaster");
                using (SqlCommand cmd = new SqlCommand("Routing.sp_InsertProcessMasterRecord", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@process_code", processCode);
                    conn.Open();
                    //cmd.ExecuteNonQuery();
                    reader = cmd.ExecuteReader();
                    dt.Load(reader);
                    processId = (int)dt.Rows[0]["id_value"];
                }
                return true;
            }
            catch (Exception e)
            {
                processId = -1;
                return false;
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        public bool FinishProcess(string processCode, int processId)
        {
            SqlConnection conn = null;
            try
            {
                //string query = "UPDATE Routing.Process_Master ";
                //query += "      SET finished = 1 ";
                //query += "      WHERE process_code = '" + processCode + "' AND ";
                //query += "            id = " + processId;
                string query = "EXEC Routing.sp_Finish_Export_Import_Process @process_code = '" + processCode + "', @process_id = " + processId;

                conn = SqlDataConnection("CityFurnitureMaster");
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        public int GetRunningProcess(string processCode)
        {
            SqlConnection conn = null;
            try
            {
                //string query = "SELECT ISNULL((SELECT TOP 1 id ";
                //query += "                     FROM Routing.Process_Master ";
                //query += "                     WHERE finished = 0 AND ";
                //query += "                           process_code = '" + processCode + "' ";
                //query += "                     ORDER BY created_datetime DESC ";
                //query += "                    ), -1) AS process_id ";
                string query = "EXEC Routing.sp_Get_Running_Process @process_code = '" + processCode + "'";

                SqlDataReader reader;
                var dt = new DataTable();
                conn = SqlDataConnection("CityFurnitureMaster");
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    conn.Open();
                    reader = cmd.ExecuteReader();
                    dt.Load(reader);
                    if (dt.Rows.Count == 1)
                    {
                        int processId = dt.Rows[0].Field<int>("process_id");
                        return processId;
                    }
                    else
                        return -1;
                }
            }
            catch (Exception e)
            {
                return -1;
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        public float GetCubesByItemNumber(string itemNumber)
        {
            SqlDataReader reader;
            var dt = new DataTable();
            SqlConnection conn = null;
            try
            {
                conn = SqlDataConnection("CityFurnitureMaster");
                using (SqlCommand cmd = new SqlCommand("Routing.sp_GetCubesByItemNumber", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@item_number", itemNumber);
                    conn.Open();
                    reader = cmd.ExecuteReader();
                    dt.Load(reader);
                    if (dt.Rows.Count == 1)
                    {
                        float result = float.Parse(dt.Rows[0]["cubes"].ToString());
                        return result;
                    }
                    else
                        return 0;
                }
            }
            catch (Exception e)
            {
                return 0;
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        public bool WaveExportedBefore(string accountId, DateTime deliveryDate)
        {
            SqlDataReader reader;
            var dt = new DataTable();
            SqlConnection conn = null;
            try
            {
                conn = SqlDataConnection("CityFurnitureMaster");
                using (SqlCommand cmd = new SqlCommand("Routing.sp_VerifyWaveExportedBefore", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    string formattedDate = deliveryDate.ToString("yyyy-MM-dd");
                    cmd.Parameters.AddWithValue("@account_id", accountId);
                    cmd.Parameters.AddWithValue("@delivery_date", formattedDate);
                    conn.Open();
                    reader = cmd.ExecuteReader();
                    dt.Load(reader);
                    if (dt.Rows.Count == 1)
                    {
                        int result = (int)dt.Rows[0]["exported_before"];
                        return result == 1;
                    }
                    else
                        return true;
                }
            }
            catch (Exception e)
            {
                return true;
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        public List<Tuple<int, string>> GetLogs(int processId, int lastLogProcessed)
        {
            List<Tuple<int, string>> logs = new List<Tuple<int, string>>();
            SqlConnection conn = null;
            try
            {
                //string query = "SELECT id, [message] ";
                //query += "      FROM Routing.Process_Logs ";
                //query += "      WHERE process_id = " + processId + " AND ";
                //query += "            id > " + lastLogProcessed;
                //query += "      ORDER BY id ASC ";
                string query = "EXEC Routing.sp_Get_Logs @process_id = " + processId + ", @last_log_processed = " + lastLogProcessed;

                SqlDataReader reader;
                var dt = new DataTable();
                conn = SqlDataConnection("CityFurnitureMaster");
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    conn.Open();
                    reader = cmd.ExecuteReader();
                    dt.Load(reader);
                    string currentMessage = "";
                    int currentid = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        currentMessage = row.Field<string>("message");
                        currentid = row.Field<int>("id");
                        logs.Add(new Tuple<int, string>(currentid, currentMessage));
                    }
                }
            }
            catch (Exception e) { }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
            return logs;
        }

        public DataTable GetPreferedRouteBySessionID(int sessionId, int viewMode)
        {
            SqlDataReader reader;
            var dt = new DataTable();
            var conn = SqlDataConnection("CFRN");
            using (SqlCommand cmd = new SqlCommand("GetPreferedRouteBySessionID", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@tSessionID", sessionId);
                cmd.Parameters.AddWithValue("@tViewMode", viewMode);
                conn.Open();
                reader = cmd.ExecuteReader();
                dt.Load(reader);
                return dt;
            }
        }

        public SqlDataReader GetRoadNetWavesBRRefByWaveID(int tnRouteWaveID)
        {
            SqlDataReader reader;
            var conn = SqlDataConnection("CityRoadNet_Local");
            using (SqlCommand cmd = new SqlCommand("GetRoadNetWavesBRRefByWaveID", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@waiveid", tnRouteWaveID);
                conn.Open();
                reader = cmd.ExecuteReader();
                return reader;
            }
        }

        public SqlDataReader GetRoutingWaves(DateTime? dteWaveDate = null)
        {
            if (dteWaveDate == null)
                dteWaveDate = DateTime.Today;
            SqlDataReader reader;
            var conn = SqlDataConnection("CityRoadNet_Local");
            using (SqlCommand cmd = new SqlCommand("GetRoadNetWavesbyDOW", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@date", dteWaveDate);
                conn.Open();
                reader = cmd.ExecuteReader();
                return reader;
            }
        }

        public IEnumerable<AS400DLVWave> GetDLVWavefromAS400(bool tbOnlyActive, DateTime? dteWaveDate = null)
        {
            var DLVWave = new List<AS400DLVWave>();

            if (dteWaveDate == null)
                dteWaveDate = DateTime.Today;
            IEnumerable<dynamic> WaveInfo = GetConnectionAS400(GlobalVars.AS400ActiveConnection).Query(@"Select * from QS36F.DLVWave");

            DLVWave = WaveInfo.Where(c => (tbOnlyActive == true) ? c.WVACTF == "Y" : c.WVACTF != "X").Select(d => new AS400DLVWave
            {
                WVWave = d.WVWAVE,
                WVName = d.WVNAME.Trim(),
                WVMonF = d.WVMONF,
                WVTueF = d.WVTUEF,
                WVWedF = d.WVWEDF,
                WVThuF = d.WVTHUF,
                WVFriF = d.WVFRIF,
                WVSatF = d.WVSATF,
                WVSunF = d.WVSUNF,
                WVActF = d.WVACTF,
                ActiveDate = ((dteWaveDate.Value.DayOfWeek == System.DayOfWeek.Monday) && d.WVMONF == "Y") ? 1 : (dteWaveDate.Value.DayOfWeek == System.DayOfWeek.Tuesday && d.WVTUEF == "Y") ? 1 : (dteWaveDate.Value.DayOfWeek == System.DayOfWeek.Wednesday && d.WVWEDF.ToString() == "Y") ? 1 : (dteWaveDate.Value.DayOfWeek == System.DayOfWeek.Thursday && d.WVTHUF == "Y") ? 1 : (dteWaveDate.Value.DayOfWeek == System.DayOfWeek.Friday && d.WVFRIF == "Y") ? 1 : (dteWaveDate.Value.DayOfWeek == System.DayOfWeek.Saturday && d.WVSATF == "Y") ? 1 : (dteWaveDate.Value.DayOfWeek == System.DayOfWeek.Sunday && d.WVSUNF == "Y") ? 1 : 0
            }).ToList();
            return DLVWave.Where(d => d.ActiveDate == 1).ToList();
        }

        public string CreateWavesIdList(SqlDataReader tdrWaves)
        {
            string routeBCodesStr = "";
            int routeBCodesCnt = 0;
            while (tdrWaves.Read())
            {
                routeBCodesStr = routeBCodesStr + "\'" + tdrWaves.GetValue(12) + "\' ";
                routeBCodesCnt++;
            }

            for (int i = routeBCodesCnt; i < 10; i++)
            {
                routeBCodesStr = routeBCodesStr + "\' \' ";
            }
            return routeBCodesStr;
        }

        //public AS400GetRteDtaMain.getrtedtaResult GetRoutingDataFromAS400APIRouteiMaster(DateTime tdRouteDate, int tnRouteWaveID, string strRoutingZone, List<WaveFiltersValues> filtersList, string tcWareHouseID)
        //{
        //    AS400GetRteDtaMain.getrtedtaInput xx = new AS400GetRteDtaMain.getrtedtaInput();
        //    xx.P_REQUEST = new AS400GetRteDtaMain.getrouterequestds();
        //    xx.P_REQUEST.WAREHOUSEID = tcWareHouseID;
        //    xx.P_REQUEST.WAVEID = tnRouteWaveID.ToString();
        //    xx.P_REQUEST.DELIVERYDATE = Convert.ToDecimal(tdRouteDate.ToString("yyyyMMdd"));
        //    AS400GetRteDtaMain.getrtedtaResult zy = new AS400GetRteDtaMain.getrtedtaResult();
        //    try
        //    {
        //       zy = GlobalVars.DtsMain.getrtedta(xx);
        //    }
        //    catch (Exception error)
        //    {
        //    }
        //    return zy;
        //}

        //public AS400GetRteDtaDetail.getrtedetResult GetRoutingDataFromAS400APIRouteiDetail(string tcUniqueOrderID, string tcBatchID)
        //{
        //    AS400GetRteDtaDetail.getrtedetInput xx = new AS400GetRteDtaDetail.getrtedetInput();
        //    xx.P_REQUEST = new AS400GetRteDtaDetail.getrouteinvoicerequestds();
        //    xx.P_REQUEST.UNIQUEORDERID = tcUniqueOrderID;
        //    xx.P_REQUEST.BATCHID = tcBatchID;

        //    AS400GetRteDtaDetail.getrtedetResult zy = new AS400GetRteDtaDetail.getrtedetResult();
        //    zy = GlobalVars.DtsDetail.getrtedet(xx);
        //    return zy;
        //}
    }
}
