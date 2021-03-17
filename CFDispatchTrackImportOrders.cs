using System;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Data;
using System.Xml.Linq;
using System.Threading;
using System.Net.Http;
using System.IO;

namespace RoutingWinApp
{
    public enum Warehouse
    {
        TAMARAC = 1,
        Ocoee = 2
    }

    public enum ProcessCode
    {
        EXPORT = 1,
        IMPORT = 2,
        DRIVERS = 3
    }

    public struct Stop
    {
        private int stop;
        private string invoice;
        private string clientName;
        public Stop(int pStop, string pInvoice, string pClientName)
        {
            stop = pStop;
            invoice = pInvoice;
            clientName = pClientName;
        }

        public int StopValue
        {
            get { return stop; }
        }

        public string InvoiceValue
        {
            get { return invoice; }
        }

        public string CustomerName
        {
            get { return clientName; }
        }
    }

    public partial class CFDispatchTrackImportOrders : Form
    {

        public List<int> Selecteditemslist;
        public string StrRoutingZone;
        public DateTime DtSelectedDate = System.DateTime.Today;
        public FormLogger Log;
        public int NProgressBarSteps;

        IEnumerable<DataRow> sessionsValues = new List<DataRow>();

        string importingWave = "";
        string importingWaveDesc = "";
        bool keepOpen = false;

        public XDocument ImportXMLDocument
        {
            get { return ImportXMLDocument; }
            set { ImportXMLDocument = value; }

        }

        public CFDispatchTrackImportOrders()
        {
            InitializeComponent();
            GetDlvSessionsSQL();
        }


        private void cmdImportOrders_click(object sender, EventArgs e)
        {
            try
            {
                if (this.cboRoutingWave.Text.Length > 0)
                {
                    keepOpen = true;
                    importingWave = cboRoutingWave.SelectedValue.ToString();
                    importingWaveDesc = cboRoutingWave.Text.ToString();
                    dtRoutingDate.Enabled = false;
                    cboRoutingWave.Enabled = false;
                    grpBoxFilter.Enabled = false;
                    cmdImportOrders.Enabled = false;
                    btnUpdateDrivers.Enabled = false;
                    cmdReset.Enabled = false;
                    txtProcessLog.Clear();
                    JobProgressBar.Style = ProgressBarStyle.Blocks;
                    bgwImportOrders.WorkerReportsProgress = true;
                    bgwImportOrders.RunWorkerAsync();
                }
                else
                    MessageBox.Show("Please select a Routing Wave from the list.");
            }
            catch (Exception ex)
            {
                keepOpen = false;
            }
        }


        //private string ProcessRequestedWaves(int tnMode)
        //{

        //    DtSelectedDate = dtRoutingDate.Value;

        //    List<WaveFiltersValues> activeFilters = new List<WaveFiltersValues>();
        //    activeFilters.Add(new WaveFiltersValues { FilterName = "NoGeoCodeStops", FilterValue = this.chkNoGeoCodeStops.Checked });
        //    activeFilters.Add(new WaveFiltersValues { FilterName = "NoCondosStops", FilterValue = this.chkNoCondosStops.Checked });
        //    activeFilters.Add(new WaveFiltersValues { FilterName = "NoShowRoomsStops", FilterValue = this.chkNoShowRoomsStops.Checked });
        //    activeFilters.Add(new WaveFiltersValues { FilterName = "NoStopsWBackOrders", FilterValue = this.chkNoStopsWBackOrders.Checked });
        //    activeFilters.Add(new WaveFiltersValues { FilterName = "NotConfirmedStops", FilterValue = this.chkNotConfirmedStops.Checked });

        //    Selecteditemslist = new List<int>();
        //    Selecteditemslist.Add(Convert.ToInt32(cboRoutingWave.SelectedValue.ToString()));
        //    StrRoutingZone = cboRoutingWave.Text.Trim();

        //    Log = new FormLogger(this);
        //    Log.ProcessLog(1,string.Format("Starting pulling '{0}' stops from AS400.", StrRoutingZone));
        //    CFDispatchTrack refRoutingWinApp = new CFDispatchTrack(Log);
        //    XDocument returnedXMLDoc = new XDocument();
        //    int retValue = 0;

