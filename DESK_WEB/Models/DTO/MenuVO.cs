using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DESK_WEB.Models.DTO
{
    public class MenuVO
    {
        public int Function_No { get; set; }
        public string Function_Name { get; set; }
        public int Parent_Function_No { get; set; }
        public string frmName { get; set; }
    }
}