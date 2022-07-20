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
            cboType.ValueMember = "Code";
            cboType.DisplayMember = "Category";
            cboType.DataSource = productSrv.GetProductType();            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ccTextBox[] txts = new ccTextBox[]
            {
                txtName, txtPrice, txtUnit
            };

            string isRequiredMsg = TextBoxUtil.IsRequiredCheck(txts);
            if (isRequiredMsg.Length > 0)
            {
                MessageBox.Show(isRequiredMsg);
                return;
            }

            if (ptbProduct.Image == null)
            {
                MessageBox.Show("이미지 없음");
            }
            else
            {
                MessageBox.Show("이미지 있음");
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
            }
        }
    }
}
