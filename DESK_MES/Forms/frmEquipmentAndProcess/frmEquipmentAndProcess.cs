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
    public partial class frmEquipmentAndProcess : FormStyle_2
    {
        public frmEquipmentAndProcess()
        {
            InitializeComponent();
            label1.Text = "설비-공정 관계 설정";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PopEquipmentAndProcessRegister pop = new PopEquipmentAndProcessRegister();
            pop.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PopEquipmentAndProcessModify pop = new PopEquipmentAndProcessModify();
            pop.ShowDialog();
        }
    }
}
