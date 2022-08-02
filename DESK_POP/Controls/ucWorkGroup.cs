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
    public partial class ucWorkGroup : UserControl
    {
        PopVO orderDetail;
        int cnt;
        public int OrderCount { get; set; }
        public ucWorkGroup(PopVO order, int count)
        {            
            orderDetail = order;
            cnt = count;
            InitializeComponent();
        }

        private void ucWorkGroup_Load(object sender, EventArgs e)
        {
            gBox.Text = $"작업{cnt}";
            txtWkCode.Text = orderDetail.Work_Code;
            txtOperation.Text = orderDetail.Operation_Name;
            txtEquipment.Text = orderDetail.Equipment_Name;
            dtpWork.Value = DateTime.Now;
            txtWStatus.Text = orderDetail.Work_Status;
            txtProductYN.Text = ""; //이거 테이블 확인해보기
        }
        //해당 작업 검색후 진행 화면으로 전송하기
        private void button2_Click(object sender, EventArgs e)
        {
            if (true) //여긴 데이터 조회 유무로 넘어가기 //작업지시 코드 상태를 조회 => 진행 전이면 add
            {
                Lot_Add frm = new Lot_Add(orderDetail.Work_Code);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    this.Show();
                }
            }
            else //진행 중 => Detail로
            {
                POP_Detail frm = new POP_Detail(orderDetail.Work_Code);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    this.Show();
                }
            }
        }
    }
}
