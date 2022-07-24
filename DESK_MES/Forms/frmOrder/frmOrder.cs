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
    public partial class frmOrder : FormStyle_1
    {
        OrderService srv;
        int orderNo = 0;
        List<OrderVO> orderList;
        List<OrderDetailVO> orderDetailList;

        public frmOrder()
        {
            InitializeComponent();
            label1.Text = "주문 관리";
        }
        private void frmOrder_Load(object sender, EventArgs e)
        {
            srv = new OrderService();

            DataGridUtil.SetInitGridView(dataGridView1);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "주문번호", "Order_No", colWidth: 120);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "거래처코드", "Client_Code", colWidth: 230);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "거래처명", "Client_Name", colWidth: 60);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "주문등록일", "Order_Date", colWidth: 80);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "주문상태", "Order_State", colWidth: 60);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "출고예정일", "Release_Date", colWidth: 120);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "출고상태", "Release_State", colWidth: 230);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생성일자", "Create_Time", colWidth: 60);
            //DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생성사용자", "Create_User_No", colWidth: 80);
            //DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "수정일자", "Update_Time", colWidth: 60);
            //DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "수정사용자", "Update_User_No", colWidth: 60);
            LoadData();
        }
        private void LoadData()
        {
            orderList = srv.GetOrderList();
            dataGridView1.DataSource = orderList;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            PopOrderRegister pop = new PopOrderRegister();
            if (pop.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (orderNo != 0)
            {
                PopOrderModify pop = new PopOrderModify(orderNo);
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            orderNo = Convert.ToInt32(dataGridView1[0, e.RowIndex].Value);

            txtOrderNo.Text = dataGridView1["Order_No", e.RowIndex].Value.ToString();
            txtClientCode.Text = dataGridView1["Client_Code", e.RowIndex].Value.ToString();
            txtClientName.Text = dataGridView1["Client_Name", e.RowIndex].Value.ToString();
            txtOrderRegiDate.Text = dataGridView1["Order_Date", e.RowIndex].Value.ToString();
            txtReleaseDate.Text = dataGridView1["Release_Date", e.RowIndex].Value.ToString();
            txtOrderState.Text = dataGridView1["Order_State", e.RowIndex].Value.ToString();
            txtCreateDate.Text = dataGridView1["Create_Time", e.RowIndex].Value.ToString();
            //txtCreateUser.Text = dataGridView1["Create_User_No", e.RowIndex].Value.ToString();
            //txtModifyDate.Text = dataGridView1["Update_Time", e.RowIndex].Value.ToString();
            //txtModifyUser.Text = dataGridView1["Update_User_No", e.RowIndex].Value.ToString();

            orderDetailList = srv.GetOrderDetailList(orderNo);
            dataGridView2.DataSource = orderDetailList;

        }

        private void btnOrderOK_Click(object sender, EventArgs e)
        {

        }

        private void btnOrderCancle_Click(object sender, EventArgs e)
        {

        }
    }
}
