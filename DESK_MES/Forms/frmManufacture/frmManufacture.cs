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
    public partial class frmManufacture : FormStyle_2
    {
        UserVO user;
        ManufactureService srv;
        List<ManufactureVO> manufactureList;
        string manufactureCode;

        public frmManufacture()
        {
            InitializeComponent();
            label1.Text = "생산계획 관리";
        }

        private void frmManufacture_Load(object sender, EventArgs e)
        {
            this.user = ((frmMain)(this.MdiParent)).userInfo;
            srv = new ManufactureService();

            comboBox1.Items.AddRange(new string[] { "선택", "품목명", "계획상태" });
            comboBox1.SelectedIndex = 0;

            button4.Visible = false;
            button2.Visible = false;

            DataGridUtil.SetInitGridView(dataGridView1);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생산코드", "Production_Code", colWidth: 100, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "주문서코드", "Order_No", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생산 품목코드", "Product_Code", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생산 품목명", "Product_Name", colWidth: 300, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생산 계획 수량", "Planned_Qty", colWidth: 100, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생산 완료 수량", "Production_Qty", colWidth: 100, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생산 시작일", "Start_Date", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생산 완료 예정일", "Estimated_Date", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            //DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생산 완료일", "Complete_Date", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생산 계획 상태", "Production_Plan_Status", colWidth: 100, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생산계획 담당자", "Production_Plan_User_Name", colWidth: 60, isVisible: false);
            //DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생산 진행 상태", "Production_Status", colWidth: 100, alignContent: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생성 시간", "Create_Time", colWidth: 60, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생성 사용자", "Create_User_Name", colWidth: 80, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "변경 시간", "Update_Time", colWidth: 60, isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "변경 사용자", "Update_User_Name", colWidth : 60, isVisible: false);

            LoadData();
        }

        private void LoadData()
        {
            manufactureList = srv.GetmanufactureList();
            dataGridView1.DataSource = manufactureList;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PopManufactureRegister pop = new PopManufactureRegister(user);
            if (pop.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PopManufactureModify pop = new PopManufactureModify();
            pop.ShowDialog();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            manufactureCode = dataGridView1[0, e.RowIndex].Value.ToString();

            txtManufactureCode.Text = dataGridView1["Production_Code", e.RowIndex].Value.ToString();
            txtOrderCode.Text = Convert.ToInt32(dataGridView1["Order_No", e.RowIndex].Value) == 0 ? "" : dataGridView1["Order_No", e.RowIndex].Value.ToString();
            txtProductCode.Text = dataGridView1["Product_Code", e.RowIndex].Value.ToString();
            txtProductName.Text = dataGridView1["Product_Name", e.RowIndex].Value.ToString();
            txtPlanQty.Text = dataGridView1["Planned_Qty", e.RowIndex].Value.ToString();
            txtFinishQty.Text = (dataGridView1["Production_Qty", e.RowIndex].Value ?? string.Empty).ToString();
            txtStartDate.Text = dataGridView1["Start_Date", e.RowIndex].Value.ToString();
            txtStartDueDate.Text = dataGridView1["Estimated_Date", e.RowIndex].Value.ToString();
            //txtFinishDate.Text = (dataGridView1["Complete_Date", e.RowIndex].Value ?? string.Empty).ToString();
            txtPlanState.Text = dataGridView1["Production_Plan_Status", e.RowIndex].Value.ToString();
            txtPlanUser.Text = dataGridView1["Production_Plan_User_Name", e.RowIndex].Value.ToString();
            //txtManufactuerState.Text = (dataGridView1["Production_Status", e.RowIndex].Value ?? string.Empty).ToString();
            dtpCreateTime.Text = dataGridView1["Create_Time", e.RowIndex].Value.ToString();
            txtCreateUserName.Text = (dataGridView1["Create_User_Name", e.RowIndex].Value ?? string.Empty).ToString();
            dtpModifyTime.Text = (dataGridView1["Update_Time", e.RowIndex].Value ?? string.Empty).ToString();
            txtModifyUserName.Text = (dataGridView1["Update_User_Name", e.RowIndex].Value ?? string.Empty).ToString();

            if (txtPlanState.Text.Contains("미정"))
            {
                button4.Visible = true;
                button2.Visible = true;
            }
            else
            {
                button4.Visible = false;
                button2.Visible = false;
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // 생산계획 확정
            // 발주 확정 처리
            ManufactureVO plan = new ManufactureVO
            {
                Production_Code = Convert.ToInt32(txtManufactureCode.Text),
                Production_Plan_Status = "확정",
                Update_User_No = user.User_No
            };
            bool result = srv.UpdateManufactureState(plan);
            if (result)
            {
                MessageBox.Show($"생산계획이 확정되었습니다.");
                LoadData();
            }
            else
            {
                MessageBox.Show("생산계획이 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 생산계획 취소
            ManufactureVO plan = new ManufactureVO
            {
                Production_Code = Convert.ToInt32(txtManufactureCode.Text),
                Production_Plan_Status = "취소",
                Update_User_No = user.User_No
            };
            bool result = srv.UpdateManufactureState(plan);
            if (result)
            {
                MessageBox.Show($"생산계획이 취소되었습니다.");
                LoadData();
            }
            else
            {
                MessageBox.Show("생산계획 취소 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<ManufactureVO> list = manufactureList.Where(p => 1 == 1).ToList();
            // 상세 검색으로 필터링
            if (panel5.Visible)
            {
                //if (!string.IsNullOrWhiteSpace(txtOrderCode.Text.Trim()))
                //    list = list.Where(p => p.Client_Code.Contains(txtClientCode.Text.ToLower())).ToList();

                //if (!string.IsNullOrWhiteSpace(txtClientName.Text.Trim()))
                //    list = list.Where(p => p.Client_Name.ToLower().Contains(txtClientName.Text.ToLower())).ToList();

                //if (cboOrderState.SelectedIndex > 0)
                //    list = list.Where(p => p.Order_State == cboOrderState.SelectedValue.ToString().Split('_')[1]).ToList();
            }
            // 일반 검색으로 필터링
            else
            {
                // 거래처코드 검색
                if (comboBox1.SelectedIndex == 1)
                    list = list.Where(p => p.Product_Name.ToLower().Contains(textBox1.Text.ToLower())).ToList();

                // 입고상태 검색
                else if (comboBox1.SelectedIndex == 2)
                    list = list.Where(p => p.Production_Plan_Status.ToLower().Contains(textBox1.Text.ToLower())).ToList();
            }

            dataGridView1.DataSource = list;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            manufactureList = srv.GetmanufactureList();
            comboBox1.SelectedIndex = 0;
            textBox1.Text = string.Empty;
            comboBox1.Enabled = textBox1.Enabled = true;
            panel5.Visible = false;
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = manufactureList;

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void frmManufacture_Shown(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }
    }
}
