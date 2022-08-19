using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DESK_DTO;
using System.Data.SqlClient;

namespace DESK_MES
{
    public class ReleaseService
    {
        public List<ReleaseVO> GetReleaseList()  // 출고 정보(주문 확정) 가져오기
        {
            ReleaseDAC dac = new ReleaseDAC();
            List<ReleaseVO> result = dac.GetReleaseList();
            dac.Dispose();

            return result;
        }
        public List<OrderDetailVO> GetOrderDetailList(int no)  // 주문 상세정보 가져오기
        {
            OrderDAC dac = new OrderDAC();
            List<OrderDetailVO> result = dac.GetOrderDetailList(no);
            dac.Dispose();

            return result;
        }

        public bool RegisterRelease(ReleaseVO release, List<OrderDetailVO> orderList, List<string> idlist)
        {
            ReleaseDAC dac = new ReleaseDAC();
            bool result = dac.RegisterRelease(release, orderList, idlist);
            dac.Dispose();

            return result;
        }

        public OrderDetailVO GetLastID()
        {
            ReleaseDAC dac = new ReleaseDAC();
            OrderDetailVO result = dac.GetLastID();
            dac.Dispose();

            return result;
        }
    }
}
