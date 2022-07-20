using DESK_DTO;
using DESK_MES.DAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESK_MES.Service
{
    class ProductService
    {
        public List<CodeCountVO> GetProductType()
        {
            ProductDAC dac = new ProductDAC();
            List<CodeCountVO> list = dac.GetProductType();
            dac.Dispose();
            return list;
        }
    }
}
