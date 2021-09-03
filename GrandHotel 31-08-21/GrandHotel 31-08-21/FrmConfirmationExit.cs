using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrandHotel_31_08_21
{
    public partial class FrmConfirmationExit : Form
    {
        Employee emp;
        Form frm;
        public FrmConfirmationExit(Employee emp,Form frm)
        {
            this.emp = emp;
            this.frm = frm;
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (rbtnLogoff.Checked)
            {
                this.Close();
                frm.Close();
                MainFrm.frm.Close();
                Class1.Count = 0;
            }
            else if (rbtnQuit.Checked)
            {
                Application.Exit();
            }
            else if (rbtnRestart.Checked)
            {
                Application.Restart();
            }
        }

        private void FrmConfirmationExit_Load(object sender, EventArgs e)
        {

            if (Class1.Count != 0)
            {
                rbtnLogoff.Text = "Log Off " + emp.Name;
            }
            else
            {
                rbtnLogoff.Visible = false;
            }
        }
    }
}
