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
    public partial class frmInspectItem : FormStyle_2
    {
        UserVO user;
        InspectService InspectSrv;
        List<InspectItemVO> InspectList;

        public frmInspectItem()
        {
            InitializeComponent();
            label1.Text = "품질-검사 항목 설정";
        }

        private void frmInspectItem_Load(object sender, EventArgs e)
        {
            this.user = ((frmMain)(this.MdiParent)).userInfo;
            InspectSrv = new InspectService();
            InitControl();
        }

        void InitControl()
        {
            InspectList = InspectSrv.GetInspectItemList();

            DataGridUtil.SetInitGridView(dgvList);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "검사 항목 번호", "Inspect_No");
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "검사 항목명", "Inspect_Name");
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "타겟값", "Target");
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "상한값", "USL");
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "하한값", "LSL");
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "등록 시간", "Create_Time");
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "등록 사용자", "Create_User_Name");
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "등록 사용자 번호", "Create_User_No", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "수정 사용자 번호", "Update_User_No", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "수정 사용자", "Update_Time", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "수정 사용자", "Update_User_Name", isVisible: false);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<InspectItemVO> list = InspectList.Where(p => 1 == 1).ToList();

            dgvList.DataSource = list;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PopInspectItemRegister pop = new PopInspectItemRegister(user);
            pop.ShowDialog();
        }
    }
}
