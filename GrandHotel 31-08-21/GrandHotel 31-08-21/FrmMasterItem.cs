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
    public partial class FrmMasterItem : Form
    {
        int id;
        public FrmMasterItem()
        {
            InitializeComponent();
        }

        private void FrmMasterItem_Load(object sender, EventArgs e)
        {
            awal();
        }

        private void awal()
        {
            id = 0;

            disabled();
            bersih();
            tampil();
        }

        private void bersih()
        {
            tbCompensation.Clear();
            tbname.Clear();
            tbRequestPrice.Clear();
        }

        private void tampil()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                dataGridView1.DataSource = db.Items.Select(s => new
                {
                    s.ID,
                    s.Name,
                    s.RequestPrice,
                    s.CompensationFee
                });
                dataGridView1.Columns["ID"].Visible = false;
                dataGridView1.Columns["RequestPrice"].HeaderText = "Request Price";
                dataGridView1.Columns["CompensationFee"].HeaderText = "Compensation Fee";
            }
        }

        private void Enable()
        {
            tbCompensation.Enabled = true;
            tbname.Enabled = true;
            tbRequestPrice.Enabled = true;
            btnCancel.Enabled = true;
            btnDelete.Enabled = false;
            btnInsert.Enabled = false;
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }
        private void disabled()
        {
            tbCompensation.Enabled = false;
            tbname.Enabled = false;
            tbRequestPrice.Enabled = false;
            btnCancel.Enabled = false;
            btnDelete.Enabled = false;
            btnInsert.Enabled = true;
            btnSave.Enabled = false;
            btnUpdate.Enabled = false;
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                DataGridViewRow r = dataGridView1.Rows[e.RowIndex];
                tbCompensation.Text = r.Cells["CompensationFee"].Value.ToString();
                id = Convert.ToInt32(r.Cells["ID"].Value.ToString());
                tbname.Text = r.Cells["Name"].Value.ToString();
                tbRequestPrice.Text = r.Cells["RequestPrice"].Value.ToString();
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
           Enable();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Enable();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                try
                {
                    if (MessageBox.Show("Are you sure want delete this item?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Item emp = db.Items.Where(s => s.ID == id).FirstOrDefault();
                        db.Items.DeleteOnSubmit(emp);
                        db.SubmitChanges();
                        MessageBox.Show("Success delete Item", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    if (tbRequestPrice.Text == "" || tbname.Text == "" || tbCompensation.Text == "")
                    {
                        MessageBox.Show("Your data is not valid,Please try again!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        
                        Item emp = db.Items.Where(s => s.ID == id).FirstOrDefault();
                        if (id == 0)
                        {
                            if (emp != null)
                            {
                                MessageBox.Show("Id Already exist", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                db.Items.InsertOnSubmit(new Item
                                {
                                   Name = tbname.Text,
                                   RequestPrice = Convert.ToInt32(tbRequestPrice.Text),
                                   CompensationFee = Convert.ToInt32(tbCompensation.Text)
                                });
                                db.SubmitChanges();
                                MessageBox.Show("Success Add Item", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                awal();
                            }
                        }
                        else
                        {
                            emp.Name = tbname.Text;
                            emp.RequestPrice = Convert.ToInt32(tbRequestPrice.Text);
                            emp.CompensationFee = Convert.ToInt32(tbCompensation.Text);
                            db.SubmitChanges();
                            MessageBox.Show("Success Update Item", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