        //    returnedXMLDoc = refRoutingWinApp.Build_DT_XML_AS400BookedStopsSingleOrderMode(DtSelectedDate.ToString("MM/dd/yyyy"), StrRoutingZone, activeFilters, Selecteditemslist, "1");
        //    if (returnedXMLDoc != null)
        //    {
        //        if (tnMode == 2)
        //        {
        //            Log.ProcessLog(1, string.Format("Sending Orders to DispatchTrack", StrRoutingZone));
        //            retValue = refRoutingWinApp.SendDataTo_DT_API(tnMode, returnedXMLDoc);
        //            if (retValue > 0)
        //                foreach (XElement order in returnedXMLDoc.Root.Descendants("service_order"))
        //                {
        //                    retValue = refRoutingWinApp.UpdateStagingSQLData(DtSelectedDate.ToString("MM/dd/yyyy"), StrRoutingZone, order.Element("number").Value, order);
        //                }                            
        //                MessageBox.Show("Import process sucessfully completed.", "Process Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //        else
        //        {
        //            foreach (XElement order in returnedXMLDoc.Root.Descendants("service_order"))
        //            {
        //                XDocument XMLSingleDoc = new XDocument();
        //                XMLSingleDoc.Declaration = new XDeclaration("1.0", "UTF-8", null);
        //                XMLSingleDoc.Add(order);
        //                retValue = refRoutingWinApp.SendDataTo_DT_API(tnMode, returnedXMLDoc);
        //                if (retValue == 0)
        //                {
        //                    Log.ProcessLog(1, string.Format("Error sending order '{0}' to DispatchTrack.", order.Element("number").Value));
        //                }
        //            }
        //        }
        //    }
        //    else
        //        MessageBox.Show("Process didn't complete. Please read process log in window.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

        //    //switch (retvalue)
        //    //    {
        //    //        case -1:
        //    //            MessageBox.Show("Process didn't complete. Please read process log in window.", "Warning Message", MessageBoxButtons.OK,MessageBoxIcon.Error);
        //    //            break;
        //    //        case 2:
        //    //            MessageBox.Show("Import Orders process has sucessfully completed. One or more Customers has multiple reservations. Please see process log for more information.", "Process Message", MessageBoxButtons.OK,MessageBoxIcon.Warning);
        //    //            break;
        //    //        default:
        //    //            MessageBox.Show("Import Orders process has sucessfully completed.", "Process Message", MessageBoxButtons.OK,MessageBoxIcon.Information);
        //    //            break;
        //    //    }

        //    return "";
        //}


        private void GetDlvSessionsSQL()
        {
            DataAccess roadNetWaves = new DataAccess();
            var dlvWaveInstance = roadNetWaves.GetSessionsMasterByDate(DtSelectedDate);

            sessionsValues = dlvWaveInstance.AsEnumerable();

            this.cboRoutingWave.DataSource = dlvWaveInstance;
            this.cboRoutingWave.DisplayMember = "SessionName";
            this.cboRoutingWave.ValueMember = "sessionid_pk";
            this.cboRoutingWave.SelectedIndex = -1;
            this.txtProcessLog.Clear();
            this.JobProgressBar.Value = 1;
            this.Refresh();
        }

        private void GetDlvWaveAS400()
        {

            //var DLVWaveInstance = new IEnumerable<AS400DLVWave>();

            DataAccess roadNetWaves = new DataAccess();
            var dlvWaveInstance = roadNetWaves.GetDLVWavefromAS400(true, DtSelectedDate);


            cboRoutingWave.DataSource = dlvWaveInstance;
            this.cboRoutingWave.DisplayMember = "WVName";
            this.cboRoutingWave.ValueMember = "WVWave";
            this.cboRoutingWave.SelectedIndex = -1;
        }


        private void GetSqlData()
        {
            DataAccess roadNetWaves = new DataAccess();
            SqlDataReader reader;
            reader = roadNetWaves.GetRoutingWaves(DtSelectedDate);


            this.cboRoutingWave.DataSource = reader;
            this.cboRoutingWave.DisplayMember = "WVName";
            this.cboRoutingWave.ValueMember = "WVWave";
            this.cboRoutingWave.SelectedIndex = -1;

        }

        private void dtRoutingDate_ValueChanged(object sender, EventArgs e)
        {
            DtSelectedDate = this.dtRoutingDate.Value;
            GetDlvSessionsSQL();
        }

        //public void UpdateProcessLog(string text)
        //{
        //    if (InvokeRequired)
        //    {
        //        this.Invoke((MethodInvoker)delegate() { UpdateProcessLog(text); });
        //        return;
        //    }
        //    this.txtProcessLog.Text = text;
        //}

        //private void cmdGetWaveInfo_Click(object sender, EventArgs e)
        //{
        //    var lbValidate = false;

