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
        ServiceHelper service = null;

        public PopClientModify(string clientCode)
        {
            InitializeComponent();
            service = new ServiceHelper("api/Client");

            ResMessage<ClientVO> resResult = service.GetAsyncT<ResMessage<ClientVO>>(clientCode);

            if (resResult.ErrCode == 0)
            {
                textBox1.Text = resResult.Data.Client_Code.ToString();
                textBox2.Text = resResult.Data.Client_Name.ToString();
                comboBox1.Text = resResult.Data.Client_Type.ToString();
                textBox5.Text = resResult.Data.Client_Number.ToString();
                textBox6.Text = resResult.Data.Client_Phone.ToString();
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            ClientVO client = new ClientVO
            {
                Client_Code = textBox1.Text,
                Client_Name = textBox2.Text,
                Client_Type = comboBox1.Text,
                Client_Phone = textBox6.Text
            };

            ResMessage<List<ClientVO>> result = service.PostAsync<ClientVO, List<ClientVO>>("UpdateClient", client);

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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string clientNO = textBox1.Text;

            ResMessage resResult = service.GetAsyncNon($"DelClient/{clientNO}");

            if (resResult.ErrCode == 0)
            {
                MessageBox.Show("삭제되었습니다.");
            }
            else
            {
                MessageBox.Show(resResult.ErrMsg);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
