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
        public PopVO userInfo { get; set; } //접속자 정보[ User_No, User_Name, u.User_Group_No, User_Group_Name, User_Group_Type, Auth_Name ]
        public POP_Main()
        {
            InitializeComponent();
        }

        private void POP_Main_Load(object sender, EventArgs e)
        {
            #region*로그인
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
            #endregion
            int littt = 3; //db에서 가져온 리스트의 갯수/<나중에 삭제>
            lblCount.Text = littt.ToString();

            for (int i = 0; i < 3; i++)
            {
                ucWorkGroup wg = new ucWorkGroup(PopVO pop);
                
                wg.Name = $"ucWorkGroup{1+i}";
                wg.Location = new Point(3+(i*342), 5);
                wg.Size = new Size(342, 338);
                splitContainer1.Panel2.Controls.Add(wg);
            }
        }
    }
}
