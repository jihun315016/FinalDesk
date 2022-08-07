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
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "작업 공정코드", "Production_Operation_Code	", colWidth: 150, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "작업 설비코드", "Production_Equipment_Code ", colWidth: 150, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "자재창고명", "Output_Warehouse_Name", colWidth: 150, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "자재Lot코드", "Input_Material_Code", colWidth: 200);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "투입 자재명", "Input_Material_Name", colWidth: 200);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "품목 구성 수량", "Qty", colWidth: 200); // bom
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "원자재 투입 수량", "Input_Material_Qty", colWidth: 200);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "재공품 사용 수량", "Halb_Material_Qty", colWidth: 200);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "작업 그룹코드", "Work_Group_Code", colWidth: 150, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "작업 그룹명", "Work_Group_Name", colWidth: 150);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "반제품 보관창고코드", "Halb_Save_Warehouse_Code", colWidth: 150, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생산품 보관창고코드", "Production_Save_WareHouse_Code", colWidth: 150, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "작업수량", "Work_Plan_Qty", colWidth: 150, isVisible: false);
            // 해당 품목에 대한 BOM 정보 불러오기
            //List<ProductVO> bomList = productSrv.GetChildParentProductList(productCode);
            List<ProductVO> bomList = productSrv.GetChildParentProductList(productCode).Where(p => p.Product_Type != "ROH").ToList();
            dataGridView1.DataSource = bomList.Where(b => b.Bom_Type == "자품목").ToList();
            //dataGridView1.DataSource = bomList;

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
            
            baseQty = Convert.ToInt32(dataGridView1[7, e.RowIndex].Value); // bom 구성수량

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
            //cboMaterialLotName.Enabled = true;
            //cboMaterialLotName.DataSource = null;
            //cboMaterialLotName.Items.Clear();

            // 자재창고에 보관된 원자재의 자재lot코드와 이름 가져오기
            string warehouseCode = cboOutputWarehouse.SelectedValue.ToString();
            List<PurchaseDetailVO> material = workSrv.GetMetarialList(warehouseCode);
            cboMaterialLotName.DisplayMember = "Product_Name";
            cboMaterialLotName.ValueMember = "Product_Code";
            cboMaterialLotName.DataSource = material;
        }

        private void cboMaterialLotName_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboselectMaterialLot.Enabled = true;

            // 창고에 보관된 자재에 해당하는 자재 Lot 목록 가져오기
            string productCode = cboMaterialLotName.SelectedValue.ToString();
            List<PurchaseDetailVO> selectLot = workSrv.GetMetarialLotList(productCode);
            cboselectMaterialLot.DisplayMember = "Lot_Code";
            cboselectMaterialLot.ValueMember = "Lot_Code";
            cboselectMaterialLot.DataSource = selectLot;
        }



        private void nmrWorkQty_ValueChanged(object sender, EventArgs e)
        {
            DataGridViewRow cRow = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];
            int planQty = Convert.ToInt32(nmrWorkQty.Text);
            int workQty = baseQty * planQty;
            cRow.Cells[8].Value = workQty;
            cRow.Cells[9].Value = workQty;
        }

        private void btnAddInfo_Click(object sender, EventArgs e)
        {
            string operationCode = (cboOperation.SelectedValue == null) ? "0" : cboOperation.SelectedValue.ToString();
            string equipmentCode = (cboEquipment.SelectedValue == null) ? "0" : cboEquipment.SelectedValue.ToString();
            string inputMaterialCode = (cboselectMaterialLot.SelectedValue == null) ? "0" : cboselectMaterialLot.SelectedValue.ToString();
            string inputMaterialName = (cboMaterialLotName.Text == null) ? "" : cboMaterialLotName.Text.ToString();
            string wrokGroupCode = (cboWorkGroup.SelectedValue == null) ? "0" : cboWorkGroup.SelectedValue.ToString();
            string wrokGroupName = (cboWorkGroup.Text == null) ? "" : cboWorkGroup.Text.ToString();
            string halbMaterialCode = (cboInputWarehouse.SelectedValue == null) ? "0" : cboInputWarehouse.SelectedValue.ToString();
            string productionSaveCode = cboProductWarehouse.SelectedValue.ToString();
            string workPalnQty = nmrWorkQty.Value.ToString();

            DataGridViewRow cRow = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];
            cRow.Cells[2].Value = operationCode;
            cRow.Cells[3].Value = equipmentCode;
            cRow.Cells[5].Value = inputMaterialCode;
            cRow.Cells[6].Value = inputMaterialName;
            cRow.Cells[10].Value = wrokGroupCode;
            cRow.Cells[11].Value = wrokGroupName;
            cRow.Cells[12].Value = halbMaterialCode;
            cRow.Cells[13].Value = productionSaveCode;
            cRow.Cells[14].Value = workPalnQty;
            groupBox4.Visible = false;

            // 클릭하면 콤보박스들 바인딩 초기화

        }

        private void btnInfoClose_Click(object sender, EventArgs e)
        {
            groupBox4.Visible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            List<WorkOrderVO> workList = new List<WorkOrderVO>();

            // 작업번호 
            //string WorkID = "WORK_20220806_00001";
            WorkOrderVO WorkID = workSrv.GetLastID();
            
            //작업 번호 생성
            string id = WorkID.Work_Code.ToString();
            string[] search = id.Split(new char[] { '_' });
            string wokrcode = search[0];
            string getDate = search[1];
            string date = DateTime.Now.ToString("yyyyMMdd");

            List<string> idlist = new List<string>();

            if (getDate.Equals(date))
            {
                int addID = int.Parse(search[2]);
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    addID++;
                    string newid = addID.ToString().PadLeft(4, '0');
                    idlist.Add(wokrcode + '_' + date + '_' + newid);
                }

                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    WorkOrderVO work = new WorkOrderVO
                    {
                        Production_No = Convert.ToInt32(txtManufactureCode.Text),
                        Product_Code = item.Cells[0].Value.ToString(),
                        Production_Operation_Code = Convert.ToInt32(item.Cells[2].Value),
                        Production_Equipment_Code = Convert.ToInt32(item.Cells[3].Value),
                        Input_Material_Code = item.Cells[5].Value.ToString(),
                        Input_Material_Qty = Convert.ToInt32(item.Cells[8].Value),
                        Halb_Material_Qty = Convert.ToInt32(item.Cells[9].Value),
                        Work_Group_Code = Convert.ToInt32(item.Cells[10].Value),
                        Halb_Save_Warehouse_Code = item.Cells[12].Value.ToString(),
                        Production_Save_WareHouse_Code = item.Cells[13].Value.ToString(),
                        Work_Plan_Qty = Convert.ToInt32(item.Cells[14].Value),
                        Work_Paln_Date = dtpWorkOrderDate.Value.ToShortDateString(),
                        Start_Due_Date = dtpWorkStartDueDate.Value.ToShortDateString(),
                        Complete_Due_Date = dtpWorkEndDueDate.Value.ToShortDateString(),
                        Work_State = "미정",
                        Material_Lot_Input_State = "대기",
                        Create_User_No = user.User_No
                    };
                    workList.Add(work);
                }
            }
            else
            {
                int addID = 0;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    addID++;
                    string newid = addID.ToString().PadLeft(4, '0');
                    idlist.Add(wokrcode + '_' + date + '_' + newid);
                }

                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    WorkOrderVO work = new WorkOrderVO
                    {
                        Production_No = Convert.ToInt32(txtManufactureCode.Text),
                        Product_Code = item.Cells[0].Value.ToString(),
                        Production_Operation_Code = Convert.ToInt32(item.Cells[2].Value),
                        Production_Equipment_Code = Convert.ToInt32(item.Cells[3].Value),
                        Input_Material_Code = item.Cells[5].Value.ToString(),
                        Input_Material_Qty = Convert.ToInt32(item.Cells[8].Value),
                        Halb_Material_Qty = Convert.ToInt32(item.Cells[9].Value),
                        Work_Group_Code = Convert.ToInt32(item.Cells[10].Value),
                        Halb_Save_Warehouse_Code = item.Cells[12].Value.ToString(),
                        Production_Save_WareHouse_Code = item.Cells[13].Value.ToString(),
                        Work_Plan_Qty = Convert.ToInt32(item.Cells[14].Value),
                        Work_Paln_Date = dtpWorkOrderDate.Value.ToShortDateString(),
                        Start_Due_Date = dtpWorkStartDueDate.Value.ToShortDateString(),
                        Complete_Due_Date = dtpWorkEndDueDate.Value.ToShortDateString(),
                        Work_State = "미정",
                        Material_Lot_Input_State = "대기",
                        Create_User_No = user.User_No
                    };
                    workList.Add(work);
                }
            }

            bool result = workSrv.RegisterWorkOrderList(workList, idlist);
            if (result)
            {
                MessageBox.Show($"작업지시가 등록되었습니다.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("작업지시 처리 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }






        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
