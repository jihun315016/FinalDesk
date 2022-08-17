using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DESK_WEB.Models.DTO
{
    public class OrderPurchaseVO
    {
        public DateTime Date { get; set; }
        public int Total { get; set; }
        public string Type { get; set; }
    }
}