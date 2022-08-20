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
    public partial class PopPurchaseModify : Form
    {
        UserVO user;
        PurchaseService srv = null;

        public PopPurchaseModify(int purchaseNo, UserVO user)
        {
            this.Icon = Icon.FromHandle(Properties.Resources.free_icon_tree.GetHicon());
            InitializeComponent();
            this.user = user;
            srv = new PurchaseService();

            PurchaseVO info = srv.GetPurchaseInfoByPurchaseCode(purchaseNo);

            textBox1.Text = Convert.ToString(info.Purchase_No);
            textBox2.Text = info.Client_Name;
            dateTimePicker1.Value = Convert.ToDateTime(info.Purchase_Date);
            dateTimePicker2.Value = Convert.ToDateTime(info.IncomingDue_date);
            comboBox1.Text = info.Purchase_State;

        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            PurchaseVO info = new PurchaseVO
            {
                Purchase_No = Convert.ToInt32(textBox1.Text),
                IncomingDue_date = dateTimePicker2.Value.ToShortDateString(),
                Purchase_State = comboBox1.Text,
                Update_User_No = user.User_No
            };

            bool result = srv.UpdatePurchaseInfo(info);
            if (result)
            {
                MessageBox.Show("성공적으로 수정되었습니다.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("수정 중 오류가 발생했습니다.");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
