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
        int selectUser;
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
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "설비번호", "Equipment_No"); //0
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "설비명", "Equipment_Name"); //1
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "유형", "Operation_Type_Name"); //2
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "상태", "Is_Inoperative"); //3
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "최근다운시간", "Inoperative_Start_Time"); //4

            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "생성시간", "Create_Time"); //5
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "생성자", "Create_User_Name"); //6
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "변경시간", "Update_Time"); //7
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "변경자", "Update_User_Name"); //8

            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "삭제 여부", "Is_Delete", isVisible: false); //9
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "유형 번호", "Operation_Type_No", isVisible: false); //10
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "생성자ID", "Create_User_No", isVisible: false); //11
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "변경자ID", "Update_User_No", isVisible: false); //12
            BindingGdv();

            flag = false;
        }

        private void dgvMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            ResetDetail();

            selectUser = Convert.ToInt32(dgvMain[0, e.RowIndex].Value);
            txtEquipNo.Text = dgvMain[0, e.RowIndex].Value.ToString();
            txtName.Text = dgvMain[1, e.RowIndex].Value.ToString();
            if (dgvMain[2, e.RowIndex].Value != null)
            {
                txtType.Text = dgvMain[2, e.RowIndex].Value.ToString();
            }
            txtInoper.Text = dgvMain[3, e.RowIndex].Value.ToString();
            if (dgvMain[4, e.RowIndex].Value != null) 
            {
                dtpInoper.Value = Convert.ToDateTime( dgvMain[4, e.RowIndex].Value);
            }
            dtpCreate.Value = Convert.ToDateTime(dgvMain[5, e.RowIndex].Value);
            txtCreateUser.Text = dgvMain[6, e.RowIndex].Value.ToString();
            if (dgvMain[7, e.RowIndex].Value != null)
            {
                dtpUpdate.Value = Convert.ToDateTime(dgvMain[7, e.RowIndex].Value);
                txtUpdateUser.Text = dgvMain[8, e.RowIndex].Value.ToString();
                dtpUpdate.Visible = true;
                txtUpdateUser.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
            }
            else
            {
                dtpUpdate.Visible = false;
                txtUpdateUser.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
            }            
        }
    }
}
