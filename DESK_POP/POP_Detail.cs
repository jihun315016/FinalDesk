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
using System.Timers;
using DESK_DTO;

namespace DESK_POP
{
    public partial class POP_Detail : Form
    {
        System.Timers.Timer qtyTimer;
        ServiceHelper serv;
        PopVO workList;
        PopVO workEqList;
        Process p;
        PopVO userV;
        string oCode;
        POP_DAC dac;
        int proId;
        public POP_Detail(string orderCode, PopVO userv)
        {
            userV = userv;
            oCode = orderCode;
            InitializeComponent();

            qtyTimer = new System.Timers.Timer(5000);
            qtyTimer.Elapsed += timer1_Tick;
            qtyTimer.AutoReset = true;
        }

        private void POP_Detail_Load(object sender, EventArgs e)
        {
            serv = new ServiceHelper("api/Pop/StartEq");
            ResMessage<PopVO> resresult = serv.GetAsyncT<ResMessage<PopVO>>(oCode);

            if (resresult.ErrCode == 0)
            {
                workList = resresult.Data;
                serv = new ServiceHelper("api/Pop/WorkAll");
                ResMessage<List<PopVO>> resresult2 = serv.GetAsyncT<ResMessage<List<PopVO>>>(oCode);
                if (resresult2.ErrCode == 0)
                {
                    workEqList = resresult2.Data[0];
                    Starttimer();
                }
                else
                {
                    MessageBox.Show(resresult.ErrMsg);
                }
            }
            else
            {
                MessageBox.Show(resresult.ErrMsg);
            }
            //DB 가서 oCode로 해당 작업가져오기
            txtWork.Text = oCode;//작업지시번호
            txtEqui.Text = workEqList.Equipment_Name;//설비 번호? 명 ? 
            if (workList.Product_Name == null)
            {

            }
            else
            {
                txtProduct.Text = workEqList.Product_Name;// 작업 품목
            }
            txtUserName.Text = userV.User_Name; //작업자명
            UserGroupNa.Text = userV.User_Group_Name; //작업팀명
            UserNo.Text = userV.User_No.ToString(); //작업자 No
            txtWorkQty.Text = workEqList.Planned_Qty.ToString(); //작업지시량
            txtStartTime.Text = Convert.ToDateTime( workEqList.Start_Date.ToString()).ToString("hh:MM:ss"); //작업 시작 시간
            txtOutput.Text = workEqList.Output_Qty.ToString(); //생산량
        }
        /// <summary>
        /// 타이머 시작, 댁 생성자 생성
        /// </summary>
        public void Starttimer()
        {
            qtyTimer.Start();
            dac = new POP_DAC();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {

                    lblNowTime.Text = DateTime.Now.ToString();
                    PopVO qtyList = dac.GetWorkQty(oCode);
                    if (qtyList != null)
                    {

                        int nowQty = qtyList.Working_Qty;

                    //oCode로 DB에서 해당 작업 내역 가져오기
                    txtWorking.Text = nowQty.ToString();   //현재작업량
                    txtRemainQtc.Text = ((workEqList.Planned_Qty) - nowQty) <= 0 ? "0" : ((workEqList.Planned_Qty) - nowQty).ToString(); //오더잔량

                    // 현제작업량/작업지시량 = 진척률
                    txtProgress.Text = (workEqList.Planned_Qty == 0) ? "0%" : $"{(nowQty / workEqList.Planned_Qty * 100)}%";  //진척률
                                                                                                                              //만약 정지가 아니면 / 시작시간 - 현재시간 /정지면 기록된 시간
                    txtRunningTime.Text = (workEqList.Start_Date != null) ? (Convert.ToDateTime(workEqList.Start_Date.ToString()) - DateTime.Now).ToString() : ""; //공정가동시간
                }

                }));
            }
        }


        private void btnBack_Click(object sender, EventArgs e)
        {
            dac.Dispose();
            this.Close();
        }

        private void txtWorkQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            string server = Application.StartupPath + "\\DESK_EQM.exe";
            serv = new ServiceHelper("api/Pop/StartEq");
            ResMessage<PopVO> resresult = serv.GetAsyncT<ResMessage<PopVO>>(oCode);
            if (resresult.ErrCode == 0)
            {
                p = Process.Start(server, oCode);
                proId = p.Id;
            }
            else
            {
                MessageBox.Show(resresult.ErrMsg);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (p.HasExited)
            {
                p.Kill();
            }
            qtyTimer.Stop();
            dac.Dispose();

        }
        private void btnEnd_Click(object sender, EventArgs e)
        {
            if (p.HasExited)
            {
                p.Kill();
            }
            qtyTimer.Stop();
            dac.Dispose();
            PopVO poV = new PopVO
            {
                Work_Code = txtWork.Text,
                Production_Equipment_Code = workEqList.Production_Equipment_Code,
                Equipment_Name= txtEqui.Text,
                Product_Name=txtProduct.Text,
                User_No = userV.User_No,
                User_Name = userV.User_Name,
                User_Group_No = userV.User_Group_No,
                User_Group_Name = userV.User_Group_Name,
                Planned_Qty = workEqList.Planned_Qty,
                Working_Qty = Convert.ToInt32( txtWorking.Text)
            };
            
            Lot_End frm = new Lot_End(txtMemo.Text, poV);
            foreach (Form frm1 in Application.OpenForms)
            {
                if (frm1.GetType() == typeof(Pop_MDIMain))
                {
                    frm.MdiParent = frm1;
                }
            }
            frm.Show();
        }
    }
}
