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

namespace DESK_POP
{
    public partial class POP_Login : Form
    {
        ServiceHelper serv;
        public PopVO userVO { get; set; }
        public POP_Login()
        {
            InitializeComponent();
        }

        private void POP_Login_Load(object sender, EventArgs e)
        {
            //초기설정
            serv = new ServiceHelper("api/Pop");
            txtID.Text = "";
            lblChk.Visible = false;
            this.ActiveControl = txtID;
            txtID.Focus();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           if (txtID.Text.Length >= 5) //나중에 == ID자릿수로 변경
            {
                txtCopy1.Text = txtID.Text;
                

                
                
                ResMessage<PopVO> resresult= serv.GetAsyncT<ResMessage<PopVO>>(txtID.Text);
                
                
                if (resresult.ErrCode == 0)
                {
                    lblChk.Visible = false;
                    userVO = resresult.Data;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    txtID.Text = txtCopy1.Text = "";
                    lblChk.Visible = true;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Hi", "hii", MessageBoxButtons.OK) == DialogResult.OK)
            {
                txtID.Text = txtCopy1.Text = "";
            }
            ResMessage<PopVO> resresult = serv.GetAsyncT<ResMessage<PopVO>>(txtID.Text);

            if (resresult.ErrCode == 0)
            {
                lblChk.Visible = false;
                userVO = resresult.Data;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                txtID.Text = txtCopy1.Text = "";
                lblChk.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtID.Text += "10002";
        }
    }
}
