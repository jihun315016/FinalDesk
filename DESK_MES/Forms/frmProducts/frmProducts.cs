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
        UserVO user;
        ProductService productSrv;
        List<ProductVO> prdList;

        public frmProducts()
        {
            InitializeComponent();
        }

        private void frmProducts_Load(object sender, EventArgs e)
        {

            this.user = ((frmMain)(this.MdiParent)).userInfo;
            productSrv = new ProductService();
            prdList = new List<ProductVO>();
            
            InitControls();
        }

        void InitControls()
        {
            dtpCreateTime.Format = DateTimePickerFormat.Custom;
            dtpCreateTime.CustomFormat = " ";
            dtpUpdateTime.Format = DateTimePickerFormat.Custom;
            dtpUpdateTime.CustomFormat = " ";

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
            }

            dgvList.DataSource = list;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PopProductsRegister pop = new PopProductsRegister(user);
            if (pop.ShowDialog() == DialogResult.OK)
            {
                prdList = productSrv.GetProductList();
                dgvList.DataSource = null;
            }
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dtpCreateTime.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";
            dtpUpdateTime.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";

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
                txtUpdateUserDetail.Text = prd.Update_User_Name;
            }
            

            if (prd.Is_Image == 1)
            {
                try
                {
                    ptbProductImage.ImageLocation = productSrv.GetImageUrl(prd.Product_Code);
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
            if (string.IsNullOrWhiteSpace(txtCodeDetail.Text))
            {
                MessageBox.Show("품번을 선택하세요.");
                return;
            }

            List<ProductVO> temp = productSrv.GetProductList(txtCodeDetail.Text);
            ProductVO prd = temp.FirstOrDefault();

            PopProductsModify pop = new PopProductsModify(prd, user);
            pop.ShowDialog();
        }

        private void btnOpenDetail_Click(object sender, EventArgs e)
        {
            if (panel5.Visible)
                comboBox1.Enabled = textBox1.Enabled = false;
            else
                comboBox1.Enabled = textBox1.Enabled = true;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            prdList = productSrv.GetProductList();
            comboBox1.Enabled = textBox1.Enabled = true;
            panel5.Visible = false;
            dgvList.DataSource = null;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Execl Files(*.xls)|*.xls";
            dlg.Title = "엑셀파일로 내보내기";

            List<ProductVO> list = dgvList.DataSource as List<ProductVO>;
            if (list == null)
            {
                MessageBox.Show("조회 항목이 없습니다.");
                return;
            }

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ExcelUtil excel = new ExcelUtil();
                List<ProductVO> output = list;

                string[] columnImport = { "Product_Code", "Product_Name", "Product_Type", "Price", "Unit", "Create_Time", "Create_User_Name" };
                string[] columnName = { "품번", "품명", "유형", "가격", "단위", "등록 시간", "등록 사용자" };

                if (excel.ExportList(output, dlg.FileName, columnImport, columnName))
                {
                    MessageBox.Show("엑셀 다운로드 완료");
                }
                else
                {
                    MessageBox.Show("엑셀 다운 실패");
                }
            }
        }
    }
}
