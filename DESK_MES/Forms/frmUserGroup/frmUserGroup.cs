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
    public partial class frmUserGroup : FormStyle_2
    {
        UserGroupService srv;
        List<UserGroupVO> allList;
        int selectUser=0;
        string user;

        public frmUserGroup()
        {
            InitializeComponent();
            label1.Text = "사용자 그룹 관리";
        }
        /// <summary>
        /// 김준모/Dgv에 데이터 바인딩(DB에서 가져옴), 화면표시된 그리드 데이터를 전역List에 기록
        /// </summary>
        public void BindingGdv()
        {
            List<UserGroupVO> list = srv.SelectUserGroupList();
            allList = list;
            dataGridView1.DataSource = allList;
        }
        /// <summary>
        /// 김준모/콤보박스 바인딩 메서드(UserGfoup전용)
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
                { Auth_Name = "전체" }
                );
            }
            cbo.DropDownStyle = ComboBoxStyle.DropDownList;
            cbo.DisplayMember = dis;
            cbo.ValueMember = val;

            cbo.DataSource = list; ;
        }
        private void frmUserGroup_Load(object sender, EventArgs e)
        {
            //기본 설정
            if (srv == null)
                srv = new UserGroupService();

            dtpCreate.Format = DateTimePickerFormat.Custom;
            dtpCreate.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";
            dtpUpdate.Format = DateTimePickerFormat.Custom;
            dtpUpdate.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";

            List<UserGroupVO> cbo= srv.SelectAuthList();
            ComboBinding(cboGroupType, cbo, "Auth_Name", "Auth_ID", blank: true) ;

            DataGridUtil.SetInitGridView(dataGridView1);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "사용자 그룹번호", "User_Group_No");
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "사용자 그룹명", "User_Group_Name");
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "사용자 그룹유형", "User_Group_TypeName");
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생성시간", "Create_Time");
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생성사용자", "Create_User_Name");
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "변경시간", "Update_Time");
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "변경사용자", "Update_User_Name");
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "사용자 그룹유형코드", "User_Group_Type", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생성사용자ID", "Create_User_No", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "변경사용자ID", "Update_User_No", isVisible: false);

            //데이터
            BindingGdv();
            List<UserGroupVO> auth = srv.SelectAuthList();
            
        }

        //등록
        private void btnAdd_Click(object sender, EventArgs e)
        {
            PopUserGroupRegister pop = new PopUserGroupRegister();
            if (pop.ShowDialog() == DialogResult.OK)
            {
                BindingGdv();
            }
        }

        //변경
        private void button5_Click(object sender, EventArgs e)
        {
            if (selectUser != 0)
            {
                PopUserGroupModify pop = new PopUserGroupModify(selectUser, user);
                if (pop.ShowDialog() == DialogResult.OK)
                {
                    BindingGdv();
                }
            }
        }

        //DGV셀클릭
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            selectUser = Convert.ToInt32(dataGridView1[0, e.RowIndex].Value);
            txtID.Text = dataGridView1[0, e.RowIndex].Value.ToString();
            txtName.Text = dataGridView1[1, e.RowIndex].Value.ToString();
            txtType.Text = dataGridView1[2, e.RowIndex].Value.ToString();
            dtpCreate.Value = Convert.ToDateTime( dataGridView1[3, e.RowIndex].Value);
            txtCreateUser.Text = dataGridView1[4, e.RowIndex].Value.ToString();
            if (dataGridView1[5, e.RowIndex].Value != null)
            {
                dtpUpdate.Value = Convert.ToDateTime(dataGridView1[5, e.RowIndex].Value);
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
            if (dataGridView1[6, e.RowIndex].Value != null)
            {
                txtUpdateUser.Text = dataGridView1[6, e.RowIndex].Value.ToString();
            }
            
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            //BindingGdv();
            //textBox1.Text = "";
            //comboBox1.SelectedIndex = 0;
            //selectUser = 0;
            //cboGroupType.SelectedIndex = 0;
            //cboUserName.SelectedIndex = 0;
            //txtID.Text = "";
            //txtName.Text = "";
            //txtType.Text = "";
            //dtpCreate.Value = DateTime.Now;
            //txtCreateUser.Text = "";
            //dtpUpdate.Value = DateTime.Now;
            //txtUpdateUser.Text = "";
        }
    }
}
