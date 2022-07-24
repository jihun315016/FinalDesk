using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESK_DTO
{
    public class UserVO
    {
        public int User_No { get; set; }
        public string User_Name { get; set; }
        public string User_Pwd { get; set; }
        public int User_Group_No { get; set; }
        public string Create_Time { get; set; }
        public int Create_User_No { get; set; }
        public string Update_Time { get; set; }
        public int Update_User_No { get; set; }
        public string Is_Delete { get; set; }

        //쿼리로 추가한것
        public string User_Group_Name { get; set; }
        public int Auth_ID { get; set; }
        public string Auth_Name { get; set; }
        public string Create_User_Name { get; set; }
        public string Update_User_Name { get; set; }      
    }
}
