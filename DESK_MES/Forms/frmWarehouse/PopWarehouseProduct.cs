using DESK_DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DESK_MES
{
    public partial class PopWarehouseProduct : Form
    {
        ServiceHelper service = null;
        List<ProductVO> allList; // 전체 품목 리스트
        List<WarehouseProductVO> insertList; // 보관 품목 리스트

        public PopWarehouseProduct(string warehouseCode)
        {
            InitializeComponent();

            service = new ServiceHelper("api/Warehouse");

            ResMessage<WarehouseVO> resResult = service.GetAsyncT<ResMessage<WarehouseVO>>(warehouseCode);

            if (resResult.ErrCode == 0)
            {
                txtCode.Text = resResult.Data.Warehouse_Code.ToString();
                txtName.Text = resResult.Data.Warehouse_Name.ToString();
                txtType.Text = resResult.Data.Warehouse_Type.ToString();
                txtAdress.Text = resResult.Data.Warehouse_Address.ToString();
            }
        }

        private void PopWarehouseProduct_Load(object sender, EventArgs e)
        {

        }
    }
}
