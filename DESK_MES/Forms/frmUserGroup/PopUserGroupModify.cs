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
    public partial class PopUserGroupModify : Form
    {
        UserGroupService srv;
        int selectUserG;
        string userName;
        public PopUserGroupModify(int select,string user)
        {
            InitializeComponent();
            selectUserG = select;
            userName= user;
        }

        private void PopUserGroupModify_Load(object sender, EventArgs e)
        {
            //기본 설정
            if (srv == null) { srv = new UserGroupService(); }

            dtpCreate.Format = DateTimePickerFormat.Custom;
            dtpCreate.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";
            dtpUpdate.Format = DateTimePickerFormat.Custom;
            dtpUpdate.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";

            txtID.Enabled = false;
            dtpCreate.Enabled = false;
            txtCreateName.Enabled = false;
            dtpUpdate.Enabled = false;
            txtUpdateName.Enabled = false;

            cboType.DropDownStyle = ComboBoxStyle.DropDownList;

            //데이터
            UserGroupVO user = srv.SelectUserGroupCell(selectUserG);
            txtID.Text = user.User_Group_No.ToString();
            txtName.Text = user.User_Group_Name;
            cboType.SelectedValue = user.User_Group_Type;
            dtpCreate.Value = Convert.ToDateTime(user.Create_Time);
            txtCreateName.Text = user.Create_User_Name;
            dtpUpdate.Value = DateTime.Now;
            txtUpdateName.Text = userName;
        }

        //수정버튼
        private void button1_Click(object sender, EventArgs e)
        {
           
            if (!string.IsNullOrWhiteSpace(txtName.Text)&& txtName.Text.Length>40)
            {
                MessageBox.Show("그룹명은 공란이 없고 글자수가 40자 이하여야 합니다.");
                return;
            }
            UserGroupVO user = new UserGroupVO
            {
                User_Group_No = Convert.ToInt32(txtID.Text),
                User_Group_Name = txtName.Text,
                User_Group_Type = Convert.ToInt32(cboType.SelectedValue),
                Update_Time = dtpUpdate.Value.ToString(),
                Update_User_Name = txtUpdateName.Text
            };
            if (srv.UpdateUserGroup(user))
            {
                MessageBox.Show("수정 성공");
            }
            else
            {
                MessageBox.Show("수정 실패");
            }

        }

        //삭제버튼
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (srv.DeleteUserGroup(Convert.ToInt32(txtID.Text)))
            {
                MessageBox.Show("삭제 성공");
            }
            else
            {
                MessageBox.Show("삭제 실패");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
