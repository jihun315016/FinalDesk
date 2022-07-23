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
    public partial class PopBOMRegister : Form
    {
        ProductService productSrv;
        List<ProductVO> prdList;

        public PopBOMRegister()
        {
            InitializeComponent();
        }

        private void PopBOMRegister_Load(object sender, EventArgs e)
        {
            productSrv = new ProductService();
            InitControl();
        }

        void InitControl()
        {
            List<CodeCountVO> typeList = productSrv.GetProductType();
            typeList.Insert(0, new CodeCountVO()
            {
                Category = "제품 유형",
                Code = String.Empty
            });

            ComboBoxUtil.SetComboBoxByList<CodeCountVO>(cboType, typeList.GetRange(0, typeList.Count - 1), "Category", "Code");
            cboType.SelectedIndex = 0;

            prdList = productSrv.GetProductList();

            cboIsCopy.Items.AddRange(new string[] { "예", "아니오" });
            cboIsCopy.SelectedIndex = 1;

            cboCopyName.Visible = false;
        }

        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboType.SelectedIndex == 0)
            {
                cboName.DataSource = null;
            }
            else
            {
                List<ProductVO> list = prdList.Where(p => p.Product_Type == cboType.SelectedValue.ToString().Split('_')[1]).ToList();
                ComboBoxUtil.SetComboBoxByList<ProductVO>(cboName, list, "Product_Name", "Product_Code");

                // TODO : 해당 유형에 해당하는 품목 리스트 조회
            }
        }

        private void cboIsCopy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboIsCopy.SelectedIndex == 0)
            {
                cboCopyName.Visible = true;
            }
        }
    }
}
