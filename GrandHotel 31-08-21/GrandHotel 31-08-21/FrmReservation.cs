using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrandHotel_31_08_21
{
    public partial class FrmReservation : Form
    {
        List<selectroom> listselect = new List<selectroom>();
        List<reqitem> listreqitem = new List<reqitem>();
        reqitem  selectItem; 
        selectroom  selected;
        int id;
        int Roomnumber;
        int roomfloor;
        string description;
        int roomprice;
        public FrmReservation()
        {
            InitializeComponent();
        }

        private void FrmReservation_Load(object sender, EventArgs e)
        {
            awal();
        }

        private void awal()
        {
            tampil();
            panel1.Visible = false;
            dataGridView3.Visible = false;
        }

        private void tampil()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                cbxItem.DataSource = db.Items.Select(s => s.Name);
                cbxRoomType.DataSource = db.RoomTypes.Select(s => s.Name);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                if (cbxRoomType.SelectedItem == null)
                {
                    MessageBox.Show("Please insert Room Type!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    dataGridView1.DataSource = from a in db.Rooms
                                               join b in db.RoomTypes
                                               on a.RoomTypeID equals b.ID
                                               where b.Name == cbxRoomType.Text
                                               select new
                                               {
                                                   a.ID,
                                                   a.RoomNumber,
                                                   a.RoomFloor,
                                                   a.Description,
                                                   b.RoomPrice
                                               };
                    dataGridView1.Columns["ID"].Visible = false;
                    dataGridView1.Columns["RoomPrice"].Visible = false;
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                DataGridViewRow r = dataGridView1.CurrentRow;
                id = Convert.ToInt32(r.Cells["ID"].Value.ToString());
                Roomnumber = Convert.ToInt32(r.Cells["RoomNumber"].Value.ToString());
                roomfloor = Convert.ToInt32(r.Cells["RoomFloor"].Value.ToString());
                roomprice = Convert.ToInt32(r.Cells["RoomPrice"].Value.ToString());
            }
        }
        private void refreshdg()
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = listselect;
            dataGridView2.DataSource = bs;
            if (listselect.Count != 0)
            {
                dataGridView2.Columns["ID"].Visible = false;
                dataGridView2.Columns["DurationNight"].Visible = false;
                dataGridView2.Columns["RoomPrice"].Visible = false;
                dataGridView2.Columns["Total"].Visible = true;
            }
            else
            {
                dataGridView2.ColumnHeadersVisible = false;
                dataGridView2.DataSource = null;
                lblTotalPrice.Text = "Total Price : Rp: 0,00";
            }
            int total = 0;
            foreach (selectroom s in listselect)
            {
                total += s.Total;
            }

            BindingSource bs2 = new BindingSource();
            bs2.DataSource = listreqitem;
            dataGridView4.DataSource = bs2;
            if (listreqitem.Count != 0)
            {
                dataGridView4.ColumnHeadersVisible = true;
                dataGridView4.Columns["Id"].Visible = false;
                dataGridView4.Columns["Remove"].DisplayIndex = 5;
            }
            else
            {
                dataGridView4.ColumnHeadersVisible = false;
                dataGridView4.DataSource = null;
            }
            int tottal = 0;
            foreach (reqitem r in listreqitem)
            {
                tottal += r.Total;
            }
            int subtot = total + tottal;
            lblTotalPrice.Text = "Total Price : Rp. " + subtot.ToString();

        }

        private void btnKanan_Click(object sender, EventArgs e)
        {
            if (tbNumberNight.Text == "")
            {
                MessageBox.Show("Please insert Number Night","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else if (id == 0)
            {
                MessageBox.Show("Please Select Room", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                selectroom sl = listselect.Where(s => s.Id == id).FirstOrDefault();
                if (sl != null)
                {
                    MessageBox.Show("Room already selected!","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    listselect.Add(new selectroom
                    {
                        Id = id,
                        RoomNumber = Roomnumber,
                        DurationNight = Convert.ToInt32( tbNumberNight.Text),
                        RoomFloor = roomfloor,
                        RoomPrice = roomprice,
                        Description = description,
                        
                    }) ;
                }
                refreshdg();
            }
        }

        private void btnKiri_Click(object sender, EventArgs e)
        {
            if (listselect.Count == 0)
            {
                MessageBox.Show("please select room","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                foreach (DataGridViewRow item in dataGridView2.Rows)
                {
                    selected = new selectroom();
                    selected = listselect[item.Index];
                }
                listselect.Remove(selected);
                refreshdg();
            }
        }

        private void tbNumberNight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && (int)e.KeyChar != (int)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void rbtnAddNew_CheckedChanged(object sender, EventArgs e)
        {
            panel1.Visible = true;
            dataGridView3.Visible = false;
            label2.Visible = false;
            tbSearch.Visible = false;
        }

        private void rbtnSearch_CheckedChanged(object sender, EventArgs e)
        {
            panel1.Visible = false;
            dataGridView3.Visible = true;
            label2.Visible = true;
            tbSearch.Visible = true;

        }

        private void tbName_KeyPress(object sender, KeyPressEventArgs e)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    if (tbName.Text == "" || tbNumber.Text == "")
                    {
                        MessageBox.Show("Please Insert data!","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                    else
                    {
                        Customer cs = db.Customers.Where(s => s.PhoneNumber == tbNumber.Text).FirstOrDefault();
                        if (cs != null)
                        {
                            MessageBox.Show("Phone Number Already exist!","Informaiton",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        }
                        else
                        {
                            db.Customers.InsertOnSubmit(new Customer
                            {
                                PhoneNumber = tbNumber.Text,
                                Name = tbName .Text,
                            });
                            db.SubmitChanges();
                            MessageBox.Show("Success Add Customer!","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                            tbName.Clear();
                            tbNumber.Clear();
                        }
                    }
                }
            }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                if (tbSearch.Text.Length == 0)
                {
                    dataGridView3.DataSource = null;
                    dataGridView3.ColumnHeadersVisible = false;
                }
                else
                {
                    var cari = db.Customers.Where(s => SqlMethods.Like(s.Name, "%" + tbSearch.Text + "%"));
                    if (cari != null)
                    {
                        dataGridView3.DataSource = cari;
                        dataGridView3.ColumnHeadersVisible = true;
                        dataGridView3.Columns["ID"].Visible = false;
                        dataGridView3.Columns["NIK"].Visible = false;
                        dataGridView3.Columns["PhoneNumber"].Visible = false;
                        dataGridView3.Columns["Age"].Visible = false;
                    }
                }
            }
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 1; i <= dataGridView3.Rows.Count; i++)
            {
                if (Convert.ToInt32(dataGridView3.Rows[i].Cells["Choose"].Value) > 0)
                {
                    dataGridView3.Rows[i].Cells["Choose"].Value = false;
                }
            }
        }

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                selectItem = new reqitem();
                selectItem = listreqitem[e.RowIndex];
                if (selectItem.Qty > 1)
                {
                    selectItem.Qty--;
                }
                else
                {
                    listreqitem.Remove(selectItem);
                }
            }
            refreshdg();
            if (dataGridView4.Rows.Count == 0)
            {
                dataGridView4.ColumnHeadersVisible = false;
            }
        }
        int idreq;
        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                if (cbxItem.SelectedItem == null || Numqty.Value == 0)
                {
                    MessageBox.Show("Please insert Item or qty!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    List<Item> li = db.Items.Select(s => s).ToList();
                    idreq = li[cbxItem.SelectedIndex].ID;
                    reqitem ri = listreqitem.Where(s => s.Id == idreq).FirstOrDefault();
                    if (ri != null)
                    {
                        ri.Qty += Convert.ToInt32(Numqty.Value);
                    }
                    else
                    {
                        listreqitem.Add(new reqitem
                        {
                            Id = idreq,
                            Item = li[cbxItem.SelectedIndex].Name,
                            Qty = Convert.ToInt32(Numqty.Value),
                            price = li[cbxItem.SelectedIndex].RequestPrice
                        }) ;
                    }
                  
                }
                refreshdg();
            }
        }
    }
    class selectroom
    {
        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public int RoomFloor { get; set; }
        public string Description { get; set; }
        public int DurationNight { get; set; }
        public int RoomPrice { get; set; }
        public int Total { get { return (DurationNight * RoomPrice); } }
    }
    class reqitem
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public int Qty { get; set; }
        public int price { get; set; }
        public int Total { get { return (Qty * price); } }
    }
}
