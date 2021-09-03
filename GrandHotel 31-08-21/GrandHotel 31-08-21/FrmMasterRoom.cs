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
    public partial class FrmMasterRoom : Form
    {
        int id;
        public FrmMasterRoom()
        {
            InitializeComponent();
        }

        private void FrmMasterRoom_Load(object sender, EventArgs e)
        {
            awal();
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                cbxRoomType.DataSource = db.RoomTypes.Select(s => s.Name);

            }
        }

        private void awal()
        {
            id = 0;
            btnInsert.Enabled = true;
            btnCancel.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = false;
            btnUpdate.Enabled = false;
            bersih();
            tampil();
            disable();
        }

        private void disable()
        {
            tbDescription.Enabled = false;
            tbRoomFloor.Enabled = false;
            tbRoomNumber.Enabled = false;
            cbxRoomType.Enabled = false;
        }
        private void enable()
        {
            tbDescription.Enabled = true;
            tbRoomFloor.Enabled = true;
            tbRoomNumber.Enabled = true;
            cbxRoomType.Enabled = false;
        }

        private void tampil()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
               dataGridView1.DataSource = db.Rooms.Select(s => new
               {
                   s.ID,
                   s.RoomNumber,
                   s.RoomType.Name,
                   s.RoomFloor,
                   s.Description
               });
                dataGridView1.Columns["ID"].Visible = false;
            }
        }

        private void bersih()
        {
            tbDescription.Clear();
            tbRoomFloor.Clear();
            tbRoomNumber.Clear();
            cbxRoomType.SelectedItem = null;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                DataGridViewRow r = dataGridView1.Rows[e.RowIndex];
                tbDescription.Text = r.Cells["Description"].Value?.ToString();
                id = Convert.ToInt32(r.Cells["ID"].Value.ToString());
                cbxRoomType.SelectedItem = r.Cells["RoomType"].Value.ToString();
                tbRoomFloor.Text = r.Cells["RoomFloor"].Value.ToString();
                tbRoomNumber.Text = r.Cells["RoomNumber"].Value.ToString();
            }
            btnInsert.Enabled = false;
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            btnCancel.Enabled = true;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            bersih();
            enable();
            btnCancel.Enabled = true;
            btnSave.Enabled = true;
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            btnInsert.Enabled = false;
            cbxRoomType.Enabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            enable();
            btnCancel.Enabled = true;
            btnSave.Enabled = true;
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            btnInsert.Enabled = false;
            cbxRoomType.Enabled = true;

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    if (MessageBox.Show("Are you sure want delete this room?","Information",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Room r = db.Rooms.Where(s => s.ID == id).FirstOrDefault();
                        db.Rooms.DeleteOnSubmit(r);
                        db.SubmitChanges();
                        MessageBox.Show("Success Delete room!","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        awal();
                    }
                }
            }
            catch (Exception o )
            {

                MessageBox.Show(o.Message, "Please Try Again", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                try
                {
                    if (tbDescription.Text == "" || tbRoomFloor.Text == "" || tbRoomNumber.Text == ""|| cbxRoomType.SelectedItem == null)
                    {
                        MessageBox.Show("Your data is not valid,Please try again!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        List<RoomType> rtl = db.RoomTypes.Select(s => s).ToList();
                        Room rt = db.Rooms.Where(s => s.ID == id).FirstOrDefault();
                        Room rtn = db.Rooms.Where(s => s.RoomNumber == Convert.ToInt32(tbRoomNumber.Text)).FirstOrDefault();
                        if (id == 0)
                        {
                            if (rtn != null)
                            {
                                MessageBox.Show("Room Number is already exist!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                db.Rooms.InsertOnSubmit(new Room
                                {
                                    RoomNumber = Convert.ToInt32(tbRoomNumber.Text),
                                    RoomTypeID = rtl[cbxRoomType.SelectedIndex].ID,
                                    RoomFloor = Convert.ToInt32(tbRoomFloor.Text),
                                    Description = tbDescription.Text
                                });
                                db.SubmitChanges();
                                MessageBox.Show("Success add room!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                awal();
                            }
                        }
                        else
                        {
                            rt.RoomFloor = Convert.ToInt32(tbRoomFloor.Text);
                            rt.RoomNumber = Convert.ToInt32(tbRoomNumber.Text);
                            rt.RoomTypeID = rtl[cbxRoomType.SelectedIndex].ID;
                            rt.Description = tbDescription.Text;
                            db.SubmitChanges();
                            MessageBox.Show("Success Update room!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            awal();
                        }
                    }
                }
                catch (Exception o)
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
