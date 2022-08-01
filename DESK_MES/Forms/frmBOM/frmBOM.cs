using DESK_DTO;
using DESK_MES.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DESK_MES
{
    public partial class frmBOM : FormStyle_2
    {
        UserVO user;
        ProductService productSrv;
        List<ProductVO> isBomProductList;

        public frmBOM()
        {
            InitializeComponent();
            label1.Text = "BOM 관리";
        }

        private void frmBOM_Load(object sender, EventArgs e)
        {
            this.user = ((frmMain)(this.MdiParent)).userInfo;
            productSrv = new ProductService();
            isBomProductList = new List<ProductVO>();

            dtpCreateTime.Format = DateTimePickerFormat.Custom;
            dtpCreateTime.CustomFormat = " ";
            dtpUpdateTime.Format = DateTimePickerFormat.Custom;
            dtpUpdateTime.CustomFormat = " ";

            initControl();
        }

        void initControl()
        {
            label1.Text = "BOM";

            comboBox1.Items.AddRange(new string[] { "검색 조건", "품번", "품명" });
            comboBox1.SelectedIndex = 0;

            List<CodeCountVO> list = productSrv.GetProductType();
            list.Insert(0, new CodeCountVO()
            {
                Code = string.Empty,
                Category = "유형 선택"
            });

            ComboBoxUtil.SetComboBoxByList<CodeCountVO>(cboTypeDetailSearch, list, "Category", "Code");

            dtpCreateTime.Format = DateTimePickerFormat.Custom;
            dtpCreateTime.CustomFormat = " ";
            dtpUpdateTime.Format = DateTimePickerFormat.Custom;
            dtpUpdateTime.CustomFormat = " ";

            isBomProductList = productSrv.GetBomList();

            DataGridUtil.SetInitGridView(dgvProductList);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvProductList, "품번", "Product_Code", colWidth: 130);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvProductList, "품명", "Product_Name", colWidth: 270);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvProductList, "유형", "Product_Type");
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvProductList, "가격", "Price");
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvProductList, "단위", "Unit");
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvProductList, "등록 시간", "Create_Time", colWidth: 270);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvProductList, "등록 사용자", "Create_User_Name", colWidth: 130);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvProductList, "수정 시간", "Update_Time", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvProductList, "수정 사용자", "Update_User_Name", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvProductList, "등록 사용자 번호", "Create_User_No", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvProductList, "수정 사용자 번호", "Update_User_No", isVisible: false);
            dgvProductList.Columns["Price"].DefaultCellStyle.Format = "###,##0";

            DataGridUtil.SetInitGridView(dgvChild);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvChild, "품번", "Product_Code");
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvChild, "품명", "Product_Name", colWidth: 230);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvChild, "유형", "Product_Type");
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvChild, "수량", "Qty", colWidth: 80);

            DataGridUtil.SetInitGridView(dgvParent);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvParent, "품번", "Product_Code");
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvParent, "품명", "Product_Name", colWidth: 230);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvParent, "유형", "Product_Type");
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvParent, "수량", "Qty", colWidth: 80);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<ProductVO> list = isBomProductList.Where(p => 1 == 1).ToList();
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
            }

            //dgvList.DataSource = list;

            dgvProductList.DataSource = list;
        }

        private void dgvProductList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            dtpCreateTime.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";
            dtpUpdateTime.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";

            List<ProductVO> temp = productSrv.GetProductList(dgvProductList["Product_Code", e.RowIndex].Value.ToString());
            ProductVO prd = temp.FirstOrDefault();
            txtCodeDetail.Text = prd.Product_Code;
            txtNameDetail.Text = prd.Product_Name;
            txtTypeDetail.Text = prd.Product_Type;
            dtpCreateTime.Value = prd.Create_Time;
            txtCreateUserDetail.Text = prd.Create_User_Name;

            if (prd.Update_Time.ToString() == "0001-01-01 오전 12:00:00")
            {
                dtpUpdateTime.Format = DateTimePickerFormat.Custom;
                dtpUpdateTime.CustomFormat = " ";
                txtUpdateUserDetail.Text = string.Empty;
            }
            else
            {
                dtpUpdateTime.Format = dtpCreateTime.Format;
                dtpUpdateTime.Value = prd.Update_Time;
            }

            List<ProductVO> bomList = productSrv.GetChildParentProductList(dgvProductList["Product_Code", e.RowIndex].Value.ToString());
            dgvChild.DataSource = bomList.Where(b => b.Bom_Type == "자품목").ToList();
            dgvParent.DataSource = bomList.Where(b => b.Bom_Type == "모품목").ToList();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            isBomProductList = productSrv.GetBomList();
            comboBox1.Enabled = textBox1.Enabled = true;
            panel5.Visible = false;
            dgvProductList.DataSource = dgvChild.DataSource = dgvParent.DataSource = null;
        }

        private void btnOpenDetail_Click(object sender, EventArgs e)
        {
            if (panel5.Visible)
                comboBox1.Enabled = textBox1.Enabled = false;
            else
                comboBox1.Enabled = textBox1.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtTypeDetail.Text == "ROH")
            {
                MessageBox.Show("부품은 삭제할 수 없습니다.");
                return;
            }

            List<ProductVO> list = dgvChild.DataSource as List<ProductVO>;
            if (list.Count == 0)
            {
                MessageBox.Show("자품목이 존재하지 않습니다.");
                return;
            }

            if (MessageBox.Show("삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                bool result = productSrv.DeleteBom(txtCodeDetail.Text);
                if (result)
                {
                    MessageBox.Show("삭제되었습니다.");
                }
                else
                {
                    MessageBox.Show("삭제에 실패했습니다.");
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PopBOMRegister pop = new PopBOMRegister(user);
            if (pop.ShowDialog() == DialogResult.OK)
            {
                btnReset_Click(this, null);
                btnSearch_Click(this, null);
            }
        }
    }
}
