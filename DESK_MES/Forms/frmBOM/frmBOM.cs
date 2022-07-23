using DESK_DTO;
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

        public frmBOM()
        {
            InitializeComponent();
            label1.Text = "BOM 관리";
        }

        private void frmBOM_Load(object sender, EventArgs e)
        {
            this.user = ((frmMain)(this.MdiParent)).userInfo;

            dtpCreateTime.Format = DateTimePickerFormat.Custom;
            dtpCreateTime.CustomFormat = " ";
            dtpUpdateTime.Format = DateTimePickerFormat.Custom;
            dtpUpdateTime.CustomFormat = " ";

            // 그리드뷰 선택하면 이거 작성
            //dtpCreateTime.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";
            //dtpUpdateTime.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PopBOMRegister pop = new PopBOMRegister(user);
            pop.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PopBOMDelete pop = new PopBOMDelete();
            pop.ShowDialog();
        }
    }
}
