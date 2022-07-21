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

        public frmUserGroup()
        {
            InitializeComponent();
            label1.Text = "사용자 그룹 관리";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PopUserGroupRegister pop = new PopUserGroupRegister();
            pop.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (selectUser != 0)
            {
                PopUserGroupModify pop = new PopUserGroupModify(selectUser);
                pop.ShowDialog();
            }
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

            DataGridUtil.SetInitGridView(dataGridView1);

            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "사용자 그룹번호", "User_Group_No");
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "사용자 그룹명", "User_Group_Name");
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "사용자 그룹유형", "User_Group_TypeName");
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생성시간", "Create_Time");
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생성사용자", "Create_User_Name");
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "변경시간", "Update_Time");
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "변경사용자", "Update_User_Name");
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "사용자 그룹유형코드", "User_Group_Type", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생성사용자ID", "Create_User_No", isVisible : false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "변경사용자ID", "Update_User_No", isVisible: false);

            //데이터
            List<UserGroupVO> list = srv.SelectUserGroupList();
            allList = list;
            dataGridView1.DataSource = allList;
        }

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
    }
}
