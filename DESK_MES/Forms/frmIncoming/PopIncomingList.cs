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
    public partial class PopIncomingList : Form
    {
        IncomingService srv;
         
        List<PurchaseDetailVO> incomingList;

        public PopIncomingList()
        {
            InitializeComponent();
            srv = new IncomingService();
        }

        private void PopIncomingList_Load(object sender, EventArgs e)
        {
            DataGridUtil.SetInitGridView(dataGridView1);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "발주코드", "Purchase_No", colWidth: 120);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "자재 LOT", "Lot_Code", colWidth: 120);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "품번", "Product_Code", colWidth: 120);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "품명", "Product_Name", colWidth: 230);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "입고수량", "Lot_Qty", colWidth: 110);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "창고명", "Warehouse_Name", colWidth: 100);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "입고완료일", "Create_Time", colWidth: 100);
            //DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "입고담당자", "Warehouse_Name", colWidth: 100);

            LoadData();
        }
        private void LoadData()
        {
            //incomingList = srv.GetIncomingList();
            //dataGridView1.DataSource = incomingList;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
