using System;
using System.Linq;
using System.Windows.Forms;


namespace RoutingWinApp
{
	public partial class CFRoutingWinAppMainToolBar: Form
	{
		public CFRoutingWinAppMainToolBar()
		{
			InitializeComponent();
            this.Text = Application.ProductName + " - v" + Application.ProductVersion;
		}

        private void cmdSettings_Click(object sender, EventArgs e)
        {
            CFDispatchTrackConfiguration CFAppSettings = new CFDispatchTrackConfiguration();
            CFAppSettings.ShowDialog();
        }

        private void cmdExportOrders_Click(object sender, EventArgs e)
        {
            CFDispatchTrackExportOrders CFMainForm = new CFDispatchTrackExportOrders();
            CFMainForm.ShowDialog();
        }

        private void cmdImportOrdersDispatchTrack_Click(object sender, EventArgs e)
        {
            CFDispatchTrackImportOrders CFMainForm = new CFDispatchTrackImportOrders();
            CFMainForm.ShowDialog();
        }

        private void cmdExportOrdersDispatchTrack_Click(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift)
            {
                CFDispatchTrackExportOrders CFMainForm = new CFDispatchTrackExportOrders();
                CFMainForm.ShowDialog();
            }
        }

        private void cmdImportOrders_Click(object sender, EventArgs e)
        {
            CFDispatchTrackImportOrders CFMainForm = new CFDispatchTrackImportOrders();
            CFMainForm.ShowDialog();
        }

        private void cmdUpdateRouteInformation_Click(object sender, EventArgs e)
        {

        }
    }
}
