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
    public partial class frmWorkOrder : FormStyle_1
    {
        public frmWorkOrder()
        {
            InitializeComponent();
            label1.Text = "작업지시 관리";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PopWorkOrderRegister pop = new PopWorkOrderRegister();
            pop.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            PopWorkOrderModify pop = new PopWorkOrderModify();
            pop.ShowDialog();
        }
    }
}
