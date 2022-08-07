using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DESK_WEB.Models
{
    public class PopVO
    {
        //login
        public int User_No { get; set; }
        public string User_Name { get; set; }
        public int User_Group_No { get; set; }
        public string User_Group_Name { get; set; }
        public int User_Group_Type { get; set; }
        public string Auth_Name { get; set; }
        //pop uc
        public int Production_No { get; set; }
        public int Production_Equipment_Code { get; set; }
        public string Equipment_Name { get; set; }
        public string Start_Due_Date { get; set; }
        public string Work_State { get; set; }
        public string Material_Lot_Input_State { get; set; }

    }
}