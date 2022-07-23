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
    public partial class frmLogin : Form
    {
        public UserVO userVO { get; set; }

        public frmLogin()
        {
            InitializeComponent();
        }
        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtUserID.isNumeric = true;
            txtUserPwd.PasswordChar = '*';
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string msg = TextBoxUtil.IsRequiredCheck(new ccTextBox[] { txtUserID, txtUserPwd });
            if( msg.Length > 0)
            {
                MessageBox.Show(msg);
                return;
            }

            LoginService LoginSrv = new LoginService();
            userVO = LoginSrv.GetUserInfo(Convert.ToInt32(txtUserID.Text.Trim()), txtUserPwd.Text.Trim());

            if (userVO != null)
            {
                this.DialogResult = DialogResult.OK;

            }
            else
            {
                MessageBox.Show("사원 정보를 확인해 주세요.");
            }
        }

        private void txtUserPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnLogin_Click(this, null);
            }
        }
    }
}
