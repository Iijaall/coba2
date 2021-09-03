using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrandHotel_31_08_21
{
    public partial class FrmLogin : Form
    {
        Employee emp;
        int count;
        int countdown = 10;
        public FrmLogin()
        {
            Thread t = new Thread(new ThreadStart(Start));
            t.Start();
            Thread.Sleep(3000);
            t.Abort();
            InitializeComponent();
        }
        System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
        private void FrmLogin_Load(object sender, EventArgs e)
        {
            lblCountt.Visible = false;
            tbpass.Text = "q";
            this.Activate();
        }
        private void Start()
        {
            Application.Run(new Splash());
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnX_Click(object sender, EventArgs e)
        {
            new FrmConfirmationExit(emp,this).ShowDialog();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                if (tbpass.Text == "" || tbuser.Text == "")
                {
                    MessageBox.Show("Your data is not valid,Please try again!","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    emp = db.Employees.Where(s => s.Username == tbuser.Text && s.Password == Class1.SHA256(tbpass.Text)).FirstOrDefault();
                    if (emp != null)
                    {
                        if (emp.Username == tbuser.Text && emp.Password == Class1.SHA256((tbpass.Text)))
                        {
                            this.Hide();
                            new MainFrm(emp,this).Show();
                            Class1.Count = 1;
                        }
                        else
                        {
                            MessageBox.Show("Your data is not valid,Please try again!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        count += 1;
                        if (count > 3 )
                        {
                            MessageBox.Show("Please wait until 10 seconds", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            tbpass.Enabled = false;
                            tbuser.Enabled = false;
                            lblCountt.Visible = true;
                            btnLogin.Enabled = false;
                            t = new System.Windows.Forms.Timer();
                            t.Interval = 1000;
                            t.Tick += timer1_Tick;
                            t.Start();
                            lblCountt.Text = "Please Wait for " + countdown + " Seconds";
                        }
                        else
                        {
                            MessageBox.Show("Your data is not valid,Please try again!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                    }
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            new FrmConfirmationExit(emp,this).ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            countdown--;
            if (countdown == 0)
            {
                tbpass.Enabled = true;
                tbuser.Enabled = true;
                lblCountt.Visible = false;
                btnLogin.Enabled = true;
                count = 0;
                countdown = 10;
                t.Stop();
            }
            lblCountt.Text = "Please Wait for " + countdown + " Seconds";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            tbpass.UseSystemPasswordChar = !checkBox1.Checked;
        }
    }
}
