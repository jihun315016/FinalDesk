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
        UserVO user;
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
            this.user = ((frmMain)(this.MdiParent)).userInfo;
            srv = new OrderService();

            comboBox1.Items.AddRange(new string[] { "선택", "거래처명", "주문상태" });
            comboBox1.SelectedIndex = 0;

            // 검색용 거래처 정보 바인딩 다시 하기
            cboClientName.Items.AddRange(new string[] { "선택", "애플가구"});
            cboClientName.SelectedIndex = 0;

            cboOrderState.Items.AddRange(new string[] { "선택", "UD", "DT", "CL" });
            cboOrderState.SelectedIndex = 0;


            button1.Visible = false;
            button2.Visible = false;

            DataGridUtil.SetInitGridView(dataGridView1);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "주문번호", "Order_No", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "거래처코드", "Client_Code", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "거래처명", "Client_Name", colWidth: 300, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "주문등록일", "Order_Date", colWidth: 200, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "주문상태", "Order_State", colWidth: 200, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "출고예정일", "Release_Date", colWidth: 200, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "출고상태", "Release_State", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생성일자", "Create_Time", colWidth: 60, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생성사용자", "Create_User_Name", colWidth: 80, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "수정일자", "Update_Time", colWidth: 60, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "수정사용자", "Update_User_Name", colWidth: 60, isVisible: false);

            DataGridUtil.SetInitGridView(dataGridView2);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "품번", "Product_Code", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "품명", "Product_Name", colWidth: 200, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "유형", "Product_Type", colWidth: 100, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "단가", "Price", colWidth: 100, alignContent: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "주문단위", "Unit", colWidth: 100, alignContent: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "수량 입력", "Qty_PerUnit", colWidth: 100, alignContent: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "총 구매수량", "TotalQty", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView2, "총액", "TotalPrice", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleRight);
            dataGridView2.Columns["Price"].DefaultCellStyle.Format = "###,##0";
            dataGridView2.Columns["Qty_PerUnit"].DefaultCellStyle.Format = "###,##0";
            dataGridView2.Columns["TotalQty"].DefaultCellStyle.Format = "###,##0";
            dataGridView2.Columns["TotalPrice"].DefaultCellStyle.Format = "###,##0";
            LoadData();
            
        }
        private void LoadData()
        {
            orderList = srv.GetOrderList();
            dataGridView1.DataSource = orderList;
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
            txtOrderState.Text = dataGridView1["Order_State", e.RowIndex].Value.ToString();
            txtCreateDate.Text = dataGridView1["Create_Time", e.RowIndex].Value.ToString();
            txtCreateUser.Text = (dataGridView1["Create_User_Name", e.RowIndex].Value ?? string.Empty).ToString();
            txtModifyDate.Text = (dataGridView1["Update_Time", e.RowIndex].Value ?? string.Empty).ToString();
            txtModifyUser.Text = (dataGridView1["Update_User_Name", e.RowIndex].Value ?? string.Empty).ToString();
            
            orderDetailList = srv.GetOrderDetailList(orderNo);
            dataGridView2.DataSource = orderDetailList;

            if(txtOrderState.Text == "UD")
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
            PopOrderRegister pop = new PopOrderRegister(user);
            if (pop.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (orderNo != 0)
            {
                PopOrderModify pop = new PopOrderModify(orderNo, user);
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

        private void button1_Click(object sender, EventArgs e)
        {

            // 주문 확정 처리
            OrderVO order = new OrderVO
            {
                Order_No = Convert.ToInt32(txtOrderNo.Text),
                Order_State = "DT",
                Update_User_No = user.User_No
            };
            bool result = srv.UpdateOrderState(order);
            if (result)
            {
                MessageBox.Show($"주문상태가 수정되었습니다.");
                LoadData();
            }
            else
            {
                MessageBox.Show("주문상태 수정 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 주문 취소 처리
            OrderVO order = new OrderVO
            {
                Order_No = Convert.ToInt32(txtOrderNo.Text),
                Order_State = "CL",
                Update_User_No = user.User_No
            };
            bool result = srv.UpdateOrderState(order);
            if (result)
            {
                MessageBox.Show($"주문상태가 수정되었습니다.");
                LoadData();
            }
            else
            {
                MessageBox.Show("주문상태 수정 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //엑셀
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Execl Files(*.xls)|*.xls";
            dlg.Title = "엑셀파일로 내보내기";

            List<OrderVO> list = dataGridView1.DataSource as List<OrderVO>;
            if (list == null)
            {
                MessageBox.Show("조회 항목이 없습니다.");
                return;
            }

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ExcelUtil excel = new ExcelUtil();
                List<OrderVO> output = list;

                string[] columnImport = { "Order_No", "Client_Code", "Client_Name", "Order_Date", "Order_State", "Release_Date", "Release_State" };
                string[] columnName = { "주문번호", "거래처코드", "거래처명", "주문등록일", "주문상태", "출고예정일", "출고상태" };

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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<OrderVO> list = orderList.Where(p => 1 == 1).ToList();
            // 상세 검색으로 필터링
            if (panel5.Visible)
            {
                if (!string.IsNullOrWhiteSpace(txtOrderCode.Text.Trim()))
                    list = list.Where(p => p.Client_Code.Contains(txtClientCode.Text.ToLower())).ToList();

                if (!string.IsNullOrWhiteSpace(txtClientName.Text.Trim()))
                    list = list.Where(p => p.Client_Name.ToLower().Contains(txtClientName.Text.ToLower())).ToList();

                if (cboOrderState.SelectedIndex > 0)
                    list = list.Where(p => p.Order_State == cboOrderState.SelectedValue.ToString().Split('_')[1]).ToList();
            }
            // 일반 검색으로 필터링
            else
            {
                // 거래처코드 검색
                if (comboBox1.SelectedIndex == 1)
                    list = list.Where(p => p.Client_Name.ToLower().Contains(textBox1.Text.ToLower())).ToList();

                // 거래처명 검색
                else if (comboBox1.SelectedIndex == 2)
                    list = list.Where(p => p.Order_State.ToLower().Contains(textBox1.Text.ToLower())).ToList();
            }

            dataGridView1.DataSource = list;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            orderList = srv.GetOrderList();
            comboBox1.SelectedIndex = 0;
            textBox1.Text = string.Empty;
            comboBox1.Enabled = textBox1.Enabled = true;
            panel5.Visible = false;
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = orderList;

            txtClientCode.Text = txtClientName.Text = string.Empty;
            cboOrderState.SelectedIndex = 0;
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void dataGridView2_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridView2.ClearSelection();
        }

        private void frmOrder_Shown(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }
    }
}
