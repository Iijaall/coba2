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
    public partial class FrmMasterRoomType : Form
    {
        int id;
        public FrmMasterRoomType()
        {
            InitializeComponent();
        }

        private void FrmMasterRoomType_Load(object sender, EventArgs e)
        {
            awal();
        }
        private void awal()
        {
            id = 0;
            btnInsert.Enabled = true;
            btnSave.Enabled = false;
            btnDelete.Enabled = false;
            btnCancel.Enabled = false;
            btnUpdate.Enabled = false;
            bersih();
            tampil();
            disable();
        }
        private void enable()
        {
            tbcapacity.Enabled = true;
            tbname.Enabled = true;
            tbroomprice.Enabled = true;
        }

        private void disable()
        {
            tbcapacity.Enabled = false;
            tbname.Enabled = false;
            tbroomprice.Enabled = false;
        }

        private void tampil()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                dataGridView1.DataSource = db.RoomTypes.Select(s => new
                {
                    s.ID,
                    s.Name,
                    s.Capacity,
                    Price = s.RoomPrice,
                });
                dataGridView1.Columns["ID"].Visible = false;
            };
        }

        private void bersih()
        {
            tbcapacity.Clear();
            tbname.Clear();
            tbroomprice.Clear();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                DataGridViewRow r = dataGridView1.Rows[e.RowIndex];
                tbcapacity.Text = r.Cells["Capacity"].Value.ToString();
                id = Convert.ToInt32(r.Cells["ID"].Value.ToString());
                tbname.Text = r.Cells["Name"].Value.ToString();
                tbroomprice.Text = r.Cells["Price"].Value.ToString();
            }
            btnInsert.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            btnCancel.Enabled = true;
            btnSave.Enabled = false;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            bersih();
            enable();
            btnSave.Enabled = true;
            btnInsert.Enabled = false;
            btnUpdate.Enabled = false;
            btnCancel.Enabled = true;
            btnDelete.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            enable();
            btnSave.Enabled = true;
            btnInsert.Enabled = false;
            btnUpdate.Enabled = false;
            btnCancel.Enabled = true;
            btnDelete.Enabled = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            using(DataClasses1DataContext db = new DataClasses1DataContext())
            {
                try
                {
                    if (MessageBox.Show("Are you sure want delete this room type?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        RoomType rt = db.RoomTypes.Where(s => s.ID == id).FirstOrDefault();
                        db.RoomTypes.DeleteOnSubmit(rt);
                        db.SubmitChanges();
                        MessageBox.Show("Success Delete Room type!","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
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
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                try
                {
                    if (tbcapacity.Text == "" || tbname.Text == "" || tbroomprice.Text == "")
                    {
                        MessageBox.Show("Your data is not valid,Please try again!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        RoomType rt = db.RoomTypes.Where(s => s.ID == id).FirstOrDefault();
                        RoomType rtn = db.RoomTypes.Where(s => s.Name == tbname.Text).FirstOrDefault();
                        if (id == 0)
                        {
                            if (rtn != null)
                            {
                                MessageBox.Show("Room type Name is already exist!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                db.RoomTypes.InsertOnSubmit(new RoomType
                                {
                                    Name = tbname.Text,
                                    Capacity = Convert.ToInt32(tbcapacity.Text),
                                    RoomPrice = Convert.ToInt32(tbroomprice.Text)
                                });
                                db.SubmitChanges();
                                MessageBox.Show("Success add room type!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                awal();
                            }
                        }
                        else
                        {
                            rt.Name = tbname.Text;
                            rt.Capacity = Convert.ToInt32(tbcapacity.Text);
                            rt.RoomPrice = Convert.ToInt32(tbroomprice.Text);
                            db.SubmitChanges();
                            MessageBox.Show("Success Update room type!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            awal();
                        }
                    }
                }
                catch (Exception o )
                {
                    MessageBox.Show(o.Message, "Please Try Again", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            awal();
        }
    }
}
