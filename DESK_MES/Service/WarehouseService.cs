using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DESK_DTO;
using System.Data.SqlClient;

namespace DESK_MES
{
    public class WarehouseService
    {
        public List<WarehouseVO> GetAllWarehouse()  // 창고 정보 가져오기
        {
            WarehouseDAC dac = new WarehouseDAC();
            List<WarehouseVO> result = dac.GetAllWarehouse();
            dac.Dispose();

            return result;
        }

        public List<WarehouseProductVO> GetWarehouseDetailList(string code) 
        {
            WarehouseDAC dac = new WarehouseDAC();
            List<WarehouseProductVO> result = dac.GetWarehouseDetailList(code);
            dac.Dispose();

            return result;
        }

        public WarehouseVO GetWarehouseInfoByCode(string code)  // 선택 창고 상세 정보 가져오기
        {
            WarehouseDAC dac = new WarehouseDAC();
            WarehouseVO result = dac.GetWarehouseInfoByCode(code);
            dac.Dispose();

            return result;
        }

        public WarehouseVO GetLastID() // 마지막 창고코드 가져오기
        {
            WarehouseDAC dac = new WarehouseDAC();
            WarehouseVO result = dac.GetLastID();
            dac.Dispose();

            return result;
        }

        public bool RegisterWarehouse(WarehouseVO warehouse)
        {
            WarehouseDAC dac = new WarehouseDAC();
            bool result = dac.RegisterWarehouse(warehouse);
            dac.Dispose();

            return result;
        }

        public bool UpdateWarehouseInfo(WarehouseVO warehouse)
        {
            WarehouseDAC dac = new WarehouseDAC();
            bool result = dac.UpdateWarehouseInfo(warehouse);
            dac.Dispose();

            return result;
        }

        public bool DeleteWarehouseInfo(string warehouse)
        {
            WarehouseDAC dac = new WarehouseDAC();
            bool result = dac.DeleteWarehouseInfo(warehouse);
            dac.Dispose();

            return result;
        }

        public List<ProductVO> GetProductListForWarehouse()
        {
            WarehouseDAC dac = new WarehouseDAC();
            List<ProductVO> result = dac.GetProductListForWarehouse();
            dac.Dispose();

            return result;
        }

        public bool SaveProductInWarehouse(string warehouseNo, List<WarehouseProductVO> saveList)
        {
            WarehouseDAC dac = new WarehouseDAC();
            bool result = dac.SaveProductInWarehouse(warehouseNo, saveList);

            return result;
        }

        
    }
}
