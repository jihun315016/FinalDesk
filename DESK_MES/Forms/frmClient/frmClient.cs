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

        public frmClient()
        {
            InitializeComponent();
            label1.Text = "거래처 관리";

        }
        private void frmClient_Load(object sender, EventArgs e)
        {
            service = new ServiceHelper("api/Client");
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
            pop.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PopClientModify pop = new PopClientModify();
            pop.Show();
        }


    }
}
