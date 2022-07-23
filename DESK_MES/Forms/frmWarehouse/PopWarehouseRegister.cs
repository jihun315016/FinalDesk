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
        ServiceHelper service = null;

        public PopWarehouseRegister()
        {
            InitializeComponent();

            service = new ServiceHelper("api/Warehouse");

            string[] type = new string[] { "자재", "제품" };
            comboBox2.Items.AddRange(type);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            WarehouseVO warehouse = new WarehouseVO
            {
                Warehouse_Code = textBox1.Text,
                Warehouse_Name = textBox3.Text,
                Warehouse_Type = comboBox2.Text,
                Warehouse_Address = textBox4.Text
            };

            ResMessage<List<WarehouseVO>> result = service.PostAsync<WarehouseVO, List<WarehouseVO>>("SaveWarehouse", warehouse);

            if (result.ErrCode == 0)
            {
                MessageBox.Show("성공적으로 등록되었습니다.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(result.ErrMsg);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
