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
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            loAppSettings.Set("NotificationEmailAddressFrom", this.txtEmailFrom.Text.Trim());
            loAppSettings.Set("NotificationEmailAddressTo", this.txtEmailTo.Text.Trim());
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }
    }

}
