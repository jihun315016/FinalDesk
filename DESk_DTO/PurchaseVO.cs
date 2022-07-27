using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESK_DTO
{
    public class PurchaseVO
    {
        public int Purchase_No { get; set; }
        public string Client_Code { get; set; }
        public string Client_Name { get; set; }
        public string Purchase_Date { get; set; }
        public string Purchase_State { get; set; }
        public string IncomingDue_date { get; set; }
        public string Is_Incoming { get; set; }
        public string Incoming_Date { get; set; }
        public string Create_Time { get; set; }
        public int Create_User_No { get; set; }
        public string Update_Time { get; set; }
        public string Update_User_No { get; set; }
        public string Warehouse_Code { get; set; }
    }
}
