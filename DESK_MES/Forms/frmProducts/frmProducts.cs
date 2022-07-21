using DESK_DTO;
using DESK_MES.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DESK_MES
{
    public partial class frmProducts : FormStyle_2
    {
        ProductService productSrv;
        List<ProductVO> prdList;

        public frmProducts()
        {
            InitializeComponent();
        }

        private void frmProducts_Load(object sender, EventArgs e)
        {
            productSrv = new ProductService();
            prdList = new List<ProductVO>();
            InitControls();
        }

        void InitControls()
        {
            label1.Text = "품목 관리";

            comboBox1.Items.AddRange(new string[] { "검색 조건", "품번", "품명" });
            comboBox1.SelectedIndex = 0;

            List<CodeCountVO> list = productSrv.GetProductType();
            list.Insert(0, new CodeCountVO()
            {
                Code = string.Empty,
                Category = "유형 선택"
            });

            ComboBoxUtil.SetComboBoxByList<CodeCountVO>(cboTypeDetailSearch, list, "Category", "Code");
            DataGridUtil.SetInitGridView(dgvList);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "품번", "Product_Code", colWidth: 150);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "품명", "Product_Name", colWidth: 300);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "유형", "Product_Type");
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "가격", "Price");
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "단위", "Unit");
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "등록 시간", "Create_Time", colWidth: 200);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "등록 사용자", "Create_User_Name", colWidth: 150);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "수정 시간", "Update_Time", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "수정 사용자", "Update_User_Name", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "등록 사용자 번호", "Create_User_No", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "수정 사용자 번호", "Update_User_No", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "이미지 여부", "Is_Image", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvList, "삭제 여부", "Is_Delete", isVisible: false);
            prdList = productSrv.GetProductList();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {            
            List<ProductVO> list = prdList.Where(p => 1 == 1).ToList();
            // 상세 검색으로 필터링
            if (panel5.Visible)
            {
                if (!string.IsNullOrWhiteSpace(txtPrdCodeDetailSearch.Text.Trim()))                
                    list = list.Where(p => p.Product_Code.ToLower().Contains(txtPrdCodeDetailSearch.Text.ToLower())).ToList();

                if (!string.IsNullOrWhiteSpace(txtPrdNameDetailSearch.Text.Trim()))
                    list = list.Where(p => p.Product_Name.ToLower().Contains(txtPrdNameDetailSearch.Text.ToLower())).ToList();

                if (cboTypeDetailSearch.SelectedIndex > 0)
                    list = list.Where(p => p.Product_Type == cboTypeDetailSearch.SelectedValue.ToString().Split('_')[1]).ToList();                
            }
            // 일반 검색으로 필터링
            else
            {
                // 품번 검색
                if (comboBox1.SelectedIndex == 1)                
                    list = list.Where(p => p.Product_Code.ToLower().Contains(textBox1.Text.ToLower())).ToList();
                
                // 품명 검색
                else if (comboBox1.SelectedIndex == 2)
                    list = list.Where(p => p.Product_Name.ToLower().Contains(textBox1.Text.ToLower())).ToList();

                // 삭제 여부 검색
                else if (comboBox1.SelectedIndex == 3)
                    list = list.Where(p => p.Is_Delete == "Y").ToList();

                else if (comboBox1.SelectedIndex == 3)
                    list = list.Where(p => p.Is_Delete == "N").ToList();
            }

            dgvList.DataSource = list;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PopProductsRegister pop = new PopProductsRegister();
            if (pop.ShowDialog() == DialogResult.OK)
            {
                prdList = productSrv.GetProductList();
                dgvList.DataSource = null;
            }
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            List<ProductVO> temp = productSrv.GetProductList(dgvList["Product_Code", e.RowIndex].Value.ToString());
            ProductVO prd = temp.FirstOrDefault();
            txtCodeDetail.Text = prd.Product_Code;
            txtNameDetail.Text = prd.Product_Name;
            txtTypeDetail.Text = prd.Product_Type;
            txtClientDetail.Text = prd.Client_Name == null ? string.Empty : prd.Client_Name;
            dtpCreateTime.Value = prd.Create_Time;
            txtCreateUserDetail.Text = prd.Create_User_Name;
            Debug.WriteLine(prd.Update_Time.ToString());
            if (prd.Update_Time.ToString() == "0001-01-01 오전 12:00:00")
            {
                dtpUpdateTime.Format = DateTimePickerFormat.Custom;
                dtpUpdateTime.CustomFormat = " ";
            }
            else
            {
                dtpUpdateTime.Format = dtpCreateTime.Format;
                dtpUpdateTime.Value = prd.Update_Time;
            }
            
            txtUpdateUserDetail.Text = prd.Update_User_Name;

            if (prd.Is_Image == 1)
            {
                try
                {
                    ptbProductImage.ImageLocation = $"https://localhost:44393/files/{prd.Product_Code}.png";
                }
                catch { }
            }
            else
            {
                ptbProductImage.Image = null;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            PopProductsModify pop = new PopProductsModify();
            pop.ShowDialog();
        }

        private void btnOpenDetail_Click(object sender, EventArgs e)
        {
            if (panel5.Visible)
                comboBox1.Enabled = textBox1.Enabled = false;
            else
                comboBox1.Enabled = textBox1.Enabled = true;
        }
    }
}
