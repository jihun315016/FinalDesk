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
    public partial class POP_WorkEndGDV : Form
    {
        ServiceHelper serv;
        ServiceHelper servG;
        PopVO userV;
        List<PopVO> allList;
        public POP_WorkEndGDV(PopVO user)
        {
            userV = user;
            InitializeComponent();
        }
        public void BindingGdv()
        {
            //List<EquipmentVO> list = 
            //allList = list;
            servG = new ServiceHelper("api/Pop/Gdv");
            ResMessage<List<PopVO>> resresult = servG.GetAsyncT<ResMessage<List<PopVO>>>(userV.User_Group_No.ToString());
            dgvMain.DataSource = null;
            dgvMain.DataSource = allList= resresult.Data;
        }
        private void POP_WorkEndGDV_Load(object sender, EventArgs e)
        {
            serv = new ServiceHelper("api/Pop/Uc");
            ResMessage<List<PopVO>> resresult = serv.GetAsyncT<ResMessage<List<PopVO>>>(userV.User_Group_No.ToString());
            


            /*  
                Production_No, Work_Code,User_Group_No,User_Group_Name, 
                Production_Equipment_Code,Equipment_Name,Production_Operation_Code,Operation_Name, 
                Start_Due_Date,Work_State_Name,Start_Date, Complete_Date,Update_Time,Update_User_No,Update_User_Name
             */
            DataGridUtil.SetInitGridView(dgvMain);

            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "작업지시번호", "Work_Code", colWidth: 200, alignContent: DataGridViewContentAlignment.MiddleCenter); //0
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "생산번호", "Production_No", colWidth: 200, alignContent: DataGridViewContentAlignment.MiddleCenter); //1
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "작업담당 그룹코드", "User_Group_No", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleRight); //2           //초당 생산량으로 변경
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "작업담당 그룹명", "User_Group_Name", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleCenter); //3
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "설비코드", "Production_Equipment_Code", colWidth: 200, alignContent: DataGridViewContentAlignment.MiddleCenter); //4

            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "설비명", "Equipment_Name", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleRight); //5
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "생성자", "Create_User_Name", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleRight); //6
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "공정코드", "Production_Operation_Code", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleRight); //7
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "공정명", "Operation_Name", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleRight); //8

            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "작업시작 예정일", "Start_Due_Date", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleRight); //9
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "작업상태", "Work_State_Name", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleRight);   //10
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "작업시작일", "Start_Date", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleRight);       //11
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "작업종료일", "Complete_Date", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleRight);    //12
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "수정일", "Update_Time", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleRight);         //13
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "수정 작업자 번호", "Update_User_No", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleRight); //14
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "수정 작업자명", "Update_User_Name", colWidth: 150, alignContent: DataGridViewContentAlignment.MiddleRight); //15
            BindingGdv();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == typeof(POP_Main))
                {
                    frm.Activate();
                    frm.BringToFront();
                    return;
                }
            }
        }
    }
}
