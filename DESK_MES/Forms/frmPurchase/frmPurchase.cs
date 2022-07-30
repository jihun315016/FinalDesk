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
        UserVO user;

        public frmPurchase()
        {
            InitializeComponent();
            label1.Text = "발주 관리";
        }
        private void frmPurchase_Load(object sender, EventArgs e)
        {
            this.user = ((frmMain)(this.MdiParent)).userInfo;
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

            DataGridUtil.SetInitGridView(dataGridView2);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "발주코드", "Purchase_No", colWidth: 120);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "품번", "Product_Code", colWidth: 120);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "품명", "Product_Name", colWidth: 230);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "유형", "Product_Type", colWidth: 60);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "단가", "Price", colWidth: 80);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "주문단위", "Unit", colWidth: 90);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "수량 입력", "Qty_PerUnit", colWidth: 90);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "총 구매수량", "TotalQty", colWidth: 110);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "총액", "TotalPrice", colWidth: 100);


            LoadData();
        }
        private void LoadData()
        {
            purchaseList = srv.GetPurchaseList();
            dataGridView1.DataSource = purchaseList;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            purchaseNo = Convert.ToInt32(dataGridView1[0, e.RowIndex].Value);

            txtPurchaseCode.Text = dataGridView1["Purchase_No", e.RowIndex].Value.ToString();
            txtClientCode.Text = dataGridView1["Client_Code", e.RowIndex].Value.ToString();
            txtClientName.Text = dataGridView1["Client_Name", e.RowIndex].Value.ToString();
            txtPurchaseRegiDate.Text = dataGridView1["Purchase_Date", e.RowIndex].Value.ToString();
            txtPurchaseState.Text = dataGridView1["Purchase_State", e.RowIndex].Value.ToString();
            txtDueDate.Text = dataGridView1["IncomingDue_date", e.RowIndex].Value.ToString();
            txtIncomingState.Text = dataGridView1["Is_Incoming", e.RowIndex].Value.ToString();
            //txtIncomingDate.Text = dataGridView1["Incoming_Date", e.RowIndex].Value.ToString();
            txtRegiDate.Text = dataGridView1["Create_Time", e.RowIndex].Value.ToString();
            //txtRegiUser.Text = dataGridView1["Create_User_No", e.RowIndex].Value.ToString();

            purchaseDetailList = srv.GetPurchaseDetailList(purchaseNo);
            dataGridView2.DataSource = purchaseDetailList;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            PopPurchaseRegister pop = new PopPurchaseRegister(user);
            if (pop.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            PopPurchaseModify pop = new PopPurchaseModify(purchaseNo, user);
            if (pop.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnIncoming_Click(object sender, EventArgs e)
        {
            if (purchaseNo != 0)
            {
                PopIncomingCreateLot pop = new PopIncomingCreateLot(purchaseNo);
                if (pop.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("입고처리할 발주코드를 선택해주세요");
                return;
            }
        }

        private void btnIncomingList_Click(object sender, EventArgs e)
        {
            PopIncomingList pop = new PopIncomingList();
            pop.ShowDialog();
        }
    }
}
