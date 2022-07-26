using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DESK_DTO;
using System.Data.SqlClient;

namespace DESK_MES
{
    public class PurchaseService
    {
        public List<PurchaseVO> GetPurchaseList()  // 주문 정보 가져오기
        {
            PurchaseDAC dac = new PurchaseDAC();
            List<PurchaseVO> result = dac.GetPurchaseList();
            dac.Dispose();

            return result;
        }

        public List<PurchaseDetailVO> GetPurchaseDetailList(int no)  // 주문 상세정보 가져오기
        {
            PurchaseDAC dac = new PurchaseDAC();
            List<PurchaseDetailVO> result = dac.GetPurchaseDetailList(no);
            dac.Dispose();

            return result;
        }

        public List<ProductVO> GetProductListForPurchase()
        {
            PurchaseDAC dac = new PurchaseDAC();
            List<ProductVO> result = dac.GetProductListForPurchase();
            dac.Dispose();

            return result;
        }

        public bool RegisterPurchase(PurchaseVO purchase, List<PurchaseDetailVO> purchaseList)
        {
            PurchaseDAC dac = new PurchaseDAC();
            bool result = dac.RegisterPurchase(purchase, purchaseList);
            dac.Dispose();

            return result;
        }

        public bool RegisterIncomingPurchase(PurchaseVO purchase, List<PurchaseDetailVO> purchaseList)
        {
            PurchaseDAC dac = new PurchaseDAC();
            bool result = dac.RegisterIncomingPurchase(purchase, purchaseList);
            dac.Dispose();

            return result;
        }
    }
}
