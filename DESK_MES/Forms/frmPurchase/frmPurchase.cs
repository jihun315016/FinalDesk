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
    public partial class frmPurchase : FormStyle_1
    {
        PurchaseService srv;
        int purchaseNo = 0;
        List<PurchaseVO> purchaseList;
        List<PurchaseDetailVO> purchaseDetailList;


        public frmPurchase()
        {
            InitializeComponent();
            label1.Text = "발주 관리";
        }
        private void frmPurchase_Load(object sender, EventArgs e)
        {
            srv = new PurchaseService();

            DataGridUtil.SetInitGridView(dataGridView1);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "발주코드", "Purchase_No", colWidth: 120);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "거래처번호", "Client_Code", colWidth: 230);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "거래처명", "Client_Name", colWidth: 60);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "발주등록일", "Purchase_Date", colWidth: 80);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "발주상태", "Purchase_State", colWidth: 60);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "입고예정일", "IncomingDue_date", colWidth: 120);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "입고상태", "Is_Incoming", colWidth: 60);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "입고일", "Incoming_Date", colWidth: 120);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "등록일자", "Create_Time", colWidth: 60);
            //DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "등록사용자", "Create_User_No", colWidth: 80);
            //DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "수정일자", "Update_Time", colWidth: 60);
            //DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "수정사용자", "Update_User_No", colWidth: 60);
            
            
            LoadData();
        }
        private void LoadData()
        {
            purchaseList = srv.GetPurchaseList();
            dataGridView1.DataSource = purchaseList;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            PopPurchaseRegister pop = new PopPurchaseRegister(); ;
            if (pop.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            PopPurchaseModify pop = new PopPurchaseModify();
            if (pop.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnIncoming_Click(object sender, EventArgs e)
        {

        }


    }
}
