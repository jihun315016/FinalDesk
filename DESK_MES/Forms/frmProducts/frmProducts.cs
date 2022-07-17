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
    public partial class frmProducts : FormStyle_2
    {
        public frmProducts()
        {
            InitializeComponent();
            label1.Text = "품목 관리";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PopProductsRegister pop = new PopProductsRegister();
            pop.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PopProductsModify pop = new PopProductsModify();
            pop.ShowDialog();
        }
    }
}
