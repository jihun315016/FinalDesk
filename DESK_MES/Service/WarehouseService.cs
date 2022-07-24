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
        public List<WarehouseProductVO> GetWarehouseDetailList(string code) 
        {
            WarehouseDAC dac = new WarehouseDAC();
            List<WarehouseProductVO> result = dac.GetWarehouseDetailList(code);
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
