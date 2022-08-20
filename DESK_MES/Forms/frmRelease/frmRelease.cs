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
    public partial class frmRelease : FormStyle_1
    {
        UserVO user;
        ReleaseService srv;
        int orderNo = 0;
        List<ReleaseVO> releaseList;
        List<OrderDetailVO> orderDetailList;
        string relesState = null;

        public frmRelease()
        {
            this.Icon = Icon.FromHandle(Properties.Resources.free_icon_tree.GetHicon());
            InitializeComponent();
            label1.Text = "출고 관리";
        }

        private void frmRelease_Load(object sender, EventArgs e)
        {
            srv = new ReleaseService();
            this.user = ((frmMain)(this.MdiParent)).userInfo;

            comboBox1.Items.AddRange(new string[] { "선택", "거래처명", "출고상태" });
            comboBox1.SelectedIndex = 0;

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
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "수량", "Qty_PerUnit", colWidth: 100, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "총 구매수량", "TotalQty", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "총액", "TotalPrice", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            dataGridView2.Columns["Price"].DefaultCellStyle.Format = "###,##0";
            dataGridView2.Columns["Qty_PerUnit"].DefaultCellStyle.Format = "###,##0";
            dataGridView2.Columns["TotalQty"].DefaultCellStyle.Format = "###,##0";
            dataGridView2.Columns["TotalPrice"].DefaultCellStyle.Format = "###,##0";
            LoadData();
        }

        private void LoadData()
        {
            releaseList = srv.GetReleaseList();
            dataGridView1.DataSource = releaseList;
            dataGridView1.ClearSelection();
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
            relesState = dataGridView1["Release_State", e.RowIndex].Value.ToString();
            txtRelease_OK_Date.Text = (dataGridView1["Release_OK_Date", e.RowIndex].Value == null) ? "": dataGridView1["Release_OK_Date", e.RowIndex].Value.ToString();

            orderDetailList = srv.GetOrderDetailList(orderNo);
            dataGridView2.DataSource = orderDetailList;
        }

        private void btnReleaseAdd_Click(object sender, EventArgs e)
        {
            if (orderNo != 0 && relesState.Contains('N'))
            {
                PopReleaseRegister pop = new PopReleaseRegister(user, orderNo);
                if (pop.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("출고등록 항목을 선택해주세요");
                return;
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<ReleaseVO> list = releaseList.Where(p => 1 == 1).ToList();
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
                    list = list.Where(p => p.Release_State.ToLower().Contains(textBox1.Text.ToLower())).ToList();
            }

            dataGridView1.DataSource = list;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            releaseList = srv.GetReleaseList();
            comboBox1.SelectedIndex = 0;
            textBox1.Text = string.Empty;
            comboBox1.Enabled = textBox1.Enabled = true;
            panel5.Visible = false;
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = releaseList;

            dataGridView2.DataSource = null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 엑셀
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Execl Files(*.xls)|*.xls";
            dlg.Title = "엑셀파일로 내보내기";

            List<ReleaseVO> list = dataGridView1.DataSource as List<ReleaseVO>;
            if (list == null)
            {
                MessageBox.Show("조회 항목이 없습니다.");
                return;
            }

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ExcelUtil excel = new ExcelUtil();
                List<ReleaseVO> output = list;

                string[] columnImport = { "Order_No", "Client_Code", "Client_Name", "Order_Date", "Release_Date", "Release_State", "Release_OK_Date" };
                string[] columnName = { "주문번호", "거래처코드", "거래처명", "주문등록일", "출고예정일", "출고상태", "출고완료일" };

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

        private void frmRelease_Shown(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void dataGridView2_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridView2.ClearSelection();
        }
    }
}
