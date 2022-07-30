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

        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }
    }
}
