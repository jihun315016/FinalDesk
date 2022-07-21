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
        ServiceHelper service = null;

        public PopClientRegister()
        {
            InitializeComponent();
            service = new ServiceHelper("api/Client");

            string[] type = new string[] { "매입처", "매출처" };
            comboBox1.Items.AddRange(type);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {            
            ClientVO client = new ClientVO
            {
                Client_Code = textBox1.Text,
                Client_Name = textBox2.Text,
                Client_Type = comboBox1.Text,
                Client_Number = textBox5.Text,
                Client_Phone = textBox6.Text                
            };

            ResMessage<List<ClientVO>> result = service.PostAsync<ClientVO, List<ClientVO>>("SaveClient", client);

            if (result.ErrCode == 0)
            {
                MessageBox.Show("성공적으로 등록되었습니다.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(result.ErrMsg);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
