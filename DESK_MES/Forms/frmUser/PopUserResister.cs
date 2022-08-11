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
    public partial class PopUserResister : Form
    {
        UserGroupService srvG;
        UserService srv;
        List<UserGroupVO> gList; //그룹 리스트
        int userNo; //유저
        string userName;
        int lastNum;
        public PopUserResister(int lastnum ,int userno, string username)
        {
            lastNum = lastnum;
            userNo = userno;
            userName = username;
            InitializeComponent();
        }
        /// <summary>
        /// 초기설정(텍스트, 콤보)
        /// </summary>
        public void StartSetting()
        {
            List<UserGroupVO> uList = srvG.SelectAuthList();
            gList = srvG.SelectGroupList();

            //var list =gList.GroupBy((f) => f.Auth_Name) as List<UserGroupVO>;
            if (lastNum == 0)
                txtNo.Text = 10001.ToString();
            else
                txtNo.Text = lastNum.ToString();    
            //사번
            txtNo.Enabled = false;
            txtName.Text = "";
            ComboBinding<UserGroupVO>(cboGroup, gList, "User_Group_Name", "User_Group_No");
            dtpCreate.Value = DateTime.Now;
            txtCreate.Text = userName;
            txtCreate.Enabled = false;

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

        private void PopUserResister_Load(object sender, EventArgs e)
        {
            srvG = new UserGroupService();
            srv = new UserService();
            //초기 설정
            StartSetting();
        }

        private void cboGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            int gID = Convert.ToInt32(cboGroup.SelectedValue);
            List<UserGroupVO> cboList = gList.FindAll((f) => f.User_Group_No.Equals(gID)).ToList();
            ComboBinding<UserGroupVO>(cboAuth, cboList, "Auth_Name", "Auth_ID");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //유효성
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
            //vo화
            UserVO uservo = new UserVO
            {
                User_Name = txtName.Text,
                User_Pwd = txtPwd.Text,
                Auth_ID = Convert.ToInt32( cboAuth.SelectedValue),
                Create_User_No = userNo,
                User_Group_No = Convert.ToInt32(cboGroup.SelectedValue)
            };
            //DB
            if (srv.InsertUser(uservo))
            {
                if (MessageBox.Show("등록 성공", "사용자 등록", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

    }
}

