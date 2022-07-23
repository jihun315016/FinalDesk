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
        List<ProductVO> selectedList;

        public PopBOMRegister()
        {
            InitializeComponent();
        }

        private void PopBOMRegister_Load(object sender, EventArgs e)
        {
            productSrv = new ProductService();
            selectedList = new List<ProductVO>();
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

            // TODO : BOM 등록되지 않은 제품 필터링 필요
            prdList = productSrv.GetProductList();

            cboIsCopy.Items.AddRange(new string[] { "예", "아니오" });
            cboIsCopy.SelectedIndex = 1;

            cboCopyName.Visible = false;

            DataGridUtil.SetInitGridView(dgvLowList);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvLowList, "품번", "Product_Code", colWidth: 150);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvLowList, "품명", "Product_Name", colWidth: 300);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvLowList, "유형", "Product_Type");            
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
                List<string> typeList = new List<string>() { "ROH" };
                if (cboType.SelectedIndex == 1)
                    typeList.Add("HALB");

                dgvLowList.DataSource = prdList.Where(p => typeList.Contains(p.Product_Type)).ToList();
            }
        }

        private void cboIsCopy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboIsCopy.SelectedIndex == 0)
            {
                cboCopyName.Visible = true;
            }
        }

        private void dgvLowList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
