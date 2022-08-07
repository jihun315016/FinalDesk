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
    public partial class PopIncomingCreateLot : Form
    {
        IncomingService srv;
        PurchaseService pursrv;
        int purchaseCode;
        PurchaseDetailVO LastID;
        string productCode;
        List<PurchaseDetailVO> WareHouseInfo;
        
        public PopIncomingCreateLot(int purchaseNo)
        {
            InitializeComponent();

            srv = new IncomingService();
            pursrv = new PurchaseService();
            purchaseCode = purchaseNo;
            
        }

        private void PopIncomingCreateLot_Load(object sender, EventArgs e)
        {
            panel4.Visible = false;

            DataGridUtil.SetInitGridView(dataGridView1);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "품번", "Product_Code", colWidth: 120);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "품명", "Product_Name", colWidth: 230);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "유형", "Product_Type", colWidth: 60);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "단가", "Price", colWidth: 80, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "주문단위", "Unit", colWidth: 90, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "수량 입력", "Qty_PerUnit", colWidth: 90, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "총 구매수량", "TotalQty", colWidth: 110);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "총액", "TotalPrice", colWidth: 100, isVisible:false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "보관 창고 코드", "Warehouse_Code", colWidth: 100);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "보관 창고명", "Warehouse_Name", colWidth: 100);
            

            List<PurchaseDetailVO> detail = pursrv.GetPurchaseDetailList(purchaseCode);
            dataGridView1.DataSource = detail;

            PurchaseVO Info = pursrv.GetPurchaseInfoByPurchaseCode(purchaseCode);
            txtPurchaseCode.Text = Info.Purchase_No.ToString();
            txtClientCode.Text = Info.Client_Code.ToString();
            txtClientName.Text = Info.Client_Name.ToString();



        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //LastID = "ROH_20220726_00000";
            LastID = srv.GetLastID();

            List<PurchaseDetailVO> lotList = new List<PurchaseDetailVO>();

            string id = LastID.Lot_Code.ToString();
            string[] search = id.Split(new char[] { '_' });
            string prodcode = search[0];
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
                    idlist.Add(prodcode + '_' + date + '_' + newid);
                }

                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    PurchaseDetailVO purchaseItem = new PurchaseDetailVO
                    {
                        Product_Code = item.Cells[0].Value.ToString(),
                        Lot_Qty = Convert.ToInt32(item.Cells[6].Value),
                        Cur_Qty = Convert.ToInt32(item.Cells[6].Value),
                        Client_Code = txtClientCode.Text,
                        Warehouse_Code = item.Cells[8].Value.ToString()
                    };
                    lotList.Add(purchaseItem);
                }
            }
            else
            {
                int addID = 0;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    addID++;
                    string newid = addID.ToString().PadLeft(4, '0');
                    idlist.Add(prodcode + '_' + date + '_' + newid);
                }

                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    PurchaseDetailVO purchaseItem = new PurchaseDetailVO
                    {
                        Product_Code = item.Cells[0].Value.ToString(),
                        Lot_Qty = Convert.ToInt32(item.Cells[6].Value),
                        Cur_Qty = Convert.ToInt32(item.Cells[6].Value),
                        Client_Code = txtClientCode.Text,
                        Warehouse_Code = item.Cells[8].Value.ToString()
                    };
                    lotList.Add(purchaseItem);
                }
            }

            PurchaseVO puchaseinfo = new PurchaseVO
            {
                Purchase_No = Convert.ToInt32(txtPurchaseCode.Text),
                Is_Incoming = "Y",
                Incoming_Date = dateTimePicker1.Value.ToShortDateString()
            };

            bool result = srv.RegisterIncomingPurchase(puchaseinfo, idlist, lotList);
            if (result)
            {
                MessageBox.Show($"입고처리 되었습니다.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("입고처리 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<PurchaseDetailVO> InsertWH = new List<PurchaseDetailVO>();
            panel4.Visible = false;
            groupBox1.Visible = true;
            // 선택한 창고를 입력 
            string code = cboWarehouse.SelectedValue.ToString();
            string name = cboWarehouse.Text;

            DataGridViewRow cRow = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];
            cRow.Cells[8].Value = code;
            cRow.Cells[9].Value = name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
            groupBox1.Visible = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cboWarehouse.DataSource = null;
            cboWarehouse.Text = null;
            cboWarehouse.Items.Clear();

            productCode = dataGridView1[0, e.RowIndex].Value.ToString();
            PurchaseDetailVO incomingProductInfo = srv.GetIncomingProductInfo(productCode);
            WareHouseInfo = srv.GetEqualWarehouse(productCode);
            cboWarehouse.DisplayMember = "Warehouse_Name";
            cboWarehouse.ValueMember = "Warehouse_Code";
            cboWarehouse.DataSource = WareHouseInfo;

            txtProductCode.Text = incomingProductInfo.Product_Code;
            txtProductName.Text = incomingProductInfo.Product_Name;
            txtTotalQty.Text = incomingProductInfo.TotalQty.ToString();
            groupBox1.Visible = false;
            panel4.Visible = true;
        }
    }
}
