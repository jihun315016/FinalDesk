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
    public partial class frmWarehouse : FormStyle_2
    {
        UserVO user;
        WarehouseService srv;
        string warehouseCode = null;
        List<WarehouseVO> warehouseList;
        List<WarehouseProductVO> warehouseDetailList;
        


        public frmWarehouse()
        {
            InitializeComponent();
            label1.Text = "창고 관리";
        }
        private void frmWarehouse_Load(object sender, EventArgs e)
        {
            this.user = ((frmMain)(this.MdiParent)).userInfo;
            srv = new WarehouseService();

            DataGridUtil.SetInitGridView(dataGridView1);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "창고코드", "Warehouse_Code", colWidth: 200, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "창고명", "Warehouse_Name", colWidth: 300, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "창고유형", "Warehouse_Type", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "창고주소", "Warehouse_Address", colWidth: 300, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생성일자", "Create_Time", colWidth: 60, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생성사용자", "Create_User_Name", colWidth: 80, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "수정일자", "Update_Time", colWidth: 60, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "수정사용자", "Update_User_Name", colWidth: 60, isVisible: false);

            DataGridUtil.SetInitGridView(dataGridView2);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "창고코드", "Warehouse_Code", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "창고명", "Warehouse_Name", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "제품코드", "Product_Code", colWidth: 200, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "제품명", "Product_Name", colWidth: 300, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "보관수량", "Product_Quantity", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "안전재고수량", "Safe_Stock", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleLeft);

            LoadData();
        }
        private void LoadData()
        {
            warehouseList = srv.GetAllWarehouse();
            dataGridView1.DataSource = warehouseList;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            warehouseCode = dataGridView1[0, e.RowIndex].Value.ToString();

            txtCode.Text = dataGridView1["Warehouse_Code", e.RowIndex].Value.ToString();
            txtName.Text = dataGridView1["Warehouse_Name", e.RowIndex].Value.ToString();
            txtType.Text = dataGridView1["Warehouse_Type", e.RowIndex].Value.ToString();
            txtAdress.Text = dataGridView1["Warehouse_Address", e.RowIndex].Value.ToString();
            dtpCreateTime.Text = dataGridView1["Create_Time", e.RowIndex].Value.ToString();
            txtCreateUser.Text = (dataGridView1["Create_User_Name", e.RowIndex].Value ?? string.Empty).ToString();
            dtpModifyTime.Text = (dataGridView1["Update_Time", e.RowIndex].Value ?? string.Empty).ToString();
            txtModifyUser.Text = (dataGridView1["Update_User_Name", e.RowIndex].Value ?? string.Empty).ToString();

            warehouseDetailList = srv.GetWarehouseDetailList(warehouseCode);
            dataGridView2.DataSource = warehouseDetailList;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PopWarehouseRegister pop = new PopWarehouseRegister(user);
            if (pop.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (warehouseCode != null)
            {
                PopWarehouseModify pop = new PopWarehouseModify(warehouseCode, user);
                if (pop.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("변경하실 항목을 선택해주세요");
                return;
            }
        }

        private void btnWarehouseProduct_Click(object sender, EventArgs e)
        {
            if (warehouseCode != null)
            {
                PopWarehouseProduct pop = new PopWarehouseProduct(warehouseCode);
                if (pop.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("창고를 선택해주세요");
                return;
            }
        }
    }
}
