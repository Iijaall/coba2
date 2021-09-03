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
    public partial class FrmMasterEmployee : Form
    {
        int id;
        public FrmMasterEmployee()
        {
            InitializeComponent();
        }

        private void FrmMasterEmployee_Load(object sender, EventArgs e)
        {
            using (DataClasses1DataContext db=new DataClasses1DataContext())
            {
                awal();
                cbxJob.DataSource = db.Jobs.Select(s => s.Name);
            }
          
        }

        private void bersih()
        {
            tbAddress.Clear();
            tbConfPass.Clear();
            tbEmail.Clear();
            tbname.Clear();
            tbPassword.Clear();
            tbUsername.Clear();
            cbxJob.SelectedItem = null;
        }

        private void disable()
        {
            tbAddress.Enabled = false;
            tbConfPass.Enabled = false;
            tbEmail.Enabled = false;
            tbname.Enabled = false;
            tbPassword.Enabled = false;
            tbUsername.Enabled = false;
            cbxJob.Enabled = false;
            btnCancel.Enabled = false;
            btnDelete.Enabled = false;
            btnInsert.Enabled = true;
            btnSave.Enabled = false;
            btnUpdate.Enabled = false;
        }
        private void enable()
        {
            tbAddress.Enabled = true;
            tbConfPass.Enabled =true;
            tbEmail.Enabled = true  ;
            tbname.Enabled = true   ;
            tbPassword.Enabled =true;
            tbUsername.Enabled =true;
            cbxJob.Enabled = true   ;
            btnCancel.Enabled = true;
            btnDelete.Enabled = false;
            btnInsert.Enabled = false;
            btnSave.Enabled = true  ;
            btnUpdate.Enabled = false;
        }

        private void awal()
        {
            id = 0;
            disable();
            bersih();
            tampil();
        }

        private void tampil()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                dataGridView1.DataSource = db.Employees.Select(s => new
                {
                    s.ID,
                    s.Username,
                    s.Name,
                    s.Email,
                    s.DateOfBirth,
                   Job =  s.Job.Name,
                    s.Address,
                });
                dataGridView1.Columns["ID"].Visible = false;
                dataGridView1.Columns["DateOfBirth"].HeaderText = "Date Of Birth";
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                DataGridViewRow r = dataGridView1.Rows[e.RowIndex];
                tbAddress.Text = r.Cells["Address"].Value.ToString();
                id = Convert.ToInt32(r.Cells["ID"].Value.ToString());
                tbEmail.Text = r.Cells["Email"].Value.ToString();
                tbname.Text = r.Cells["Name"].Value.ToString();
                tbUsername.Text = r.Cells["Username"].Value.ToString();
            }
            btnUpdate.Enabled = true;
            btnInsert.Enabled = false;
            btnCancel.Enabled = true;
            btnDelete.Enabled = true;
            btnSave.Enabled = true;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            bersih();
            enable();
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            enable();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                try
                {
                     if (MessageBox.Show("Are you sure want delete this employee?","Information",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                     {
                            Employee emp = db.Employees.Where(s => s.ID == id).FirstOrDefault();
                            db.Employees.DeleteOnSubmit(emp);
                            db.SubmitChanges();
                            MessageBox.Show("Success delete employee","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                            awal();
                     }
                }
                catch (Exception o)
                {

                    MessageBox.Show(o.Message, "Please Try Again", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    if (tbAddress.Text == "" || tbConfPass.Text == "" || tbEmail.Text == "" || tbname.Text == "" || tbPassword.Text == "" || tbUsername.Text == "" || cbxJob.SelectedItem == null )
                    {
                        MessageBox.Show("Your data is not valid,Please try again!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if(!tbPassword.Text.Equals(tbConfPass.Text))
                    {
                        MessageBox.Show("Password and confirm pass must be same!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (!Class1.RegexEmail.IsMatch(tbEmail.Text))
                    {
                        MessageBox.Show("your email is not valid!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (tbname.Text.Length > 20 || tbname.Text.Length < 5)
                    {
                        MessageBox.Show("Name must 5 - 20 Characters !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        List<Job> lj = db.Jobs.Select(s => s).ToList();
                        Employee emp = db.Employees.Where(s => s.ID == id).FirstOrDefault();
                        if (id == 0)
                        {
                            if (emp != null)
                            {
                                MessageBox.Show("Id Already exist","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                            }
                            else
                            {
                                db.Employees.InsertOnSubmit(new Employee
                                {
                                    Address =tbAddress.Text,
                                    Username = tbUsername.Text,
                                    Name = tbname.Text,
                                    Email = tbEmail.Text,
                                    DateOfBirth = dateTimePicker1.Value,
                                    JobID = lj[cbxJob.SelectedIndex].ID,
                                    Password = Class1.SHA256(tbPassword.Text),
                                    
                                });
                                db.SubmitChanges();
                                MessageBox.Show("Success Add Employee","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                                awal();
                            }
                        }
                        else
                        {
                            emp.Address = tbAddress.Text;
                            emp.Username = tbUsername.Text;
                            emp.Name = tbname.Text;
                            emp.Email = tbEmail.Text;
                            emp.DateOfBirth = dateTimePicker1.Value;
                            emp.JobID = lj[cbxJob.SelectedIndex].ID;
                            emp.Password = Class1.SHA256(tbPassword.Text);
                            db.SubmitChanges();
                            MessageBox.Show("Success Update Employee", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            awal();
                        }
                        
                    }
                }
            }
            catch (Exception o)
            {

                MessageBox.Show(o.Message, "Please Try Again", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            awal();
        }
    }
}
