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
    public partial class PopEquipmentAndProcessRegUpd : Form
    {
        EquipmentService EquipmentSrv;
        OperationService operationSrv;
        List<EquipmentVO> equipmentList;
        List<EquipmentVO> selectedInspect;
        UserVO user;

        public PopEquipmentAndProcessRegUpd(UserVO user, OperationVO oper, bool isReg)
        {
            InitializeComponent();
            this.user = user;
            lblOperationName.Text = oper.Operation_Name;
            lblOperationName.Tag = oper.Operation_No;

            lblTitle.Tag = isReg; // 등록 or 수정 유무
            if (isReg)
            {
                lblTitle.Text = "공정-공정 등록";
                btnSave.Text = "등록";
            }
            else
            {
                lblTitle.Text = "공정-공정 수정";
                btnSave.Text = "수정";
            }
        }

        private void PopEquipmentAndProcessRegUpd_Load(object sender, EventArgs e)
        {
            EquipmentSrv = new EquipmentService();

            InitControl();
        }

        void InitControl()
        {
            equipmentList = EquipmentSrv.SelectEquipmentAllList();

            // 설비 관계 수정인 경우
            if (!(bool)lblTitle.Tag)
            {
                // TODO : 선택된 공정에 대한 설비 항목 리스트 조회 -> foreach
                //List<int> inspectItems = operationSrv.GetInspectListByOperation(Convert.ToInt32(lblOperationName.Tag));
                //foreach (int item in inspectItems)
                //{

                //    foreach (EquipmentVO equipment in equipmentList)
                //    {
                //        if (item == equipment.Equipment_No)
                //        {
                //            selectedInspect.Add(equipment);
                //        }
                //    }
                //}

                selectedInspect.ForEach(s => equipmentList.Remove(s));
            }


            foreach (DataGridView dgv in new DataGridView[] { dgvEquipment, dgvRegistered })
            {
                DataGridUtil.SetInitGridView(dgv);
                DataGridUtil.SetDataGridViewColumn_TextBox(dgv, "설비 번호", "Equipment_No", colWidth: 130);
                DataGridUtil.SetDataGridViewColumn_TextBox(dgv, "설비명", "Equipment_Name", colWidth: 170);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgvEquipment.DataSource = null;
            dgvEquipment.DataSource = equipmentList.Where(eq => eq.Equipment_Name.Contains(txtName.Text.Trim())).ToList();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            equipmentList = EquipmentSrv.SelectEquipmentAllList();
            dgvEquipment.DataSource = null;
            dgvEquipment.DataSource = equipmentList;
            txtName.Text = string.Empty;
        }
    }
}
