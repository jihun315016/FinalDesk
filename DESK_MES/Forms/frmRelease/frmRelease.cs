﻿using System;
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
    public partial class frmRelease : FormStyle_1
    {
        UserVO user;
        ReleaseService srv;
        int orderNo = 0;
        List<ReleaseVO> releaseList;
        List<OrderDetailVO> orderDetailList;

        public frmRelease()
        {
            InitializeComponent();
            label1.Text = "출고 관리";
        }

        private void frmRelease_Load(object sender, EventArgs e)
        {
            srv = new ReleaseService();
            this.user = ((frmMain)(this.MdiParent)).userInfo;

            DataGridUtil.SetInitGridView(dataGridView1);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "주문번호", "Order_No", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "거래처코드", "Client_Code", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "거래처명", "Client_Name", colWidth: 200, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "주문등록일", "Order_Date", colWidth: 200, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "주문상태", "Order_State", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "출고예정일", "Release_Date", colWidth: 200, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "출고상태", "Release_State", colWidth: 100, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "출고완료일", "Release_OK_Date", colWidth: 200, alignContent: DataGridViewContentAlignment.MiddleCenter);

            DataGridUtil.SetInitGridView(dataGridView2);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "품번", "Product_Code", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "품명", "Product_Name", colWidth: 200, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "유형", "Product_Type", colWidth: 100, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "단가", "Price", colWidth: 100, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "주문단위", "Unit", colWidth: 100, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "수량 입력", "Qty_PerUnit", colWidth: 100, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "총 구매수량", "TotalQty", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "총액", "TotalPrice", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);

            LoadData();
        }

        private void LoadData()
        {
            releaseList = srv.GetReleaseList();
            dataGridView1.DataSource = releaseList;
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
            txtRelease_State.Text = dataGridView1["Release_State", e.RowIndex].Value.ToString();
            //txtRelease_OK_Date.Text = dataGridView1["Release_OK_Date", e.RowIndex].Value.ToString();

            orderDetailList = srv.GetOrderDetailList(orderNo);
            dataGridView2.DataSource = orderDetailList;
        }

        private void btnReleaseAdd_Click(object sender, EventArgs e)
        {
            PopReleaseRegister pop = new PopReleaseRegister(user);
            if (pop.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnBarcord_Click(object sender, EventArgs e)
        {
            PopBarcode pop = new PopBarcode();
            if (pop.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }
    }
}
