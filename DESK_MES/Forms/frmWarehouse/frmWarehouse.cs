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
    public partial class frmWarehouse : FormStyle_2
    {
        public frmWarehouse()
        {
            InitializeComponent();
            label1.Text = "창고 관리";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PopWarehouseRegister pop = new PopWarehouseRegister();
            pop.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            PopWarehouseModify pop = new PopWarehouseModify();
            pop.ShowDialog();
        }
    }
}