        //    if (this.cboRoutingWave.Text.Length > 0)
        //    {
        //        if (DtSelectedDate < DateTime.Today)
        //        {
        //            DialogResult lcValidationResult = MessageBox.Show("Are you sure you want to request AS400 orders for a date prior current date?", "Action Confirmation Request", MessageBoxButtons.YesNo);
        //            if (lcValidationResult == DialogResult.Yes)
        //                lbValidate = true;
        //        }
        //        else
        //            lbValidate = true;
        //        if (lbValidate == true)
        //        {

        //            this.Refresh();

        //            NProgressBarSteps = 5;
        //            JobProgressBar.Minimum = 1;
        //            JobProgressBar.Maximum = 100;
        //            JobProgressBar.Step = (int)100 / NProgressBarSteps;
        //            JobProgressBar.Value = 1;
        //            JobProgressBar.Style = ProgressBarStyle.Blocks;
        //            cmdImportOrders.Enabled = false;
        //            txtProcessLog.Clear();
        //            Cursor.Current = Cursors.WaitCursor;
        //            ProcessRequestedWaves(0);
        //            JobProgressBar.Value = 100;
        //            Cursor.Current = Cursors.Default;
        //            this.cmdImportOrders.Enabled = true;
        //        }
        //    }
        //    else
        //        MessageBox.Show("Please select a Routing Wave from the list.");
        //}

        private void cboRoutingWave_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView dtRow = (DataRowView)this.cboRoutingWave.SelectedItem;
            if (dtRow != (null))
            {
                this.chkNoRegularInvoices.Enabled = true;
                this.chkNoRegularInvoices.Checked = false;
                this.btnUpdateDrivers.Enabled = false;
            }
            else
            {
                this.chkNoRegularInvoices.Checked = false;
                this.btnUpdateDrivers.Enabled = true;

            }
            this.Refresh();

        }

        private void btnUpdateDrivers_Click(object sender, EventArgs e)
        {
            keepOpen = true;
            dtRoutingDate.Enabled = false;
            cboRoutingWave.Enabled = false;
            grpBoxFilter.Enabled = false;
            cmdImportOrders.Enabled = false;
            btnUpdateDrivers.Enabled = false;
            cmdReset.Enabled = false;
            txtProcessLog.Clear();
            JobProgressBar.Style = ProgressBarStyle.Blocks;
            bgwUpdateDTDrivers.WorkerReportsProgress = true;
            bgwUpdateDTDrivers.RunWorkerAsync();
        }

