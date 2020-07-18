//=================================================//
// Sean Clark
// HomeForm
// Allows client to load which type of game they would like to play or exit the application
//=================================================//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeanClarkAssgt
{
    public partial class HomeForm : Form
    {
        public HomeForm()
        {
            InitializeComponent();
        }

        // Exit application
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Hides current form and loads PvP form
        private void btnPVP_Click(object sender, EventArgs e)
        {
            this.Hide();

            PvPForm.PvPForm pvpForm = new PvPForm.PvPForm();

            pvpForm.ShowDialog();
        }

        // Hides current form and load PvE form
        private void btnPVE_Click(object sender, EventArgs e)
        {
            this.Hide();

            PvEForm.PvEForm pveForm = new PvEForm.PvEForm();

            pveForm.ShowDialog();
        }
    }
}
