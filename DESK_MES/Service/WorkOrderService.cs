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
        public List<PurchaseDetailVO> GetProductionSaveWarehouse(string code)
        {
            WorkOrderDAC dac = new WorkOrderDAC();
            List<PurchaseDetailVO> result = dac.GetProductionSaveWarehouse(code);
            dac.Dispose();

            return result;
        }

        public List<ProductVO> GetBomListForRegisert(string code)
        {
            WorkOrderDAC dac = new WorkOrderDAC();
            List<ProductVO> result = dac.GetBomListForRegisert(code);
            dac.Dispose();

            return result;
        }

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

        public List<PurchaseDetailVO> GetOutputWarehouse()
        {
            WorkOrderDAC dac = new WorkOrderDAC();
            List<PurchaseDetailVO> result = dac.GetOutputWarehouse();
            dac.Dispose();

            return result;
        }
        public List<UserGroupVO> GetWorkGroupList()
        {
            WorkOrderDAC dac = new WorkOrderDAC();
            List<UserGroupVO> result = dac.GetWorkGroupList();
            dac.Dispose();

            return result;
        }

        public List<PurchaseDetailVO> GetMetarialList(string no)
        {
            WorkOrderDAC dac = new WorkOrderDAC();
            List<PurchaseDetailVO> result = dac.GetMetarialList(no);
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

        public List<PurchaseDetailVO> GetInputWarehouse(string no)
        {
            WorkOrderDAC dac = new WorkOrderDAC();
            List<PurchaseDetailVO> result = dac.GetInputWarehouse(no);
            dac.Dispose();

            return result;
        }

        public WorkOrderVO GetLastID()
        {
            WorkOrderDAC dac = new WorkOrderDAC();
            WorkOrderVO result = dac.GetLastID();
            dac.Dispose();

            return result;
        }


        public bool RegisterWorkOrderList(List<WorkOrderVO> workList, List<string> workIDList)
        {
            WorkOrderDAC dac = new WorkOrderDAC();
            bool result = dac.RegisterWorkOrderList(workList, workIDList);
            dac.Dispose();

            return result;
        }
    }
}
