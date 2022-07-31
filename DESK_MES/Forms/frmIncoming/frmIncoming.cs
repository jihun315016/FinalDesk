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
    public partial class frmIncoming : Form
    {
        int purchaseNo = 0;

        public frmIncoming()
        {
            InitializeComponent();
            panel5.Visible = false;
            label1.Text = "입고 관리";
        }

        private void btnIncoming_Click(object sender, EventArgs e)
        {
            if (purchaseNo != 0)
            {
                PopIncomingCreateLot pop = new PopIncomingCreateLot(purchaseNo);
                if (pop.ShowDialog() == DialogResult.OK)
                {
                    //LoadData();
                }
            }
            else
            {
                MessageBox.Show("입고처리할 발주코드를 선택해주세요");
                return;
            }
        }

        private void btnOpenDetail_Click(object sender, EventArgs e)
        {
            if (panel5.Visible == true)
            {
                panel5.Visible = false;
            }
            else
            {
                panel5.Visible = true;
            }
        }
    }
}
