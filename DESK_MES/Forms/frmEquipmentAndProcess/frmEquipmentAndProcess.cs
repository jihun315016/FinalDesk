﻿using DESK_DTO;
using DESK_MES.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DESK_MES
{
    public partial class frmEquipmentAndProcess : FormStyle_2
    {
        List<OperationVO> operationList;
        OperationService operationSrv;
        EquipmentService equipmentSrv;

        public frmEquipmentAndProcess()
        {
            InitializeComponent();
        }

        private void frmEquipmentAndProcess_Load(object sender, EventArgs e)
        {
            operationSrv = new OperationService();
            operationList = operationSrv.GetOperationList();
            equipmentSrv = new EquipmentService();

            dtpCreateTime.Format = DateTimePickerFormat.Custom;
            dtpCreateTime.CustomFormat = " ";
            dtpUpdateTime.Format = DateTimePickerFormat.Custom;
            dtpUpdateTime.CustomFormat = " ";

            InitControl();
        }

        private void InitControl()
        {
            label1.Text = "설비 - 공정 관계";

            comboBox1.Items.AddRange(new string[] { "선택", "공정 번호", "공정명" });
            comboBox1.SelectedIndex = 0;

            DataGridUtil.SetInitGridView(dgvOperation);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvOperation, "공정 번호", "Operation_No", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvOperation, "공정명", "Operation_Name", colWidth: 200, alignContent: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvOperation, "불량 체크 여부", "Is_Check_Deffect", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvOperation, "검사 데이터 체크 여부", "Is_Check_Inspect", colWidth: 200, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvOperation, "자재 사용 여부", "Is_Check_Marerial", colWidth: 200, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvOperation, "등록 시간", "Create_Time", colWidth: 200, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvOperation, "등록 사용자", "Create_User_Name", colWidth: 120, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvOperation, "등록 사용자 번호", "Create_User_No", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvOperation, "수정 시간", "Update_Time", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvOperation, "수정 사용자 번호", "Update_User_No", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvOperation, "수정 사용자", "Update_User_Name", isVisible: false);

            DataGridUtil.SetInitGridView(dgvEquipment);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvEquipment, "설비 번호", "Equipment_No", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvEquipment, "설비명", "Equipment_Name", colWidth: 300, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvEquipment, "비가동 여부", "Is_Inoperative", colWidth: 200, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvEquipment, "등록 시간", "Create_Time", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvEquipment, "등록 사용자", "Create_User_Name", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvEquipment, "수정 시간", "Update_Time", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvEquipment, "수정 사용자", "Update_User_Name", isVisible: false);

            dgvOperation.DataSource = operationList;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<OperationVO> list = operationList.Where(o => 1 == 1).ToList();

            // 공정 번호 검색
            if (comboBox1.SelectedIndex == 1)
                list = list.Where(l => l.Operation_No.ToString().ToLower().Contains(textBox1.Text)).ToList();

            // 공정명 검색
            else if (comboBox1.SelectedIndex == 2)
                list = list.Where(l => l.Operation_Name.ToLower().Contains(textBox1.Text)).ToList();

            dgvOperation.DataSource = list;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            operationList = operationSrv.GetOperationList();
            comboBox1.Enabled = textBox1.Enabled = true;
            dgvOperation.DataSource = null;
            dgvEquipment.DataSource = null;
        }

        private void dgvOperation_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            dtpCreateTime.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";
            dtpUpdateTime.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";

            List<OperationVO> temp = operationSrv.GetOperationList(Convert.ToInt32(dgvOperation["Operation_No", e.RowIndex].Value));
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

            List<EquipmentVO> list = equipmentSrv.SelectEquipmentByOperation(Convert.ToInt32(dgvOperation["Operation_No", e.RowIndex].Value));
            dgvEquipment.DataSource = list;
            dgvEquipment.ClearSelection();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtOperNoDetail.Text))
            {
                MessageBox.Show("공정을 선택해주세요.");
                return;
            }

            if (dgvEquipment.Rows.Count > 0)
            {
                MessageBox.Show("이미 공정에 등록된 검사 데이터 항목이 있습니다.");
                return;
            }

            OperationVO oper = operationSrv.GetOperationList(Convert.ToInt32(txtOperNoDetail.Text)).FirstOrDefault();
            PopEquipmentAndProcessRegUpd pop = new PopEquipmentAndProcessRegUpd(oper, true);
            pop.ShowDialog();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtOperNoDetail.Text))
            {
                MessageBox.Show("공정을 선택해주세요.");
                return;
            }

            if (dgvEquipment.Rows.Count < 1)
            {
                MessageBox.Show("공정에 등록된 설비가 없습니다.");
                return;
            }

            OperationVO oper = operationSrv.GetOperationList(Convert.ToInt32(txtOperNoDetail.Text)).FirstOrDefault();
            PopEquipmentAndProcessRegUpd pop = new PopEquipmentAndProcessRegUpd(oper, false);
            pop.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtOperNoDetail.Text))
            {
                MessageBox.Show("공정을 선택해주세요.");
                return;
            }
            
            if (MessageBox.Show("공정에 해당하는 설비를 삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                bool result = operationSrv.DeleteEOItem(Convert.ToInt32(txtOperNoDetail.Text));
                if (result)
                {
                    MessageBox.Show("삭제되었습니다.");
                    btnReset_Click(this, null);
                    btnSearch_Click(this, null);
                }
                else
                {
                    MessageBox.Show("삭제에 실패했습니다.");
                }
            }
        }

        private void frmEquipmentAndProcess_Shown(object sender, EventArgs e)
        {
            dgvOperation.ClearSelection();
        }
    }
}
