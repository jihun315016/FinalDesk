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
    public partial class PopOIItemRelationRegister : Form
    {
        InspectService inspectSrv;
        List<InspectItemVO> InspectList;
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
            InitControl();
        }

        void InitControl()
        {
            InspectList = inspectSrv.GetInspectItemList();

            foreach (DataGridView dgv in new DataGridView[] { dgvInspect, dgvRegistered })
            {
                DataGridUtil.SetInitGridView(dgv);
                DataGridUtil.SetDataGridViewColumn_TextBox(dgv, "검사 항목 번호", "Inspect_No", colWidth: 130);
                DataGridUtil.SetDataGridViewColumn_TextBox(dgv, "검사 항목명", "Inspect_Name", colWidth: 150);
                DataGridUtil.SetDataGridViewColumn_TextBox(dgv, "타겟값", "Target", colWidth: 120);
                DataGridUtil.SetDataGridViewColumn_TextBox(dgv, "상한값", "USL", colWidth: 120);
                DataGridUtil.SetDataGridViewColumn_TextBox(dgv, "하한값", "LSL", colWidth: 120);
            }
        }
    }
}
