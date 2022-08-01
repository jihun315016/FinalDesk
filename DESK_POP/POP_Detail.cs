using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DESK_POP
{
    public partial class POP_Detail : Form
    {
        string oCode;
        public POP_Detail(string orderCode)
        {
            oCode = orderCode;
            InitializeComponent();
        }

        private void POP_Detail_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblNowTime.Text = DateTime.Now.ToString();
        }
    }
}
