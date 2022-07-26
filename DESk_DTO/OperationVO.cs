using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESK_DTO
{
    public class OperationVO
    {
        public int Operation_No { get; set; }
        public string Operation_Name { get; set; }
        public string Is_Check_Deffect { get; set; }
        public string Is_Check_Inspect { get; set; }
        public string Is_Check_Marerial { get; set; }
        public DateTime Create_Time { get; set; }
        public int Create_User_No { get; set; }
        public DateTime Update_Time { get; set; }
        public int Update_User_No { get; set; }
        public string Is_Delete { get; set; }

        public string Create_User_Name { get; set; }
        public string Update_User_Name { get; set; }
    }
}
