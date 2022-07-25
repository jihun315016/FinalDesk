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
        public List<OperationVO> GetOperationList()
        {
            OperationDAC dac = new OperationDAC();
            List<OperationVO> list = dac.GetOperationList();
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
    }
}