        private void bgwImportOrders_DoWork(object sender, DoWorkEventArgs e)
        {
            var lbValidate = false;
            var tokenSource = new CancellationTokenSource();
            if (DtSelectedDate < DateTime.Today)
            {
                DialogResult lcValidationResult = MessageBox.Show("Are you sure you want to request AS400 orders for a date prior current date?", "Action Confirmation Request", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification);
                if (lcValidationResult == DialogResult.Yes)
                    lbValidate = true;
            }
            else
                lbValidate = true;

            DataAccess dataAccess = new DataAccess();

            try
            {
                bool stopExport;
                if (dataAccess.CheckWaveExportedtoAS400Already(DtSelectedDate, importingWaveDesc) == "BAD")
                {
                    DialogResult lcValidationRes = MessageBox.Show("WARNING - WARNING - WARNING\n\nThe orders for the selected Date and Wave have been exported to the AS400 already.\n\nContinuing the process will add - change - delete data in/from Dispatch Track that is not included in this batch.\n\nAre you sure you want to procedd?\n\nPress 'Yes' to continue if you are sure that is correct, 'No' to cancel export process.", "WARNING - Action Confirmation Request", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification);
                    stopExport = (lcValidationRes != DialogResult.Yes);
                    if (stopExport)
                        lbValidate = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                lbValidate = false;
            }

            if (lbValidate == true)
            { 
                try
                {
                    bool stopExport;
                    if (dataAccess.CheckLockStatus(DtSelectedDate, importingWave))
                    {
                        DialogResult lcValidationRes = MessageBox.Show("Orders for selected wave/date has been exported to Dispatch Track before.\\nPlease make sure routes and time windows are locked in Dispatch Track before exporting again.\\nPress 'Yes' to continue if routes are locked, 'No' to cancel export process and check out routes in Dispatch Track.", "Action Confirmation Request", MessageBoxButtons.YesNo);
                        stopExport = (lcValidationRes != DialogResult.Yes);
                        if (stopExport)
                            lbValidate = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    lbValidate = false;
                }
            }

            if (lbValidate == true)
            {
                try
                {
                    string processCode = ProcessCode.IMPORT.ToString();
                    int currentProcessId;
                    if (dataAccess.CreateProcess(processCode, out currentProcessId))
                    {
                        var token = tokenSource.Token;
                        RunProcessLogs(token, processCode, currentProcessId);

                        bgwImportOrders.ReportProgress(20);
                        cmdImportOrders.Enabled = false;
                        Cursor.Current = Cursors.WaitCursor;

                        CFDispatchTrackApplicationSettings loAppSettings = new CFDispatchTrackApplicationSettings();
                        string apiHost = loAppSettings.Get("DispatchTrackAPIHost");
                        string apiPort = loAppSettings.Get("DispatchTrackAPIPort");
                        string apiImportOrders = "";
                        string url = "";
                        string date = DtSelectedDate.ToString("yyyyMMdd");
                        string warehouseId = int.Parse(sessionsValues.First(cdr => cdr["sessionid_pk"].ToString() == importingWave)["cfwh_id"].ToString()).ToString();
                        //string warehouseId = ((int)Enum.Parse(typeof(Warehouse), Warehouse.TAMARAC.ToString())).ToString();
                        apiImportOrders = loAppSettings.Get("DispatchTrackImportOrders");
                        //if (this.chkNoRegularInvoices.Checked == false)
                        //    url = apiHost + ":" + apiPort + apiImportOrders + warehouseId + "/" + date + "/" + importingWave + "/ALL/" + currentProcessId;
                        //else
                        //    url = apiHost + ":" + apiPort + apiImportOrders + warehouseId + "/" + date + "/" + importingWave + "/NRI/" + currentProcessId ;
                        if(chkNoRegularInvoices.Checked)
                            url = apiHost + (apiPort.Trim().Length > 0 ? ":" + apiPort : "" ) + apiImportOrders + warehouseId + "/" + date + "/" + importingWave + "/NRI/" + currentProcessId;
                        else if(chkOnlyTransfers.Checked)
                            url = apiHost + (apiPort.Trim().Length > 0 ? ":" + apiPort : "") + apiImportOrders + warehouseId + "/" + date + "/" + importingWave + "/TRA/" + currentProcessId;
                        else
                            url = apiHost + (apiPort.Trim().Length > 0 ? ":" + apiPort : "") + apiImportOrders + warehouseId + "/" + date + "/" + importingWave + "/ALL/" + currentProcessId;

                        bgwImportOrders.ReportProgress(25);

                        HttpResponseMessage aSyncResponse;

                        bool res = NodeAPI.UploadOrdersData(url, out aSyncResponse);

                        Cursor.Current = Cursors.Default;

                        if (!res)
                        {
                            MessageBox.Show("Unsuccessful call to node.js api. Reason Phrase: " + aSyncResponse.ReasonPhrase);
                        }
                        else
                        {
                            bgwImportOrders.ReportProgress(100);
                            MessageBox.Show("Orders successfully imported.");
                        }
                    }
                    else
                        MessageBox.Show("Error while creating the process.");

                }
                catch (Exception error)
                {
                    MessageBox.Show(error.ToString());
                }
            }
            Thread.Sleep(1000);
            tokenSource.Cancel();  //this change the value of token.IsCancellationRequested from 'false' to 'true', stopping the while loop in method ProcessLogs
        }

        private void bgwImportOrders_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            JobProgressBar.Value = e.ProgressPercentage;
        }

        private void bgwImportOrders_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            JobProgressBar.Value = 100;
            dtRoutingDate.Enabled = true;
            cboRoutingWave.Enabled = true;
            grpBoxFilter.Enabled = true;
            cmdReset.Enabled = true;
            cmdImportOrders.Enabled = true;
            btnUpdateDrivers.Enabled = true;

            keepOpen = false;
        }

        delegate void ShowLogMessagesDelegate(string text);

        private async void RunProcessLogs(CancellationToken ct, string processCode, int processId)
        {
            var progress = new Progress<Tuple<string, bool>>(t => ShowLogMessages(t.Item1));
            await Task.Factory.StartNew(() => ProcessLogs(progress, ct, processCode, processId), ct);
        }

