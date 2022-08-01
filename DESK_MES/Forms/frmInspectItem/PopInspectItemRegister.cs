using DESK_DTO;
using DESK_MES.Service;
using System;
using System.Windows.Forms;

namespace DESK_MES
{
    public partial class PopInspectItemRegister : Form
    {
        UserVO user;
        InspectService InspectSrv;

        public PopInspectItemRegister(UserVO user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void PopInspectItemRegister_Load(object sender, EventArgs e)
        {
            InspectSrv = new InspectService();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string msg = TextBoxUtil.IsRequiredCheck(new ccTextBox[] { txtInspectName, txtTarget, txtLsl, txtUsl });
            if (msg.Length > 0)
            {
                MessageBox.Show(msg);
                return;
            }

            InspectItemVO item = new InspectItemVO()
            {
                Inspect_Name = txtInspectName.Text,
                Target = Convert.ToInt32(txtTarget.Text),
                LSL = Convert.ToInt32(txtLsl.Text),
                USL = Convert.ToInt32(txtUsl.Text),
                Create_User_No = user.User_No
            };

            bool result = InspectSrv.SaveInspectItem(item);
            if (result)
            {
                MessageBox.Show("검사 항목이 등록되었습니다.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("등록에 실패했습니다.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
