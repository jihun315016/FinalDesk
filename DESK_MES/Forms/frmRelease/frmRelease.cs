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
    public partial class frmRelease : FormStyle_1
    {
        public frmRelease()
        {
            InitializeComponent();
            label1.Text = "출고 관리";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PopReleaseProduct pop = new PopReleaseProduct();
            pop.ShowDialog();
        }
    }
}
