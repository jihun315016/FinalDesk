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
    public partial class frmProcess : FormStyle_2
    {
        UserVO user;

        public frmProcess()
        {
            InitializeComponent();
            label1.Text = "공정 관리";
        }

        private void frmProcess_Load(object sender, EventArgs e)
        {
            this.user = ((frmMain)(this.MdiParent)).userInfo;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PopProcessRegister pop = new PopProcessRegister(user);
            pop.ShowDialog();
        }
    }
}
