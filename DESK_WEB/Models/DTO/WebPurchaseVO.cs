using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DESK_WEB.Models.DTO
{
    public class WebPurchaseVO
    {
        public int Purchase_No { get; set; }
        public DateTime Purchase_Date { get; set; }
        public DateTime Incoming_Date { get; set; }
        public string Client_Name { get; set; }
        public string Product_Code { get; set; }
        public string Product_Name { get; set; }
        public int TotalQty { get; set; }
        public int Price { get; set; }
        public int TotalPrice { get; set; }
    }
}