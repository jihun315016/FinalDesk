using DESK_DTO;
using DESK_MES.Service;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DESK_MES
{
    public partial class PopProductsModify : Form
    {
        UserVO user;
        ProductService productSrv;
        ServiceHelper srvHelper;

        public PopProductsModify(ProductVO prd, UserVO user)
        {
            InitializeComponent();
            this.user = user;
            InitControl(prd);
        }

        void InitControl(ProductVO prd)
        {
            productSrv = new ProductService();
            srvHelper = new ServiceHelper("api/Client");

            txtCode.Text = prd.Product_Code;
            txtName.Text = prd.Product_Name;            

            List<CodeCountVO> codeCountList = productSrv.GetProductType();
            ComboBoxUtil.SetComboBoxByList<CodeCountVO>(cboType, productSrv.GetProductType(), "Category", "Code");
            List<string> codeList = new List<string>();
            codeCountList.ForEach(c => codeList.Add(c.Code));
            int typeIndex = codeList.IndexOf(prd.Product_Code.Substring(0, prd.Product_Code.LastIndexOf('_')));
            cboType.SelectedIndex = typeIndex;

            // 원자재가 아니라면
            if (typeIndex < 2)
            {
                cboClient.Enabled = false;
            }
            else
            {
                ResMessage<List<ClientVO>> resResult = srvHelper.GetAsyncT<ResMessage<List<ClientVO>>>("ClientByType/ven");
                List<ClientVO> clientList = resResult.Data;
                ComboBoxUtil.SetComboBoxByList(cboClient, clientList, "Client_Name", "Client_Code");
                List<string> clientCodeList = new List<string>();
                clientList.ForEach(c => clientCodeList.Add(c.Client_Code));
                int clientIndex = clientCodeList.IndexOf(prd.Client_Code);
                cboClient.SelectedIndex = clientIndex;
            }

            // 재공품이라면 가격, 단위가 없음
            if (typeIndex == 1)
            {
                txtPrice.Text = String.Empty;
                txtPrice.ReadOnly = true;
                txtUnit.Text = String.Empty;
                txtUnit.ReadOnly = true;
            }
            else
            {
                txtPrice.Text = prd.Price.ToString();
                txtUnit.Text = prd.Unit.ToString();
            }

            if(prd.Is_Image == 1)
            {
                try
                {
                    ptbProduct.ImageLocation = productSrv.GetImageUrl(prd.Product_Code);
                }
                catch { }
            }
            else
            {
                ptbProduct.Image = null;
            }
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

        private void btnImgDelete_Click(object sender, EventArgs e)
        {
            ptbProduct.Image = null;            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ProductVO prd = new ProductVO()
            {
                Product_Code = txtCode.Text,
                Product_Name = txtName.Text,
                Product_Type = cboType.SelectedValue.ToString().Split('_')[1],
                Price = Convert.ToInt32(txtPrice.Text),
                Unit = Convert.ToInt32(txtUnit.Text),
                Client_Code = string.IsNullOrWhiteSpace(cboClient.Text) ? null : cboClient.SelectedValue.ToString(),
                Update_User_No = user.User_No
            };

            if (ptbProduct.Image == null)
            {
                prd.Is_Image = 0;
            }
            else
            {
                ptbProduct.Image = null;
                prd.Is_Image = 1;
            }

            StringBuilder resultMessage = new StringBuilder();
            bool result = productSrv.UpdateProduct(prd);
            if (result)
            {
                if (prd.Is_Image == 1 && ptbProduct.Tag != null)
                {
                    int lastIndex = ptbProduct.Tag.ToString().LastIndexOf('\\');
                    string path = ptbProduct.Tag.ToString().Substring(0, lastIndex);
                    bool isSaveImg = productSrv.SaveProductImage(txtCode.Text, ptbProduct.Tag.ToString());
                    if (!isSaveImg)
                    {
                        resultMessage.Append("이미지 저장에 실패했습니다.");
                    }
                }
                resultMessage.Append("수정이 완료되었습니다.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                resultMessage.Append("수정에 실패했습니다.");
            }

            MessageBox.Show(resultMessage.ToString());
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                bool result = productSrv.DeleteProduct(txtCode.Text);
                if (result)
                {
                    MessageBox.Show("삭제되었습니다.");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("삭제에 실패했습니다.");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
