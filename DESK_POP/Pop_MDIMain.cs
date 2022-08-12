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

namespace DESK_POP
{
    public partial class Pop_MDIMain : Form
    {
        public PopVO userInfo { get; set; }
        public Pop_MDIMain()
        {
            InitializeComponent();
        }

        private void Pop_MDIMain_Load(object sender, EventArgs e)
        {
            #region*로그인
            this.Hide();
            POP_Login pop = new POP_Login();
            if (pop.ShowDialog(this) != DialogResult.OK)
            {
                this.Close();
                //return;
            }
            else
            {
                this.Show();
                this.userInfo = pop.userVO;

                POP_Main frm = new POP_Main();
                frm.MdiParent = this;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
            }
            #endregion
        }
    }
}
