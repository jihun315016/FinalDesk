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
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생산코드", "Production_Code", colWidth: 100, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "주문서코드", "Order_No", colWidth: 60, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생산 품목코드", "Product_Code", colWidth: 80, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생산 품목명", "Product_Name", colWidth: 300, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생산 계획 수량", "Planned_Qty", colWidth: 100, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생산 완료 수량", "Production_Qty", colWidth: 100, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생산 시작 예정일", "Start_Date", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생산 완료 예정일", "Estimated_Date", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생산 완료일", "Complete_Date", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생산 계획 상태", "Production_Plan_Status", colWidth: 60, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생산계획 담당자", "Production_Plan_User_Name", colWidth: 60, isVisible: false);
            //DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생산 진행 상태", "Production_Status", colWidth: 100, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생성 시간", "Create_Time", colWidth: 60, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생성 사용자", "Create_User_Name", colWidth: 80, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "변경 시간", "Update_Time", colWidth: 60, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "변경 사용자", "Update_User_Name", colWidth: 60, isVisible: false);

            DataGridUtil.SetInitGridView(dataGridView2);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "작업 지시 코드", "Purchase_No", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "작업 품목명", "Product_Code", colWidth: 300, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "공정명", "Product_Name", colWidth: 230, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "설비명", "Product_Type", colWidth: 60, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "자재창고명", "Price", colWidth: 80, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "자재Lot코드", "Unit", colWidth: 90, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "품목 구성 수량", "Qty_PerUnit", colWidth: 90, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "자재 투입 수량", "TotalQty", colWidth: 110, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "생산 수량", "TotalPrice", colWidth: 150, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "작업 지시 수량", "Qty_PerUnit", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "작업 완료 수량", "TotalQty", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "작업 지시 상태", "TotalPrice", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "자재불출여부", "TotalPrice", colWidth: 100, isVisible: false);

            LoadData();

            groupBoxW.Visible = false;
        }
        private void LoadData()
        {
            manufactureList = mSrv.GetmanufactureList();
            dataGridView1.DataSource = manufactureList;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            ProductionCode = Convert.ToInt32(dataGridView1[0, e.RowIndex].Value);
            productCode = dataGridView1[2, e.RowIndex].Value.ToString();

            groupBoxW.Visible = false;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            PopWorkOrderRegister pop = new PopWorkOrderRegister(ProductionCode, productCode, user);
            pop.ShowDialog();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            PopWorkOrderModify pop = new PopWorkOrderModify();
            pop.ShowDialog();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            groupBoxW.Visible = true;
        }
    }
}
