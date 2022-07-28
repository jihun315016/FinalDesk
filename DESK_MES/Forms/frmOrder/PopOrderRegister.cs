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
    public partial class PopOrderRegister : Form
    {
        UserVO user;
        OrderService srv = null;
        List<ProductVO> allList; // 전체 품목 리스트

        public PopOrderRegister(UserVO user)
        {
            InitializeComponent();
            srv = new OrderService();
            this.user = user;
        }

        private void PopOrderRegister_Load(object sender, EventArgs e)
        {

            DataGridUtil.SetInitGridView(dgvAllProduct);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvAllProduct, "품번", "Product_Code", colWidth: 120);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvAllProduct, "품명", "Product_Name", colWidth: 230);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvAllProduct, "유형", "Product_Type", colWidth: 60);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvAllProduct, "단가", "Price", colWidth: 80);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvAllProduct, "주문단위", "Unit", colWidth: 120);

            DataGridViewButtonColumn btnAddItem = new DataGridViewButtonColumn();
            btnAddItem.Name = "";
            btnAddItem.Text = "추가";
            btnAddItem.Width = 70;
            btnAddItem.DefaultCellStyle.Padding = new Padding(5, 1, 5, 1);
            btnAddItem.UseColumnTextForButtonValue = true;
            dgvAllProduct.Columns.Add(btnAddItem);

            foreach (DataGridViewRow row in dgvAllProduct.Rows)
            {
                row.Cells[6].Value = "Product_Code";
            }
            dgvAllProduct.CellClick += DgvAllProductADD_CellClick;

            DataGridUtil.SetInitGridView(dgvOrderList);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvOrderList, "품번", "Product_Code", colWidth: 120);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvOrderList, "품명", "Product_Name", colWidth: 230);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvOrderList, "유형", "Product_Type", colWidth: 60);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvOrderList, "단가", "Price", colWidth: 80);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvOrderList, "주문단위", "Unit", colWidth: 90);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvOrderList, "수량 입력", "Qty_PerUnit", colWidth: 90);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvOrderList, "총 구매수량", "TotalQty", colWidth: 110);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvOrderList, "총액", "TotalPrice", colWidth: 100);

            dgvOrderList.Columns["Qty_PerUnit"].ReadOnly = false;
            DataGridViewButtonColumn btnAddItem1 = new DataGridViewButtonColumn();
            btnAddItem1.Name = "";
            btnAddItem1.Text = "삭제";
            btnAddItem1.Width = 70;
            btnAddItem1.DefaultCellStyle.Padding = new Padding(5, 1, 5, 1);
            btnAddItem1.UseColumnTextForButtonValue = true;
            dgvOrderList.Columns.Add(btnAddItem1);

            foreach (DataGridViewRow row in dgvOrderList.Rows)
            {
                row.Cells[6].Value = "Product_Code";
            }
            dgvOrderList.CellClick += DgvDeleteOrderList_CellClick;

            List<OrderVO> client = srv.GetClientList();
            cboClient.DisplayMember = "Client_Name";
            cboClient.ValueMember = "Client_Code";
            cboClient.DataSource = client;

            LoadData();
        }

        private void LoadData()
        {
            allList = srv.GetProductListForOrder();
            dgvAllProduct.DataSource = allList;
        }

        private void DgvAllProductADD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                MessageBox.Show("버튼을 선택하세요");
                return;
            }

            if (e.ColumnIndex == dgvAllProduct.Columns[""].Index)
            {
                string code = dgvAllProduct["Product_Code", e.RowIndex].Value.ToString();
                string name = dgvAllProduct["Product_Name", e.RowIndex].Value.ToString();
                string type = dgvAllProduct["Product_Type", e.RowIndex].Value.ToString();
                string Price = dgvAllProduct["Price", e.RowIndex].Value.ToString();
                string Unit = dgvAllProduct["Unit", e.RowIndex].Value.ToString();

                foreach (DataGridViewRow item in dgvOrderList.Rows)
                {
                    string selectedProduce_ID = item.Cells[0].Value.ToString();

                    if (code.Equals(selectedProduce_ID))
                    {
                        MessageBox.Show("이미 추가된 제품입니다.");
                        return;
                    }
                }
                dgvOrderList.Rows.Add(code, name, type, Price, Unit);
            }
        }

        private void DgvDeleteOrderList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                MessageBox.Show("버튼을 선택하세요");
                return;
            }

            if (e.ColumnIndex == dgvOrderList.Columns[""].Index)
            {
                DataGridViewRow dataGridViewRow = dgvOrderList.Rows[e.RowIndex];
                dgvOrderList.Rows.Remove(dataGridViewRow);
            }
        }

        private void dgvOrderList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvOrderList.Columns["Qty_PerUnit"].Index)
            {
                // 그리드뷰 안에서 주문 제품별 총합

                int price = Convert.ToInt32(dgvOrderList["Price", e.RowIndex].Value);
                int unit = Convert.ToInt32(dgvOrderList["Unit", e.RowIndex].Value);
                int Qty = Convert.ToInt32(dgvOrderList["Qty_PerUnit", e.RowIndex].Value);

                dgvOrderList["TotalQty", e.RowIndex].Value = (unit * Qty).ToString();
                dgvOrderList["TotalPrice", e.RowIndex].Value = (price * unit * Qty).ToString();

            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            List<OrderDetailVO> orderList = new List<OrderDetailVO>();

            if (dgvOrderList.Rows.Count < 1)
            {
                MessageBox.Show("주문할 제품을 선택하여 주십시오.");
                return;
            }

            // 주문저장 (Orders, OrderDetails)
            foreach (DataGridViewRow item in dgvOrderList.Rows)
            {
                if (item.Cells[5].Value == null)
                {
                    MessageBox.Show("주문 수량이 0입니다.");
                    return;
                }
                else if (item.Cells[5].Value != null)
                {
                    OrderDetailVO orderitem = new OrderDetailVO
                    {
                        Product_Code = item.Cells[0].Value.ToString(),
                        Qty_PerUnit = Convert.ToInt32(item.Cells[5].Value),
                        TotalQty = Convert.ToInt32(item.Cells[6].Value),
                        TotalPrice = Convert.ToInt32(item.Cells[7].Value)
                    };
                    orderList.Add(orderitem);
                }
            }

            OrderVO order = new OrderVO
            {
                Client_Code = cboClient.SelectedValue.ToString(),
                Order_Date = dtpOrderRegiDate.Value.ToShortDateString(),
                Release_Date = dtpReleaseDate.Value.ToShortDateString(),
                Release_State = "N",
                Create_User_No = user.User_No
            };

            bool result = srv.RegisterOrder(order, orderList);
            if (result)
            {
                MessageBox.Show($"주문이 등록되었습니다.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("주문처리 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
