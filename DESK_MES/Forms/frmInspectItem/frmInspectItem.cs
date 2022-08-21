using DESK_DTO;
using DESK_MES.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DESK_MES
{
    public partial class frmInspectItem : FormStyle_2
    {
        UserVO user;
        InspectService InspectSrv;
        List<InspectItemVO> inspectList;

        public frmInspectItem()
        {
            InitializeComponent();
            label1.Text = "품질 검사 항목 설정";
        }

        private void frmInspectItem_Load(object sender, EventArgs e)
        {
            this.user = ((frmMain)(this.MdiParent)).userInfo;
            InspectSrv = new InspectService();
            InitControl();
        }

        void InitControl()
        {
            inspectList = InspectSrv.GetInspectItemList();

            dtpCreateTime.Format = DateTimePickerFormat.Custom;
            dtpCreateTime.CustomFormat = " ";
            dtpUpdateTime.Format = DateTimePickerFormat.Custom;
            dtpUpdateTime.CustomFormat = " ";

            comboBox1.Items.AddRange(new string[] { "선택", "검사 항목 번호", "검사 항목명" });
            comboBox1.SelectedIndex = 0;

            DataGridUtil.SetInitGridView(dgvList);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "검사 항목 번호", "Inspect_No", colWidth: 200, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "검사 항목명", "Inspect_Name", colWidth: 300, alignContent: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "타겟값", "Target", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "상한값", "USL", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "하한값", "LSL", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "등록 시간", "Create_Time", colWidth: 200, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "등록 사용자", "Create_User_Name", colWidth: 180, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "등록 사용자 번호", "Create_User_No", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "수정 사용자 번호", "Update_User_No", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "수정 사용자", "Update_Time", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "수정 사용자", "Update_User_Name", isVisible: false);
            dgvList.Columns["Target"].DefaultCellStyle.Format = "###,##0";
            dgvList.Columns["USL"].DefaultCellStyle.Format = "###,##0";
            dgvList.Columns["LSL"].DefaultCellStyle.Format = "###,##0";

            dgvList.DataSource = inspectList;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<InspectItemVO> list = inspectList.Where(p => 1 == 1).ToList();

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

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            dtpCreateTime.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";
            dtpUpdateTime.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";

            List<InspectItemVO> temp = InspectSrv.GetInspectItemList(Convert.ToInt32(dgvList["Inspect_No", e.RowIndex].Value.ToString()));
            InspectItemVO item = temp.FirstOrDefault();
            txtInspectNoDetail.Text = item.Inspect_No.ToString();
            txtInspectNameDetail.Text = item.Inspect_Name;
            txtTargetDetail.Text = item.Target.ToString();
            txtLslDetail.Text = item.LSL.ToString();
            txtUslDetail.Text = item.USL.ToString();
            dtpCreateTime.Value = item.Create_Time;
            txtCreateUserDetail.Text = item.Create_User_Name;

            if (item.Update_Time.ToString() == "0001-01-01 오전 12:00:00")
            {
                dtpUpdateTime.Format = DateTimePickerFormat.Custom;
                dtpUpdateTime.CustomFormat = " ";
                txtUpdateUserDetail.Text = string.Empty;
            }
            else
            {
                dtpUpdateTime.Format = dtpCreateTime.Format;
                dtpUpdateTime.Value = item.Update_Time;
                txtUpdateUserDetail.Text = item.Update_User_Name;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PopInspectItemRegister pop = new PopInspectItemRegister(user);
            if (pop.ShowDialog() == DialogResult.OK)
            {
                btnReset_Click(this, null);
                btnSearch_Click(this, null);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInspectNoDetail.Text))
            {
                MessageBox.Show("검사 항목을 선택해주세요.");
                return;
            }

            List<InspectItemVO> temp = InspectSrv.GetInspectItemList(Convert.ToInt32(txtInspectNoDetail.Text));
            InspectItemVO item = temp.FirstOrDefault();

            PopInspectItemModify pop = new PopInspectItemModify(item, user);
            if (pop.ShowDialog() == DialogResult.OK)
            {
                btnReset_Click(this, null);
                btnSearch_Click(this, null);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            inspectList = InspectSrv.GetInspectItemList();
            comboBox1.Enabled = textBox1.Enabled = true;
            panel5.Visible = false;
            dgvList.DataSource = null;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Execl Files(*.xls)|*.xls";
            dlg.Title = "엑셀파일로 내보내기";

            List<InspectItemVO> list = dgvList.DataSource as List<InspectItemVO>;
            if (list == null)
            {
                MessageBox.Show("조회 항목이 없습니다.");
                return;
            }

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ExcelUtil excel = new ExcelUtil();
                List<InspectItemVO> output = list;

                string[] columnImport = { "Inspect_No", "Inspect_Name", "Target", "USL", "LSL", "Create_Time", "Create_User_Name" };
                string[] columnName = { "검사 항목 번호", "검사 항목명", "타겟값", "상한값", "하한값", "등록 시간", "등록 사용자" };

                if (excel.ExportList(output, dlg.FileName, columnImport, columnName))
                {
                    MessageBox.Show("엑셀 다운로드 완료");
                }
                else
                {
                    MessageBox.Show("엑셀 다운 실패");
                }
            }
        }

        private void frmInspectItem_Shown(object sender, EventArgs e)
        {
            dgvList.ClearSelection();
        }
    }
}
