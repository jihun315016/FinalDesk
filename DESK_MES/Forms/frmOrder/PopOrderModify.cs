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
    public partial class PopOrderModify : Form
    {
        OrderService srv;

        public PopOrderModify(int orderNo)
        {
            InitializeComponent();
            srv = new OrderService();
            OrderVO info = srv.GetOrderListByOrderNO(orderNo);

            txtOrderNo.Text = info.Order_No.ToString();
            txtClientCode.Text = info.Client_Code;
            txtClientName.Text = info.Client_Name;
            txtOrderRegiDate.Text = info.Order_Date;
            dtpReleaseDate.Value = Convert.ToDateTime(info.Release_Date);
            cboOrderState.Text = info.Order_State;
            txtCreateDate.Text = info.Create_Time;

        }
        private void PopOrderModify_Load(object sender, EventArgs e)
        {

        }
        private void btnModify_Click(object sender, EventArgs e)
        {
            OrderVO order = new OrderVO
            {
                Order_No = Convert.ToInt32(txtOrderNo.Text),
                Release_Date = dtpReleaseDate.Value.ToShortDateString(),
                Order_State = cboOrderState.Text.ToString()
            };
            bool result = srv.UpdateOrderInfo(order);
            if (result)
            {                
                MessageBox.Show($"주문 정보가 수정되었습니다.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("주문정보 수정 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
