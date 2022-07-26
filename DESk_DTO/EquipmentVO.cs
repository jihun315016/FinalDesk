using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESK_DTO
{
    public class EquipmentVO
    {
        public int Equipment_No { get; set; }
        public string Equipment_Name { get; set; }
        public int Operation_Type_No { get; set; }
        public string Operation_Type_Name { get; set; }
        public string Is_Inoperative { get; set; }
        public string Inoperative_Start_Time { get; set; }
        public string Create_Time { get; set; }
        public int Create_User_No { get; set; }
        public string Create_User_Name { get; set; }
        public string Update_Time { get; set; }
        public int Update_User_No { get; set; }
        public string Update_User_Name { get; set; }
        public string Is_Delete { get; set; }
        //
        public int Code { get; set; }
        public string Name { get; set; }
    }
}
