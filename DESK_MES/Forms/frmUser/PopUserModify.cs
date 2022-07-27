using DESK_DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DESK_MES
{
    public partial class PopUserModify : Form
    {
        UserGroupService srvG;
        UserService srv;
        List<UserGroupVO> gList;
        int userNo; //접속자 번호
        string userName; // 접속자 이름
        UserVO userlist; //유저 리스트

        public PopUserModify(int userno, string username, UserVO userV)
        {
            userNo = userno;
            userName = username;
            userlist = userV;
            InitializeComponent();
        }
        /// <summary>
        /// 김준모/콤보박스 바인딩(조건 : 리스트{화면표시값, 벨류값} 필수) 
        /// </summary>
        /// <typeparam name="T">해당VO</typeparam>
        /// <param name="cbo">콤보박스</param>
        /// <param name="list">바인딩 할 List</param>
        /// <param name="dis">화면표시, 블랭크추가시 prop명</param>
        /// <param name="val">cbo벨류값</param>
        /// <param name="blank">콤보박스 블랭크 유무 토글</param>
        /// <param name="blankText">콤보박스 블랭크 텍스트란</param>
        private void ComboBinding<T>(ComboBox cbo, List<T> list, string dis, string val, bool blank = false, string blankText = "전체") where T : class
        {
            if (blank)
            {
                T obj = default(T);

                obj = Activator.CreateInstance<T>();
                obj.GetType().GetProperty(dis).SetValue(obj, blankText);

                list.Insert(0, obj);
            }
            cbo.DataSource = null;
            cbo.DropDownStyle = ComboBoxStyle.DropDownList;
            cbo.DisplayMember = dis;
            cbo.ValueMember = val;

            cbo.DataSource = list;
        }

        private void PopUserModify_Load(object sender, EventArgs e)
        {
            srvG = new UserGroupService();
            srv = new UserService();
            //기본설정
            txtNo.Text = userlist.User_No.ToString();
            txtNo.Enabled = false;
            txtName.Text = userlist.User_Name;
            txtPwd.Text = userlist.User_Pwd;
            txtCreate.Text = userlist.Create_User_Name;
            txtCreate.Enabled = false;
            txtUpdateUser.Text = userlist.Update_User_Name;
            txtUpdateUser.Enabled = false;

            List<UserGroupVO> uList = srvG.SelectAuthList(); 
            gList = srvG.SelectGroupList();

            List<UserVO> del = new List<UserVO>();
            del.Add(new UserVO { Is_Delete = "N" });
            del.Add(new UserVO { Is_Delete = "Y" });

            ComboBinding<UserVO>(cboDelete, del, "Is_Delete", "Is_Delete");
            ComboBinding<UserGroupVO>(cboUG, gList, "User_Group_Name", "User_Group_No",blank:true,blankText:"없음");

            cboUG.SelectedValue = userlist.User_Group_No;
            cboDelete.SelectedValue = userlist.Is_Delete;
        }

        private void cboUG_SelectedIndexChanged(object sender, EventArgs e)
        {
            int gID = Convert.ToInt32(cboUG.SelectedValue);
            List<UserGroupVO> cboList = gList.FindAll((f) => f.User_Group_No.Equals(gID)).ToList();
            ComboBinding<UserGroupVO>(cboAuth, cboList, "Auth_Name", "Auth_ID");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ccTextBox[] ctx = new ccTextBox[] { txtName, txtPwd };
            StringBuilder sb = new StringBuilder();
            string isRequiredMsg = TextBoxUtil.IsRequiredCheck(ctx);
            if (isRequiredMsg.Length > 0)
            {
                sb.Append(isRequiredMsg);

                if (txtName.Text.Length > 30)
                {
                    sb.Append($"\n[{txtName.Tag}]의 글자수는 30개 이하만 가능합니다");
                }
                if (txtPwd.Text.Length > 30)
                {
                    sb.Append($"\n[{txtPwd.Tag}]의 글자수는 12개 이하만 가능합니다");
                }
                MessageBox.Show(sb.ToString());
                return;
            }

            //vo
            UserVO uservo = new UserVO
            {
                User_No = Convert.ToInt32(txtNo.Text),
                User_Name = txtName.Text,
                User_Group_No = Convert.ToInt32(cboUG.SelectedValue),
                User_Pwd = txtPwd.Text,
                Auth_ID = Convert.ToInt32(cboAuth.SelectedValue),
                Is_Delete = cboDelete.Text,
                Update_User_No =  userNo
            };

            if (srv.UpdateUser(uservo))
            {
                if (MessageBox.Show("사용자 정보 변경 완료", "사용자 정보 변경", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("사옹자 정보 변경 실패");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("삭제하시겠니까?", "삭제", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                UserVO userV = new UserVO
                {
                    User_No = Convert.ToInt32(txtNo.Text),
                    Is_Delete = "Y"
                };
               if (srv.DeleteUser(userV))
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
