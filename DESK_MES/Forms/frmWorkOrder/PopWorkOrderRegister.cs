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


            DataGridUtil.SetInitGridView(dataGridView1);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "작업 품번", "Product_Code", colWidth: 120); // bom
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생산 품목명", "Product_Name", colWidth: 200); // bom
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "공정명", "Production_Operation_Name", colWidth: 150, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "설비명", "Production_Equipment_Name", colWidth: 150, isVisible:false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "자재창고명", "Output_Warehouse_Name", colWidth: 150, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "자재Lot코드", "Input_Material_Code", colWidth: 200);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "투입 자재명", "Input_Material_Name", colWidth: 200);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "품목 구성 수량", "Qty", colWidth: 120); // bom
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "자재 투입 수량", "Input_Material_Qty", colWidth: 120);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생산 예상 수량", "Production_Qty", colWidth: 120);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "작업 그룹명", "Work_Group_Name", colWidth: 150, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "보관창고명", "Input_Warehouse_Name", colWidth: 150, isVisible: false);

            // 해당 품목에 대한 BOM 정보 불러오기
            List<ProductVO> bomList = productSrv.GetChildParentProductList(productCode).Where(p => p.Product_Type != "ROH").ToList();
            dataGridView1.DataSource = bomList.Where(b => b.Bom_Type == "자품목").ToList();

            // 해당 생산계획코드에 대한 정보 불러오기
            manufactureInfo = mSrv.GetmanufactureList().Where(p => p.Production_Code == ProductionCode).ToList();
            foreach(ManufactureVO info in manufactureInfo)
            {
                txtManufactureCode.Text = info.Production_Code.ToString();
                txtProductCode.Text = info.Product_Code.ToString();
                txtProductName.Text = info.Product_Name.ToString();
                txtPlanQty.Text = info.Planned_Qty.ToString();
                dtpPlanDate.Value = Convert.ToDateTime(info.Start_Date);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0) return;
            productCode = null;
            productCode = dataGridView1[0, e.RowIndex].Value.ToString();
            txtProductNameInfo.Text = dataGridView1[1, e.RowIndex].Value.ToString();
            baseQty = Convert.ToInt32(dataGridView1[7, e.RowIndex].Value);
            //생산 예정 수량 입력해라

            int planQty = Convert.ToInt32(txtPlanQty.Text);
            int workQty = baseQty * planQty;
            dataGridView1["Input_Material_Qty", e.RowIndex].Value = workQty.ToString();

            List<OperationVO> operation = workSrv.GetOperationList(productCode);
            cboOperation.DisplayMember = "Operation_Name";
            cboOperation.ValueMember = "Operation_No";
            cboOperation.DataSource = operation;

            List<PurchaseDetailVO> OutWarehouse = workSrv.GetOutputWarehouse(productCode);
            cboOutputWarehouse.DisplayMember = "Warehouse_Name";
            cboOutputWarehouse.ValueMember = "Warehouse_Code";
            cboOutputWarehouse.DataSource = OutWarehouse;

            string inputProduct = txtProductCode.Text.ToString();
            List<PurchaseDetailVO> InputWarehouse = workSrv.GetOutputWarehouse(inputProduct);
            cboInputWarehouse.DisplayMember = "Warehouse_Name";
            cboInputWarehouse.ValueMember = "Warehouse_Code";
            cboInputWarehouse.DataSource = InputWarehouse;
        }

        private void cboOperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            int operationNo = Convert.ToInt32(cboOperation.SelectedValue);

            List<EquipmentVO> process = workSrv.GetProcessList(operationNo);
            cboEquipment.DisplayMember = "Equipment_Name";
            cboEquipment.ValueMember = "Equipment_No";
            cboEquipment.DataSource = process;
        }

        private void cboOutputWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            string warehouseCode = cboOutputWarehouse.SelectedValue.ToString();
            List<PurchaseDetailVO> materialLot = workSrv.GetMetarialLotList(warehouseCode);
            cboMaterialLotName.DisplayMember = "Product_Name";
            cboMaterialLotName.ValueMember = "Lot_Code";
            cboMaterialLotName.DataSource = materialLot;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
        }
    }
}
