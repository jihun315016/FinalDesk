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

        public frmProducts()
        {
            InitializeComponent();
        }

        private void frmProducts_Load(object sender, EventArgs e)
        {
            productSrv = new ProductService();
            InitControls();
        }

        void InitControls()
        {
            label1.Text = "품목 관리";

            List<CodeCountVO> list = productSrv.GetProductType();
            list.Insert(0, new CodeCountVO()
            {
                Code = string.Empty,
                Category = "유형 선택"
            });
            comboBox1.ValueMember = "Code";
            comboBox1.DisplayMember = "Category";
            comboBox1.DataSource = list;

            //품번, 품명, 유형, 가격, 단위, 생성 시간, 생성 사용자
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
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string type = comboBox1.SelectedValue == string.Empty ? string.Empty : comboBox1.SelectedValue.ToString().Split('_')[1];
            dgvList.DataSource = productSrv.GetProductList(type);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PopProductsRegister pop = new PopProductsRegister();
            pop.ShowDialog();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            PopProductsModify pop = new PopProductsModify();
            pop.ShowDialog();
        }
    }
}
