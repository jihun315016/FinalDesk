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
    public partial class frmManufacture : FormStyle_2
    {
        public frmManufacture()
        {
            InitializeComponent();
            label1.Text = "생산계획 관리";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PopManufactureRegister pop = new PopManufactureRegister();
            pop.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PopManufactureModify pop = new PopManufactureModify();
            pop.ShowDialog();
        }
    }
}
