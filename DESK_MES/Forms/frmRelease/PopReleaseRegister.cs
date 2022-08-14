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
    public partial class PopReleaseRegister : Form
    {
        int selectedOrderNo;
        ReleaseService rSrv;
        OrderService oSrv;
        UserVO user;

        public PopReleaseRegister(UserVO user, int orderNo)
        {
            InitializeComponent();

            rSrv = new ReleaseService();
            oSrv = new OrderService();
            selectedOrderNo = orderNo;
            this.user = user;
        }

        private void PopReleaseRegister_Load(object sender, EventArgs e)
        {
            DataGridUtil.SetInitGridView(dataGridView1);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "품번", "Product_Code", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "품명", "Product_Name", colWidth: 200, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "유형", "Product_Type", colWidth: 100, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "단가", "Price", colWidth: 100, isVisible:false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "주문단위", "Unit", colWidth: 100, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "수량", "Qty_PerUnit", colWidth: 100, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "총 구매수량", "TotalQty", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "총액", "TotalPrice", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);

            List<OrderDetailVO> list = rSrv.GetOrderDetailList(selectedOrderNo);
            dataGridView1.DataSource = list;

            OrderVO info = oSrv.GetOrderListByOrderNO(selectedOrderNo);

            txtPurchaseCode.Text = info.Order_No.ToString();
            txtClientCode.Text = info.Client_Code;
            txtClientName.Text = info.Client_Name;
            txtOrderDate.Text = info.Order_Date;
            txtRealeseDueDate.Text = info.Release_Date;
            cboReleaseState.Text = info.Release_State;

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            List<OrderDetailVO> orderList = new List<OrderDetailVO>();

            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }

            // 주문저장 (Orders, OrderDetails)
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                if (item.Cells[5].Value != null)
                {
                    OrderDetailVO orderitem = new OrderDetailVO
                    {
                        Product_Code = item.Cells[0].Value.ToString(),
                        TotalQty = Convert.ToInt32(item.Cells[5].Value)
                    };
                    orderList.Add(orderitem);
                }
            }

            ReleaseVO release = new ReleaseVO
            {
                Order_No = selectedOrderNo,
                Release_OK_Date = dtpReleaseOkDate.Value.ToShortDateString(),
                Release_State = "Y",
                Update_User_No = user.User_No
            };

            bool result = rSrv.RegisterRelease(release, orderList);
            if (result)
            {
                MessageBox.Show($"출고 처리되었습니다.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("출고처리 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }
    }
}
