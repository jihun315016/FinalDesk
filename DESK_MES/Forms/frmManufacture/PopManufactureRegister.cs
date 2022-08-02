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

        public PopManufactureRegister(UserVO user)
        {
            InitializeComponent();
            srv = new ManufactureService();
            this.user = user;
        }

        private void PopManufactureRegister_Load(object sender, EventArgs e)
        {
            panel5.Visible = false;
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                panel5.Visible = true;

                cboOrderCode.DataSource = null;
                cboOrderCode.Items.Clear();
                List<ManufactureVO> orderNo = srv.GetOrderList();
                cboOrderCode.DisplayMember = "Order_No";
                cboOrderCode.ValueMember = "Order_No";
                cboOrderCode.DataSource = orderNo;
            }
            else
            {

                cboOrderCode.DataSource = null;
                cboOrderCode.Items.Clear();
                panel5.Visible = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
