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
    public partial class frmUserGroup : FormStyle_2
    {
        public frmUserGroup()
        {
            InitializeComponent();
            label1.Text = "사용자 그룹 관리";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PopUserGroupRegister pop = new PopUserGroupRegister();
            pop.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            PopUserGroupModify pop = new PopUserGroupModify();
            pop.ShowDialog();
        }
    }
}
