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

            //dtpCreateTime.Format = DateTimePickerFormat.Custom;
            //dtpCreateTime.CustomFormat = " ";
            //dtpUpdateTime.Format = DateTimePickerFormat.Custom;
            //dtpUpdateTime.CustomFormat = " ";

            comboBox1.Items.AddRange(new string[] { "검색 조건", "검사 항목 번호", "검사 항목명" });
            comboBox1.SelectedIndex = 0;

            DataGridUtil.SetInitGridView(dgvList);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "검사 항목 번호", "Inspect_No", colWidth: 160);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "검사 항목명", "Inspect_Name", colWidth: 170);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "타겟값", "Target", colWidth: 130);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "상한값", "USL", colWidth: 130);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "하한값", "LSL", colWidth: 130);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "등록 시간", "Create_Time", colWidth: 200);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "등록 사용자", "Create_User_Name", colWidth: 180);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "등록 사용자 번호", "Create_User_No", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "수정 사용자 번호", "Update_User_No", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "수정 사용자", "Update_Time", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "수정 사용자", "Update_User_Name", isVisible: false);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<InspectItemVO> list = InspectList.Where(p => 1 == 1).ToList();

            // 상세 검색
            if (panel5.Visible)
            {
                if (!string.IsNullOrWhiteSpace(txtInspectNoSearch.Text.Trim()))
                    list = list.Where(i => i.Inspect_No.ToString().ToLower().Contains(txtInspectNoSearch.Text.ToLower())).ToList();

                if (!string.IsNullOrWhiteSpace(txtInspectNameSearch.Text.Trim()))
                    list = list.Where(i => i.Inspect_Name.ToLower().Contains(txtInspectNameSearch.Text.ToLower())).ToList();
            }
            // 일반 검색
            else
            {
                // 품번 검색
                if (comboBox1.SelectedIndex == 1)
                    list = list.Where(i => i.Inspect_No.ToString().Contains(textBox1.Text.ToLower())).ToList();

                // 품명 검색
                else if (comboBox1.SelectedIndex == 2)
                    list = list.Where(i => i.Inspect_Name.ToLower().Contains(textBox1.Text.ToLower())).ToList();
            }

            dgvList.DataSource = list;
        }

        private void btnOpenDetail_Click(object sender, EventArgs e)
        {
            if (panel5.Visible)
                comboBox1.Enabled = textBox1.Enabled = false;
            else
                comboBox1.Enabled = textBox1.Enabled = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PopInspectItemRegister pop = new PopInspectItemRegister(user);
            pop.ShowDialog();
        }
    }
}
