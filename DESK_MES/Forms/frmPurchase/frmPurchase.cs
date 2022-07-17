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
    public partial class frmPurchase : FormStyle_1
    {
        public frmPurchase()
        {
            InitializeComponent();
            label1.Text = "발주 관리";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PopPurchaseRegister pop = new PopPurchaseRegister(); ;
            pop.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PopPurchaseModify pop = new PopPurchaseModify();
            pop.ShowDialog();
        }
    }
}
