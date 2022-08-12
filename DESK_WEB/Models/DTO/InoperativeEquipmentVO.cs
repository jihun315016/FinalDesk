using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DESK_WEB.Models.DTO
{
    public class InoperativeEquipmentVO
    {
        public int Equipment_No { get; set; }
        public string Equipment_Name { get; set; }
        public DateTime Inoperative_Start_Time { get; set; }
        public DateTime Inoperative_End_Time { get; set; }
        public int MinuiteDiff { get; set; }
        public string User_Name { get; set; }
        public string Inoperative_Reason { get; set; }
        public string Action_History { get; set; }
    }
}