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

            comboBox1.Items.AddRange(new string[] { "선택", "거래처코드", "거래처명" });
            comboBox1.SelectedIndex = 0;

            cboClientType.Items.AddRange(new string[] { "선택", "CUS", "VEN" });
            cboClientType.SelectedIndex = 0;

            DataGridUtil.SetInitGridView(dataGridView1);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "거래처코드", "Client_Code", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "거래처명", "Client_Name", colWidth: 200, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "거래처유형", "Client_Type", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "사업자등록번호", "Client_Number", colWidth: 200, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "연락처", "Client_Phone", colWidth: 200, alignContent: DataGridViewContentAlignment.MiddleCenter);
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<ClientVO> list = clientList.Where(p => 1 == 1).ToList();
            // 상세 검색으로 필터링
            if (panel5.Visible)
            {
                if (!string.IsNullOrWhiteSpace(txtClientCode.Text.Trim()))
                    list = list.Where(p => p.Client_Code.ToLower().Contains(txtClientCode.Text.ToLower())).ToList();

                if (!string.IsNullOrWhiteSpace(txtClientName.Text.Trim()))
                    list = list.Where(p => p.Client_Name.ToLower().Contains(txtClientName.Text.ToLower())).ToList();

                if (cboClientType.SelectedIndex > 0)
                    list = list.Where(p => p.Client_Type == cboClientType.SelectedValue.ToString().Split('_')[1]).ToList();
            }
            // 일반 검색으로 필터링
            else
            {
                // 거래처코드 검색
                if (comboBox1.SelectedIndex == 1)
                    list = list.Where(p => p.Client_Code.ToLower().Contains(textBox1.Text.ToLower())).ToList();

                // 거래처명 검색
                else if (comboBox1.SelectedIndex == 2)
                    list = list.Where(p => p.Client_Name.ToLower().Contains(textBox1.Text.ToLower())).ToList();
            }

            dataGridView1.DataSource = list;
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
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Execl Files(*.xls)|*.xls";
            dlg.Title = "엑셀파일로 내보내기";

            List<ClientVO> list = dataGridView1.DataSource as List<ClientVO>;
            if (list == null)
            {
                MessageBox.Show("조회 항목이 없습니다.");
                return;
            }

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ExcelUtil excel = new ExcelUtil();
                List<ClientVO> output = list;

                string[] columnImport = { "Client_Code", "Client_Name", "Client_Type", "Client_Number", "Client_Phone"};
                string[] columnName = { "거래처코드", "거래처명", "거래처유형", "사업자등록번호", "연락처" };

                if (excel.ExportList(output, dlg.FileName, columnImport, columnName))
                {
                    MessageBox.Show("엑셀 다운로드 완료");
                }
                else
                {
                    MessageBox.Show("엑셀 다운 실패");
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            clientList = srv.GetClientList();
            comboBox1.SelectedIndex = 0;
            textBox1.Text = string.Empty;
            comboBox1.Enabled = textBox1.Enabled = true;
            panel5.Visible = false;
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = clientList;

            txtClientCode.Text = txtClientName.Text = string.Empty;
            cboClientType.SelectedIndex = 0;
        }
    }
}
