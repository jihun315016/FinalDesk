using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESK_DTO
{
    public class PopVO
    {
        //유저 정보
        public int User_No { get; set; }
        public string User_Name { get; set; }
        public int User_Group_No { get; set; }
        public string User_Group_Name { get; set; }
        public int User_Group_Type { get; set; }
        public string Auth_Name { get; set; }
        //작업 정보
        public string Production_No { get; set; }
        public string Work_Code { get; set; }
        public int Operation_Code { get; set; }
        public string Operation_Name { get; set; } //쿼리문에서 오더 명 찾기
        public string Work_Status { get; set; }
        public int Planned_Qty { get; set; }
        public int Work_Qty { get; set; }
        public string Start_Date { get; set; }
        public string Estimated_Date { get; set; }
        public string Complete_Date { get; set; }
        public string Create_Time { get; set; }
        public int Create_User_No { get; set; }
        public string Update_Time { get; set; }
        public int Update_User_No { get; set; }
        public int Work_UserGroup_No { get; set; }
        //설비정보
        public int Equipment_No { get; set; }
        public string Equipment_Name { get; set; }

    }
}
