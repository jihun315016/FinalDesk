using DESK_DTO;
using DESK_MES.Service;
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
        OperationService operationSrv;
        UserVO user;
        List<OperationVO> operationList;

        public frmProcess()
        {
            InitializeComponent();
            label1.Text = "공정 관리";
        }

        private void frmProcess_Load(object sender, EventArgs e)
        {
            operationSrv = new OperationService();
            this.user = ((frmMain)(this.MdiParent)).userInfo;
            operationList = operationSrv.GetOperationList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PopProcessRegister pop = new PopProcessRegister(user);
            pop.ShowDialog();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgvList.DataSource = operationList;
        }
    }
}
