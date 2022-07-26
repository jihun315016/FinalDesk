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
    public partial class frmEquipment : FormStyle_2
    {
        EquipmentService srv;
        List<EquipmentVO> allList;
        List<EquipmentVO> saveList;
        int userNO;
        string userName;
        string saveFileName;
        bool flag = false;
        public frmEquipment()
        {
            InitializeComponent();
            label1.Text = "설비 관리";
        }
        public void BindingGdv()
        {
            List<EquipmentVO> list = srv.SelectEquipmentAllList();
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
                { Auth_Name = "전체", User_Group_Name = "전체" }
                );
            }
            cbo.DropDownStyle = ComboBoxStyle.DropDownList;
            cbo.DisplayMember = dis;
            cbo.ValueMember = val;

            cbo.DataSource = list;
        }
        private void ResetDetail()
        {
            txtEquipNo.Text = "";
            txtName.Text = "";
            txtType.Text = "";
            txtInoper.Text = "";
            dtpInoper.Value = DateTime.Now; //여기 정보 기입
            dtpCreate.Value = DateTime.Now;
            txtCreateUser.Text = "";
            dtpUpdate.Value = DateTime.Now;
            txtUpdateUser.Text = "";
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            PopEquipmentRegister pop = new PopEquipmentRegister();
            pop.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PopEquipmentModify pop = new PopEquipmentModify();
            pop.ShowDialog();
        }

        private void frmEquipment_Load(object sender, EventArgs e)
        {
            if (srv == null)
                srv = new EquipmentService();
            //UserGroupService srvG = new UserGroupService();

            dtpCreate.Format = DateTimePickerFormat.Custom;
            dtpCreate.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";
            dtpUpdate.Format = DateTimePickerFormat.Custom;
            dtpUpdate.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";


            cboDate.Items.Add("전체");
            cboDate.Items.Add("생성시간");
            cboDate.Items.Add("변경시간");
            cboDate.SelectedIndex = 0;
            cboDate.DropDownStyle = ComboBoxStyle.DropDownList;

            DataGridUtil.SetInitGridView(dgvMain);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "설비 번호", "Equipment_No"); //0
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "설비명", "Equipment_Name"); //1
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "비가동 여부", "Is_Inoperative"); //2
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "생성 시간", "Create_Time"); //3
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "생성 사용자명", "Create_User_Name"); //4

            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "변경 시간", "Update_Time"); //5
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "변경 사용자명", "Update_User_Name"); //6
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "삭제 여부", "Is_Delete"); //7

            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "생성사용자ID", "Create_User_No", isVisible: false); //8
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "변경사용자ID", "Update_User_No", isVisible: false); //9
            BindingGdv();

            flag = false;
        }
    }
}
