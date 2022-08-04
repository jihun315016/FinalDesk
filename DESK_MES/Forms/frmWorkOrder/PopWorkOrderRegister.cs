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
using DESK_MES.Service;

namespace DESK_MES
{
    public partial class PopWorkOrderRegister : Form
    {
        UserVO user;
        WorkOrderService workSrv;
        ProductService productSrv;
        ManufactureService mSrv;
        int ProductionCode;
        string productCode;
        int baseQty;

        List<ProductVO> isBomProductList;
        List<ManufactureVO> manufactureInfo;

        public PopWorkOrderRegister(int ProductionCode, string productCode, UserVO user)
        {
            InitializeComponent();
            this.user = user;
            this.ProductionCode = ProductionCode;
            this.productCode = productCode;
            workSrv = new WorkOrderService();
            productSrv = new ProductService();
            mSrv = new ManufactureService();

        }

        private void PopWorkOrderRegister_Load(object sender, EventArgs e)
        {
            
            groupBox4.Visible = false;

            DataGridUtil.SetInitGridView(dataGridView1);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "작업 품번", "Product_Code", colWidth: 120); // bom
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생산 품목명", "Product_Name", colWidth: 200); // bom
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "작업 공정코드", "Production_Operation_Code	", colWidth: 150);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "작업 설비코드", "Production_Equipment_Code ", colWidth: 150);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "자재창고명", "Output_Warehouse_Name", colWidth: 150);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "자재Lot코드", "Input_Material_Code", colWidth: 200);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "투입 자재명", "Input_Material_Name", colWidth: 200);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "품목 구성 수량", "Qty", colWidth: 120); // bom
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "원자재 투입 수량", "Input_Material_Qty", colWidth: 120);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "재공품 사용 수량", "Halb_Material_Qty", colWidth: 120);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "작업 그룹코드", "Work_Group_Code", colWidth: 150);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "반제품 보관창고코드", "Halb_Save_Warehouse_Code", colWidth: 150);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생산품 보관창고코드", "Production_Save_WareHouse_Code", colWidth: 150);
            // 해당 품목에 대한 BOM 정보 불러오기
            List<ProductVO> bomList = productSrv.GetChildParentProductList(productCode);
            //List<ProductVO> bomList = productSrv.GetChildParentProductList(productCode).Where(p => p.Product_Type != "ROH").ToList();
            //dataGridView1.DataSource = bomList.Where(b => b.Bom_Type == "자품목").ToList();
            dataGridView1.DataSource = bomList;

            // 해당 생산계획코드에 대한 정보 불러오기
            manufactureInfo = mSrv.GetmanufactureList().Where(p => p.Production_Code == ProductionCode).ToList();
            foreach(ManufactureVO info in manufactureInfo)
            {
                txtManufactureCode.Text = info.Production_Code.ToString();
                txtProductCode.Text = info.Product_Code.ToString();
                txtProductName.Text = info.Product_Name.ToString();
                txtPlanQty.Text = info.Planned_Qty.ToString();
            }

            // 생산품목 보관 창고 지정
            List<PurchaseDetailVO> saveWarehouse = workSrv.GetProductionSaveWarehouse(productCode);
            cboProductWarehouse.DisplayMember = "Warehouse_Name";
            cboProductWarehouse.ValueMember = "Warehouse_Code";
            cboProductWarehouse.DataSource = saveWarehouse;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            groupBox4.Visible = true;

            if (e.RowIndex < 0) return;
            productCode = null;
            productCode = dataGridView1[0, e.RowIndex].Value.ToString();
            txtProductNameInfo.Text = dataGridView1[1, e.RowIndex].Value.ToString();
            //baseQty = Convert.ToInt32(dataGridView1[7, e.RowIndex].Value);
            //int planQty = Convert.ToInt32(txtPlanQty.Text);
            //int workQty = baseQty * planQty;
            //dataGridView1["Input_Material_Qty", e.RowIndex].Value = workQty.ToString();

            if (productCode.Contains("HALB"))
            {
                // 공정(반제품이 아닐 시 선택 불가)
                List<OperationVO> operation = workSrv.GetOperationList(productCode);
                cboOperation.DisplayMember = "Operation_Name";
                cboOperation.ValueMember = "Operation_No";
                cboOperation.DataSource = operation;

                // 작업 그룹 정보
                List<UserGroupVO> workGroup = workSrv.GetWorkGroupList();
                cboWorkGroup.DisplayMember = "User_Group_Name";
                cboWorkGroup.ValueMember = "User_Group_No";
                cboWorkGroup.DataSource = workGroup;

                // 반제품 작업 후 보관될 반제품 창고
                string inputProduct = productCode;
                List<PurchaseDetailVO> InputWarehouse = workSrv.GetInputWarehouse(inputProduct);
                cboInputWarehouse.DisplayMember = "Warehouse_Name";
                cboInputWarehouse.ValueMember = "Warehouse_Code";
                cboInputWarehouse.DataSource = InputWarehouse;
            }
            else
            {
                cboOperation.DataSource = null;
                cboOperation.Enabled = false;

                cboEquipment.DataSource = null;
                cboEquipment.Enabled = false;

                cboWorkGroup.DataSource = null;
                cboWorkGroup.Enabled = false;

                cboInputWarehouse.DataSource = null;
                cboInputWarehouse.Enabled = false;
            }
            
            // 자재창고 콤보박스
            List<PurchaseDetailVO> OutWarehouse = workSrv.GetOutputWarehouse();
            cboOutputWarehouse.DisplayMember = "Warehouse_Name";
            cboOutputWarehouse.ValueMember = "Warehouse_Code";
            cboOutputWarehouse.DataSource = OutWarehouse;


        }

        private void cboOperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboEquipment.Enabled = true;
            cboEquipment.DataSource = null;
            cboEquipment.Items.Clear();
            int operationNo = Convert.ToInt32(cboOperation.SelectedValue);
            // 공정 콤보박스에서 선택된 항목에 따른 설비 정보 가져오기
            List<EquipmentVO> process = workSrv.GetProcessList(operationNo);
            cboEquipment.DisplayMember = "Equipment_Name";
            cboEquipment.ValueMember = "Equipment_No";
            cboEquipment.DataSource = process;
        }

        private void cboOutputWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboMaterialLotName.DataSource = null;
            cboMaterialLotName.Items.Clear();

            // 자재창고에 보관된 원자재의 자재lot코드와 이름 가져오기
            string warehouseCode = cboOutputWarehouse.SelectedValue.ToString();
            List<PurchaseDetailVO> materialLot = workSrv.GetMetarialLotList(warehouseCode);
            cboMaterialLotName.DisplayMember = "Product_Name";
            cboMaterialLotName.ValueMember = "Lot_Code";
            cboMaterialLotName.DataSource = materialLot;
        }

        private void btnAddInfo_Click(object sender, EventArgs e)
        {
            string operationCode = (cboOperation.SelectedValue == null) ? "": cboOperation.SelectedValue.ToString();
            string equipmentCode = (cboEquipment.SelectedValue == null) ? "" : cboEquipment.SelectedValue.ToString();
            string inputMaterialCode = (cboMaterialLotName.SelectedValue == null) ? "" : cboMaterialLotName.SelectedValue.ToString();
            string wrokGroupCode = (cboWorkGroup.SelectedValue == null) ? "" : cboWorkGroup.SelectedValue.ToString();
            string halbMaterialCode = (cboInputWarehouse.SelectedValue == null) ? "" : cboInputWarehouse.SelectedValue.ToString();
            string productionSaveCode = cboProductWarehouse.SelectedValue.ToString();

            DataGridViewRow cRow = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];
            cRow.Cells[2].Value = operationCode;
            cRow.Cells[3].Value = equipmentCode;
            cRow.Cells[5].Value = inputMaterialCode;
            cRow.Cells[10].Value = wrokGroupCode;
            cRow.Cells[11].Value = halbMaterialCode;
            cRow.Cells[12].Value = productionSaveCode;
        }

        private void btnInfoClose_Click(object sender, EventArgs e)
        {
            groupBox4.Visible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ///작업 번호 생성
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
