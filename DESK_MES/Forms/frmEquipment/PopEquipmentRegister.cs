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
    public partial class PopEquipmentRegister : Form
    {
        EquipmentService srv;
        UserVO user;
        int? newNum;
        public PopEquipmentRegister(int lastNum, UserVO userV)
        {
            newNum = lastNum;
            user = userV;
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PopEquipmentRegister_Load(object sender, EventArgs e)
        {
            if (srv == null)
                srv = new EquipmentService();
            //
            ComboBoxUtil.ComboBinding<EquipmentVO>(comboBox1, srv.SelectOperationTypeList(), "Name", "Code");

            dateTimePicker3.Format = DateTimePickerFormat.Custom;
            dateTimePicker3.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";

            txtCode.Enabled = false;
            if (newNum != null && newNum != 0)
            {
                txtCode.Text = newNum.ToString();
            }
            txtName.Text = "";
            txtUser.Enabled = false;
            if (user != null)
            {
                txtUser.Text= user.User_Name;
            }
        }
    }
}