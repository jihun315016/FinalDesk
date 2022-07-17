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
    public partial class frmInspectItem : FormStyle_2
    {
        public frmInspectItem()
        {
            InitializeComponent();
            label1.Text = "품질-검사 항목 설정";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PopInspectItemRegister pop = new PopInspectItemRegister();
            pop.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PopInspectItemModify pop = new PopInspectItemModify();
            pop.ShowDialog();
        }
    }
}
