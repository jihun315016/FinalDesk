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
        ServiceHelper serv;
        List<PopVO> allList;        
        public PopVO userInfo { get; set; } //접속자 정보[ User_No, User_Name, u.User_Group_No, User_Group_Name, User_Group_Type, Auth_Name ]
        public POP_Main()
        {
            InitializeComponent();
        }

        private void POP_Main_Load(object sender, EventArgs e)
        {
            this.userInfo = ((Pop_MDIMain)this.MdiParent).userInfo;
            serv = new ServiceHelper("api/Pop/Uc");
            ResMessage<List<PopVO>> resresult= serv.GetAsyncT < ResMessage<List<PopVO>>>(userInfo.User_Group_No.ToString());

            if (resresult.ErrCode == 0)
            {
                allList = resresult.Data;               
            }
            else
            {
                MessageBox.Show(resresult.ErrMsg);
            }
            //

            lblCount.Text = allList.Count.ToString();
            int hinum = 340;
            int row = 1;
            for (int i = 1; i <= allList.Count; i++)
            {
                ucWorkGroup wg = new ucWorkGroup(allList[i - 1], userInfo);

                wg.Size = new Size(342, 338);
                wg.Name = $"ucWorkGroup{i}";
                wg.OrderCount = i;
                if (i % 6 == 0)
                {
                    wg.Location = new Point(3 + ((i - 1) * 342), 5 + (hinum * row));
                    hinum++;
                    row++;
                }
                else
                {
                    wg.Location = new Point(3 + ((i - 1) * 342), 5);
                }

                splitContainer1.Panel2.Controls.Add(wg);
            }
        }

        private void btnGrd_Click(object sender, EventArgs e)
        {
            POP_WorkEndGDV frm = new POP_WorkEndGDV(userInfo);
            foreach (Form frm1 in Application.OpenForms)
            {
                if (frm1.GetType() == typeof(Pop_MDIMain))
                {
                    frm.MdiParent = frm1;
                }
            }
            frm.Show();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2.Controls.Clear();
            ResMessage<List<PopVO>> resresult = serv.GetAsyncT<ResMessage<List<PopVO>>>(userInfo.User_Group_No.ToString());

            if (resresult.ErrCode == 0)
            {
                allList = resresult.Data;
            }
            else
            {
                MessageBox.Show(resresult.ErrMsg);
            }
            //

            lblCount.Text = allList.Count.ToString();
            int hinum = 340;
            int row = 1;
            for (int i = 1; i <= allList.Count; i++)
            {
                ucWorkGroup wg = new ucWorkGroup(allList[i - 1], userInfo);

                wg.Size = new Size(342, 338);
                wg.Name = $"ucWorkGroup{i}";
                wg.OrderCount = i;
                if (i % 6 == 0)
                {
                    wg.Location = new Point(3 + ((i - 1) * 342), 5 + (hinum * row));
                    hinum++;
                    row++;
                }
                else
                {
                    wg.Location = new Point(3 + ((i - 1) * 342), 5);
                }

                splitContainer1.Panel2.Controls.Add(wg);
            }
        }

        private void POP_Main_Activated(object sender, EventArgs e)
        {
            btnLogOut_Click(this, null);
        }
    }
}
