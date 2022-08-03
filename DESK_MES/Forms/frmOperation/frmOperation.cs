using DESK_DTO;
using DESK_MES.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Windows.Forms;

namespace DESK_MES
{
    public partial class frmOperation : FormStyle_2
    {
        OperationService operationSrv;
        UserVO user;
        List<OperationVO> operationList;

        public frmOperation()
        {
            InitializeComponent();
        }

        private void frmProcess_Load(object sender, EventArgs e)
        {
            operationSrv = new OperationService();
            this.user = ((frmMain)(this.MdiParent)).userInfo;
            InitControl();
        }

        void InitControl()
        {
            dtpCreateTime.Format = DateTimePickerFormat.Custom;
            dtpCreateTime.CustomFormat = " ";
            dtpUpdateTime.Format = DateTimePickerFormat.Custom;
            dtpUpdateTime.CustomFormat = " ";

            label1.Text = "공정 관리";
            operationList = operationSrv.GetOperationList();

            comboBox1.Items.AddRange(new string[] { "검색 조건", "공정 번호", "공정명" });
            comboBox1.SelectedIndex = 0;

            string[] isChackArr = new string[] { "검사 여부", "예", "아니오" };
            cboIsDeffect.Items.AddRange(isChackArr);
            cboIsInspect.Items.AddRange(isChackArr);
            cboMaterial.Items.AddRange(isChackArr);
            cboIsDeffect.SelectedIndex = cboIsInspect.SelectedIndex = cboMaterial.SelectedIndex = 0;

            DataGridUtil.SetInitGridView(dgvList);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "공정 번호", "Operation_No");
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "공정명", "Operation_Name", colWidth: 160);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "불량 체크 여부", "Is_Check_Deffect", colWidth: 180);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "검사 데이터 체크 여부", "Is_Check_Inspect", colWidth: 180);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "자재 사용 여부", "Is_Check_Marerial", colWidth: 180);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "등록 시간", "Create_Time", colWidth: 170);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "등록 사용자", "Create_User_Name", colWidth: 120);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "등록 사용자 번호", "Create_User_No", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "수정 시간", "Update_Time", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "수정 사용자 번호", "Update_User_No", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "수정 사용자", "Update_User_Name", isVisible: false);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<OperationVO> list = operationList.Where(p => 1 == 1).ToList();
            // 상세 검색
            if (panel5.Visible)
            {
                if (!string.IsNullOrWhiteSpace(txtOperNo.Text.Trim()))
                    list = list.Where(p => p.Operation_No.ToString().ToLower().Contains(txtOperNo.Text.ToLower())).ToList();

                if (!string.IsNullOrWhiteSpace(txtOperName.Text.Trim()))
                    list = list.Where(p => p.Operation_Name.ToLower().Contains(txtOperName.Text.ToLower())).ToList();

                if (cboIsDeffect.SelectedIndex == 1)
                    list = list.Where(p => p.Is_Check_Deffect == "Y").ToList();
                else if (cboIsDeffect.SelectedIndex == 2)
                    list = list.Where(p => p.Is_Check_Deffect == "N").ToList();

                if (cboIsInspect.SelectedIndex == 1)
                    list = list.Where(p => p.Is_Check_Inspect == "Y").ToList();
                else if (cboIsInspect.SelectedIndex == 2)
                    list = list.Where(p => p.Is_Check_Inspect == "N").ToList();

                if (cboMaterial.SelectedIndex == 1)
                    list = list.Where(p => p.Is_Check_Marerial == "Y").ToList();
                else if (cboMaterial.SelectedIndex == 2)
                    list = list.Where(p => p.Is_Check_Marerial == "N").ToList();
            }
            // 일반 검색
            else
            {
                // 품번 검색
                if (comboBox1.SelectedIndex == 1)
                    list = list.Where(o => o.Operation_No.ToString().ToLower().Contains(textBox1.Text.ToLower())).ToList();

                // 품명 검색
                else if (comboBox1.SelectedIndex == 2)
                    list = list.Where(o => o.Operation_Name.ToLower().Contains(textBox1.Text.ToLower())).ToList();
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

        private void btnReset_Click(object sender, EventArgs e)
        {
            operationList = operationSrv.GetOperationList();
            comboBox1.Enabled = textBox1.Enabled = true;
            panel5.Visible = false;
            dgvList.DataSource = null;
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            dtpCreateTime.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";
            dtpUpdateTime.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";

            List<OperationVO> temp = operationSrv.GetOperationList(Convert.ToInt32(dgvList["Operation_No", e.RowIndex].Value));
            OperationVO oper = temp.FirstOrDefault();
            txtOperNoDetail.Text = oper.Operation_No.ToString();
            txtOperNameDetail.Text = oper.Operation_Name;
            txtIsDeffectDetail.Text = oper.Is_Check_Deffect == "Y" ? "예" : "아니오";
            txtIsInspectDetail.Text = oper.Is_Check_Inspect == "Y" ? "예" : "아니오";
            txtMaterialDetail.Text = oper.Is_Check_Marerial == "Y" ? "예" : "아니오";
            dtpCreateTime.Value = oper.Create_Time;
            txtCreateUserDetail.Text = oper.Create_User_Name;

            if (oper.Update_Time.ToString() == "0001-01-01 오전 12:00:00")
            {
                dtpUpdateTime.Format = DateTimePickerFormat.Custom;
                dtpUpdateTime.CustomFormat = " ";
                txtUpdateUserDetail.Text = string.Empty;
            }
            else
            {
                dtpUpdateTime.Format = dtpCreateTime.Format;
                dtpUpdateTime.Value = oper.Update_Time;
                txtUpdateUserDetail.Text = oper.Update_User_Name;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PopOperationRegister pop = new PopOperationRegister(user);
            if (pop.ShowDialog() == DialogResult.OK)
            {
                btnReset_Click(this, null);
                btnSearch_Click(this, null);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtOperNoDetail.Text))
            {
                MessageBox.Show("공정을 선택하세요.");
                return;
            }

            OperationVO oper = operationSrv.GetOperationList(Convert.ToInt32(txtOperNoDetail.Text)).FirstOrDefault();
            PopOperationModify pop = new PopOperationModify(user, oper);
            if (pop.ShowDialog() == DialogResult.OK)
            {
                btnReset_Click(this, null);
                btnSearch_Click(this, null);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Execl Files(*.xls)|*.xls";
            dlg.Title = "엑셀파일로 내보내기";

            List<OperationVO> list = dgvList.DataSource as List<OperationVO>;
            if (list == null)
            {
                MessageBox.Show("조회 항목이 없습니다.");
                return;
            }

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ExcelUtil excel = new ExcelUtil();
                List<OperationVO> output = list;

                string[] columnImport = { "Operation_No", "Operation_Name", "Is_Check_Deffect", "Is_Check_Inspect", "Is_Check_Marerial" };
                string[] columnName = { "공정 번호", "공정명", "불량 체크 여부", "검사 데이터 체크 여부", "자재 사용 여부" };

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
    }
}
