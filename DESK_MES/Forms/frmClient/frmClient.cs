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
    public partial class frmClient : FormStyle_2
    {
        public frmClient()
        {
            InitializeComponent();
            label1.Text = "거래처 관리";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PopClientRegister pop = new PopClientRegister();
            pop.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PopClientModify pop = new PopClientModify();
            pop.Show();
        }
    }
}
