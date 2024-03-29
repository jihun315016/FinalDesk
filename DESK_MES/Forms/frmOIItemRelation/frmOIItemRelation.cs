﻿using DESK_DTO;
using DESK_MES.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DESK_MES
{
    public partial class frmOIItemRelation : FormStyle_1
    {
        OperationService operationSrv;
        DataSet ds;
        DataSet tempDs;

        public frmOIItemRelation()
        {
            InitializeComponent();
        }

        private void frmOperationInspectItemRelation_Load(object sender, EventArgs e)
        {
            operationSrv = new OperationService();
            ds = operationSrv.GetOIRelation(); // 검사 데이터를 등록하는 공정만 
            InitControl();

            dtpCreateTime.Format = DateTimePickerFormat.Custom;
            dtpCreateTime.CustomFormat = " ";
            dtpUpdateTime.Format = DateTimePickerFormat.Custom;
            dtpUpdateTime.CustomFormat = " ";
        }

        void InitControl()
        {
            label1.Text = "공정-검사데이터 관계";           

            comboBox1.Items.AddRange(new string[] { "선택", "공정 번호", "공정명" });
            comboBox1.SelectedIndex = 0;

            DataGridUtil.SetInitGridView(dgvOperation);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvOperation, "공정 번호", "Operation_No", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvOperation, "공정명", "Operation_Name", colWidth: 200, alignContent: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvOperation, "불량 체크 여부", "Is_Check_Deffect", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvOperation, "검사 데이터 체크 여부", "Is_Check_Inspect", colWidth: 200, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvOperation, "자재 사용 여부", "Is_Check_Marerial", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvOperation, "등록 시간", "Create_Time", colWidth: 200, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvOperation, "등록 사용자", "Create_User_Name", colWidth: 120, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvOperation, "등록 사용자 번호", "Create_User_No", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvOperation, "수정 시간", "Update_Time", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvOperation, "수정 사용자 번호", "Update_User_No", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvOperation, "수정 사용자", "Update_User_Name", isVisible: false);

            DataGridUtil.SetInitGridView(dgvInspectItem);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvInspectItem, "공정 번호", "Operation_No", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvInspectItem, "검사 데이터 번호", "Inspect_No", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvInspectItem, "검사 데이터 항목명", "Inspect_Name", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvInspectItem, "타겟값", "Target", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvInspectItem, "상한값", "USL", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvInspectItem, "하한값", "LSL", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleRight);
            dgvInspectItem.Columns["Target"].DefaultCellStyle.Format = "###,##0";
            dgvInspectItem.Columns["USL"].DefaultCellStyle.Format = "###,##0";
            dgvInspectItem.Columns["LSL"].DefaultCellStyle.Format = "###,##0";

            tempDs = new DataSet();
            DataView dv = new DataView(ds.Tables[0]);

            // 공정 번호 검색
            if (comboBox1.SelectedIndex == 1)
                dv.RowFilter = $"CONVERT(Operation_No, System.String) LIKE '%{textBox1.Text}%'";

            // 공정명 검색
            else if (comboBox1.SelectedIndex == 2)
                dv.RowFilter = $"Operation_Name LIKE '%{textBox1.Text}%'";

            tempDs.Tables.Add(dv.ToTable());
            tempDs.Tables.Add(new DataView(ds.Tables[1]).ToTable());

            DataRelation relation = new DataRelation("OIRelation", tempDs.Tables["Table"].Columns["Operation_No"], tempDs.Tables["Table1"].Columns["Operation_No"]);
            dgvOperation.DataSource = tempDs;
            dgvOperation.DataMember = "Table";
            dgvInspectItem.DataSource = null;
            try
            {
                tempDs.Relations.Add(relation);
                dgvInspectItem.DataSource = tempDs;
                dgvInspectItem.DataMember = "Table.OIRelation";
            }
            catch (Exception err) { }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            tempDs = new DataSet();
            DataView dv = new DataView(ds.Tables[0]);
            
            // 공정 번호 검색
            if (comboBox1.SelectedIndex == 1)
                dv.RowFilter = $"CONVERT(Operation_No, System.String) LIKE '%{textBox1.Text}%'";

            // 공정명 검색
            else if (comboBox1.SelectedIndex == 2)
                dv.RowFilter = $"Operation_Name LIKE '%{textBox1.Text}%'";

            tempDs.Tables.Add(dv.ToTable());
            tempDs.Tables.Add(new DataView(ds.Tables[1]).ToTable());

            DataRelation relation = new DataRelation("OIRelation", tempDs.Tables["Table"].Columns["Operation_No"], tempDs.Tables["Table1"].Columns["Operation_No"]);
            dgvOperation.DataSource = tempDs;
            dgvOperation.DataMember = "Table";
            dgvInspectItem.DataSource = null;
            try
            {
                tempDs.Relations.Add(relation);
                dgvInspectItem.DataSource = tempDs;
                dgvInspectItem.DataMember = "Table.OIRelation";
            }
            catch (Exception err) { }
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

            
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ds = operationSrv.GetOIRelation();
            comboBox1.Enabled = textBox1.Enabled = true;
            dgvOperation.DataSource = null;
            dgvInspectItem.DataSource = null;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtOperNoDetail.Text))
            {
                MessageBox.Show("공정을 선택해주세요.");
                return;
            }

            if (dgvInspectItem.Rows.Count > 0)
            {
                MessageBox.Show("이미 공정에 등록된 검사 데이터 항목이 있습니다.");
                return;
            }

            OperationVO oper = operationSrv.GetOperationList(Convert.ToInt32(txtOperNoDetail.Text)).FirstOrDefault();
            PopOIItemRelationRegUpd pop = new PopOIItemRelationRegUpd(oper, true);
            pop.ShowDialog();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtOperNoDetail.Text))
            {
                MessageBox.Show("공정을 선택해주세요.");
                return;
            }

            if (dgvInspectItem.Rows.Count < 1)
            {
                MessageBox.Show("공정에 등록된 검사 데이터 항목이 없습니다.");
                return;
            }

            OperationVO oper = operationSrv.GetOperationList(Convert.ToInt32(txtOperNoDetail.Text)).FirstOrDefault();
            PopOIItemRelationRegUpd pop = new PopOIItemRelationRegUpd(oper, false);
            pop.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtOperNoDetail.Text))
            {
                MessageBox.Show("공정을 선택해주세요.");
                return;
            }

            if (MessageBox.Show("공정에 해당하는 검사 데이터 항목을 삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                bool result = operationSrv.DeleteOIIetm(Convert.ToInt32(txtOperNoDetail.Text));
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

        private void frmOIItemRelation_Shown(object sender, EventArgs e)
        {
            dgvOperation.ClearSelection();
            dgvInspectItem.ClearSelection();
        }
    }
}
