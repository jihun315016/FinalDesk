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
    public partial class PopOPitemRelationRegUpd : Form
    {
        ProductService productSrv;
        OperationService operationSrv;
        List<ProductVO> productList;
        List<ProductVO> selectedInspect;

        public PopOPitemRelationRegUpd(OperationVO oper, bool isReg)
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

        private void PopOPitemRelationRegUpd_Load(object sender, EventArgs e)
        {
            productSrv = new ProductService();
            operationSrv = new OperationService();
            selectedInspect = new List<ProductVO>();
            InitControl();
        }

        private void InitControl()
        {
            productList = productSrv.GetProductList().Where(p => Array.IndexOf(new string[] { "FERT", "HALB" }, p.Product_Type) > -1).ToList();

            foreach (DataGridView dgv in new DataGridView[] { dgvInspect, dgvRegistered })
            {
                DataGridUtil.SetInitGridView(dgv);
                //DataGridUtil.SetDataGridViewColumn_TextBox(dgv, "검사 항목 번호", "Inspect_No", colWidth: 130);
                //DataGridUtil.SetDataGridViewColumn_TextBox(dgv, "검사 항목명", "Inspect_Name", colWidth: 170);
            }

            // 품목 관계 수정인 경우
            //if (!(bool)lblTitle.Tag)
            //{
            //    List<int> inspectItems = operationSrv.GetInspectListByOperation(Convert.ToInt32(lblOperationName.Tag));
            //    foreach (int item in inspectItems)
            //    {

            //        foreach (InspectItemVO inspect in inspectList)
            //        {
            //            if (item == inspect.Inspect_No)
            //            {
            //                selectedInspect.Add(inspect);
            //            }
            //        }
            //    }
            //}

            //selectedInspect.ForEach(s => inspectList.Remove(s));
            //dgvRegistered.DataSource = selectedInspect;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnReset_Click(object sender, EventArgs e)
        {

        }
    }
}
