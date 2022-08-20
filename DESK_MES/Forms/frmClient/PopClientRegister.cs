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
    public partial class PopClientRegister : Form
    {
        UserVO user;
        ClientService srv = null;

        public PopClientRegister(UserVO user)
        {
            InitializeComponent();
            this.Icon = Icon.FromHandle(Properties.Resources.free_icon_tree.GetHicon());
            this.user = user;
            srv = new ClientService();

            // CUS : 매출처(제품)
            // VEN : 매입처(원자재)
            string[] type = new string[] { "선택", "CUS", "VEN" };
            comboBox1.Items.AddRange(type);
            comboBox1.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClientVO lastID = srv.GetLastID();
            string id = lastID.Client_Code.ToString();
            string[] search = id.Split(new char[] { '_' });
            string name = search[0];
            string num = search[1];

            string addID = (int.Parse(search[1]) + 1).ToString().PadLeft(4, '0');

            string newid = (name + "_" + addID);

            ClientVO client = new ClientVO
            {
                Client_Code = newid,
                Client_Name = textBox2.Text,
                Client_Type = comboBox1.Text,
                Client_Number = textBox5.Text,
                Client_Phone = textBox6.Text,
                Create_User_No = user.User_No
            };

            bool result = srv.RegisterClient(client);
            if (result)
            {
                MessageBox.Show($"거래처가 등록되었습니다.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("주문처리 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
