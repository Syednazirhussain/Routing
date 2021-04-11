using System;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Text;
using System.Runtime.Serialization;
using RoutingWinApp;
using System.Threading;
using static RoutingWinApp.DataAccess;

namespace RoutingWinApp
{
    public struct TVITEM
    {
        public int mask;
        public IntPtr hItem;
        public int state;
        public int stateMask;
        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPTStr)]
        public String lpszText;
        public int cchTextMax;
        public int iImage;
        public int iSelectedImage;
        public int cChildren;
        public IntPtr lParam;
    }

    public partial class CFDispatchTrackExportOrders : Form
	{
        //Begin hiding treeview checkboxes variables
        public const int TVIF_STATE = 0x8;
        public const int TVIS_STATEIMAGEMASK = 0xF000;
        public const int TV_FIRST = 0x1100;
        public const int TVM_SETITEM = TV_FIRST + 63;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        //End hiding treeview checkboxes variables

        public List<int> Selecteditemslist;
		public string StrRoutingZone;
		public DateTime DteProcess;
		public FormLogger Log;
		public int NProgressBarSteps;
		public int SelectSessionIdentityID;
		public string SelectSessionName;
		public DataSet DsRNDatabase;
		public DateTime DtSelectedDate;

        string exportingWave = "";
        bool keepOpen = false;
        bool dtRoutingDateState = false;
        bool cmdResetState = false;
        bool cboBuildRoutingWaveState = false;
        bool cmdExportOrdersState = false;
        bool opgGeoCodeState = false;
        bool opgBuildRoutesState = false;
        bool chkVerifiedState = false;
        bool txtExportAlertVisibility = false;
        Color txtExportAlertColor = Color.White;

        IEnumerable<DataRow> sessionsValues = new List<DataRow>();

        List<string> noAS400;
        List<string> zoneChanged;
        List<string> positionChanged;
        List<string> notComing;
        List<string> importedNotComing;
        Dictionary<string, Dictionary<Stop, List<string>>> dictRes;
        List<string> invalidRoutes;
        List<string> invalidGeoCodes;
        Dictionary<Tuple<double, double>, List<Tuple<string, string>>> repeatedGeoCodes;
        bool loadingRoutesSuccess = false;
        bool exceptionGettingRecords = false;
        int totalOrders;
        int totalRoutes;
        int totalStops;

        //public RoutingRouteInfoRetrieveOptions RtgrterO = new RoutingRouteInfoRetrieveOptions();

        //public RoutingRouteCriteria RtgrteC = new RoutingRouteCriteria();
        //public RoutingRouteIdentity RtgrteI = new RoutingRouteIdentity();
        //public RoutingRoute RtgRoute = new RoutingRoute();
        //public RoutingRoute[] RtgRoutes;
        //public RoutingRouteStatus RtgrteS = new RoutingRouteStatus();

        //public RoutingSessionCriteria RtgSessionC = new RoutingSessionCriteria();
        //public RoutingSessionIdentity RtgSessionI = new RoutingSessionIdentity();
        //public RoutingSession[] RtvRtgSessions ;
        //public RoutingSession RtvRtgSession;

        //public Location[] Locations;
        //public LocationIdentity LocationI = new LocationIdentity();
        //public Location location = new Location();
        //public LocationRetrieveOptions LocationO = new LocationRetrieveOptions();

        //public RoutingStopIdentity RtgStopI = new RoutingStopIdentity();
        //public RoutingStopCriteria RtgStopC = new RoutingStopCriteria();
        //public RoutingStop RtgStop = new RoutingStop();
        //public RoutingStop[] RtgStops;

        //public RoutingOrderIdentity RtgOrderI = new RoutingOrderIdentity();
        //public RoutingOrder RtgOrder = new RoutingOrder();
        //public RoutingOrder[] RtgOrders;

        //public TimeZoneOptions cftimeZoneOptions = new TimeZoneOptions();

        public CFDispatchTrackExportOrders()
		{
			InitializeComponent();

            RoutesList.CheckBoxes = true;
            RoutesList.DrawMode = TreeViewDrawMode.OwnerDrawAll;
            RoutesList.DrawNode += new DrawTreeNodeEventHandler(RoutesList_DrawNode);

			DtSelectedDate = new DateTime(dtRoutingDate.Value.Year, dtRoutingDate.Value.Month, dtRoutingDate.Value.Day);
            txtExportAlert.Visible = false;
            txtExportAlert.BackColor = Color.White;
            //GetRoadnetSessions();
            GetDlvSessionsSQL();
		}

        private void HideCheckBox(TreeNode node)
        {
            TVITEM tvi = new TVITEM();
            tvi.hItem = node.Handle;
            tvi.mask = TVIF_STATE;
            tvi.stateMask = TVIS_STATEIMAGEMASK;
            tvi.state = 0;
            IntPtr lparam = System.Runtime.InteropServices.Marshal.AllocHGlobal(System.Runtime.InteropServices.Marshal.SizeOf(tvi));
            System.Runtime.InteropServices.Marshal.StructureToPtr(tvi, lparam, false);
            SendMessage(node.TreeView.Handle, TVM_SETITEM, IntPtr.Zero, lparam);
        }

        private void RoutesList_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            //e.DrawDefault = true;
            //if (e.Node.Level > 0)
            //    HideCheckBox(e.Node);

            e.DrawDefault = true;

            if (e.Node.Level > 0)
            {
                HideCheckBox(e.Node);
                e.DrawDefault = true;
            }
            else
            {
                e.DrawDefault = true;
            }
        }

        private void GetDlvSessionsSQL()
        {
            DataAccess roadNetWaves = new DataAccess();
            var dlvWaveInstance = roadNetWaves.GetSessionsMasterByDate(DtSelectedDate);

            sessionsValues = dlvWaveInstance.AsEnumerable();

            this.cboBuildRoutingWave.DataSource = dlvWaveInstance;
            this.cboBuildRoutingWave.DisplayMember = "sessionName";
            this.cboBuildRoutingWave.ValueMember = "sessionid_pk";
            this.cboBuildRoutingWave.SelectedIndex = -1;
            this.txtProcessLog.Clear();
            this.JobProgressBar.Value = 1;
            this.Refresh();
        }

        private void cmdExportOrders_Click(object sender, EventArgs e)
		{
            bool noRoutesChecked = true;
            foreach (TreeNode tn in RoutesList.Nodes)
            {
                if (tn.Checked)
                    noRoutesChecked = false;
            }
            if (noRoutesChecked)
            {
                MessageBox.Show("You need to check some routes before exporting.");
                return;
            }

            string confMessage = "Are you sure you want to Export the selected Data to the AS400?";

            DateTime currentDatetime = DateTime.Now;
            if (DtSelectedDate.Date < currentDatetime.Date)
                confMessage = "Selected date is earlier than today. " + confMessage;
            DataAccess da = new DataAccess();

			var DoubleOk = false;
            
			DialogResult lcValidationResult = MessageBox.Show(confMessage, "Action Confirmation Request", MessageBoxButtons.YesNo);
			if (lcValidationResult == DialogResult.Yes)
			{
                bool stopExport = false;
                if (da.WaveExportedBefore(exportingWave, DtSelectedDate))
                {
                    DialogResult lcValidationResult2 = MessageBox.Show("You agree to continue eventhough picked account had been exported before. Are you sure you want to proceed with the export?", "Action Confirmation Request", MessageBoxButtons.YesNo);
                    stopExport = (lcValidationResult2 != DialogResult.Yes);
                }

                if (stopExport)
                    return;

				if ((chkVerified.Enabled) && (opgBuildRoutes.Checked))
				{
					DialogResult lcValidationResult2 = MessageBox.Show("You agree to continue eventhough you were asked to agree manually because some possible issues were detected. Are you sure you want to proceed with the export?", "Action Confirmation Request", MessageBoxButtons.YesNo);
					if (lcValidationResult2 == DialogResult.Yes)
						DoubleOk = true;
				}
				else
                    DoubleOk = !chkVerified.Enabled && opgBuildRoutes.Checked;

				if (DoubleOk == true)
				{
                    //NEW VERSION USING DISPATCH TRACK API
                    try
                    {
                        NProgressBarSteps = 2;
                        JobProgressBar.Minimum = 1;
                        JobProgressBar.Maximum = 100;
                        JobProgressBar.Step = (int)100 / NProgressBarSteps;
                        JobProgressBar.Value = 1;
                        JobProgressBar.Style = ProgressBarStyle.Blocks;
                        cmdExportOrders.Enabled = false;
                        txtProcessLog.Clear();
                        Cursor.Current = Cursors.WaitCursor;

                        //getting current state
                        dtRoutingDateState = dtRoutingDate.Enabled;
                        cmdResetState = cmdReset.Enabled;
                        cboBuildRoutingWaveState = cboBuildRoutingWave.Enabled;
                        cmdExportOrdersState = cmdExportOrders.Enabled;
                        opgBuildRoutesState = opgBuildRoutes.Enabled;
                        chkVerifiedState = chkVerified.Enabled;
                        txtExportAlertVisibility = txtExportAlert.Visible;
                        txtExportAlertColor = txtExportAlert.BackColor;

                        //disable all controls
                        dtRoutingDate.Enabled = false;
                        cmdReset.Enabled = false;
                        cboBuildRoutingWave.Enabled = false;
                        cmdExportOrders.Enabled = false;
                        opgBuildRoutes.Enabled = false;
                        chkVerified.Enabled = false;
                        txtExportAlert.Visible = false;
                        txtExportAlert.BackColor = Color.White;

                        keepOpen = true;
                        //exportingWave = cboBuildRoutingWave.SelectedValue.ToString();
                        bgwExportOrders.WorkerReportsProgress = true;
                        bgwExportOrders.RunWorkerAsync();
                    }
                    catch (Exception err)
                    {
                        keepOpen = false;
                        //set previous state
                        dtRoutingDate.Enabled = dtRoutingDateState;
                        cmdReset.Enabled = cmdResetState;
                        cboBuildRoutingWave.Enabled = cboBuildRoutingWaveState;
                        cmdExportOrders.Enabled = cmdExportOrdersState;
                        opgBuildRoutes.Enabled = opgBuildRoutesState;
                        chkVerified.Enabled = chkVerifiedState;
                        txtExportAlert.Visible = txtExportAlertVisibility;
                        txtExportAlert.BackColor = txtExportAlertColor;
                    }
                }

			}
		}

		private void GetRoadnetSessions()
		{
			//DsRNDatabase.Tables["RNSessions"].Clear();
			//RtgSessionC.regionIdentity = GlobalVars.DefaultRegion;
			//RtgSessionC.dateStart = DtSelectedDate;
			//RtgSessionC.dateStartSpecified = true;
			//RtgSessionC.dateEnd = DtSelectedDate;
			//RtgSessionC.dateEndSpecified = true;
			//RtgrterO.level = RoutingDetailLevel.rdlOrder;
			//RtgrterO.retrieveBuilt = false;
			//RtgrterO.retrieveActive = false;
			//RtgrterO.timeZoneOptions = cftimeZoneOptions;
			//RtvRtgSessions = GlobalVars.Rts.RetrieveRoutingSessionsByCriteria(RtgSessionC, RtgrterO);

			//if (RtvRtgSessions.Count() > 0)
			//{
			//	foreach (RoutingSession rtgSession in RtvRtgSessions)
			//	{
			//		DataRow drsession = DsRNDatabase.Tables["RNSessions"].NewRow();
			//		drsession["SessionID"] = rtgSession.sessionIdentity.internalSessionID;
			//		drsession["SessionName"] = rtgSession.description;
			//		DsRNDatabase.Tables["RNSessions"].Rows.Add(drsession);
			//	}
			//	this.cboBuildRoutingWave.Enabled = true;
			//}
			//else
			//{
			//	this.cboBuildRoutingWave.Enabled = false;
			//}
			//this.cboBuildRoutingWave.DataSource = DsRNDatabase.Tables["RNSessions"];
			//this.cboBuildRoutingWave.DisplayMember = "SessionName";
			//this.cboBuildRoutingWave.ValueMember = (string)"SessionID";
			//this.cboBuildRoutingWave.SelectedIndex = -1;
			//this.RoutesList.Nodes.Clear();
			//this.opgGeoCode.Text = "Unassign Orders for GeoCode Update";
			//this.opgBuildRoutes.Text = "Build Routes to WMS";
			//this.opgGeoCode.Enabled = false;
			//this.opgBuildRoutes.Enabled = false;
		}

		public void GetRoadnetSessionInformation()
		{
            //var totUnassign = 0;
            //var totBuild = 0;

            //this.RoutesList.Nodes.Clear();
            //RtgStopC.internalSessionID = SelectSessionIdentityID;
            //RtgStopC.internalSessionIDSpecified = true;
            //RtgStopC.regionID = GlobalVars.DefaultRegion;
            //RtgrterO.level = RoutingDetailLevel.rdlOrder;
            //RtgrterO.retrieveBuilt = false;
            //RtgrterO.retrieveActive = false;
            //RtgrterO.retrievePublished = false;
            //RtgrterO.retrieveActivities = true;
            //RtgrterO.timeZoneOptions = cftimeZoneOptions;
            //RtgStops = GlobalVars.Rts.RetrieveRoutingUnassignsByCriteria(RtgStopC, RtgrterO);

            //totUnassign = RtgStops.Count();

            //RtgrterO.retrieveBuilt = true;
            //RtgrterO.retrieveActive = false;
            //RtgrterO.retrievePublished = false;
            //RtgrterO.retrieveActivities = true;
            //RtgrterO.level = RoutingDetailLevel.rdlStop;
            //RtgrterO.timeZoneOptions = cftimeZoneOptions;

            //RtgrteC.internalSessionID = SelectSessionIdentityID;
            //RtgrteC.dateStart = DtSelectedDate;
            //RtgrteC.dateStartSpecified = true;
            //RtgrteC.regionIdentity = GlobalVars.DefaultRegion;
            //RtgRoutes = GlobalVars.Rts.RetrieveRoutingRoutesByCriteria(RtgrteC, RtgrterO);

            //totBuild = RtgRoutes.Count();

            //this.opgGeoCode.Enabled = totUnassign > 0 ? true : false;
            //this.opgBuildRoutes.Enabled = totBuild > 0 ? true : false;

            //this.opgGeoCode.Text = "Unassign Orders for GeoCode Update" + " (" + totUnassign.ToString() + ")";
            //this.opgBuildRoutes.Text = "Build Routes to WMS" + " (" + totBuild.ToString() + ")";
        }

		public Int32 GetMinutesSinceMidnight(DateTime dtStartDate, DateTime dtCurrent)
		{
			TimeSpan sinceMidnight = dtCurrent - Convert.ToDateTime(dtStartDate.Month.ToString() + "/" + dtStartDate.Day.ToString() + "/" + dtStartDate.Year.ToString());
			Double nTotalMinutes = sinceMidnight.TotalMinutes;
			return Convert.ToInt32(nTotalMinutes);
		}
		
		public void GetRoadnetSessionOrders()
		{
			//var totBadUnassign = 0;

			//this.RoutesList.Nodes.Clear();
			//DsRNDatabase.Tables["AS400Routeo"].Clear();

			//if (SelectSessionIdentityID > 0)
			//{
			//	foreach (RoutingStop rtgStopRec in RtgStops)
			//	{
			//		LocationO.retrieveActiveAlerts = false;
			//		LocationO.retrieveConsigneeHistory = false;
			//		LocationO.retrieveLocationPreferences = true;
			//		LocationO.retrieveServiceTimeOverrides = false;
			//		LocationO.retrieveTimeWindowOverrides = false;

			//		LocationI.locationID = rtgStopRec.locationIdentity.locationID;
			//		LocationI.locationType = rtgStopRec.locationIdentity.locationType;
			//		LocationI.regionID = GlobalVars.DefaultRegion;

   //                 location = GlobalVars.Rts.RetrieveLocationByIdentityEx(LocationI, LocationO);

			//		this.RoutesList.Nodes.Add(rtgStopRec.locationIdentity.locationID, rtgStopRec.locationIdentity.locationID + "    " + rtgStopRec.locationName);
			//		if (location.locConfidence == GeocodeConfidence.gcLow)
			//		{
			//			this.RoutesList.Nodes[rtgStopRec.locationIdentity.locationID].ForeColor = System.Drawing.Color.Red;
			//			totBadUnassign++;
			//		}

			//		if (Location != null)
			//		{
			//			DataRow drAS400Routeo = DsRNDatabase.Tables["AS400Routeo"].NewRow();
			//			drAS400Routeo["RNSessionId"] = SelectSessionIdentityID;
			//			drAS400Routeo["RNSessionDate"] = rtgStopRec.sessionDate;
			//			drAS400Routeo["RNFullName"] = "";
			//			drAS400Routeo["RNStopId"] = rtgStopRec.stopIdentity.internalStopID;
			//			drAS400Routeo["RNStopPos"] = rtgStopRec.sequenceNumber;
			//			drAS400Routeo["RNZipcode"] = rtgStopRec.address.postalCode.Trim();
			//			drAS400Routeo["RNTWStart"] = Convert.ToDateTime(rtgStopRec.tw1OpenTime).ToLongTimeString();
			//			drAS400Routeo["RNTWEnd"] = Convert.ToDateTime(rtgStopRec.tw1CloseTime).ToLongTimeString();
			//			drAS400Routeo["RNCubes"] = rtgStopRec.deliveryQuantity.size1.ToString();
			//			drAS400Routeo["RNCustName"] = rtgStopRec.locationName.Trim();
			//			drAS400Routeo["RoZone"] = location.userDefinedField1;
			//			drAS400Routeo["RoDate"] = Convert.ToInt32(rtgStopRec.orders[0].beginDate.Year.ToString().Substring(2) + rtgStopRec.orders[0].beginDate.DayOfYear.ToString().PadLeft(3, '0'));
			//			drAS400Routeo["RoPos"] = location.userDefinedField3;
			//			drAS400Routeo["RoInv"] = Convert.ToInt32(rtgStopRec.locationIdentity.locationID.Substring(2, 6));
			//			drAS400Routeo["RoRouT"] = "99";
			//			drAS400Routeo["RoRTyp"] = "-";
			//			drAS400Routeo["RoDist"] = rtgStopRec.distance;
			//			drAS400Routeo["RoXGeo"] = location.longitude.ToString().Substring(0, 3) + "." + location.longitude.ToString().Substring(3);
			//			drAS400Routeo["RoYGeo"] = location.latitude.ToString().Substring(0, 2) + "." + location.latitude.ToString().Substring(2);
			//			drAS400Routeo["RoSMin"] = 0;
			//			drAS400Routeo["RoDMin"] = Convert.ToInt32(rtgStopRec.serviceTime / 60);
			//			DsRNDatabase.Tables["AS400Routeo"].Rows.Add(drAS400Routeo);
			//		}
			//	}

			//	if (totBadUnassign > 0)
			//	{
			//		MessageBox.Show(totBadUnassign.ToString() + " stops with Low GeoCode Confidence. Please verify.", "Bad Export Data Found", MessageBoxButtons.OK, MessageBoxIcon.Stop);
			//		this.cmdExportOrders.Enabled = false;
			//	}
			//	else
			//		this.cmdExportOrders.Enabled = this.RoutesList.Nodes.Count > 0 ? true : false;
			//}
			//else
			//	this.cmdExportOrders.Enabled = false;
		}

		public void GetRoadnetSessionBuildRoutes()
		{
            //NEW VERSION
            cmdExportOrders.Enabled = false;
            RoutesList.Nodes.Clear();
            chkVerified.Checked = false;
            chkVerified.Enabled = false;
            
            if (SelectSessionIdentityID > 0)
            {
                //getting current state
                dtRoutingDateState = dtRoutingDate.Enabled;
                cmdResetState = cmdReset.Enabled;
                cboBuildRoutingWaveState = cboBuildRoutingWave.Enabled;
                cmdExportOrdersState = cmdExportOrders.Enabled;
                opgBuildRoutesState = opgBuildRoutes.Enabled;
                chkVerifiedState = chkVerified.Enabled;

                //disable all controls
                dtRoutingDate.Enabled = false;
                cmdReset.Enabled = false;
                cboBuildRoutingWave.Enabled = false;
                cmdExportOrders.Enabled = false;
                opgBuildRoutes.Enabled = false;
                chkVerified.Enabled = false;

                noAS400 = null;
                zoneChanged = null;
                positionChanged = null;
                notComing = null;
                importedNotComing = null;
                dictRes = null;
                invalidRoutes = null;
                repeatedGeoCodes = null;
                loadingRoutesSuccess = false;
                JobProgressBar.Value = 15;
                totalRoutes = 0;
                totalOrders = 0;

                bgwLoadingRoutes.WorkerReportsProgress = true;
                bgwLoadingRoutes.RunWorkerAsync();
            }
            else
            {
                chkVerified.Checked = false;
                chkVerified.Enabled = false;
                cmdExportOrders.Enabled = false;
            }
        }

		public Tuple<int, List<string>> CheckRouteStopsInRouteODataHistory(DataTable dtRoutingData, int rnSessionID)
		{
            dtRoutingData.DefaultView.Sort = "RNSessionDate, RoRout, RNStopPos";

			List<int> days = dtRoutingData.AsEnumerable().Select(a => a.Field<int>("RoDate")).Distinct().ToList();
			var dateFrom = days.Min();
			var dateTo = days.Max();
			
			var discrepancies = 0;
			List<string> strRecords = new List<string>();

			DataTable dtResult = new DataTable();
			DataAccess SQLFunctions = new DataAccess();
			dtResult.Load(SQLFunctions.GetRoutingDateSessionHistoryDataOnlyStops(dateFrom, dateTo, rnSessionID));

			if (dtResult.Rows.Count > 0)
			{
				foreach (DataRow dtr in dtResult.Rows)
				{
					if (dtRoutingData.Select("RoDate = " + dtr["RoDate"].ToString().PadLeft(3, '0') + "AND RoInv = " + dtr["RoInv"].ToString() + "AND RoRout = '" + dtr["RoRout"].ToString() + "'").Count() == 0)
					{
						discrepancies ++;
						strRecords.Add (dtr["RoInv"].ToString() + " - " + dtr["RoRout"].ToString());

					}
				}
			}
			return new Tuple<int, List<string>>(discrepancies, strRecords);
		}

		private void dtRoutingDate_ValueChanged(object sender, EventArgs e)
		{
			DtSelectedDate = new DateTime(dtRoutingDate.Value.Year, dtRoutingDate.Value.Month, dtRoutingDate.Value.Day);
            txtExportAlert.Visible = false;
            //GetRoadnetSessions();
            GetDlvSessionsSQL();
        }

		//public void UpdateProcessLog(string text)
		//{
		//	if (InvokeRequired)
		//	{
		//		this.Invoke((MethodInvoker)delegate() { UpdateProcessLog(text); });
		//		return;
		//	}
		//	this.txtProcessLog.Text = text;
		//}

		private void opgBuildRoutes_CheckedChanged(object sender, EventArgs e)
		{
			chkVerified.Checked = false;
            txtExportAlert.Visible = false;
            
            cmdExportOrders.Enabled = false;
            if (opgBuildRoutes.Checked)
            {
                GetRoadnetSessionBuildRoutes();
            }

        }

        private void opgGeoCode_CheckedChanged(object sender, EventArgs e)
		{
			GetRoadnetSessionOrders();
		}

		private void cboBuildRoutingWave_SelectedIndexChanged(object sender, EventArgs e)
		{
            txtExportAlert.Visible = false;
            opgBuildRoutes.Checked = false;
            RoutesList.Nodes.Clear();
            lblListHeader.Text = "Details:";
            lblNodesSelected.Text = "Selected Nodes: 0";
            txtProcessLog.Clear();
            ComboBox cmb = (ComboBox)sender;
			try 
			{
				SelectSessionIdentityID = (int)cmb.SelectedValue;
                SelectSessionName = cmb.Text.ToString();
                exportingWave = cboBuildRoutingWave.SelectedValue.ToString();

                DateTime dateTime = this.dtRoutingDate.Value;
                //string selectedDate = String.Format("{2}-{1}-{0}", dateTime.Day, dateTime.Month, dateTime.Year);

                this.cboBuildRoutingWaveSub.DataSource = null;
                this.cboBuildRoutingWaveSub.Items.Clear();

                DataAccess da = new DataAccess();
                var dlvWaveInstance = da.GetSessionsMasterByDateExport(dateTime, SelectSessionName);
                if (dlvWaveInstance.Rows.Count > 0)
                {
                    sessionsValues = dlvWaveInstance.AsEnumerable();
                    this.cboBuildRoutingWaveSub.DataSource = dlvWaveInstance;
                    this.cboBuildRoutingWaveSub.DisplayMember = "sessionname";
                    this.cboBuildRoutingWaveSub.ValueMember = "sessionid_pk";
                    this.cboBuildRoutingWaveSub.SelectedIndex = -1;
                    this.txtProcessLog.Clear();
                    this.cboBuildRoutingWaveSub.Enabled = true;
                }
                else
                {
                    this.cboBuildRoutingWaveSub.Enabled = false;
                }
            }
            catch
			{
				SelectSessionIdentityID = 0;
                SelectSessionName = "";
                exportingWave = "";
			}
        }

        //private void cboBuildRoutingWave_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ComboBox cmb = (ComboBox)sender;
        //    int selectedIndex = cmb.SelectedIndex;
        //    DataRowView dtRow = (DataRowView)cboBuildRoutingWave.SelectedItem;
        //    if (dtRow != (null))
        //    {
        //        this.chkNoRegularInvoices.Enabled = true;
        //        this.chkNoRegularInvoices.Checked = false;
        //        this.btnUpdateDrivers.Enabled = false;
        //    }
        //    else
        //    {
        //        this.chkNoRegularInvoices.Checked = false;
        //        this.btnUpdateDrivers.Enabled = true;

        //    }

        //    if (selectedIndex != -1)
        //    {
        //        string selectedValue = cmb.Text.ToString();
        //        DateTime dateTime = this.dtRoutingDate.Value;
        //        string selectedDate = String.Format("{2}-{1}-{0}", dateTime.Day, dateTime.Month, dateTime.Year);

        //        this.flag = 0;
        //        this.cboRoutingWaveSub.DataSource = null;
        //        this.cboRoutingWaveSub.Items.Clear();

        //        DataAccess roadNetWaves = new DataAccess();
        //        var dlvWaveInstance = roadNetWaves.GetSessionsMasterByDistributionCenter(selectedDate, selectedValue);
        //        if (dlvWaveInstance.Rows.Count > 0)
        //        {
        //            sessionsValues = dlvWaveInstance.AsEnumerable();
        //            this.cboRoutingWaveSub.DataSource = dlvWaveInstance;
        //            this.cboRoutingWaveSub.DisplayMember = "sessionname";
        //            this.cboRoutingWaveSub.ValueMember = "sessionid_pk";
        //            this.cboRoutingWaveSub.SelectedIndex = -1;
        //            this.txtProcessLog.Clear();
        //            this.cboRoutingWaveSub.Enabled = true;
        //        }
        //        else
        //        {
        //            this.cboRoutingWaveSub.Enabled = false;
        //        }
        //    }
        //    this.Refresh();
        //}






        private void cmdReset_Click(object sender, EventArgs e)
		{
			DtSelectedDate = System.DateTime.Now;
			dtRoutingDate.Value = DtSelectedDate;
            RoutesList.Nodes.Clear();
            lblListHeader.Text = "Details:";
            lblNodesSelected.Text = "Selected Nodes: 0";
            txtExportAlert.Visible = false;
        }

		private void cmdExpand_Click(object sender, EventArgs e)
		{
			this.RoutesList.ExpandAll();
		}

		private void cmdCollapse_Click(object sender, EventArgs e)
		{
			this.RoutesList.CollapseAll();
		}

		private void CFRoadNetExportOrders_Load(object sender, EventArgs e)
		{
		}

		private void opgExportMode_Paint(object sender, PaintEventArgs e)
		{
		}

		private void chkVerified_CheckedChanged(object sender, EventArgs e)
		{
			if (this.chkVerified.Checked == true)
				this.cmdExportOrders.Enabled = true;
			else
				if (this.chkVerified.Enabled == true)
					this.cmdExportOrders.Enabled = false;
		}


        private void bgwExportOrders_DoWork(object sender, DoWorkEventArgs e)
        {
            var tokenSource = new CancellationTokenSource();
            try
            {
                DataAccess dataAccess = new DataAccess();
                string processCode = ProcessCode.EXPORT.ToString();
                int currentProcessId;
                if (dataAccess.CreateProcess(processCode, out currentProcessId))
                {
                    //InsertRouteODataInHistory(dtRoutingDataOneDate, 1);

                    var token = tokenSource.Token;
                    RunProcessLogs(token, currentProcessId);

                    bgwExportOrders.ReportProgress(20);

                    string date = DtSelectedDate.ToString("yyyyMMdd");
                    //string warehouseId = ((int)Enum.Parse(typeof(Warehouse), Warehouse.TAMARAC.ToString())).ToString();
                    string warehouseId = int.Parse(sessionsValues.First(cdr => cdr["sessionid_pk"].ToString() == exportingWave)["cfwh_id"].ToString()).ToString();

                    CFDispatchTrackApplicationSettings loAppSettings = new CFDispatchTrackApplicationSettings();
                    string apiHost = loAppSettings.Get("DispatchTrackAPIHost");
                    string apiPort = loAppSettings.Get("DispatchTrackAPIPort");
                    string apiExportOrders = loAppSettings.Get("DispatchTrackExportOrders");

                    string url = apiHost + (apiPort.Trim().Length > 0 ? ":" + apiPort : "") + apiExportOrders;

                    bgwExportOrders.ReportProgress(25);
                    List<string> checkedRoutes = new List<string>();
                    foreach (TreeNode currentNode in RoutesList.Nodes)
                        if (currentNode.Checked)
                        {
                            string finalValue = "";
                            string[] stringArray = currentNode.Text.Split(new char[] { ' ' });
                            if (stringArray.Length > 1)
                                finalValue = stringArray[0].Trim();
                            else
                                finalValue = currentNode.Text.Trim();
                            checkedRoutes.Add(finalValue);
                        }
                    bool res = NodeAPI.ExportOrdersDataByRoute(url, date, warehouseId, exportingWave, currentProcessId.ToString(), checkedRoutes);

                    if (!res)
                        MessageBox.Show("Error while exporting the orders.");
                    else
                    {
                        MessageBox.Show("Orders successfully exported.");    //Commented code above because export history is being saved now from the node API directly. This is because when a 
                        bgwExportOrders.ReportProgress(100);                 //bad gateway error (aws server ends the connection from the windows app) was received during the export process the history was not being saved.
                    }
                }
                else
                    MessageBox.Show("Error while creating the process.");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
            Thread.Sleep(1000);
            tokenSource.Cancel();  //this change the value of token.IsCancellationRequested from 'false' to 'true', stopping the while loop in method ProcessLogs

        }

        private void bgwExportOrders_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            JobProgressBar.Value = e.ProgressPercentage;
        }

        private void bgwExportOrders_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            keepOpen = false;

            dtRoutingDate.Enabled = dtRoutingDateState;
            cmdReset.Enabled = cmdResetState;
            cboBuildRoutingWave.Enabled = cboBuildRoutingWaveState;
            cmdExportOrders.Enabled = cmdExportOrdersState;
            opgBuildRoutes.Enabled = opgBuildRoutesState;
            chkVerified.Enabled = chkVerifiedState;
        }

        private void CFDispatchTrackExportOrders_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (keepOpen)
                e.Cancel = true;
        }

        public int ToJulianDate(DateTime date)
        {
            string dayOfYear = date.DayOfYear.ToString().PadLeft(3, '0');
            string year = (date.Year % 100).ToString();
            return int.Parse(year + dayOfYear);
        }

        public void InsertRouteODataInHistory()
        {
            DataAccess da = new DataAccess();
            Dictionary<string, float> itemCubes = new Dictionary<string, float>();
            List<Tuple<string, int>> deletedRoutes = new List<Tuple<string, int>>();

            string now = DateTime.Now.ToString("yyyy-MM-dd");
            string date = DtSelectedDate.ToString("yyyyMMdd");
            //string warehouseId = ((int)Enum.Parse(typeof(Warehouse), Warehouse.TAMARAC.ToString())).ToString();
            string warehouseId = int.Parse(sessionsValues.First(cdr => cdr["sessionid_pk"].ToString() == exportingWave)["cfwh_id"].ToString()).ToString();

            CFDispatchTrackApplicationSettings loAppSettings = new CFDispatchTrackApplicationSettings();
            string apiHost = loAppSettings.Get("DispatchTrackAPIHost");
            string apiPort = loAppSettings.Get("DispatchTrackAPIPort");
            string apiGetOrdersInfo = loAppSettings.Get("DispatchTrackGetOrdersInfo");

            string url = apiHost + (apiPort.Trim().Length > 0 ? ":" + apiPort : "") + apiGetOrdersInfo + warehouseId + "/" + date + "/" + exportingWave;

            //string currentSessionName = cboBuildRoutingWave.Text;
            string currentSessionName = SelectSessionName;
            string currentSessionDate = DtSelectedDate.ToString("yyyy-MM-dd");
            int currentRoDate = ToJulianDate(DtSelectedDate);
            string currentRoute = "";
            string currentOrder = "";
            string currentRouteZone = "";
            string currentStop = "";
            string currentRoutePos = "";
            string currentRouteDistance = "";
            string currentZipCode = "";
            string currentTimeWindowStart = "";
            string currentTimeWindowEnd = "";
            string currentFullName = "";
            string currentHasBackorders = "";
            double currentLatitude;
            double currentLongitude;
            string currentCreateDT = "";
            float currentCubes = 0;
            string currentDeliveryDate = "";

            string currentRouteType = "";
            string currentHasRestrictions = "";
            string currentIsStore = "";
            string currentIsFreeShip = "";

            int currentStopID = 0;
            int currentRoSMin = 0;
            int currentRoDMin = 0;
            
            dynamic jsonData = NodeAPI.GetFullOrdersDataFromDispatchTrack(url);

            ShowLogMessages("Saving records into data history." + Environment.NewLine);
            string previousRoute = "";
            foreach (var orderData in jsonData.data)
            {
                currentRoute = orderData.routeID;

                if(currentRoute != previousRoute)
                    ShowLogMessages("Saving route: " + orderData.routeID + Environment.NewLine);
                previousRoute = currentRoute;

                currentOrder = orderData.uniqueOrderID;
                string[] auxArray = currentOrder.Split(new char[] { '-' });
                if (auxArray.Length == 2)
                    currentOrder = auxArray[1].Trim();
                else
                    currentOrder = "0";
                currentLatitude = orderData.orderDetails.customer.First.latitude.First;
                currentLongitude = orderData.orderDetails.customer.First.longitude.First;
                currentStop = orderData.stopSeqID == null || orderData.stopSeqID.ToString().Trim().Length == 0 ? "0" : orderData.stopSeqID.ToString().Trim();
                currentRouteZone = orderData.as400Zone;
                currentRoutePos = orderData.orderDetails.extra.First.routepos.First == null || orderData.orderDetails.extra.First.routepos.First.ToString().Trim().Length == 0 ? "0" : orderData.orderDetails.extra.First.routepos.First.ToString().Trim();
                currentRouteDistance = orderData.routeMiles == null || orderData.routeMiles.ToString().Trim().Length == 0 ? "0" : orderData.routeMiles.ToString().Trim();
                currentZipCode = orderData.orderDetails.customer.First.zip.First;
                currentTimeWindowStart = orderData.orderDetails.time_window_start.First;
                currentTimeWindowEnd = orderData.orderDetails.time_window_end.First;
                currentFullName = orderData.orderDetails.customer.First.first_name.First + " " + orderData.orderDetails.customer.First.last_name.First;
                currentHasBackorders = orderData.orderDetails.extra.First.backorderflg.First;
                currentCreateDT = now;
                currentDeliveryDate = orderData.deliveryDate.ToString().Trim();

                currentRouteType = "1";
                currentHasRestrictions = "N";
                currentIsStore = "N";
                currentIsFreeShip = "N";

                if (!deletedRoutes.Contains(new Tuple<string, int>(currentRoute, currentRoDate)))
                {
                    da.DeleteRoutingSessionHistoryData(currentRoute, currentRoDate);
                    deletedRoutes.Add(new Tuple<string, int>(currentRoute, currentRoDate));
                }

                string currentItemNumber = "";
                float cubesValue = 0;
                int itemQuantity = 0;
                currentCubes = 0;
                if (orderData.orderDetails.items != null) //check the order has any item
                {
                    foreach (var item in orderData.orderDetails.items[0].item)
                    {
                        currentItemNumber = item.item_id.First;
                        currentItemNumber = currentItemNumber.TrimStart(new char[] { '0' });
                        itemQuantity = item.quantity.First;
                        if (itemCubes.ContainsKey(currentItemNumber.ToString().Trim()))
                            cubesValue = itemCubes[currentItemNumber.ToString().Trim()];
                        else
                        {
                            cubesValue = da.GetCubesByItemNumber(currentItemNumber.ToString().Trim());
                            itemCubes.Add(currentItemNumber.ToString().Trim(), cubesValue);
                        }
                        currentCubes += cubesValue * itemQuantity;
                    }
                }

                da.InsertRoutingSessionHistoryData(DateTime.Parse(currentCreateDT),
                                                   DateTime.Parse(currentSessionDate),
                                                   currentStopID,
                                                   int.Parse(currentStop),
                                                   currentRouteZone,
                                                   currentRoDate,
                                                   int.Parse(currentRoutePos),
                                                   int.Parse(currentOrder),
                                                   currentRoute,
                                                   currentRouteType,
                                                   (int)float.Parse(currentRouteDistance),
                                                   currentLongitude.ToString(),
                                                   currentLatitude.ToString(),
                                                   currentRoSMin,
                                                   currentRoDMin,
                                                   currentZipCode,
                                                   currentTimeWindowStart,
                                                   currentTimeWindowEnd,
                                                   (int)currentCubes,
                                                   currentFullName,
                                                   currentHasRestrictions,
                                                   currentHasBackorders,
                                                   currentIsStore,
                                                   currentSessionName,
                                                   currentIsFreeShip,
                                                   currentDeliveryDate
                                                  );
            }
        }

        public void InsertRouteODataInHistory(DataTable dtRoutingDataOneDate, int nRunningMode)
        {
            //nRunningMode = 1 DELETE + ADD,  nRunningMode = 2 DELETE ONLY

            DataAccess SQLFunctions = new DataAccess();
            var prevRoute = "";
            var prevDate = 0;

            foreach (DataRow dtrow in dtRoutingDataOneDate.Rows)
            {

                //Delete existing records for the same route and date
                if (prevRoute != dtrow["RoRout"].ToString() || (prevDate != (int)dtrow["RoDate"]))
                {
                    if (SQLFunctions.GetRoutingSessionHistoryData(dtrow["RoRout"].ToString(), (int)dtrow["RoDate"]).HasRows)
                    {
                        SQLFunctions.DeleteRoutingSessionHistoryData(dtrow["RoRout"].ToString(), (int)dtrow["RoDate"]);
                    }
                    prevRoute = dtrow["RoRout"].ToString();
                    prevDate = (int)dtrow["RoDate"];
                }

                if (nRunningMode == 1)
                {
                    SQLRouteoHist RouteHistory = new SQLRouteoHist();

                    RouteHistory.RNSessionID = (int)dtrow["RNSessionId"];
                    RouteHistory.RNSessionDate = Convert.ToDateTime(dtrow["RNSessionDate"]);
                    RouteHistory.RNStopID = (int)dtrow["RNStopId"];
                    RouteHistory.RNStopPos = (int)dtrow["RNStopPos"];
                    RouteHistory.RoZone = dtrow["RoZone"].ToString();
                    RouteHistory.RoDate = (int)dtrow["RoDate"];
                    RouteHistory.RoPos = (int)dtrow["RoPos"];
                    RouteHistory.RoInv = (int)dtrow["RoInv"];
                    RouteHistory.RoRouT = dtrow["RoRout"].ToString();
                    RouteHistory.RoRTyp = dtrow["RoRTyp"].ToString();
                    RouteHistory.RoDist = (int)dtrow["RoDist"];
                    RouteHistory.RoXGeo = dtrow["RoXGeo"].ToString();
                    RouteHistory.RoYGeo = dtrow["RoYGeo"].ToString();
                    RouteHistory.RoSMin = (int)dtrow["ROSmin"];
                    RouteHistory.RoDMin = (int)dtrow["RoDMin"];
                    RouteHistory.RNZipCode = dtrow["RNZipCode"].ToString();
                    RouteHistory.RNTWStart = dtrow["RNTWStart"].ToString();
                    RouteHistory.RNTWEnd = dtrow["RNTWEnd"].ToString();
                    RouteHistory.RNCubes = Convert.ToInt32(dtrow["RNCubes"]);
                    RouteHistory.RNFullName = dtrow["RNFullName"].ToString();
                    RouteHistory.RNHasRestrictions = dtrow["RNHasRestrictions"].ToString();
                    RouteHistory.RNHasBackOrders = dtrow["RNHasBackOrders"].ToString();
                    RouteHistory.RNIsStore = dtrow["RNIsStore"].ToString();
                    RouteHistory.RNSessionName = dtrow["RNSessionName"].ToString();
                    RouteHistory.RNIsFreeShip = dtrow["RNIsFreeShip"].ToString();
                    try
                    {
                        SQLFunctions.ExportRoutingSessionHistoryData(null, RouteHistory);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        delegate void ShowLogMessagesDelegate(string text);

        private void ShowLogMessages(string message)
        {
            if (txtProcessLog.InvokeRequired)
            {
                ShowLogMessagesDelegate d = new ShowLogMessagesDelegate(ShowLogMessages);
                Invoke(d, new object[] { message });
            }
            else
            {
                //txtProcessLog.Text = message + txtProcessLog.Text;
                Color colorToUse;
                if (message.StartsWith("g__"))
                {
                    message = message.Substring(3);
                    colorToUse = Color.Green;
                }
                else if (message.StartsWith("r__"))
                {
                    message = message.Substring(3);
                    colorToUse = Color.Red;
                }
                else
                    colorToUse = Color.Black;
                txtProcessLog.SelectAll();
                string oldText = txtProcessLog.SelectedRtf;

                txtProcessLog.Text = message;
                txtProcessLog.Select(0, message.Length);
                txtProcessLog.SelectionColor = colorToUse;
                txtProcessLog.DeselectAll();
                txtProcessLog.SelectionStart = txtProcessLog.TextLength;
                txtProcessLog.SelectedRtf = oldText;
                txtProcessLog.DeselectAll();
            }
        }

        private async void RunProcessLogs(CancellationToken ct, int processId)
        {
            var progress = new Progress<Tuple<string, bool>>(t => ShowLogMessages(t.Item1));
            await Task.Factory.StartNew(() => ProcessLogs(progress, ct, processId), ct);
        }

        private void ProcessLogs(IProgress<Tuple<string, bool>> progress, CancellationToken ct, int processId)
        {
            try
            {
                progress.Report(new Tuple<string, bool>("", true));
                List<Tuple<int, string>> currentLogs = new List<Tuple<int, string>>();
                int lastLog = 0;
                DataAccess da = new DataAccess();
                string processCode = ProcessCode.EXPORT.ToString();
                //int processId = da.GetRunningProcess(processCode);
                string currentLog = "";
                if (processId >= 0)
                {
                    while(!ct.IsCancellationRequested)
                    {
                        currentLogs = da.GetLogs(processId, lastLog);
                        foreach (var log in currentLogs)
                        {
                            lastLog = log.Item1;
                            currentLog = log.Item2;
                            //progress.Report(new Tuple<string, bool>(currentLog + Environment.NewLine, false));
                            ShowLogMessages(currentLog + Environment.NewLine);
                        }
                    }
                    da.FinishProcess(processCode, processId);
                }
                else
                {
                    MessageBox.Show("No process currently running.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while getting the process' logs.");
            }
        }

        delegate void SetDetailTextCallback(string text);

        private void SetDetailText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (lblListHeader.InvokeRequired)
            {
                SetDetailTextCallback d = new SetDetailTextCallback(SetDetailText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                lblListHeader.Text = text;
            }
        }

        private void bgwLoadingRoutes_DoWork(object sender, DoWorkEventArgs e)
        {
            exceptionGettingRecords = false;
            try
            {
                string date = DtSelectedDate.ToString("yyyyMMdd");
                //string warehouseId = ((int)Enum.Parse(typeof(Warehouse), Warehouse.TAMARAC.ToString())).ToString();

                string warehouseId = int.Parse(sessionsValues.First(cdr => cdr["sessionid_pk"].ToString() == exportingWave)["cfwh_id"].ToString()).ToString();

                CFDispatchTrackApplicationSettings loAppSettings = new CFDispatchTrackApplicationSettings();

                string reviewFilter = loAppSettings.Get(SelectSessionName);
                string apiHost = loAppSettings.Get("DispatchTrackAPIHost");
                string apiPort = loAppSettings.Get("DispatchTrackAPIPort");
                string apiGetOrdersInfo = loAppSettings.Get("DispatchTrackGetOrdersInfo");
                string apiCheckBeforeExport = loAppSettings.Get("DispatchTrackCheckDataBeforeExporting");
                ShowLogMessages("Pulling routing data from Dispatch Track process started" + Environment.NewLine);
                string url = apiHost + (apiPort.Trim().Length > 0 ? ":" + apiPort : "") + apiGetOrdersInfo + warehouseId + "/" + date + "/" + exportingWave;
                loadingRoutesSuccess = NodeAPI.GetOrdersDataFromDispatchTrack(url, out dictRes, out invalidRoutes, out invalidGeoCodes, out repeatedGeoCodes, out totalRoutes, out totalStops, out totalOrders);
                url = apiHost + (apiPort.Trim().Length > 0 ? ":" + apiPort : "") + apiCheckBeforeExport + warehouseId + "/" + date + "/" + exportingWave + "/" + reviewFilter;
                ShowLogMessages("Comparing invoices in Dispatch Track with invoices in the AS400." + Environment.NewLine);
                NodeAPI.CheckDataInDTBeforeExporting(url, out noAS400, out zoneChanged, out positionChanged, out notComing, out importedNotComing);
                SetDetailText("Details: Total Routes (" + totalRoutes.ToString().Trim() + ")  Total Stops (" + totalStops.ToString().Trim() + ")  Total Orders (" + totalOrders.ToString().Trim()  + ")");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                exceptionGettingRecords = true;
            }
        }

        private void bgwLoadingRoutes_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //if (e.ProgressPercentage == 5)
            //{
            //    //getting current state
            //    dtRoutingDateState = dtRoutingDate.Enabled;
            //    cmdResetState = cmdReset.Enabled;
            //    cboBuildRoutingWaveState = cboBuildRoutingWave.Enabled;
            //    cmdExportOrdersState = cmdExportOrders.Enabled;
            //    opgGeoCodeState = opgGeoCode.Enabled;
            //    opgBuildRoutesState = opgBuildRoutes.Enabled;
            //    chkVerifiedState = chkVerified.Enabled;

            //    //disable all controls
            //    dtRoutingDate.Enabled = false;
            //    cmdReset.Enabled = false;
            //    cboBuildRoutingWave.Enabled = false;
            //    cmdExportOrders.Enabled = false;
            //    opgGeoCode.Enabled = false;
            //    opgBuildRoutes.Enabled = false;
            //    chkVerified.Enabled = false;
            //}
            //else if (e.ProgressPercentage == 91)
            //{
            //    dtRoutingDate.Enabled = dtRoutingDateState;
            //    cmdReset.Enabled = cmdResetState;
            //    cboBuildRoutingWave.Enabled = cboBuildRoutingWaveState;
            //    opgGeoCode.Enabled = opgGeoCodeState;
            //    opgBuildRoutes.Enabled = opgBuildRoutesState;

            //    chkVerified.Checked = false;
            //    chkVerified.Enabled = false;
            //    cmdExportOrders.Enabled = false;
            //}
            //JobProgressBar.Value = e.ProgressPercentage;
        }

        private void bgwLoadingRoutes_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!exceptionGettingRecords)
            {
                JobProgressBar.Value = 90;

                RoutesList.Nodes.Clear();
                if (loadingRoutesSuccess && dictRes != null && dictRes.Keys.Count > 0 && noAS400 != null && zoneChanged != null && positionChanged != null && notComing != null && importedNotComing != null)
                {
                    //if (noAS400.Count > 0 || zoneChanged.Count > 0 || positionChanged.Count > 0)
                    //    MessageBox.Show("Some invoices changed in the AS400 while routing. Please check the data before exporting.");
                    bool dataChanged = noAS400.Count > 0 || zoneChanged.Count > 0 || positionChanged.Count > 0;

                    if (notComing.Count > 0)
                    {
                        notComing = notComing.Select(invNumber => ("000000" + invNumber).Substring(invNumber.Length)).ToList();
                        notComing.Sort();
                        for (int i = 0; i < notComing.Count; i++)
                            ShowLogMessages("r__" + notComing[i] + " ALERT!" + Environment.NewLine);
                        ShowLogMessages("r__" + notComing.Count + " invoices in the AS400 for this wave are not coming from Dispatch Track!" + Environment.NewLine);
                    }

                    if (importedNotComing.Count > 0)
                    {
                        importedNotComing.Sort();
                        for (int i = 0; i < importedNotComing.Count; i++)
                            ShowLogMessages("r__" + importedNotComing[i] + " ALERT!" + Environment.NewLine);
                        ShowLogMessages("r__" + importedNotComing.Count + " invoices that were imported for this wave are not coming from Dispatch Track!" + Environment.NewLine);
                    }

                    var items = dictRes.OrderBy(elem => elem.Key);
                    bool showAuxMessage = false;
                    string invoiceLevelMessage = "";
                    foreach (var routeData in items)
                    {
                        showAuxMessage = routeData.Value.Where(v => noAS400.Contains(v.Key.InvoiceValue)).Count() > 0 ||
                                         routeData.Value.Where(v => zoneChanged.Contains(v.Key.InvoiceValue)).Count() > 0 ||
                                         routeData.Value.Where(v => positionChanged.Contains(v.Key.InvoiceValue)).Count() > 0;
                        RoutesList.Nodes.Add(routeData.Key + (showAuxMessage ? " (Wrong Invoice)" : "" ));
                        int currentStop = -1;
                        string currentCustomer = "";
                        foreach (var orderData in routeData.Value)
                        {
                            if (orderData.Key.StopValue != currentStop)
                            {
                                showAuxMessage = routeData.Value.Where(v => v.Key.CustomerName == orderData.Key.CustomerName && noAS400.Contains(v.Key.InvoiceValue)).Count() > 0 ||
                                                 routeData.Value.Where(v => v.Key.CustomerName == orderData.Key.CustomerName && zoneChanged.Contains(v.Key.InvoiceValue)).Count() > 0 ||
                                                 routeData.Value.Where(v => v.Key.CustomerName == orderData.Key.CustomerName && positionChanged.Contains(v.Key.InvoiceValue)).Count() > 0;
                                currentStop = orderData.Key.StopValue;
                                currentCustomer = orderData.Key.CustomerName;
                                RoutesList.Nodes[RoutesList.Nodes.Count - 1].Nodes.Add("Stop " + currentStop.ToString() + " " + currentCustomer + (showAuxMessage ? " (Wrong Invoice)" : "")).Checked = true;
                            }

                            showAuxMessage = noAS400.Contains(orderData.Key.InvoiceValue) || zoneChanged.Contains(orderData.Key.InvoiceValue) || positionChanged.Contains(orderData.Key.InvoiceValue);
                            if (noAS400.Contains(orderData.Key.InvoiceValue))
                                invoiceLevelMessage = " (Invoice not in AS400)";
                            else if (zoneChanged.Contains(orderData.Key.InvoiceValue))
                                invoiceLevelMessage = " (Zone changed in AS400)";
                            else if (positionChanged.Contains(orderData.Key.InvoiceValue))
                                invoiceLevelMessage = " (Position changed in AS400)";
                            else
                                invoiceLevelMessage = "";
                            RoutesList.Nodes[RoutesList.Nodes.Count - 1].Nodes[RoutesList.Nodes[RoutesList.Nodes.Count - 1].Nodes.Count - 1].Nodes.Add("Invoice " + orderData.Key.InvoiceValue + (showAuxMessage ? invoiceLevelMessage : "")).Checked = true;
                            foreach (string itemData in orderData.Value)
                                RoutesList.Nodes[RoutesList.Nodes.Count - 1].Nodes[RoutesList.Nodes[RoutesList.Nodes.Count - 1].Nodes.Count - 1].Nodes[RoutesList.Nodes[RoutesList.Nodes.Count - 1].Nodes[RoutesList.Nodes[RoutesList.Nodes.Count - 1].Nodes.Count - 1].Nodes.Count - 1].Nodes.Add("Item " + itemData).Checked = true;
                        }
                    }

                    if (invalidRoutes.Count > 0)
                    {
                        MessageBox.Show(invalidRoutes.Count + " build routes found with Wrong 'Route ID'. Please verify.", "Bad Export Data Found", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        chkVerified.Checked = false;
                        chkVerified.Enabled = false;
                        cmdExportOrders.Enabled = false;
                    }
                    else if (repeatedGeoCodes.Count > 0)
                    {
                        string info = "";
                        foreach (var c in repeatedGeoCodes)
                        {
                            info += "Coordinates (" + c.Key.Item1 + "," + c.Key.Item2 + ") in routes: ";
                            foreach (var s in c.Value)
                                info += s.Item2 + ", ";
                        }
                        MessageBox.Show(repeatedGeoCodes.Count + " customer(s) with identical GEOCODE found in multiple routes. " + info, "Customer in Multiple Routes", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        chkVerified.Checked = false;
                        chkVerified.Enabled = true;
                        cmdExportOrders.Enabled = false;
                    }
                    else if (invalidGeoCodes.Count > 0)
                    {
                        string info = "";
                        foreach (var c in invalidGeoCodes)
                        {
                            if (info.Length > 0)
                                info += ",";
                            info += c;
                        }
                        MessageBox.Show(invalidGeoCodes.Count + " customer(s) with invalid GEOCODE found. Details: " + info, "Customer with invalid GEOCODES", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        chkVerified.Checked = false;
                        chkVerified.Enabled = true;
                        cmdExportOrders.Enabled = false;
                    }
                    else
                    {
                        chkVerified.Checked = true;
                        chkVerified.Enabled = false;
                        cmdExportOrders.Enabled = true;
                    }

                    if (dataChanged)
                    {
                        MessageBox.Show("Some invoices changed in the AS400 while routing. Please check the data before exporting.");
                        chkVerified.Checked = false;
                        chkVerified.Enabled = true;
                        cmdExportOrders.Enabled = false;
                    }

                    if (notComing.Count > 0)
                    {
                        MessageBox.Show("Invoices in the AS400 for this wave are not coming from Dispatch Track");
                        chkVerified.Checked = false;
                        chkVerified.Enabled = true;
                        cmdExportOrders.Enabled = false;
                    }

                    if (importedNotComing.Count > 0)
                    {
                        MessageBox.Show("Invoices that were imported for this wave are not coming from Dispatch Track");
                        chkVerified.Checked = false;
                        chkVerified.Enabled = true;
                        cmdExportOrders.Enabled = false;
                    }

                    TreeNode currentNode;
                    for (int i = 0; i < RoutesList.Nodes.Count; i++)
                    {
                        currentNode = RoutesList.Nodes[i];
                        currentNode.Checked = true;
                    }
                }
                else if (loadingRoutesSuccess)
                {
                    RoutesList.Nodes.Clear();
                    RoutesList.Nodes.Add("No routes matching current criteria.");
                    chkVerified.Checked = false;
                    chkVerified.Enabled = false;
                    cmdExportOrders.Enabled = false;
                    HideCheckBox(RoutesList.Nodes[0]);
                    txtExportAlert.Visible = false;
                }
                else
                {
                    RoutesList.Nodes.Clear();
                    MessageBox.Show("Error while getting the routing data from dispatch track.");
                    chkVerified.Checked = false;
                    chkVerified.Enabled = false;
                    cmdExportOrders.Enabled = false;
                    txtExportAlert.Visible = false;
                }
                //set previous state
                dtRoutingDate.Enabled = dtRoutingDateState;
                cmdReset.Enabled = cmdResetState;
                cboBuildRoutingWave.Enabled = cboBuildRoutingWaveState;
                opgBuildRoutes.Enabled = opgBuildRoutesState;
                JobProgressBar.Value = 100;
            }
            else
            {
                //MessageBox.Show("Error while getting the routing data from dispatch track.");
                RoutesList.Nodes.Clear();
                //set previous state
                dtRoutingDate.Enabled = dtRoutingDateState;
                cmdReset.Enabled = cmdResetState;
                cboBuildRoutingWave.Enabled = cboBuildRoutingWaveState;
                opgBuildRoutes.Enabled = opgBuildRoutesState;
                chkVerified.Checked = false;
                chkVerified.Enabled = false;
                cmdExportOrders.Enabled = false;
                txtExportAlert.Visible = false;
            }
            ShowLogMessages("Pulling routing data from Dispatch Track process completed" + Environment.NewLine);
        }

        private void RoutesList_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Checked)
            {
                int a = 0;
                a += 1;
            }
            else
            {
                int b = 0;
                b += 1;
            }
            lblNodesSelected.Text = "Selected Routes: "+TreeviewCountCheckedNodes(RoutesList.Nodes).ToString().Trim();
        }

        private void RoutesList_AfterSelect(object sender, TreeViewEventArgs e)
        {
        }

        private int TreeviewCountCheckedNodes(TreeNodeCollection treeNodeCollection)
        {
            int countchecked = 0;
            if (treeNodeCollection != null)
            {
                foreach (TreeNode node in treeNodeCollection)
                {
                    if (node.Parent == null && node.Checked)
                    {
                        countchecked++;
                    }
                }
            }
            return countchecked;
        }

    }

    [Serializable()]
	public class CFRNCustomExceptionDispatchTrack : Exception, ISerializable
	{
		public CFRNCustomExceptionDispatchTrack() : base() { }
		public CFRNCustomExceptionDispatchTrack(string message) : base(message) { }
		public CFRNCustomExceptionDispatchTrack(string message, System.Exception inner) : base(message, inner) { }
		public CFRNCustomExceptionDispatchTrack(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}