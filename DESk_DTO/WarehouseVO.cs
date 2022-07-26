using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESK_DTO
{
    public class WarehouseVO
    {
        public string Warehouse_Code { get; set; }
        public string Warehouse_Name { get; set; }
        public string Warehouse_Address { get; set; }
        public string Warehouse_Type { get; set; }
        public string Create_Time { get; set; }
        public int Create_User_No { get; set; }
        public string Update_Time { get; set; }
        public int Update_User_No { get; set; }
        public string Is_Delete { get; set; }
    }
}
