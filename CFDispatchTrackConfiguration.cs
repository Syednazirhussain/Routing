using System;
using System.Linq;
using System.Windows.Forms;
using RoutingWinApp;

namespace RoutingWinApp
{
    public partial class CFDispatchTrackConfiguration : Form
    {
        CFDispatchTrackApplicationSettings loAppSettings = new CFDispatchTrackApplicationSettings();

        public CFDispatchTrackConfiguration()
        {
            InitializeComponent();
            GetValues();
        }

        public void GetValues()
        {
            this.txtEmailFrom.Text = loAppSettings.Get("NotificationEmailAddressFrom");
            this.txtEmailTo.Text = loAppSettings.Get("NotificationEmailAddressTo");
            this.txtDispatchTrackURL.Text = loAppSettings.Get("DispatchTrackAPIHost");
            this.txtDispatchTrackPort.Text = loAppSettings.Get("DispatchTrackAPIPort");
            this.txtDispatchTrackImportOrder.Text = loAppSettings.Get("DispatchTrackImportOrders");
            this.txtDispatchTrackExportOrder.Text = loAppSettings.Get("DispatchTrackExportOrders");
            this.txtDispatchTrackGetOrderInfo.Text = loAppSettings.Get("DispatchTrackGetOrdersInfo");
            this.txtDispatchTrackUpdateDriver.Text = loAppSettings.Get("DispatchTrackUpdateDrivers");
            this.txtDispatchTrackUpdateRouteHist.Text = loAppSettings.Get("DispatchTrackUpdateRouteOHist");
            this.txtDispatchTrackDateBeforeExport.Text = loAppSettings.Get("DispatchTrackCheckDataBeforeExporting");
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            loAppSettings.Set("NotificationEmailAddressFrom", this.txtEmailFrom.Text.Trim());
            loAppSettings.Set("NotificationEmailAddressTo", this.txtEmailTo.Text.Trim());
            loAppSettings.Set("DispatchTrackAPIHost", this.txtDispatchTrackURL.Text.Trim());
            loAppSettings.Set("DispatchTrackAPIPort", this.txtDispatchTrackPort.Text.Trim());
            loAppSettings.Set("DispatchTrackImportOrders", this.txtDispatchTrackImportOrder.Text.Trim());
            loAppSettings.Set("DispatchTrackExportOrders", this.txtDispatchTrackExportOrder.Text.Trim());
            loAppSettings.Set("DispatchTrackGetOrdersInfo", this.txtDispatchTrackGetOrderInfo.Text.Trim());
            loAppSettings.Set("DispatchTrackUpdateDrivers", this.txtDispatchTrackUpdateDriver.Text.Trim());
            loAppSettings.Set("DispatchTrackUpdateRouteOHist", this.txtDispatchTrackUpdateRouteHist.Text.Trim());
            loAppSettings.Set("DispatchTrackCheckDataBeforeExporting", this.txtDispatchTrackDateBeforeExport.Text.Trim());
            this.Close();
        }
    }

}
