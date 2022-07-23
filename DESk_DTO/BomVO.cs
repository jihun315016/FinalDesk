using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESK_DTO
{
    public class BomVO
    {
        public string Parent_Product_Code { get; set; }
        public string Child_Product_Code { get; set; }
        public int Qty { get; set; }
        public string Create_Time { get; set; }
        public string Create_User_No { get; set; }

        public string Child_Name { get; set; }
        public string Child_Type { get; set; }
    }
}
