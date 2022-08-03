using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DESK_DTO;
using System.Data.SqlClient;

namespace DESK_MES
{
    public class WorkOrderService
    {
        public List<OperationVO> GetOperationList(string code)
        {
            WorkOrderDAC dac = new WorkOrderDAC();
            List<OperationVO> result = dac.GetOperationList(code);
            dac.Dispose();

            return result;
        }


        public List<EquipmentVO> GetProcessList(int operationNo)
        {
            WorkOrderDAC dac = new WorkOrderDAC();
            List<EquipmentVO> result = dac.GetProcessList(operationNo);
            dac.Dispose();

            return result;
        }

        public List<PurchaseDetailVO> GetOutputWarehouse(string no)
        {
            WorkOrderDAC dac = new WorkOrderDAC();
            List<PurchaseDetailVO> result = dac.GetOutputWarehouse(no);
            dac.Dispose();

            return result;
        }

        public List<PurchaseDetailVO> GetMetarialLotList(string no)
        {
            WorkOrderDAC dac = new WorkOrderDAC();
            List<PurchaseDetailVO> result = dac.GetMetarialLotList(no);
            dac.Dispose();

            return result;
        }
    }
}
