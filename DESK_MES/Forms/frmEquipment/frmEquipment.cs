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
    public partial class frmEquipment : FormStyle_2
    {
        public frmEquipment()
        {
            InitializeComponent();
            label1.Text = "설비 관리";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PopEquipmentRegister pop = new PopEquipmentRegister();
            pop.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PopEquipmentModify pop = new PopEquipmentModify();
            pop.ShowDialog();
        }
    }
}
