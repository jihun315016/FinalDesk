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
        PurchaseService srv;
        int purchaseCode;
        List<PurchaseDetailVO> lotList;
        public PopIncomingCreateLot(int purchaseNo)
        {
            InitializeComponent();

            srv = new PurchaseService();
            purchaseCode = purchaseNo;
            
        }

        private void PopIncomingCreateLot_Load(object sender, EventArgs e)
        {
            DataGridUtil.SetInitGridView(dataGridView1);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "발주코드", "Purchase_No", colWidth: 120);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "품번", "Product_Code", colWidth: 120);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "품명", "Product_Name", colWidth: 230);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "유형", "Product_Type", colWidth: 60);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "단가", "Price", colWidth: 80, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "주문단위", "Unit", colWidth: 90, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "수량 입력", "Qty_PerUnit", colWidth: 90, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "총 구매수량", "TotalQty", colWidth: 110);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "총액", "TotalPrice", colWidth: 100, isVisible:false);

            List<PurchaseDetailVO> detail = srv.GetPurchaseDetailList(purchaseCode);
            dataGridView1.DataSource = detail;

            PurchaseVO Info = srv.GetPurchaseInfoByPurchaseCode(purchaseCode);
            txtPurchaseCode.Text = Info.Purchase_No.ToString();
            txtClientCode.Text = Info.Client_Code.ToString();
            txtClientName.Text = Info.Client_Name.ToString();
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string nowid = "ROH_20220726_00000";

            string[] search = nowid.Split(new char[] { '_' });
            string prodcode = search[0];
            string date = DateTime.Now.ToString("yyyyMMdd");
            int addID = int.Parse(search[2]);

            List<string> list = new List<string>();
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                addID++;
                string newid = addID.ToString().PadLeft(4, '0');
                list.Add(prodcode + '_' + date + '_' + newid);
            }

            for (int i = 0; i < list.Count; i++)
            {
                MessageBox.Show(list[i]);
            }

            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                if (item.Cells[5].Value != null)
                {
                    PurchaseDetailVO orderitem = new PurchaseDetailVO
                    {
                        Product_Code = item.Cells[0].Value.ToString(),
                        Qty_PerUnit = Convert.ToInt32(item.Cells[5].Value),
                        TotalQty = Convert.ToInt32(item.Cells[6].Value),
                        TotalPrice = Convert.ToInt32(item.Cells[7].Value)
                    };
                    lotList.Add(orderitem);
                }
            }

            //PurchaseVO order = new PurchaseVO
            //{
            //    Client_Code = txtClientCode.Text.ToString(),
            //    Order_Date = dtpOrderRegiDate.Value.ToShortDateString(),
            //    Release_Date = dtpReleaseDate.Value.ToShortDateString(),
            //    Release_State = "출고 대기",
            //    Create_Time = dtpCreateDate.Value.ToShortDateString()
            //};

            //bool result = srv.RegisterOrder(order, orderList);
            //if (result)
            //{
            //    MessageBox.Show($"주문이 등록되었습니다.");
            //    this.DialogResult = DialogResult.OK;
            //    this.Close();
            //}
            //else
            //{
            //    MessageBox.Show("주문처리 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            //}

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
