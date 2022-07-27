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
using DESK_MES.Service;

namespace DESK_MES
{
    public partial class frmOIItemRelation : FormStyle_1
    {
        OperationService operationSrv;
        UserVO user;
        DataSet ds;

        public frmOIItemRelation()
        {
            InitializeComponent();
        }

        private void frmOperationInspectItemRelation_Load(object sender, EventArgs e)
        {
            operationSrv = new OperationService();
            ds = operationSrv.GetOIRelation();
            this.user = ((frmMain)(this.MdiParent)).userInfo;
            InitControl();
        }

        void InitControl()
        {
            label1.Text = "공정 - 검사 데이터 항목 관리";           

            comboBox1.Items.AddRange(new string[] { "검색 조건", "공정 번호", "공정명" });
            comboBox1.SelectedIndex = 0;

            DataGridUtil.SetInitGridView(dgvOperation);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvOperation, "공정 번호", "Operation_No");
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvOperation, "공정명", "Operation_Name", colWidth: 130);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvOperation, "불량 체크 여부", "Is_Check_Deffect", colWidth: 180);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvOperation, "검사 데이터 체크 여부", "Is_Check_Inspect", colWidth: 180);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvOperation, "자재 사용 여부", "Is_Check_Marerial", colWidth: 180);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvOperation, "등록 시간", "Create_Time", colWidth: 200);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvOperation, "등록 사용자", "Create_User_Name", colWidth: 120);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvOperation, "등록 사용자 번호", "Create_User_No", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvOperation, "수정 시간", "Update_Time", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvOperation, "수정 사용자 번호", "Update_User_No", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvOperation, "수정 사용자", "Update_User_Name", isVisible: false);

            dgvOperation.DataSource = ds.Tables[0];
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //List<OperationVO> list = operationList.Where(p => 1 == 1).ToList();

            DataView dv = new DataView(ds.Tables[0]);
            

            // 품번 검색
            if (comboBox1.SelectedIndex == 1)
                dv.RowFilter = $"CONVERT(Operation_No, System.String) LIKE '%{textBox1.Text}%'";

            // 품명 검색
            else if (comboBox1.SelectedIndex == 2)
                dv.RowFilter = $"Operation_Name LIKE '%{textBox1.Text}%'";

            dgvOperation.DataSource = dv.ToTable();
        }

        private void btnOpenDetail_Click(object sender, EventArgs e)
        {
            if (panel5.Visible)
                comboBox1.Enabled = textBox1.Enabled = false;
            else
                comboBox1.Enabled = textBox1.Enabled = true;
        }

        private void dgvOperation_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            dtpCreateTime.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";
            dtpUpdateTime.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";

            List<OperationVO> temp = operationSrv.GetOperationList(Convert.ToInt32(dgvOperation["Operation_No", e.RowIndex].Value));
            OperationVO oper = temp.FirstOrDefault();
            txtOperNoDetail.Text = oper.Operation_No.ToString();
            txtOperNameDetail.Text = oper.Operation_Name;
            txtIsDeffectDetail.Text = oper.Is_Check_Deffect == "Y" ? "예" : "아니오";
            txtIsInspectDetail.Text = oper.Is_Check_Inspect == "Y" ? "예" : "아니오";
            txtMaterialDetail.Text = oper.Is_Check_Marerial == "Y" ? "예" : "아니오";
            dtpCreateTime.Value = oper.Create_Time;
            txtCreateUserDetail.Text = oper.Create_User_Name;

            if (oper.Update_Time.ToString() == "0001-01-01 오전 12:00:00")
            {
                dtpUpdateTime.Format = DateTimePickerFormat.Custom;
                dtpUpdateTime.CustomFormat = " ";
                txtUpdateUserDetail.Text = string.Empty;
            }
            else
            {
                dtpUpdateTime.Format = dtpCreateTime.Format;
                dtpUpdateTime.Value = oper.Update_Time;
                txtUpdateUserDetail.Text = oper.Update_User_Name;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtOperNoDetail.Text))
            {
                MessageBox.Show("공정을 선택하세요.");
                return;
            }

            OperationVO oper = operationSrv.GetOperationList(Convert.ToInt32(txtOperNoDetail.Text)).FirstOrDefault();

            PopOIItemRelationRegister pop = new PopOIItemRelationRegister(user, oper);
            pop.ShowDialog();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ds = operationSrv.GetOIRelation();
            comboBox1.Enabled = textBox1.Enabled = true;
            dgvOperation.DataSource = null;
            dataGridView2.DataSource = null;
        }
    }
}
