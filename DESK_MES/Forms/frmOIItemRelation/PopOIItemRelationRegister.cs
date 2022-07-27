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
    public partial class PopOIItemRelationRegister : Form
    {
        InspectService inspectSrv;
        OperationService operationSrv;
        List<InspectItemVO> inspectList;
        List<InspectItemVO> selectedInspect;
        UserVO user;

        public PopOIItemRelationRegister(UserVO user, OperationVO oper)
        {
            InitializeComponent();
            this.user = user;
            lblOperationName.Text = oper.Operation_Name;
            lblOperationName.Tag = oper.Operation_No;
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

            foreach (DataGridView dgv in new DataGridView[] { dgvInspect, dgvRegistered })
            {
                DataGridUtil.SetInitGridView(dgv);
                DataGridUtil.SetDataGridViewColumn_TextBox(dgv, "검사 항목 번호", "Inspect_No", colWidth: 130);
                DataGridUtil.SetDataGridViewColumn_TextBox(dgv, "검사 항목명", "Inspect_Name", colWidth: 150);
                DataGridUtil.SetDataGridViewColumn_TextBox(dgv, "타겟값", "Target", colWidth: 120);
                DataGridUtil.SetDataGridViewColumn_TextBox(dgv, "상한값", "USL", colWidth: 120);
                DataGridUtil.SetDataGridViewColumn_TextBox(dgv, "하한값", "LSL", colWidth: 120);
            }

            dgvInspect.DataSource = inspectList;
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
            bool result = (operationSrv.SaveOIRelation(Convert.ToInt32(lblOperationName.Tag), user.User_No, selectedInspect));
            if (result)
            {
                MessageBox.Show("공정 - 검사 데이터 관계가 등록되었습니다.");
                this.Close();
            }
            else
            {
                MessageBox.Show("등록에 실패했습니다.");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
