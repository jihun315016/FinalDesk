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
        public bool SaveOperation(OperationVO oper)
        {
            OperationDAC dac = new OperationDAC();
            bool result = dac.SaveProcess(oper);
            dac.Dispose();
            return result;
        }
    }
}
