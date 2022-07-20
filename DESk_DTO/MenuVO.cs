using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESK_DTO
{
    public class MenuVO
    {
        // Function_No, Function_Name, Child_Function_No, frmName
        public int Function_No { get; set; }
        public string Function_Name { get; set; }
        public int Parent_Function_No { get; set; }
        public string frmName { get; set; }

    }
}
