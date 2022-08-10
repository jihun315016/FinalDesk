using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DESK_DTO;

namespace DESK_MES
{
    public class IncomingService
    {
        public List<PurchaseVO> GetIncomingList()  // 입고 정보 가져오기
        {
            IncomingDAC dac = new IncomingDAC();
            List<PurchaseVO> result = dac.GetIncomingList();
            dac.Dispose();

            return result;
        }

        public List<PurchaseDetailVO> GetLotDetailList(string code)
        {
            IncomingDAC dac = new IncomingDAC();
            List<PurchaseDetailVO> result = dac.GetLotDetailList(code);
            dac.Dispose();

            return result;
        }

        public PurchaseDetailVO GetLastID()
        {
            IncomingDAC dac = new IncomingDAC();
            PurchaseDetailVO result = dac.GetLastID();
            dac.Dispose();

            return result;
        }

        // 입고하는 품목 정보 가져오기
        public PurchaseDetailVO GetIncomingProductInfo(string no)
        {
            IncomingDAC dac = new IncomingDAC();
            PurchaseDetailVO result = dac.GetIncomingProductInfo(no);
            dac.Dispose();

            return result;
        }

        // 창고에 보관된 자재 정보 가져오기 (입고시 해당 창고의 자재 수량 증가)
        public List<PurchaseDetailVO> GetEqualWarehouse(string no)
        {
            IncomingDAC dac = new IncomingDAC();
            List<PurchaseDetailVO> result = dac.GetEqualWarehouse(no);
            dac.Dispose();

            return result;
        }

        // 선택 발주 입고처리하기
        public bool RegisterIncomingPurchase(PurchaseVO purchase, List<string> lotIDList, List<PurchaseDetailVO> purchaseList)
        {
            IncomingDAC dac = new IncomingDAC();
            bool result = dac.RegisterIncomingPurchase(purchase, lotIDList, purchaseList);
            dac.Dispose();

            return result;
        }
    }
}
