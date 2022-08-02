using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DESK_DTO;
using System.Data.SqlClient;

namespace DESK_MES
{
    public class ManufactureService
    {
        // 주문서 가져오기(combobox용)
        public List<ManufactureVO> GetOrderList() 
        {
            ManufactuerDAC dac = new ManufactuerDAC();
            List<ManufactureVO> result = dac.GetOrderList();
            dac.Dispose();

            return result;
        }
        // 주문서에 따른 생산할 제품 목록 가져오기
        public List<ManufactureVO> GetOrderProductListForManufacture(int orderNo)
        {
            ManufactuerDAC dac = new ManufactuerDAC();
            List<ManufactureVO> result = dac.GetOrderProductListForManufacture(orderNo);
            dac.Dispose();

            return result;
        }

    }
}
