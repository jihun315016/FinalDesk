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
    public partial class frmBOM : FormStyle_2
    {
        UserVO user;
        ProductService productSrv;
        List<ProductVO> bomList;

        public frmBOM()
        {
            InitializeComponent();
            label1.Text = "BOM 관리";
        }

        private void frmBOM_Load(object sender, EventArgs e)
        {
            this.user = ((frmMain)(this.MdiParent)).userInfo;
            productSrv = new ProductService();
            bomList = new List<ProductVO>();

            dtpCreateTime.Format = DateTimePickerFormat.Custom;
            dtpCreateTime.CustomFormat = " ";
            dtpUpdateTime.Format = DateTimePickerFormat.Custom;
            dtpUpdateTime.CustomFormat = " ";

            initControl();
        }

        void initControl()
        {
            dtpCreateTime.Format = DateTimePickerFormat.Custom;
            dtpCreateTime.CustomFormat = " ";
            dtpUpdateTime.Format = DateTimePickerFormat.Custom;
            dtpUpdateTime.CustomFormat = " ";

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
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            
            bomList = productSrv.GetBomList();
            dgvProductList.DataSource = bomList;
        }

        private void dgvProductList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
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
            }
            else
            {
                dtpUpdateTime.Format = dtpCreateTime.Format;
                dtpUpdateTime.Value = prd.Update_Time;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PopBOMRegister pop = new PopBOMRegister(user);
            pop.ShowDialog();
        }
    }
}
