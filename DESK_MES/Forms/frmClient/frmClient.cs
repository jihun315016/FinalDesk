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
    public partial class frmClient : FormStyle_2
    {
        UserVO user;
        ClientService srv;
        List<ClientVO> clientList;
        string clientCode = null;

        public frmClient()
        {
            InitializeComponent();
            label1.Text = "거래처 관리";

        }
        private void frmClient_Load(object sender, EventArgs e)
        {
            this.user = ((frmMain)(this.MdiParent)).userInfo;
            srv = new ClientService();


            DataGridUtil.SetInitGridView(dataGridView1);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "거래처코드", "Client_Code", colWidth: 100);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "거래처명", "Client_Name", colWidth: 60);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "거래처유형", "Client_Type", colWidth: 80);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "사업자등록번호", "Client_Number", colWidth: 60);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "연락처", "Client_Phone", colWidth: 120);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생성일자", "Create_Time", colWidth: 60, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생성사용자", "Create_User_Name", colWidth: 80, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "수정일자", "Update_Time", colWidth: 60, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "수정사용자", "Update_User_Name", colWidth: 60, isVisible: false);

            dtpCreateTime.Format = DateTimePickerFormat.Custom;
            dtpCreateTime.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";

            dtpModifyTime.Format = DateTimePickerFormat.Custom;
            dtpModifyTime.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";

            LoadData();
        }

        private void LoadData()
        {
            clientList = srv.GetClientList();
            dataGridView1.DataSource = clientList;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            clientCode = dataGridView1[0, e.RowIndex].Value.ToString();

            txtCode.Text = dataGridView1["Client_Code", e.RowIndex].Value.ToString();
            txtName.Text = dataGridView1["Client_Name", e.RowIndex].Value.ToString();
            txtType.Text = dataGridView1["Client_Type", e.RowIndex].Value.ToString();
            txtNumber.Text = dataGridView1["Client_Number", e.RowIndex].Value.ToString();
            txtPhone.Text = dataGridView1["Client_Phone", e.RowIndex].Value.ToString();
            dtpCreateTime.Text = dataGridView1["Create_Time", e.RowIndex].Value.ToString();
            txtCreateUser.Text = (dataGridView1["Create_User_Name", e.RowIndex].Value ?? string.Empty).ToString();
            dtpModifyTime.Text = (dataGridView1["Update_Time", e.RowIndex].Value ?? string.Empty).ToString();
            txtModifyUser.Text = (dataGridView1["Update_User_Name", e.RowIndex].Value ?? string.Empty).ToString();

        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            PopClientRegister pop = new PopClientRegister(user);
            if (pop.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (clientCode != null)
            {
                PopClientModify pop = new PopClientModify(clientCode, user);
                if (pop.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("변경하실 항목을 선택해주세요");
                return;                
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {

        }


    }
}
