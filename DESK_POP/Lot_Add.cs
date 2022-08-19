using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DESK_DTO;

namespace DESK_POP
{
    public partial class Lot_Add : Form
    {
        PopVO sTWork;
        PopVO userV;
        public Lot_Add(PopVO work)
        {
            sTWork = work;
            InitializeComponent();
        }

        private void Lot_Add_Load(object sender, EventArgs e)
        {

            userV = ((Pop_MDIMain)this.MdiParent).userInfo;
            txtWkCode.Text = sTWork.Work_Code;
            txtOperation.Text = sTWork.Operation_Name;
            txtEquipment.Text = sTWork.Equipment_Name;
            dtpWorkStartDue.Value = Convert.ToDateTime(sTWork.Start_Due_Date);
            dtpWorkEndDue.Value = Convert.ToDateTime(sTWork.Complete_Due_Date);
            txtWorkState.Text = sTWork.Work_State_Name;
            txtOutput.Text = sTWork.Product_Name;
            txtQtyDue.Text = sTWork.Work_Plan_Qty.ToString();
            txtWorkUserName.Text = userV.User_Name;
            txtWorkUserGroup.Text = userV.User_Group_Name;
            //생산 제품명 A 그리드 뷰로 표시
        }
        int proId;
        private void btnStart_Click(object sender, EventArgs e)
        {
            string server = Application.StartupPath + "\\DESK_EQM.exe";
            if (MessageBox.Show("해당 작업을 시작합니다","작업시작",MessageBoxButtons.OKCancel) == DialogResult.OK) // 시작합니다
            {
                Process p = Process.Start(server, txtWkCode.Text);
                proId = p.Id;
                POP_Detail pop = new POP_Detail(txtWkCode.Text);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
