using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DESK_POP
{
    public partial class POP_Detail : Form
    {
        string oCode;
        public POP_Detail(string orderCode)
        {
            oCode = orderCode;
            InitializeComponent();
        }

        private void POP_Detail_Load(object sender, EventArgs e)
        {
            //DB 가서 oCode로 해당 작업가져오기
            txtWork.Text = "";//작업지시번호
            txtEqui.Text = "";//설비 번호? 명 ? 
            txtProduct.Text = "";// 작업 품목
            txtUserName.Text = ""; //작업자명
            UserGroupNa.Text = ""; //작업팀명
            UserNo.Text = ""; //작업자 No
            txtWorkQty.Text = ""; //작업지시량
            txtStartTime.Text = ""; //작업 시작 시간
            txtOutput.Text = ""; //생산량
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblNowTime.Text = DateTime.Now.ToString();

            //oCode로 DB에서 해당 작업 내역 가져오기
            txtWorking.Text = "";   //현재작업량
            txtRemainQtc.Text = ""; //오더잔량

            // 현제작업량/작업지시량 = 진척률
            txtProgress.Text = "";  //진척률
            //만약 정지가 아니면 / 시작시간 - 현재시간 /정지면 기록된 시간
            txtRunningTime.Text = ""; //공정가동시간

        }

        private void btnEnd_Click(object sender, EventArgs e)
        {

        }
    }
}
