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
        ServiceHelper service = null;

        public PopWarehouseModify(string warehouseCode)
        {
            InitializeComponent();

            service = new ServiceHelper("api/Warehouse");

            ResMessage<WarehouseVO> resResult = service.GetAsyncT<ResMessage<WarehouseVO>>(warehouseCode);

            if (resResult.ErrCode == 0)
            {
                txtCode.Text = resResult.Data.Warehouse_Code.ToString();
                txtName.Text = resResult.Data.Warehouse_Name.ToString();
                cboType.Text = resResult.Data.Warehouse_Type.ToString();
                txtAddr.Text = resResult.Data.Warehouse_Address.ToString();
            }
        }
        private void btnModify_Click(object sender, EventArgs e)
        {
            WarehouseVO warehouse = new WarehouseVO
            {
                Warehouse_Code = txtCode.Text,
                Warehouse_Name = txtName.Text,
                Warehouse_Type = cboType.Text,
                Warehouse_Address = txtAddr.Text
            };

            ResMessage<List<WarehouseVO>> result = service.PostAsync<WarehouseVO, List<WarehouseVO>>("UpdateWarehouse", warehouse);

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
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string warehouseNO = txtCode.Text.Trim();

            ResMessage resResult = service.GetAsyncNon($"DelWarehouse/{warehouseNO}");

            if (resResult.ErrCode == 0)
            {
                MessageBox.Show("삭제되었습니다.");
            }
            else
            {
                MessageBox.Show(resResult.ErrMsg);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
