using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DESK_WEB.Models
{
    public class PopVO
    {
        public int User_No { get; set; }
        public string User_Name { get; set; }
        public int User_Group_No { get; set; }
        public string User_Group_Name { get; set; }
        public int User_Group_Type { get; set; }
        public string Auth_Name { get; set; }
    }
}