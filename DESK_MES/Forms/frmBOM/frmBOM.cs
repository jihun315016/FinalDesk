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
    public partial class frmBOM : FormStyle_2
    {
        public frmBOM()
        {
            InitializeComponent();
            label1.Text = "BOM 관리";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PopBOMRegister pop = new PopBOMRegister();
            pop.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PopBOMDelete pop = new PopBOMDelete();
            pop.ShowDialog();
        }
    }
}
