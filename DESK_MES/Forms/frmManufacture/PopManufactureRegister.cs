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
    public partial class PopManufactureRegister : Form
    {
        UserVO user;
        ManufactureService srv = null;
        bool chk = false;

        public PopManufactureRegister(UserVO user)
        {
            InitializeComponent();
            srv = new ManufactureService();
            this.user = user;
        }

        private void PopManufactureRegister_Load(object sender, EventArgs e)
        {
            panel5.Visible = false;

            string[] productType = new string[] { "선택", "FERT", "HALB" };
            cboType.Items.AddRange(productType);
            cboType.SelectedIndex = 0;

            checkBox1.Checked = chk;
 
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!chk)
            {
                panel5.Visible = true;
                panel6.Visible = false;

                cboOrderCode.DataSource = null;
                cboOrderCode.Items.Clear();
                List<ManufactureVO> orderNo = srv.GetOrderList();
                cboOrderCode.DisplayMember = "Order_No";
                cboOrderCode.ValueMember = "Order_No";
                cboOrderCode.DataSource = orderNo;

                checkBox2.Checked = true;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (!chk)
            {
                panel5.Visible = false;
                panel6.Visible = true;

                cboType.SelectedIndex = 0;

                cboOrderCode.DataSource = null;
                cboOrderCode.Text = null;
                cboOrderCode.Items.Clear();

                cboProductCode.DataSource = null;
                cboProductCode.Text = null;
                cboProductCode.Items.Clear();

                checkBox1.Checked = false;
            }

        }

        private void cboOrderCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            int orderNo = Convert.ToInt32(cboOrderCode.SelectedValue);

            List<ManufactureVO> OrderproductList = srv.GetOrderProductListForManufacture(orderNo);
            cboProductCode.DisplayMember = "Product_Name";
            cboProductCode.ValueMember = "Product_Code";
            cboProductCode.DataSource = OrderproductList;
        }

        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboProductCode.DataSource = null;
            cboProductCode.Text = null;
            cboProductCode.Items.Clear();

            string type = cboType.Text.ToString();

            List<ManufactureVO> productList = srv.GetProductListByType(type);
            cboProductCode.DisplayMember = "Product_Name";
            cboProductCode.ValueMember = "Product_Code";
            cboProductCode.DataSource = productList;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ManufactureVO plan = new ManufactureVO
            {
                // 주문서코드
                Order_No = Convert.ToInt32(cboOrderCode.SelectedValue),
                Product_Code = cboProductCode.SelectedValue.ToString(),
                Planned_Qty = Convert.ToInt32(numericUpDown1.Value),
                Start_Date = dtpStartDate.Value.ToShortDateString(),
                Estimated_Date = dtpFinishDueDate.Value.ToShortDateString(),
                Production_Plan_Status = "미정",
                Production_Plan_User_No = user.User_No,
                Create_User_No = user.User_No
            };

            bool result = srv.RegisterManufacturePlan(plan);
            if (result)
            {
                MessageBox.Show($"생산계획이 등록되었습니다.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("생산계획처리 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
