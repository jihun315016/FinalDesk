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
        public UserVO userVO { get; set; }
        public POP_Login()
        {
            InitializeComponent();
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void POP_Login_Load(object sender, EventArgs e)
        {
            txtID.Text = "";
            lblChk.Visible = false;
            this.ActiveControl = txtID;
            txtID.Focus();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (txtID.Text.Length < 5) //나중에 == ID자릿수로 변경
            {
                txtID.Text += "dddd";
                //button1_Click(this, null);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hi");
            //유저 유무 확인

            //DB 단에 검색

            //패스(다음화면으로)

            // lblChk.Visible = true; // 틀리면 보여주기 //시간후 사라지기
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtID.Text += "10002";
        }
    }
}
