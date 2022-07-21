using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESK_DTO
{
    public class ClientVO
    {
        //Client_Code 
        //Client_Name
        //Client_Type
        //Create_Time
        //Create_User_No
        //Update_Time
        //Update_User_No
        //Is_Delete
        public string Client_Code { get; set; }
        public string Client_Name { get; set; }
        public string Client_Type { get; set; }
        public string Client_Number { get; set; }
        public string Client_Phone { get; set; }
        public string Create_Time { get; set; }
        public int Create_User_No { get; set; }
        public string Update_Time { get; set; }
        public int Update_User_No { get; set; }
        public string Is_Delete { get; set; }
    }
}
