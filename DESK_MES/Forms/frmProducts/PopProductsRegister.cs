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
    public partial class PopProductsRegister : Form
    {
        ProductService productSrv;

        public PopProductsRegister()
        {
            InitializeComponent();
            productSrv = new ProductService();
        }

        private void PopProductsRegister_Load(object sender, EventArgs e)
        {
            ComboBoxUtil.SetComboBoxByList<CodeCountVO>(cboType, productSrv.GetProductType(), "Category", "Code");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ccTextBox[] txts = new ccTextBox[] { txtName };

            string isRequiredMsg = TextBoxUtil.IsRequiredCheck(txts);
            if (isRequiredMsg.Length > 0)
            {
                MessageBox.Show(isRequiredMsg);
                return;
            }

            ProductVO prd = new ProductVO()
            {
                Product_Name = txtName.Text,
                Product_Type = cboType.SelectedValue.ToString().Split('_')[1], // FERT ...
                Price = txtPrice.Text == string.Empty ? -1 : Convert.ToInt32(txtPrice.Text),
                Unit = txtUnit.Text == string.Empty ? -1 : Convert.ToInt32(txtUnit.Text),
                Is_Delete = "N"
            };

            if (ptbProduct.Image == null)
            {
                prd.Is_Image = 0;                
            }
            else
            {                
                ptbProduct.Image.Dispose();
                ptbProduct.Image = null;                
                prd.Is_Image = 1;
            }

            StringBuilder resultMessage = new StringBuilder();
            string SaveProductCode = productSrv.SaveProduct(cboType.SelectedValue.ToString(), 10001, prd);            

            // 제품 등록 성공
            if (!string.IsNullOrWhiteSpace(SaveProductCode))
            {
                // 제품 등록 성공 후 이미지 존재
                if (prd.Is_Image == 1)
                {
                    bool isSaveImg = productSrv.SaveProductImage(SaveProductCode, ptbProduct.Tag.ToString());
                    if (!isSaveImg)
                    {
                        resultMessage.Append("이미지 저장에 실패했습니다.");
                    }
                }
                resultMessage.Append("저장이 완료되었습니다.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                resultMessage.Append("저장에 실패했습니다.");
            }

            MessageBox.Show(resultMessage.ToString());
        }

        private void btnImgUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image File|*.jpg;*.jpeg;*.png;*.bmp";

            DialogResult result = dlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                ptbProduct.Image = Image.FromFile(dlg.FileName);
                ptbProduct.Tag = dlg.FileName;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
