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
    public partial class frmOrder : FormStyle_1
    {
        public frmOrder()
        {
            InitializeComponent();
            label1.Text = "주문 관리";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PopOrderRegister pop = new PopOrderRegister();
            pop.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PopOrderModify pop = new PopOrderModify();
            pop.ShowDialog();
        }
    }
}
