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
    public partial class frmIncoming : Form
    {
        IncomingService srv;
        PurchaseService pursrv;
        UserVO user;
        int purchaseNo = 0;
        List<PurchaseVO> incomingList;
        List<PurchaseDetailVO> incomingDetailList;
        List<PurchaseDetailVO> purchaseDetailList;

        public frmIncoming()
        {
            InitializeComponent();
            panel5.Visible = false;
            label1.Text = "입고 관리";
        }

        private void frmIncoming_Load(object sender, EventArgs e)
        {
            this.user = ((frmMain)(this.MdiParent)).userInfo;
            srv = new IncomingService();
            pursrv = new PurchaseService();

            DataGridUtil.SetInitGridView(dataGridView1);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "발주코드", "Purchase_No", colWidth: 200, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "거래처번호", "Client_Code", colWidth: 230, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "거래처명", "Client_Name", colWidth: 300, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "입고예정일", "IncomingDue_date", colWidth: 200, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "입고완료일", "Incoming_Date", colWidth: 200, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "입고상태", "Is_Incoming", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "등록일자", "Create_Time", colWidth: 60, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "등록사용자", "Create_User_Name", colWidth: 80, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "수정일자", "Update_Time", colWidth: 60, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "수정사용자", "Update_User_Name", colWidth: 60, isVisible: false);

            DataGridUtil.SetInitGridView(dataGridView2);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "발주코드", "Purchase_No", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "품번", "Product_Code", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "품명", "Product_Name", colWidth: 300, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "유형", "Product_Type", colWidth: 100, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "단가", "Price", colWidth: 80, alignContent: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "주문단위", "Unit", colWidth: 90, alignContent: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "수량 입력", "Qty_PerUnit", colWidth: 120, alignContent: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "총 구매수량", "TotalQty", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "총액", "TotalPrice", colWidth: 120, alignContent: DataGridViewContentAlignment.MiddleRight);

            LoadData();
        }
        private void LoadData()
        {
            incomingList = srv.GetIncomingList();
            dataGridView1.DataSource = incomingList;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            purchaseNo = Convert.ToInt32(dataGridView1[0, e.RowIndex].Value);

            txtPurchaseCode.Text = dataGridView1["Purchase_No", e.RowIndex].Value.ToString();
            txtClientCode.Text = dataGridView1["Client_Code", e.RowIndex].Value.ToString();
            txtClientName.Text = dataGridView1["Client_Name", e.RowIndex].Value.ToString();
            txtDueDate.Text = dataGridView1["IncomingDue_date", e.RowIndex].Value.ToString();
            txtIncomingDate.Text = (dataGridView1["Incoming_Date", e.RowIndex].Value ?? string.Empty).ToString();
            txtIncomingState.Text = dataGridView1["Is_Incoming", e.RowIndex].Value.ToString();
            txtRegiDate.Text = (dataGridView1["Create_Time", e.RowIndex].Value ?? string.Empty).ToString();
            txtRegiUser.Text = (dataGridView1["Create_User_Name", e.RowIndex].Value ?? string.Empty).ToString();
            txtModidate.Text = (dataGridView1["Update_Time", e.RowIndex].Value ?? string.Empty).ToString();
            txtModiname.Text = (dataGridView1["Update_User_Name", e.RowIndex].Value ?? string.Empty).ToString();

            purchaseDetailList = pursrv.GetPurchaseDetailList(purchaseNo);
            dataGridView2.DataSource = purchaseDetailList;
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

        private void btnOpenDetail_Click(object sender, EventArgs e)
        {
            if (panel5.Visible == true)
            {
                panel5.Visible = false;
            }
            else
            {
                panel5.Visible = true;
            }
        }


    }
}
