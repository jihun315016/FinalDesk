using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DESK_MES
{
    public partial class PopSelectBomQty : Form
    {
        public int Qty { get; set; }

        public PopSelectBomQty()
        {
            InitializeComponent();
        }

        private void btnInput_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtQty.Text))
            {
                Qty = Convert.ToInt32(txtQty.Text);
                this.DialogResult = DialogResult.OK;
            }
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnInput_Click(this, null);
            }
        }
    }
}
