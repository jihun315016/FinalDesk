using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESK_DTO
{
    public class ManufactureVO
    {
        public int Production_Code { get; set; } // 생산번호
        public int Order_No { get; set; } // 주문코드
        public string Product_Code { get; set; } // 생산제품코드
        public string Product_Name { get; set; } // 생산제품명
        public int Planned_Qty { get; set; } // 생산 계획 수량
        public int Production_Qty { get; set; } // 생산 완료 수량
        public string Start_Date { get; set; }  // 생산 시작일
        public string Estimated_Date { get; set; } // 생산 완료 예정일
        public string Complete_Date { get; set; } // 생산 완료일
        public string Production_Plan_Status { get; set; } // 생산 계획 상태 (미정/ 확정)
        public string Production_Status { get; set; } // 생산 진행 상태 (대기, 생산중, 완료)
        public int Production_Plan_User_No { get; set; } // 생산계획 담당자
        public string Production_Plan_User_Name { get; set; }// 생산계획 담당자명
        public string Create_Time { get; set; }
        public int Create_User_No { get; set; }
        public string Create_User_Name { get; set; }
        public string Update_Time { get; set; }
        public int Update_User_No { get; set; }
        public string Update_User_Name { get; set; }
    }
}
