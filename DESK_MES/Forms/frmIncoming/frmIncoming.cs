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
        string productCode = null;
        string incomingstate = null;
        List<PurchaseVO> incomingList;
        List<PurchaseDetailVO> incomingDetailList;
        List<PurchaseDetailVO> purchaseDetailList;
        List<PurchaseDetailVO> lotDetailList;

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

            comboBox1.Items.AddRange(new string[] { "선택", "거래처명", "입고상태" });
            comboBox1.SelectedIndex = 0;

            DataGridUtil.SetInitGridView(dataGridView1);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "발주코드", "Purchase_No", colWidth: 200, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "거래처번호", "Client_Code", colWidth: 230, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "거래처명", "Client_Name", colWidth: 300, alignContent: DataGridViewContentAlignment.MiddleLeft);
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
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "품명", "Product_Name", colWidth: 300, alignContent: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "유형", "Product_Type", colWidth: 100, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "단가", "Price", colWidth: 80, alignContent: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "주문단위", "Unit", colWidth: 90, alignContent: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "수량 입력", "Qty_PerUnit", colWidth: 120, alignContent: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "총 구매수량", "TotalQty", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "총액", "TotalPrice", colWidth: 120, alignContent: DataGridViewContentAlignment.MiddleRight);
            dataGridView2.Columns["Price"].DefaultCellStyle.Format = "###,##0";
            dataGridView2.Columns["Qty_PerUnit"].DefaultCellStyle.Format = "###,##0";
            dataGridView2.Columns["TotalQty"].DefaultCellStyle.Format = "###,##0";
            dataGridView2.Columns["TotalPrice"].DefaultCellStyle.Format = "###,##0";


            DataGridUtil.SetInitGridView(dataGridView3);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView3, "자재 Lot 코드", "Lot_Code", colWidth: 140, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView3, "제품명", "Product_Name", colWidth: 215, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView3, "창고명", "Warehouse_Name", colWidth: 80, alignContent: DataGridViewContentAlignment.MiddleCenter);

            dataGridView3.Visible = false;
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
            incomingstate = dataGridView1["Is_Incoming", e.RowIndex].Value.ToString();

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
            dataGridView3.Visible = false;
        }

        private void btnIncoming_Click(object sender, EventArgs e)
        {
            if (purchaseNo != 0 && incomingstate == "N")
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

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            productCode = dataGridView2[1, e.RowIndex].Value.ToString();

            lotDetailList = srv.GetLotDetailList(productCode);
            dataGridView3.Visible = true;
            dataGridView3.DataSource = lotDetailList;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<PurchaseVO> list = incomingList.Where(p => 1 == 1).ToList();
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
                    list = list.Where(p => p.Client_Name.ToLower().Contains(textBox1.Text.ToLower())).ToList();

                // 입고상태 검색
                else if (comboBox1.SelectedIndex == 2)
                    list = list.Where(p => p.Is_Incoming.ToLower().Contains(textBox1.Text.ToLower())).ToList();
            }

            dataGridView1.DataSource = list;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            incomingList = srv.GetIncomingList();
            comboBox1.SelectedIndex = 0;
            textBox1.Text = string.Empty;
            comboBox1.Enabled = textBox1.Enabled = true;
            panel5.Visible = false;
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = incomingList;

            dataGridView2.DataSource = null;
            dataGridView3.DataSource = null;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //엑셀
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Execl Files(*.xls)|*.xls";
            dlg.Title = "엑셀파일로 내보내기";

            List<PurchaseVO> list = dataGridView1.DataSource as List<PurchaseVO>;
            if (list == null)
            {
                MessageBox.Show("조회 항목이 없습니다.");
                return;
            }

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ExcelUtil excel = new ExcelUtil();
                List<PurchaseVO> output = list;

                string[] columnImport = { "Purchase_No", "Client_Code", "Client_Name", "IncomingDue_date", "Incoming_Date", "Is_Incoming" };
                string[] columnName = { "발주코드", "거래처번호", "거래처명", "입고예정일", "입고완료일", "입고상태" };

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

        private void frmIncoming_Shown(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }
    }
}
