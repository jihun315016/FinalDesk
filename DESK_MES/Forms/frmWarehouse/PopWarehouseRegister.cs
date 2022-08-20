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
    public partial class PopWarehouseRegister : Form
    {
        UserVO user;
        WarehouseService srv;

        public PopWarehouseRegister(UserVO user)
        {
            this.Icon = Icon.FromHandle(Properties.Resources.free_icon_tree.GetHicon());
            InitializeComponent();
            this.user = user;
            srv = new WarehouseService();
            string[] type = new string[] { "자재", "제품" };
            comboBox2.Items.AddRange(type);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            WarehouseVO lastID = srv.GetLastID();
            string id = lastID.Warehouse_Code.ToString();
            string[] search = id.Split(new char[] { '_' });
            string name = search[0];
            string num = search[1];

            string addID = (int.Parse(search[1]) + 1).ToString().PadLeft(4, '0');

            string newid = (name + "_" + addID);

            WarehouseVO warehouse = new WarehouseVO
            {
                Warehouse_Code = newid,
                Warehouse_Name = textBox3.Text,
                Warehouse_Type = comboBox2.Text,
                Warehouse_Address = textBox4.Text,
                Create_User_No = user.User_No
            };

            bool result = srv.RegisterWarehouse(warehouse);
            if (result)
            {
                MessageBox.Show($"창고가 등록되었습니다.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("창고 등록 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
