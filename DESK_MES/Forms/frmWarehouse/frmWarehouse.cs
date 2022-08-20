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
        string productCode = null;
        List<WarehouseVO> warehouseList;
        List<WarehouseProductVO> warehouseDetailList;
        List<PurchaseDetailVO> lotDetailList;



        public frmWarehouse()
        {
            InitializeComponent();
            label1.Text = "창고 관리";
        }
        private void frmWarehouse_Load(object sender, EventArgs e)
        {
            this.user = ((frmMain)(this.MdiParent)).userInfo;
            srv = new WarehouseService();

            comboBox1.Items.AddRange(new string[] { "선택", "창고명", "창고유형" });
            comboBox1.SelectedIndex = 0;

            dtpCreateTime.Format = DateTimePickerFormat.Custom;
            dtpCreateTime.CustomFormat = " ";
            
            dtpModifyTime.Format = DateTimePickerFormat.Custom;
            dtpModifyTime.CustomFormat = " ";

            DataGridUtil.SetInitGridView(dataGridView1);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "창고코드", "Warehouse_Code", colWidth: 200, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "창고명", "Warehouse_Name", colWidth: 300, alignContent: DataGridViewContentAlignment.MiddleLeft);
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
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "제품명", "Product_Name", colWidth: 300, alignContent: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "보관수량", "Product_Quantity", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "안전재고수량", "Safe_Stock", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleLeft);

            DataGridUtil.SetInitGridView(dataGridView3);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView3, "자재 Lot 코드", "Lot_Code", colWidth: 140, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView3, "제품명", "Product_Name", colWidth: 215, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView3, "수량", "Cur_Qty", colWidth: 65, alignContent: DataGridViewContentAlignment.MiddleCenter);

            dataGridView3.Visible = false;
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

            dtpCreateTime.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";
            dtpModifyTime.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";

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
            dataGridView3.Visible = false;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            productCode = dataGridView2[2, e.RowIndex].Value.ToString();

            lotDetailList = srv.GetLotDetailList(productCode);
            dataGridView3.DataSource = lotDetailList;
            dataGridView3.Visible = true;
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<WarehouseVO> list = warehouseList.Where(p => 1 == 1).ToList();
            // 상세 검색으로 필터링
            if (panel5.Visible)
            {
                //if (!string.IsNullOrWhiteSpace(txtOrderCode.Text.Trim()))
                //    list = list.Where(p => p.Client_Code.Contains(txtClientCode.Text.ToLower())).ToList();

                //if (!string.IsNullOrWhiteSpace(txtClientName.Text.Trim()))
                //    list = list.Where(p => p.Client_Name.ToLower().Contains(txtClientName.Text.ToLower())).ToList();

                //if (cboOrderState.SelectedIndex > 0)
                //    list = list.Where(p => p.Order_State == cboOrderState.SelectedValue.ToString().Split('_')[1]).ToList();
            }
            // 일반 검색으로 필터링
            else
            {
                // 거래처코드 검색
                if (comboBox1.SelectedIndex == 1)
                    list = list.Where(p => p.Warehouse_Name.ToLower().Contains(textBox1.Text.ToLower())).ToList();

                // 창고유형 검색
                else if (comboBox1.SelectedIndex == 2)
                    list = list.Where(p => p.Warehouse_Type.ToLower().Contains(textBox1.Text.ToLower())).ToList();
            }

            dataGridView1.DataSource = list;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            warehouseList = srv.GetAllWarehouse();
            comboBox1.SelectedIndex = 0;
            textBox1.Text = string.Empty;
            comboBox1.Enabled = textBox1.Enabled = true;
            panel5.Visible = false;
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = warehouseList;

            dataGridView2.DataSource = null;
            dataGridView3.DataSource = null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //엑셀
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Execl Files(*.xls)|*.xls";
            dlg.Title = "엑셀파일로 내보내기";

            List<WarehouseVO> list = dataGridView1.DataSource as List<WarehouseVO>;
            if (list == null)
            {
                MessageBox.Show("조회 항목이 없습니다.");
                return;
            }

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ExcelUtil excel = new ExcelUtil();
                List<WarehouseVO> output = list;

                string[] columnImport = { "Warehouse_Code", "Warehouse_Name", "Warehouse_Type", "Warehouse_Address"};
                string[] columnName = { "창고코드", "창고명", "창고유형", "창고주소" };

                if (excel.ExportList(output, dlg.FileName, columnImport, columnName))
                {
                    MessageBox.Show("엑셀 다운로드 완료");
                }
                else
                {
                    MessageBox.Show("엑셀 다운 실패");
                }
            }
        }

        private void frmWarehouse_Shown(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }
    }
}
