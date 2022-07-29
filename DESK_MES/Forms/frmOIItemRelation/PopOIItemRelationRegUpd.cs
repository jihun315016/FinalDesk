using DESK_DTO;
using DESK_MES.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DESK_MES
{
    public partial class PopOIItemRelationRegUpd : Form
    {
        InspectService inspectSrv;
        OperationService operationSrv;
        List<InspectItemVO> inspectList;
        List<InspectItemVO> selectedInspect;

        public PopOIItemRelationRegUpd(OperationVO oper, bool isReg)
        {
            InitializeComponent();
            lblOperationName.Text = oper.Operation_Name;
            lblOperationName.Tag = oper.Operation_No;

            lblTitle.Tag = isReg; // 등록 or 수정 유무
            if (isReg)
            {
                lblTitle.Text = "공정-검사 데이터 등록";
                btnSave.Text = "등록";
            }
            else
            {
                lblTitle.Text = "공정-검사 데이터 수정";
                btnSave.Text = "수정";
            }
        }

        private void PopOperationInspectItemRelationRegister_Load(object sender, EventArgs e)
        {
            inspectSrv = new InspectService();
            operationSrv = new OperationService();
            selectedInspect = new List<InspectItemVO>();
            InitControl();
        }

        void InitControl()
        {
            inspectList = inspectSrv.GetInspectItemList();

            // 검사 데이터 항목 수정인 경우
            if (!(bool)lblTitle.Tag)
            {
                List<int> inspectItems = operationSrv.GetInspectListByOperation(Convert.ToInt32(lblOperationName.Tag));
                foreach (int item in inspectItems)
                {
                    
                    foreach (InspectItemVO inspect in inspectList)
                    {
                        if (item == inspect.Inspect_No)
                        {
                            selectedInspect.Add(inspect);
                        }
                    }
                }

                selectedInspect.ForEach(s => inspectList.Remove(s));
            }

            foreach (DataGridView dgv in new DataGridView[] { dgvInspect, dgvRegistered })
            {
                DataGridUtil.SetInitGridView(dgv);
                DataGridUtil.SetDataGridViewColumn_TextBox(dgv, "검사 항목 번호", "Inspect_No", colWidth: 130);
                DataGridUtil.SetDataGridViewColumn_TextBox(dgv, "검사 항목명", "Inspect_Name", colWidth: 170);
            }

            dgvInspect.DataSource = inspectList;
            dgvRegistered.DataSource = selectedInspect;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgvInspect.DataSource = null;
            dgvInspect.DataSource = inspectList.Where(i => i.Inspect_Name.Contains(txtName.Text.Trim())).ToList();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            inspectList = inspectSrv.GetInspectItemList();
            dgvInspect.DataSource = null;
            dgvInspect.DataSource = inspectList;
            txtName.Text = string.Empty;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dgvInspect.CurrentCell != null)
            {
                InspectItemVO item = inspectList.Where(i => i.Inspect_No == Convert.ToInt32(dgvInspect["Inspect_No", dgvInspect.CurrentCell.RowIndex].Value)).FirstOrDefault();
                selectedInspect.Add(item);
                inspectList.Remove(item);

                dgvInspect.DataSource = null;
                dgvRegistered.DataSource = null;
                dgvInspect.DataSource = inspectList;
                dgvRegistered.DataSource = selectedInspect;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            InspectItemVO item = selectedInspect.Where(s => s.Inspect_No == Convert.ToInt32(dgvRegistered["Inspect_No", dgvRegistered.CurrentCell.RowIndex].Value)).FirstOrDefault();
            if (item != null)
            {
                inspectList.Add(item);
                selectedInspect.Remove(item);

                dgvInspect.DataSource = null;
                dgvRegistered.DataSource = null;
                dgvInspect.DataSource = inspectList;
                dgvRegistered.DataSource = selectedInspect;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool result = (operationSrv.SaveOIRelation(Convert.ToInt32(lblOperationName.Tag), selectedInspect));
            string msg = (bool)lblTitle.Tag ? "등록" : "수정";
            if (result)
            {
                MessageBox.Show($"공정 - 검사 데이터 관계가 {msg}되었습니다.");
                this.Close();
            }
            else
            {
                MessageBox.Show($"{msg}에 실패했습니다.");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
