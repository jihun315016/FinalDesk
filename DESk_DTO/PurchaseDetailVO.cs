using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESK_DTO
{
    public class PurchaseDetailVO
    {
        public int Purchase_No { get; set; }
        public string Product_Code { get; set; }
        public int TotalQty { get; set; }
        public int TotalPrice { get; set; }
        public int Qty_PerUnit { get; set; }
        public string Product_Name { get; set; }
        public string Product_Type { get; set; }
        public int Price { get; set; }
        public int Unit { get; set; }

        // 자재LOT 테이블
        public string Lot_Code { get; set; }
        public int Client_Code { get; set; }
        public string Lot_Time { get; set; } // 입고된 시간
        public int Lot_Qty { get; set; } // 입고수량 = 발주 수량
        public string Cur_Qty { get; set; } // 현재수량 = 입고수량 = 생산 후 잔여 수량
        public string Warehouse_Code { get; set; }
        public int Create_Time { get; set; }
        public string Create_User_No { get; set; } // 입고된 시간
    }
}
