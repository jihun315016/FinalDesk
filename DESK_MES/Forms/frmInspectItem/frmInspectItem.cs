using DESK_DTO;
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
        UserVO user;

        public frmInspectItem()
        {
            InitializeComponent();
            label1.Text = "품질-검사 항목 설정";
        }

        private void frmInspectItem_Load(object sender, EventArgs e)
        {
            this.user = ((frmMain)(this.MdiParent)).userInfo;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PopInspectItemRegister pop = new PopInspectItemRegister(user);
            pop.ShowDialog();
        }
    }
}
