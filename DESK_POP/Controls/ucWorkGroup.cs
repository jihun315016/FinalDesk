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
        PopVO workDetail;
        ServiceHelper serv;
        public int OrderCount { get; set; }
        public ucWorkGroup(PopVO order)
        {            
            orderDetail = order;
            InitializeComponent();
        }

        private void ucWorkGroup_Load(object sender, EventArgs e)
        {
            gBox.Text = $"작업{OrderCount}";
            txtWkCode.Text = orderDetail.Work_Code;
            //DB
            serv = new ServiceHelper("api/Pop/Detail");
            ResMessage<PopVO> resresult = serv.GetAsyncT<ResMessage<PopVO>>(orderDetail.Work_Code);
            if (resresult.ErrCode == 0)
            {
                workDetail = resresult.Data;
            }
            else
            {
                MessageBox.Show(resresult.ErrMsg);
            }
            //
            txtOperation.Text = orderDetail.Operation_Name;
            txtEquipment.Text = orderDetail.Equipment_Name; //이건 있음
            dtpWork.Value = Convert.ToDateTime(orderDetail.Start_Due_Date);
            txtWStatus.Text = orderDetail.Work_State_Name;
            txtProductYN.Text = orderDetail.Material_Lot_Input_State_Name; //이거 테이블 확인해보기
        }
        //해당 작업 검색후 진행 화면으로 전송하기
        private void button2_Click(object sender, EventArgs e)
        {
            if (workDetail.Work_State ==1) //여긴 데이터 조회 유무로 넘어가기 //작업지시 코드 상태를 조회 => 진행 전이면 add
            {
                Lot_Add frm = new Lot_Add(workDetail);
                foreach (Form frm1 in Application.OpenForms)
                {
                    if (frm1.GetType() == typeof(Pop_MDIMain))
                    {
                        frm.MdiParent = frm1;
                    }
                }
                frm.Show();
            }
            else //진행 중 => Detail로
            {
                POP_Detail frm = new POP_Detail(orderDetail.Work_Code);
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
}
