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
    public partial class frmManufacture : FormStyle_2
    {
        ManufactureService srv;
        int purchaseNo = 0;
        UserVO user;

        public frmManufacture()
        {
            InitializeComponent();
            label1.Text = "생산계획 관리";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PopManufactureRegister pop = new PopManufactureRegister(user);
            pop.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PopManufactureModify pop = new PopManufactureModify();
            pop.ShowDialog();
        }

        private void frmManufacture_Load(object sender, EventArgs e)
        {
            this.user = ((frmMain)(this.MdiParent)).userInfo;
            srv = new ManufactureService();
        }
    }
}
