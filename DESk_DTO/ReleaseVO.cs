using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESK_DTO
{
    public class ReleaseVO
    {
        public int Order_No { get; set; }
        public string Client_Code { get; set; }
        public string Client_Name { get; set; }
        public string Order_Date { get; set; }
        public string Order_State { get; set; }
        public string Release_Date { get; set; }
        public string Release_State { get; set; }
        public string Release_OK_Date { get; set; }
        public string Create_Time { get; set; }
        public int Create_User_No { get; set; }
        public string Update_Time { get; set; }
        public int Update_User_No { get; set; }
    }
}
