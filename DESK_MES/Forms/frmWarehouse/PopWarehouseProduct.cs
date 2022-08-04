using DESK_DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DESK_MES.Service;

namespace DESK_MES
{
    public partial class PopWarehouseProduct : Form
    {
        WarehouseService srv = null;
        List<ProductVO> allList; // 전체 품목 리스트

        public PopWarehouseProduct(string warehouseCode)
        {
            InitializeComponent();

            srv = new WarehouseService();

            WarehouseVO info = srv.GetWarehouseInfoByCode(warehouseCode);

            txtCode.Text = info.Warehouse_Code;
            txtName.Text = info.Warehouse_Name;
            txtType.Text = info.Warehouse_Type;
            txtAdress.Text = info.Warehouse_Address;
        }

        private void PopWarehouseProduct_Load(object sender, EventArgs e)
        {

            DataGridUtil.SetInitGridView(dgvProduct);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvProduct, "품번", "Product_Code", colWidth: 120);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvProduct, "품명", "Product_Name", colWidth: 230);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvProduct, "유형", "Product_Type", colWidth: 60);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvProduct, "가격", "Price", colWidth: 80);

            DataGridViewButtonColumn btnAddItem = new DataGridViewButtonColumn();
            btnAddItem.Name = "";
            btnAddItem.Text = "추가";
            btnAddItem.Width = 70;
            btnAddItem.DefaultCellStyle.Padding = new Padding(5, 1, 5, 1);
            btnAddItem.UseColumnTextForButtonValue = true;
            dgvProduct.Columns.Add(btnAddItem);

            foreach (DataGridViewRow row in dgvProduct.Rows)
            {
                row.Cells[6].Value = "Product_Code";
            }
            dgvProduct.CellClick += DgvProductADD_CellClick;

            
            DataGridUtil.SetInitGridView(dgvSelectedProduct);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvSelectedProduct, "품번", "Product_Code", colWidth: 120);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvSelectedProduct, "품명", "Product_Name", colWidth: 230);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvSelectedProduct, "유형", "Product_Type", colWidth: 60);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvSelectedProduct, "보관수량", "Product_Quantity", colWidth: 90);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvSelectedProduct, "안전재고수량", "Safe_Stock", colWidth: 110);

            dgvSelectedProduct.Columns["Product_Quantity"].ReadOnly = false;
            dgvSelectedProduct.Columns["Safe_Stock"].ReadOnly = false;
            DataGridViewButtonColumn btnAddItem1 = new DataGridViewButtonColumn();
            btnAddItem1.Name = "";
            btnAddItem1.Text = "삭제";
            btnAddItem1.Width = 70;
            btnAddItem1.DefaultCellStyle.Padding = new Padding(5, 1, 5, 1);
            btnAddItem1.UseColumnTextForButtonValue = true;
            dgvSelectedProduct.Columns.Add(btnAddItem1);

            foreach (DataGridViewRow row in dgvSelectedProduct.Rows)
            {
                row.Cells[6].Value = "Product_Code";
            }
            dgvSelectedProduct.CellClick += DgvDeleteSelectedProduct_CellClick;

            LoadData();
        }
        private void LoadData()
        {
            allList = srv.GetProductListForWarehouse();
            dgvProduct.DataSource = allList;
        }
        private void DgvDeleteSelectedProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                MessageBox.Show("버튼을 선택하세요");
                return;
            }

            if (e.ColumnIndex == dgvSelectedProduct.Columns[""].Index)
            {
                DataGridViewRow dataGridViewRow = dgvSelectedProduct.Rows[e.RowIndex];
                dgvSelectedProduct.Rows.Remove(dataGridViewRow);
            }
        }



        private void DgvProductADD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                MessageBox.Show("버튼을 선택하세요");
                return;
            }

            if (e.ColumnIndex == dgvProduct.Columns[""].Index)
            {
                string code = dgvProduct["Product_Code", e.RowIndex].Value.ToString();
                string name = dgvProduct["Product_Name", e.RowIndex].Value.ToString();
                string type = dgvProduct["Product_Type", e.RowIndex].Value.ToString();

                foreach (DataGridViewRow item in dgvSelectedProduct.Rows)
                {
                    string selectedProduce_ID = item.Cells[0].Value.ToString();

                    if (code.Equals(selectedProduce_ID))
                    {
                        MessageBox.Show("이미 추가된 제품입니다.");
                        return;
                    }
                }
                dgvSelectedProduct.Rows.Add(code, name, type);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            List<WarehouseProductVO> saveList = new List<WarehouseProductVO>();
            
            // 창고저장 
            foreach (DataGridViewRow list in dgvSelectedProduct.Rows)
            {
                if (list.Cells[5].Value == null)
                {
                    MessageBox.Show("안전재고 수량이 0입니다.");
                    return;
                }
                else if (list.Cells[5].Value != null)
                {
                    WarehouseProductVO saveProduct = new WarehouseProductVO
                    {
                        Product_Code = list.Cells[0].Value.ToString(),
                        Product_Quantity = Convert.ToInt32(list.Cells[3].Value),
                        Safe_Stock = Convert.ToInt32(list.Cells[4].Value),
                    };
                    saveList.Add(saveProduct);
                }
            }
            string warehouseNo = txtCode.Text;
            bool result = srv.SaveProductInWarehouse(warehouseNo, saveList);
            if (result)
            {
                MessageBox.Show($"창고에 품목이 등록되었습니다");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("처리 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
