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
    public partial class PopUserGroupRegister : Form
    {
        UserGroupService srv;
        UserVO userVV;
        int UserNo;
        public PopUserGroupRegister(UserVO uNo)
        {
            this.Icon = Icon.FromHandle(Properties.Resources.free_icon_tree.GetHicon());
            userVV = uNo;
            InitializeComponent();
        }

        private void PopUserGroupRegister_Load(object sender, EventArgs e)
        {
            //기본 설정
            srv = new UserGroupService();
            List<UserGroupVO>list = srv.SelectAuthList();
            ComboBinding(comboBox1, list);

            if (list == null)
            {
                txtNO.Text = "1001";
            }
            else
            {
                list.ForEach((f) => txtNO.Text = (f.LastUser_No + 1).ToString());
            }
            txtNO.Enabled = false;

            dtpCreate.Format = DateTimePickerFormat.Custom;
            dtpCreate.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";
            dtpCreate.Value = DateTime.Now;
            dtpCreate.Enabled = false;

            //여기서 부모폼에서 가져온거 쓰자
            txtCreateName.Enabled = false;
        }
        private void ComboBinding(ComboBox cbo,List<UserGroupVO>list)
        {
            cbo.DropDownStyle = ComboBoxStyle.DropDownList;
            cbo.DisplayMember = "Auth_Name";
            cbo.ValueMember = "Auth_ID";

            cbo.DataSource = list; ;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
           
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (! string.IsNullOrWhiteSpace(txtName.Text) && txtName.Text.Length < 40)
            {
                
                UserGroupVO userG = new UserGroupVO
                {
                    User_Group_Name = txtName.Text.Trim(),
                    User_Group_Type = Convert.ToInt32(comboBox1.SelectedValue),
                    Create_User_No = userVV.User_No
                };
                bool flag= srv.InsertUserGroup(userG);
                if (flag)
                {
                    if (MessageBox.Show("입력 성공", "등록", MessageBoxButtons.OK) == DialogResult.OK)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("입력실패");
                }
            }
            else
            {
                MessageBox.Show("그룹명은 공란이 없고 글자수가 40자 이하여야 합니다.");
            }
        }        
    }
}