        private void ProcessLogs(IProgress<Tuple<string, bool>> progress, CancellationToken ct, string processCode, int processId)
        {
            try
            {
                progress.Report(new Tuple<string, bool>("", true));
                List<Tuple<int, string>> currentLogs = new List<Tuple<int, string>>();
                int lastLog = 0;
                DataAccess da = new DataAccess();
                //string processCode = ProcessCode.IMPORT.ToString();
                //int processId = da.GetRunningProcess(processCode);
                string currentLog = "";
                if (processId >= 0)
                {
                    while (!ct.IsCancellationRequested)
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


                //txtProcessLog.Text = txtProcessLog.Text.Insert(0, message);

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

        private void CFDispatchTrackImportOrders_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (keepOpen)
                e.Cancel = true;
        }

        private void bgwUpdateDTDrivers_DoWork(object sender, DoWorkEventArgs e)
        {
            var lbValidate = false;
            var tokenSource = new CancellationTokenSource();
            try
            {
                if (DtSelectedDate < DateTime.Today)
                {
                    DialogResult lcValidationResult = MessageBox.Show("Are you sure you want to update drivers for a date prior the current date?\r\nPlease press YES to continue.", "Action Confirmation Request", MessageBoxButtons.YesNo);
                    if (lcValidationResult == DialogResult.Yes)
                        lbValidate = true;
                }
                else
                {
                    DialogResult lcValidationResult = MessageBox.Show("Drivers information will be send to Dispatch Track for " + DtSelectedDate.ToString("MM/dd/yyyy") + ".\r\nPlease press YES to continue.", "Action Confirmation Request", MessageBoxButtons.YesNo);
                    if (lcValidationResult == DialogResult.Yes)
                        lbValidate = true;
                }
                if (lbValidate == true)
                {
                    DataAccess dataAccess = new DataAccess();
                    string processCode = ProcessCode.DRIVERS.ToString();
                    int currentProcessId;
                    if (dataAccess.CreateProcess(processCode, out currentProcessId))
                    {
                        var token = tokenSource.Token;
                        RunProcessLogs(token, processCode, currentProcessId);

                        CFDispatchTrackApplicationSettings loAppSettings = new CFDispatchTrackApplicationSettings();
                        string apiHost = loAppSettings.Get("DispatchTrackAPIHost");
                        string apiPort = loAppSettings.Get("DispatchTrackAPIPort");
                        string apiImportDrivers = loAppSettings.Get("DispatchTrackUpdateDrivers");
                        string requestDate = DtSelectedDate.ToString("yyyyMMdd");
                        string url = apiHost + (apiPort.Trim().Length > 0 ? ":" + apiPort : "") + apiImportDrivers + requestDate + "/" + currentProcessId;

                        NodeAPI.UpdateDriversInformation(url);

                        MessageBox.Show("Update Drivers process complete.");

                    }
                    else
                        MessageBox.Show("Error while creating the process.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            Thread.Sleep(1000);
            tokenSource.Cancel();  //this change the value of token.IsCancellationRequested from 'false' to 'true', stopping the while loop in method ProcessLogs
        }

        private void bgwUpdateDTDrivers_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            JobProgressBar.Value = e.ProgressPercentage;
        }

        private void bgwUpdateDTDrivers_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            JobProgressBar.Value = 100;
            dtRoutingDate.Enabled = true;
            cboRoutingWave.Enabled = true;
            grpBoxFilter.Enabled = true;
            cmdReset.Enabled = true;
            cmdImportOrders.Enabled = true;
            btnUpdateDrivers.Enabled = true;

            keepOpen = false;
        }

        private void cmdReset_Click(object sender, EventArgs e)
        {
            DtSelectedDate = System.DateTime.Now;
            dtRoutingDate.Value = DtSelectedDate;
            dtRoutingDate.Enabled = true;
            cboRoutingWave.Enabled = true;
            cmdReset.Enabled = true;
            grpBoxFilter.Enabled = true;
            cmdImportOrders.Enabled = true;
            btnUpdateDrivers.Enabled = true;
            txtProcessLog.Clear();
            JobProgressBar.Style = ProgressBarStyle.Blocks;
            if (chkOnlyTransfers.Checked)
                chkOnlyTransfers.Checked = false;
            if (chkNoRegularInvoices.Checked)
                chkNoRegularInvoices.Checked = false;
        }

        private void CFDispatchTrackImportOrders_Load(object sender, EventArgs e)
        {

        }

        private void chkNoRegularInvoices_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOnlyTransfers.Checked && chkNoRegularInvoices.Checked)
                chkOnlyTransfers.Checked = false;
        }

        private void chkOnlyTransfers_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNoRegularInvoices.Checked && chkOnlyTransfers.Checked)
                chkNoRegularInvoices.Checked = false;
        }
    }
}