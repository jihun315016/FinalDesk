using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DESK_DTO;

namespace DESK_MES
{
    public partial class frmClient : FormStyle_2
    {
        ServiceHelper service;
        string clientCode = null;

        public frmClient()
        {
            InitializeComponent();
            label1.Text = "거래처 관리";

        }
        private void frmClient_Load(object sender, EventArgs e)
        {

            service = new ServiceHelper("api/Client");

            LoadData();
            dtpCreateTime.Format = DateTimePickerFormat.Custom;
            dtpCreateTime.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";

            dtpModifyTime.Format = DateTimePickerFormat.Custom;
            dtpModifyTime.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";
        }

        private void LoadData()
        {
            ResMessage<List<ClientVO>> result = service.GetAsync<List<ClientVO>>("Client");
            if (result != null)
            {
                dataGridView1.DataSource = result.Data;
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            PopClientRegister pop = new PopClientRegister();
            if (pop.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (clientCode != null)
            {
                PopClientModify pop = new PopClientModify(clientCode);
                if (pop.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
            else
            {
                return;
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            clientCode = dataGridView1[0, e.RowIndex].Value.ToString();

            ResMessage<ClientVO> resResult = service.GetAsyncT<ResMessage<ClientVO>>(clientCode);

            if (resResult.ErrCode == 0)
            {
                txtCode.Text = resResult.Data.Client_Code.ToString();
                txtName.Text = resResult.Data.Client_Name.ToString();
                txtType.Text = resResult.Data.Client_Type.ToString();
                txtNumber.Text = resResult.Data.Client_Number.ToString();
                txtPhone.Text = resResult.Data.Client_Phone.ToString();                
            }
        }
    }
}
