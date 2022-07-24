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
        List<UserGroupVO> gList;
        string userNo;
        string userName;
        UserVO userlist;

        public PopUserModify(string userno, string username, UserVO userV)
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

            var list = gList.GroupBy((f) => f.Auth_Name) as List<UserGroupVO>;
            ComboBinding<UserGroupVO>(cboUG, list, "User_Group_Name", "User_Group_No");


        }

        private void cboAuth_SelectedIndexChanged(object sender, EventArgs e)
        {
            int gID = Convert.ToInt32(cboUG.SelectedValue);
            List<UserGroupVO> cboList = gList.FindAll((f) => f.User_Group_No.Equals(gID)).ToList();
            ComboBinding<UserGroupVO>(cboAuth, cboList, "Auth_Name", "Auth_ID");
        }
    }
}
