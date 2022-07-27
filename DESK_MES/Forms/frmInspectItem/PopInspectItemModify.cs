using DESK_DTO;
using DESK_MES.Service;
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
    public partial class PopInspectItemModify : Form
    {
        InspectService inspectSrv;
        UserVO user;

        public PopInspectItemModify(InspectItemVO item, UserVO user)
        {
            InitializeComponent();
            InitControl(item);
            this.user = user;
        }

        private void PopInspectItemModify_Load(object sender, EventArgs e)
        {
            inspectSrv = new InspectService();
        }

        void InitControl(InspectItemVO item)
        {
            txtInspectNo.Text = item.Inspect_No.ToString();
            txtInspectName.Text = item.Inspect_Name;
            txtTarget.Text = item.Target.ToString();
            txtLsl.Text = item.LSL.ToString();
            txtUsl.Text = item.USL.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            InspectItemVO item = new InspectItemVO()
            {
                Inspect_No = Convert.ToInt32(txtInspectNo.Text),
                Inspect_Name = txtInspectName.Text,
                Target = Convert.ToInt32(txtTarget.Text),
                LSL = Convert.ToInt32(txtLsl.Text),
                USL = Convert.ToInt32(txtUsl.Text),
                Update_User_No = user.Update_User_No
            };

            bool result = inspectSrv.UpdateInspectItem(item);
            if (result)
            {
                MessageBox.Show("수정이 완료되었습니다.");
                this.Close();
            }
            else
            {
                MessageBox.Show("수정에 실패했습니다.");
            }
        }
    }
}
