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
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
        }

      

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value != 100)
            {
                progressBar1.Value += 10;
            }
            else
            {
                timer1.Stop();
            }
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer1.Interval = 460;
            timer1.Tick += timer1_Tick;
            progressBar1.Maximum = 100;
        }
    }
}
