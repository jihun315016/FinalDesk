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
        public List<ManufactureVO> GetProductListByType(string type)
        {
            ManufactuerDAC dac = new ManufactuerDAC();
            List<ManufactureVO> result = dac.GetProductListByType(type);
            dac.Dispose();

            return result;
        }

        public bool RegisterManufacturePlan(ManufactureVO plan)
        {
            ManufactuerDAC dac = new ManufactuerDAC();
            bool result = dac.RegisterManufacturePlan(plan);
            dac.Dispose();

            return result;
        }

        public List<ManufactureVO> GetmanufactureList()  // 생산 정보 가져오기
        {
            ManufactuerDAC dac = new ManufactuerDAC();
            List<ManufactureVO> result = dac.GetmanufactureList();
            dac.Dispose();

            return result;
        }

        public bool UpdateManufactureState(ManufactureVO plan)
        {
            ManufactuerDAC dac = new ManufactuerDAC();
            bool result = dac.UpdateManufactureState(plan);
            dac.Dispose();

            return result;
        }
    }
}
