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
using System.Net.Http;
using Newtonsoft.Json;

namespace DESK_MES
{
    public partial class PopWarehouseModify : Form
    {
        UserVO user;
        WarehouseService srv;

        public PopWarehouseModify(string warehouseCode, UserVO user)
        {
            InitializeComponent();
            this.user = user;
            srv = new WarehouseService();

            WarehouseVO info = srv.GetWarehouseInfoByCode(warehouseCode);
            
            txtCode.Text = info.Warehouse_Code;
            txtName.Text = info.Warehouse_Name;
            cboType.Text = info.Warehouse_Type;
            txtAddr.Text = info.Warehouse_Address;
        }
        private void btnModify_Click(object sender, EventArgs e)
        {
            WarehouseVO warehouse = new WarehouseVO
            {
                Warehouse_Code = txtCode.Text,
                Warehouse_Name = txtName.Text,
                Warehouse_Type = cboType.Text,
                Warehouse_Address = txtAddr.Text,
                Update_User_No = user.User_No
            };
            bool result = srv.UpdateWarehouseInfo(warehouse);
            if (result)
            {
                MessageBox.Show("성공적으로 수정되었습니다.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("수정 중 오류가 발생했습니다.");
            }

        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string warehouseNO = txtCode.Text.Trim();

            bool result = srv.DeleteWarehouseInfo(warehouseNO);
            if (result)
            {
                MessageBox.Show("성공적으로 삭제되었습니다.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("수정 중 오류가 발생했습니다.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
