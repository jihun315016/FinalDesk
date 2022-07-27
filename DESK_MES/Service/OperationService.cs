using DESK_DTO;
using DESK_MES.DAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESK_MES.Service
{
    public class OperationService
    {
        public List<OperationVO> GetOperationList(int no = 0)
        {
            OperationDAC dac = new OperationDAC();
            List<OperationVO> list = dac.GetOperationList(no);
            dac.Dispose();
            return list;
        }

        public bool SaveOperation(OperationVO oper)
        {
            OperationDAC dac = new OperationDAC();
            bool result = dac.SaveOperation(oper);
            dac.Dispose();
            return result;
        }

        public bool SaveOIRelation(int operNo, int userNo, List<InspectItemVO> inspectList)
        {
            OperationDAC dac = new OperationDAC();
            bool result = dac.SaveOIRelation(operNo, userNo, inspectList);
            dac.Dispose();
            return result;
        }

        public bool UpdateOperation(OperationVO oper)
        {
            OperationDAC dac = new OperationDAC();
            bool result = dac.UpdateOperation(oper);
            dac.Dispose();
            return result;
        }

        public bool DeleteOperation(int operNo)
        {
            OperationDAC dac = new OperationDAC();
            bool result = dac.DeleteOperation(operNo);
            dac.Dispose();
            return result;
        }
    }
}
