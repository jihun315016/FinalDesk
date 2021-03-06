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
    public partial class frmWarehouse : FormStyle_2
    {
        ServiceHelper service;
        string warehouseCode = null;

        public frmWarehouse()
        {
            InitializeComponent();
            label1.Text = "창고 관리";
        }
        private void frmWarehouse_Load(object sender, EventArgs e)
        {
            service = new ServiceHelper("api/Warehouse");
            LoadData();
        }
        private void LoadData()
        {
            ResMessage<List<WarehouseVO>> result = service.GetAsync<List<WarehouseVO>>("Warehouse");
            if (result != null)
            {
                dataGridView1.DataSource = result.Data;
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            warehouseCode = dataGridView1[0, e.RowIndex].Value.ToString();

            ResMessage<WarehouseVO> resResult = service.GetAsyncT<ResMessage<WarehouseVO>>(warehouseCode);

            if (resResult.ErrCode == 0)
            {
                txtCode.Text = resResult.Data.Warehouse_Code.ToString();
                txtName.Text = resResult.Data.Warehouse_Name.ToString();
                txtType.Text = resResult.Data.Warehouse_Type.ToString();
                txtAdress.Text = resResult.Data.Warehouse_Address.ToString();
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            PopWarehouseRegister pop = new PopWarehouseRegister();
            if (pop.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            PopWarehouseModify pop = new PopWarehouseModify();
            if (pop.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }
    }
}
