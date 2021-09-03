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
    public partial class MainFrm : Form
    {
        Employee emp;
        public static Form frm;
        public MainFrm(Employee emp,Form frm)
        {
            this.emp = emp;
            MainFrm.frm = frm;
            InitializeComponent();
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            lblhello.Text = "Hello, " + emp.Name;
            timer1.Start();
            lblRole.Text = "Role : " + emp.Job.Name;
            if (emp.JobID == 1)
            {
                btn1.Text = "Reservation";
                btn2.Text = "Check In";
                btn3.Text = "Request Additional Item";
                btn4.Text = "Check Out";
                btn5.Text = "Master Room Type";
                btn6.Text = "Master Room";
                btn7.Text = "Master Item";
                btn8.Text = "Logout";
            }
            else if (emp.JobID == 2)
            {
                btn1.Text = "Cleaning Room";
                btn2.Text = "Logout";
                btn3.Visible = false;
                btn4.Visible = false;
                btn5.Visible = false;
                btn6.Visible = false;
                btn7.Visible = false;
                btn8.Visible = false;
            }
            else if (emp.JobID == 3)
            {
                btn1.Text = "Add HouseKeeping Schedule";
                btn2.Text = "Logout";
                btn3.Visible = false;
                btn4.Visible = false;
                btn5.Visible = false;
                btn6.Visible = false;
                btn7.Visible = false;
                btn8.Visible = false;
            }
            else if (emp.JobID == 4)
            {
                btn1.Text = "Master Employee";
                btn2.Text = "Logout";
                btn3.Visible = false;
                btn4.Visible = false;
                btn5.Visible = false;
                btn6.Visible = false;
                btn7.Visible = false;
                btn8.Visible = false;
            }
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnX_Click(object sender, EventArgs e)
        {
            new FrmConfirmationExit(emp,this).ShowDialog();
        }

        private void lbljam_Click(object sender, EventArgs e)
        {
           
            
        }
        private void changepage(Form frm)
        {
            panel5.Controls.Clear();
            frm.TopLevel = false;
            panel5.Controls.Add(frm);
            frm.Show();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            lbljam.Text = DateTime.Now.ToString("dd/MM/yyyy" + " - " + "hh:mm:ss");
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            if (btn1.Text == "Reservation")
            {
                btn1.Text = "Reservation";
                label5.Text = "Reservation";
                changepage(new FrmReservation());
            }
            else if (btn1.Text == "Cleaning Room")
            {
                btn1.Text = "Cleaning Room";
                label5.Text = "Cleaning Room";
                changepage(new FrmCleaningRoom());
            }
            else if (btn1.Text == "Add HouseKeeping Schedule")
            {
                btn1.Text = "Add HouseKeeping Schedule";
                label5.Text = "Add HouseKeeping Schedule";
                changepage(new FrmCleaningRoom());
            }
            else if (btn1.Text == "Master Employee")
            {
                btn1.Text = "Master Employee";
                label5.Text = "Master Employee";
                changepage(new FrmMasterEmployee());
            }
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            if(btn2.Text == "Check In")
            {
                btn2.Text = "Check In";
                label5.Text = "Check In";
                changepage(new FrmCheckIn());
            }
            else if (btn2.Text == "Logout")
            {
                if (MessageBox.Show("Are you sure want logout?","Information",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Hide();
                    new FrmLogin().Show();
                    Class1.Count = 0;
                }
            }
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            if (btn3.Text == "Request Additional Item")
            {
                btn3.Text = "Request Additional Item";
                label5.Text = "Request Additional Item";
                changepage(new FrmRequestAdditionalItem());
            }
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            if (btn4.Text == "Check Out")
            {
                btn4.Text = "Check Out";
                label5.Text = "Check Out";
                changepage(new FrmCheckOut());
            }
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            if (btn5.Text == "Master Room Type")
            {
                btn5.Text = "Master Room Type";
                label5.Text = "Master Room Type";
                changepage(new FrmMasterRoomType());
            }
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            if (btn6.Text == "Master Room")
            {
                btn6.Text = "Master Room";
                label5.Text = "Master Room";
                changepage(new FrmMasterRoom());
            }
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            if (btn7.Text == "Master Item")
            {
                btn7.Text = "Master Item";
                label5.Text = "Master Item";
                changepage(new FrmMasterItem());
            }
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            if (btn8.Text == "Logout")
            {
                if (MessageBox.Show("Are you sure want logout?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Hide();
                    new FrmLogin().Show();
                    Class1.Count = 0;
                }
            }
        }
    }
}
