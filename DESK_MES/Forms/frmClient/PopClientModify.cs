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
using System.Net.Http;
using Newtonsoft.Json;


namespace DESK_MES
{
    public partial class PopClientModify : Form
    {
        UserVO user;
        ClientService srv = null;
        

        public PopClientModify(string clientCode, UserVO user)
        {
            InitializeComponent();
            this.user = user;
            srv = new ClientService();

            ClientVO info = srv.GetClientInfoByCode(clientCode);

            textBox1.Text = info.Client_Code;
            textBox2.Text = info.Client_Name;
            comboBox1.Text = info.Client_Type;
            textBox5.Text = info.Client_Number;
            textBox6.Text = info.Client_Phone;

 

        }
        private void PopClientModify_Load(object sender, EventArgs e)
        {
            
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            ClientVO client = new ClientVO
            {
                Client_Code = textBox1.Text,
                Client_Name = textBox2.Text,
                Client_Type = comboBox1.Text,
                Client_Phone = textBox6.Text,
                Update_User_No = user.User_No
            };

            bool result = srv.UpdateClientInfo(client);
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string clientNO = textBox1.Text;

            bool result = srv.DeleteClientInfo(clientNO);
            if (result)
            {
                MessageBox.Show("성공적으로 삭제되었습니다.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("수정 중 오류가 발생했습니다.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
