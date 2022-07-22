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
    public partial class frmUser : FormStyle_2
    {
        UserService srv;
        List<UserVO> allList;
        List<UserVO> saveList;
        string user;
        string saveFileName;
        int selectUser;
        bool flag = false;
        public frmUser()
        {
            InitializeComponent();
            label1.Text = "사용자 관리";
        }
        /// <summary>
        /// 김준모/Dgv에 데이터 바인딩(DB에서 가져옴), 화면표시된 그리드 데이터를 전역List에 기록
        /// </summary>
        public void BindingGdv()
        {
            List<UserVO> list = srv.SelectUserList();
            saveList = allList = list;
            dgvMain.DataSource = null;
            dgvMain.DataSource = allList;
        }
        /// <summary>
        /// 김준모/콤보박스 바인딩 메서드(UserVO)
        /// </summary>
        /// <param name="cbo"></param>
        /// <param name="list"></param>
        /// <param name="dis"></param>
        /// <param name="val"></param>
        private void ComboBinding(ComboBox cbo, List<UserGroupVO> list, string dis, string val, bool blank = false)
        {
            if (blank)
            {
                list.Insert(0, new UserGroupVO
                { Auth_Name = "전체",User_Group_Name = "전체" }
                );
            }
            cbo.DropDownStyle = ComboBoxStyle.DropDownList;
            cbo.DisplayMember = dis;
            cbo.ValueMember = val;

            cbo.DataSource = list;
        }

        private void frmUser_Load(object sender, EventArgs e)
        {
            if (srv == null)
                srv = new UserService();
           UserGroupService srvG = new UserGroupService();

            dtpCreate.Format = DateTimePickerFormat.Custom;
            dtpCreate.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";
            dtpUpdate.Format = DateTimePickerFormat.Custom;
            dtpUpdate.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";

            List<UserGroupVO> cbo = srvG.SelectAuthList();
            ComboBinding(cboAuth, cbo, "Auth_Name", "Auth_ID", blank: true);
            comboBox1.Items.Add("사번");
            comboBox1.Items.Add("사용자명");
            comboBox1.SelectedIndex = 0;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            List<UserGroupVO> cbo2 = srvG.SelectGroupNameList();
            ComboBinding(cboGroupType, cbo2, "User_Group_Name", "User_Group_No", blank: true);

            cboDate.Items.Add("전체");
            cboDate.Items.Add("생성시간");
            cboDate.Items.Add("변경시간");
            cboDate.SelectedIndex = 0;
            cboDate.DropDownStyle = ComboBoxStyle.DropDownList;

            DataGridUtil.SetInitGridView(dgvMain);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "사번", "User_No"); //0
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "사용자명", "User_Name"); //1
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "사용자 그룹", "User_Group_Name"); //2
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "비밀번호", "User_Pwd"); //3
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "권한", "Auth_Name"); //4

            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "생성시간", "Create_Time"); //5
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "생성사용자", "Create_User_Name"); //6
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "변경시간", "Update_Time"); //7
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "변경사용자", "Update_User_Name"); //8

            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "퇴사여부", "Is_Delete"); //9

            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "사용자 그룹유형코드", "User_Group_Type", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "생성사용자ID", "Create_User_No", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "변경사용자ID", "Update_User_No", isVisible: false);
            BindingGdv();

            flag = false;
        }
        private void dgvMain_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;

            selectUser = Convert.ToInt32(dgvMain[0, e.RowIndex].Value);
            txtUserNo.Text = dgvMain[0, e.RowIndex].Value.ToString();
            txtName.Text = dgvMain[1, e.RowIndex].Value.ToString();

            if (dgvMain[2, e.RowIndex].Value != null) { txtUserG.Text = dgvMain[2, e.RowIndex].Value.ToString(); }

            if (dgvMain[4, e.RowIndex].Value != null) { txtAuth.Text = dgvMain[4, e.RowIndex].Value.ToString(); }

            txtPwd.Text = dgvMain[3, e.RowIndex].Value.ToString();
            
            dtpCreate.Value = Convert.ToDateTime(dgvMain[5, e.RowIndex].Value);
            txtCreateUser.Text = dgvMain[6, e.RowIndex].Value.ToString();
            if (dgvMain[7, e.RowIndex].Value != null)
            {
                dtpUpdate.Value = Convert.ToDateTime(dgvMain[7, e.RowIndex].Value);
                dtpUpdate.Visible = true;
                txtUpdateUser.Visible = true;
                lblTime.Visible = true;
                lblUser.Visible = true;
            }
            else
            {
                dtpUpdate.Visible = false;
                txtUpdateUser.Visible = false;
                lblTime.Visible = false;
                lblUser.Visible = false;
            }
            if (dgvMain[7, e.RowIndex].Value != null)
            {
                txtUpdateUser.Text = dgvMain[8, e.RowIndex].Value.ToString();
            }

            txtDelete.Text = dgvMain[9, e.RowIndex].Value.ToString();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            PopUserResister pop = new PopUserResister();
            pop.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            PopUserModify pop = new PopUserModify();
            pop.ShowDialog();
        }

        private void btnOpenDetail_Click(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                //일반검색
                textBox1.Enabled = true;
                cboGroupType.SelectedIndex = 0;
                cboAuth.SelectedIndex = 0;
                cboDate.SelectedIndex = 0;
            }
            else
            {
                flag = true;
                //상세검색
                textBox1.Enabled = false;
                comboBox1.SelectedIndex = 0;
            }
        }
    }
}
