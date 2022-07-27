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

        public PopOIItemRelationRegister()
        {
            InitializeComponent();
        }

        private void PopOperationInspectItemRelationRegister_Load(object sender, EventArgs e)
        {
            inspectSrv = new InspectService();
        }

        void InitControl()
        {
            InspectList = inspectSrv.GetInspectItemList();

            DataGridUtil.SetInitGridView(dgvInspect);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvInspect, "검사 항목 번호", "Inspect_No", colWidth: 160);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvInspect, "검사 항목명", "Inspect_Name", colWidth: 170);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvInspect, "타겟값", "Target", colWidth: 130);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvInspect, "상한값", "USL", colWidth: 130);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvInspect, "하한값", "LSL", colWidth: 130);
        }
    }
}
