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

            comboBox1.Items.AddRange(new string[] { "선택", "거래처명", "발주상태" });
            comboBox1.SelectedIndex = 0;

            button1.Visible = false;
            button2.Visible = false;

            DataGridUtil.SetInitGridView(dataGridView1);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "발주코드", "Purchase_No", colWidth: 200, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "거래처번호", "Client_Code", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "거래처명", "Client_Name", colWidth: 300, alignContent: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "발주등록일", "Purchase_Date", colWidth: 200, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "발주상태", "Purchase_State", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "입고예정일", "IncomingDue_date", colWidth: 200, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "입고상태", "Is_Incoming", colWidth: 60, isVisible:false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "등록일자", "Create_Time", colWidth: 120, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "등록사용자", "Create_User_Name", colWidth: 80, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "수정일자", "Update_Time", colWidth: 120, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "수정사용자", "Update_User_Name", colWidth: 60, isVisible: false);

            DataGridUtil.SetInitGridView(dataGridView2);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "발주코드", "Purchase_No", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "품번", "Product_Code", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "품명", "Product_Name", colWidth: 300, alignContent: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "유형", "Product_Type", colWidth: 100, isVisible: false, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "단가", "Price", colWidth: 80, alignContent: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "주문단위", "Unit", colWidth: 90, alignContent: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "수량 입력", "Qty_PerUnit", colWidth: 120, alignContent: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "총 구매수량", "TotalQty", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "총액", "TotalPrice", colWidth: 120, alignContent: DataGridViewContentAlignment.MiddleRight);
            dataGridView2.Columns["Price"].DefaultCellStyle.Format = "###,##0";
            dataGridView2.Columns["Qty_PerUnit"].DefaultCellStyle.Format = "###,##0";
            dataGridView2.Columns["TotalQty"].DefaultCellStyle.Format = "###,##0";
            dataGridView2.Columns["TotalPrice"].DefaultCellStyle.Format = "###,##0";

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
            txtRegiDate.Text = (dataGridView1["Create_Time", e.RowIndex].Value ?? string.Empty).ToString();
            txtRegiUser.Text = (dataGridView1["Create_User_Name", e.RowIndex].Value ?? string.Empty).ToString();
            txtModifydate.Text = (dataGridView1["Update_Time", e.RowIndex].Value ?? string.Empty).ToString();
            txtModifyName.Text = (dataGridView1["Update_User_Name", e.RowIndex].Value ?? string.Empty).ToString();


            purchaseDetailList = srv.GetPurchaseDetailList(purchaseNo);
            dataGridView2.DataSource = purchaseDetailList;
            dataGridView2.ClearSelection();
            if (txtPurchaseState.Text == "UD")
            {
                button1.Visible = true;
                button2.Visible = true;
            }
            else
            {
                button1.Visible = false;
                button2.Visible = false;
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            // 발주 확정 처리
            PurchaseVO order = new PurchaseVO
            {
                Purchase_No = Convert.ToInt32(txtPurchaseCode.Text),
                Purchase_State = "DT",
                Is_Incoming = "N",
                Update_User_No = user.User_No
            };
            bool result = srv.UpdatePurchaseOK(order);
            if (result)
            {
                MessageBox.Show($"발주가 확정되었습니다.");
                LoadData();
            }
            else
            {
                MessageBox.Show("발주확정 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 발주 취소
            PurchaseVO order = new PurchaseVO
            {
                Purchase_No = Convert.ToInt32(txtPurchaseCode.Text),
                Purchase_State = "CL",
                Update_User_No = user.User_No
            };
            bool result = srv.UpdatePurchaseCancle(order);
            if (result)
            {
                MessageBox.Show($"발주가 취소되었습니다.");
                LoadData();
            }
            else
            {
                MessageBox.Show("발주취소 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<PurchaseVO> list = purchaseList.Where(p => 1 == 1).ToList();
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
                    list = list.Where(p => p.Purchase_State.ToLower().Contains(textBox1.Text.ToLower())).ToList();
            }

            dataGridView1.DataSource = list;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            purchaseList = srv.GetPurchaseList();
            comboBox1.SelectedIndex = 0;
            textBox1.Text = string.Empty;
            comboBox1.Enabled = textBox1.Enabled = true;
            panel5.Visible = false;
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = purchaseList;

            dataGridView2.DataSource = null;
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

                string[] columnImport = { "Purchase_No", "Client_Code", "Client_Name", "Purchase_Date", "Purchase_State", "IncomingDue_date", "Is_Incoming" };
                string[] columnName = { "발주코드", "거래처번호", "거래처명", "발주등록일", "발주상태", "입고예정일", "입고상태" };

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

        private void frmPurchase_Shown(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[0].Value == null)
                return;


            if (dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString().Contains("UD"))
            {
                e.CellStyle.BackColor = Color.Salmon;
                e.CellStyle.ForeColor = Color.White;
            }
            else
            {
                e.CellStyle.BackColor = Color.White;
                e.CellStyle.ForeColor = Color.Black;
            }
        }
    }
}
