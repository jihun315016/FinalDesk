using DESK_DTO;
using DESK_MES.DAC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESK_MES.Service
{
    public class InspectService
    {
        public List<InspectItemVO> GetInspectItemList(int no = 0)
        {
            InspectDAC dac = new InspectDAC();
            List<InspectItemVO> list = dac.GetInspectItemList(no);
            dac.Dispose();
            return list;
        }

        public bool SaveInspectItem(InspectItemVO item)
        {
            InspectDAC dac = new InspectDAC();
            bool result = dac.SaveInspectItem(item);
            dac.Dispose();
            return result;
        }
    }
}
