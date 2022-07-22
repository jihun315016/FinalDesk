using DESK_DTO;
using DESK_MES.Service;
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
    public partial class PopProductsModify : Form
    {
        ProductService productSrv;
        ServiceHelper srvHelper;

        public PopProductsModify(ProductVO prd)
        {
            InitializeComponent();
            productSrv = new ProductService();
            srvHelper = new ServiceHelper("api/Client");

            txtCode.Text = prd.Product_Code;
            txtName.Text = prd.Client_Name;            
            txtPrice.Text = prd.Price.ToString();
            txtUnit.Text = prd.Unit.ToString();

            List<CodeCountVO> codeList = productSrv.GetProductType();
            ComboBoxUtil.SetComboBoxByList<CodeCountVO>(cboType, productSrv.GetProductType(), "Category", "Code");
            ResMessage<List<ClientVO>> resResult = srvHelper.GetAsyncT<ResMessage<List<ClientVO>>>("ClientByType/ven");
            List<ClientVO> list = resResult.Data;
            ComboBoxUtil.SetComboBoxByList(cboClient, list, "Client_Name", "Client_Code");

            cboType.SelectedValue = prd.Product_Type;
            ////cboClient.SelectedValue = prd.Client_Code;
        }

        private void PopProductsModify_Load(object sender, EventArgs e)
        {

        }
    }
}
