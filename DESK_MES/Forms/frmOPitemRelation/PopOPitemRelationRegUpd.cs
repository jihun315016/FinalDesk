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
    public partial class PopOPitemRelationRegUpd : Form
    {
        ProductService productSrv;
        OperationService operationSrv;
        List<ProductVO> productList;
        List<ProductVO> selectedInspect;

        public PopOPitemRelationRegUpd(OperationVO oper, bool isReg)
        {
            InitializeComponent();

            lblOperationName.Text = oper.Operation_Name;
            lblOperationName.Tag = oper.Operation_No;

            lblTitle.Tag = isReg; // 등록 or 수정 유무
            if (isReg)
            {
                lblTitle.Text = "공정-검사 데이터 등록";
                btnSave.Text = "등록";
            }
            else
            {
                lblTitle.Text = "공정-검사 데이터 수정";
                btnSave.Text = "수정";
            }
        }

        private void PopOPitemRelationRegUpd_Load(object sender, EventArgs e)
        {
            productSrv = new ProductService();
            operationSrv = new OperationService();
            selectedInspect = new List<ProductVO>();
            InitControl();
        }

        private void InitControl()
        {
            productList = productSrv.GetProductList().Where(p => Array.IndexOf(new string[] { "FERT", "HALB" }, p.Product_Type) > -1).ToList();

            foreach (DataGridView dgv in new DataGridView[] { dgvProduct, dgvRegistered })
            {
                DataGridUtil.SetInitGridView(dgv);
                DataGridUtil.SetDataGridViewColumn_TextBox(dgv, "품번", "Product_Code", colWidth: 120);
                DataGridUtil.SetDataGridViewColumn_TextBox(dgv, "품명", "Product_Name", colWidth: 210);
            }

            // 품목 관계 수정인 경우
            //if (!(bool)lblTitle.Tag)
            //{
            //    List<int> inspectItems = operationSrv.GetInspectListByOperation(Convert.ToInt32(lblOperationName.Tag));
            //    foreach (int item in inspectItems)
            //    {

            //        foreach (ProductVO product in productList)
            //        {
            //            if (item == product.Product_Code)
            //            {
            //                selectedInspect.Add(product);
            //            }
            //        }
            //    }
            //}

            selectedInspect.ForEach(s => productList.Remove(s));
            dgvRegistered.DataSource = selectedInspect;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgvProduct.DataSource = null;
            dgvProduct.DataSource = productList.Where(p => p.Product_Name.Contains(txtName.Text.Trim())).ToList();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            productList = productSrv.GetProductList().Where(p => Array.IndexOf(new string[] { "FERT", "HALB" }, p.Product_Type) > -1).ToList();
            dgvProduct.DataSource = null;
            dgvProduct.DataSource = productList;
            txtName.Text = string.Empty;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dgvProduct.CurrentCell != null)
            {
                ProductVO item = productList.Where(p => p.Product_Code == dgvProduct["Product_Code", dgvProduct.CurrentCell.RowIndex].Value).FirstOrDefault();
                selectedInspect.Add(item);
                productList.Remove(item);

                dgvProduct.DataSource = null;
                dgvRegistered.DataSource = null;
                dgvProduct.DataSource = productList;
                dgvRegistered.DataSource = selectedInspect;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvRegistered.CurrentCell == null) return;

            ProductVO item = selectedInspect.Where(s => s.Product_Code == dgvRegistered["Product_Code", dgvRegistered.CurrentCell.RowIndex].Value).FirstOrDefault();
            if (item != null)
            {
                productList.Add(item);
                selectedInspect.Remove(item);

                dgvProduct.DataSource = null;
                dgvRegistered.DataSource = null;
                dgvProduct.DataSource = productList;
                dgvRegistered.DataSource = selectedInspect;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //bool result = (operationSrv.SaveOIRelation(Convert.ToInt32(lblOperationName.Tag), selectedInspect));
            //string msg = (bool)lblTitle.Tag ? "등록" : "수정";
            //if (result)
            //{
            //    MessageBox.Show($"공정 - 품목 관계가 {msg}되었습니다.");
            //    this.Close();
            //}
            //else
            //{
            //    MessageBox.Show($"{msg}에 실패했습니다.");
            //}
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
