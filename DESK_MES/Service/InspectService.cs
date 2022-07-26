using DESK_DTO;
using DESK_MES.DAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESK_MES.Service
{
    public class InspectService
    {
        public bool SaveInspectItem(InspectItemVO item)
        {
            InspectDAC dac = new InspectDAC();
            bool result = dac.SaveInspectItem(item);
            dac.Dispose();
            return result;
        }
    }
}
