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
        ServiceHelper serv;
        PopVO sTWork;
        PopVO userV;
        PopVO msgList;
        public Lot_Add(PopVO work)
        {
            sTWork = work;
            InitializeComponent();
        }

        private void Lot_Add_Load(object sender, EventArgs e)
        {
            DataGridUtil.SetInitGridView(dgvMain);

            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "품번", "Equipment_No", colWidth: 200, alignContent: DataGridViewContentAlignment.MiddleCenter); //0
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "품명", "Equipment_Name", colWidth: 200, alignContent: DataGridViewContentAlignment.MiddleLeft); //1

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
                serv = new ServiceHelper("api/Pop/StartEq");
                ResMessage<PopVO> resresult = serv.GetAsyncT<ResMessage<PopVO>>(sTWork.Work_Code);

                if (resresult.ErrCode == 0)
                {
                    msgList = resresult.Data;

                    Process p = Process.Start(server, txtWkCode.Text);
                    proId = p.Id;
                    POP_Detail pop = new POP_Detail(txtWkCode.Text);
                    pop.Show();
                }
                else
                {
                    MessageBox.Show(resresult.ErrMsg);
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
