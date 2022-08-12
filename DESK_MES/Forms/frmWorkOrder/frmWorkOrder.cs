using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DESK_DTO;

namespace DESK_MES
{
    public partial class frmWorkOrder : FormStyle_1
    {
        UserVO user;
        WorkOrderService wSrv;
        List<WorkOrderVO> workList;
        ManufactureService mSrv;
        List<ManufactureVO> manufactureList;
        int ProductionCode;
        string productCode;
        string manufactureCode;

        public frmWorkOrder()
        {
            InitializeComponent();
            label1.Text = "작업지시 관리";
        }

        private void frmWorkOrder_Load(object sender, EventArgs e)
        {
            this.user = ((frmMain)(this.MdiParent)).userInfo;
            wSrv = new WorkOrderService();
            mSrv = new ManufactureService();

            DataGridUtil.SetInitGridView(dataGridView1);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생산코드", "Production_Code", colWidth: 95, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "주문서코드", "Order_No", colWidth: 60, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생산 품목코드", "Product_Code", colWidth: 80, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생산 품목명", "Product_Name", colWidth: 300, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생산 계획 수량", "Planned_Qty", colWidth: 140, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생산 완료 수량", "Production_Qty", colWidth: 140, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생산 시작 예정일", "Start_Date", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생산 완료 예정일", "Estimated_Date", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생산 완료일", "Complete_Date", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생산 계획 상태", "Production_Plan_Status", colWidth: 60, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생산계획 담당자", "Production_Plan_User_Name", colWidth: 60, isVisible: false);            

            DataGridUtil.SetInitGridView(dataGridView2);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "작업 지시 코드", "Production_No", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "작업 품목명", "Product_Name", colWidth: 300, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "공정명", "Production_Operation_Name", colWidth: 230, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "설비명", "Production_Equipment_Name", colWidth: 60, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "작업 지시 수량", "Work_Plan_Qty", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "작업 완료 수량", "Work_Complete_Qty", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "작업 지시 상태", "Work_Order_State_Name", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "자재불출여부", "Material_Lot_Input_Name", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);

            LoadData();

            groupBoxW.Visible = false;
        }
        private void LoadData()
        {
            manufactureList = mSrv.GetmanufactureList();
            dataGridView1.DataSource = manufactureList.Where(e => e.Production_Plan_Status.Contains("확정")).ToList();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            ProductionCode = Convert.ToInt32(dataGridView1[0, e.RowIndex].Value);
            productCode = dataGridView1[2, e.RowIndex].Value.ToString();

            workList = wSrv.GetworkList(ProductionCode);
            dataGridView2.DataSource = workList;

            groupBoxW.Visible = false;


            manufactureCode = dataGridView1[0, e.RowIndex].Value.ToString();

            txtManufactureCode.Text = dataGridView1["Production_Code", e.RowIndex].Value.ToString();
            txtOrderCode.Text = Convert.ToInt32(dataGridView1["Order_No", e.RowIndex].Value) == 0 ? "" : dataGridView1["Order_No", e.RowIndex].Value.ToString();
            txtProductCode.Text = dataGridView1["Product_Code", e.RowIndex].Value.ToString();
            txtProductName.Text = dataGridView1["Product_Name", e.RowIndex].Value.ToString();
            txtPlanQty.Text = dataGridView1["Planned_Qty", e.RowIndex].Value.ToString();
            txtFinishQty.Text = (dataGridView1["Production_Qty", e.RowIndex].Value ?? string.Empty).ToString();
            txtStartDate.Text = dataGridView1["Start_Date", e.RowIndex].Value.ToString();
            txtStartDueDate.Text = dataGridView1["Estimated_Date", e.RowIndex].Value.ToString();

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            groupBoxW.Visible = true;

            string material_state = (dataGridView2[7, e.RowIndex].Value == null) ? "" : dataGridView2[7, e.RowIndex].Value.ToString();
            string work_state = dataGridView2[6, e.RowIndex].Value.ToString();
            if ( material_state.Trim().ToString() == "대기" || material_state.Trim().ToString() == "")
            {
                btnWorkEachOK.Visible = true;
                btnMaterialEachOut.Visible = true;

                if (work_state.Trim().ToString() == "미정")
                {
                    btnWorkEachOK.Visible = true;
                    btnMaterialEachOut.Visible = true;
                }
                else
                {
                    btnWorkEachOK.Visible = false;
                    btnMaterialEachOut.Visible = true;
                }
            }
            else
            {
                btnWorkEachOK.Visible = false;
                btnMaterialEachOut.Visible = false;
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if(productCode!=null &&  dataGridView2.Rows.Count < 1)
            {
                PopWorkOrderRegister pop = new PopWorkOrderRegister(ProductionCode, productCode, user);
                pop.ShowDialog();
            }
            else
            {
                MessageBox.Show("이미 작업 지시가 생성되었습니다.");
                return;
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            PopWorkOrderModify pop = new PopWorkOrderModify();
            pop.ShowDialog();
        }

        private void btnWorkAllOK_Click(object sender, EventArgs e)
        {
            // 작업상태 일괄 변경 : 미정 => 확정
        }

        private void btnMaterialAllOut_Click(object sender, EventArgs e)
        {
            // 자재불출상태 일괄 변경: 미정 => 확정
        }

        private void btnWorkEachOK_Click(object sender, EventArgs e)
        {
            // 작업상태 개별 변경 : 미정 => 확정
        }

        private void btnMaterialEachOut_Click(object sender, EventArgs e)
        {
            // 자재불출상태 개별 변경: 미정 => 확정
        }
    }
}
