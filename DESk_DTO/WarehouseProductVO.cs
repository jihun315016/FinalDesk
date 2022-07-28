using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESK_DTO
{
    public class WarehouseProductVO
    {
        public string Warehouse_Code { get; set; }
        public string Warehouse_Name { get; set; }
        public string Product_Code { get; set; }
        public string Product_Name { get; set; }
        public int Product_Quantity { get; set; }
        public int Safe_Stock { get; set; }

    }
}
