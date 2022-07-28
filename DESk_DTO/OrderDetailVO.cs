using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESK_DTO
{
    public class OrderDetailVO
    {
        public int Order_No { get; set; }
        public string Product_Code { get; set; }
        public string Product_Name { get; set; }
        public string Product_Type { get; set; }
        public int Price { get; set; }
        public int Unit { get; set; }
        public int TotalPrice { get; set; }
        public int Qty_PerUnit { get; set; }
        public int TotalQty { get; set; }

    }
}
