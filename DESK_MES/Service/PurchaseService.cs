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
        public List<PurchaseVO> GetPurchaseList()  // 발주 정보 가져오기
        {
            PurchaseDAC dac = new PurchaseDAC();
            List<PurchaseVO> result = dac.GetPurchaseList();
            dac.Dispose();

            return result;
        }

        public PurchaseVO GetPurchaseInfoByPurchaseCode(int no)
        {
            PurchaseDAC dac = new PurchaseDAC();
            PurchaseVO result = dac.GetPurchaseInfoByPurchaseCode(no);
            dac.Dispose();

            return result;
        }
        public List<PurchaseDetailVO> GetPurchaseDetailList(int no)  // 발주 상세정보 가져오기
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

        public List<PurchaseVO> GetClientList()  // 발주 거래처 가져오기
        {
            PurchaseDAC dac = new PurchaseDAC();
            List<PurchaseVO> result = dac.GetClientList();
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

        public PurchaseDetailVO GetLastID()
        {
            PurchaseDAC dac = new PurchaseDAC();
            PurchaseDetailVO result = dac.GetLastID();
            dac.Dispose();

            return result;
        }


        public PurchaseDetailVO GetIncomingProductInfo(string no)
        {
            PurchaseDAC dac = new PurchaseDAC();
            PurchaseDetailVO result = dac.GetIncomingProductInfo(no);
            dac.Dispose();

            return result;
        }
        public List<PurchaseDetailVO> GetEqualWarehouse(string no)
        {
            PurchaseDAC dac = new PurchaseDAC();
            List<PurchaseDetailVO> result = dac.GetEqualWarehouse(no);
            dac.Dispose();

            return result;
        }
        public bool RegisterIncomingPurchase(PurchaseVO purchase, List<string> lotIDList, List<PurchaseDetailVO> purchaseList)
        {
            PurchaseDAC dac = new PurchaseDAC();
            bool result = dac.RegisterIncomingPurchase(purchase, lotIDList, purchaseList);
            dac.Dispose();

            return result;
        }

        public List<PurchaseDetailVO> GetIncomingList()  // 입고목록 가져오기
        {
            PurchaseDAC dac = new PurchaseDAC();
            List<PurchaseDetailVO> result = dac.GetIncomingList();
            dac.Dispose();

            return result;
        }

        public bool UpdatePurchaseInfo(PurchaseVO info)
        {
            PurchaseDAC dac = new PurchaseDAC();
            bool result = dac.UpdatePurchaseInfo(info);
            dac.Dispose();

            return result;
        }
    }
}
