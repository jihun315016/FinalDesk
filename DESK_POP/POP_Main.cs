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
    public partial class POP_Main : Form
    {
        public UserVO userInfo { get; set; }
        public POP_Main()
        {
            InitializeComponent();
        }

        private void POP_Main_Load(object sender, EventArgs e)
        {
            this.Hide();
            POP_Login pop = new POP_Login();
            if (pop.ShowDialog() != DialogResult.OK)
            {
                this.Close();
                return;
            }
            else
            {
                this.Show();
                this.userInfo = pop.userVO;
            }
        }
    }
}
