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
        List<ProductVO> prdNoneBomList;
        List<ProductVO> prdAllList;
        List<BomVO> selectedList;
        UserVO user;

        public PopBOMRegister(UserVO user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void PopBOMRegister_Load(object sender, EventArgs e)
        {
            productSrv = new ProductService();
            selectedList = new List<BomVO>();
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
            
            prdNoneBomList = productSrv.GetProductList(isBom: true);
            prdAllList = productSrv.GetProductList();

            cboIsCopy.Items.AddRange(new string[] { "예", "아니오" });
            cboIsCopy.SelectedIndex = 1;

            cboCopyName.Visible = false;

            DataGridUtil.SetInitGridView(dgvLowList);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvLowList, "품번", "Product_Code", colWidth: 220);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvLowList, "품명", "Product_Name", colWidth: 320);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvLowList, "유형", "Product_Type");
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvLowList, "가격", "Price");

            DataGridUtil.SetInitGridView(dgvChildList);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvChildList, "품번", "Child_Product_Code", colWidth: 220);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvChildList, "품명", "Child_Name", colWidth: 320);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvChildList, "유형", "Child_Type");
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvChildList, "수량", "Qty");
        }

        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboType.SelectedIndex == 0)
            {
                cboName.DataSource = null;
            }
            else
            {
                List<ProductVO> list = prdNoneBomList.Where(p => p.Product_Type == cboType.SelectedValue.ToString().Split('_')[1]).ToList();
                ComboBoxUtil.SetComboBoxByList<ProductVO>(cboName, list, "Product_Name", "Product_Code");

                List<string> typeList = new List<string>() { "ROH" };
                if (cboType.SelectedIndex == 1)
                    typeList.Add("HALB");

                dgvLowList.DataSource = null;
                dgvLowList.DataSource = prdAllList.Where(p => typeList.Contains(p.Product_Type)).ToList();
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
            PopSelectBomQty pop = new PopSelectBomQty();
            if (pop.ShowDialog() == DialogResult.OK)
            {
                BomVO newPrd = new BomVO()
                {
                    Parent_Product_Code = cboName.SelectedValue.ToString(),
                    Child_Product_Code = dgvLowList["Product_Code", e.RowIndex].Value.ToString(),
                    Child_Name = dgvLowList["Product_Name", e.RowIndex].Value.ToString(),
                    Child_Type = dgvLowList["Product_Type", e.RowIndex].Value.ToString(),
                    Qty = pop.Qty
                };

                BomVO item = selectedList.FirstOrDefault(s => s.Child_Product_Code == newPrd.Child_Product_Code);

                // 하위 항목 새로 등록
                if (item == null)
                {
                    selectedList.Add(newPrd);
                }
                // 이미 등록된 항목인 경우 (수량 추가)
                else
                {
                    item.Qty = item.Qty + pop.Qty;
                }
                dgvChildList.DataSource = null;
                dgvChildList.DataSource = selectedList;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgvLowList.DataSource = null;
            dgvLowList.DataSource = prdAllList.Where(p => p.Product_Name.Contains(txtName.Text.Trim())).ToList();
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSearch_Click(this, null);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool result = productSrv.SaveBom(selectedList, user.User_No);
            if (result)
            {
                MessageBox.Show("BOM 등록에 성공하셨습니다.");
                this.Close();
            }
            else
            {
                MessageBox.Show("BOM 등록에 실패하셨습니다.");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
