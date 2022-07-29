using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DESK_DTO;
using System.Data.SqlClient;

namespace DESK_MES
{
    public class ClientService
    {
        public List<ClientVO> GetClientList()  // 거래처 정보 가져오기
        {
            ClientDAC dac = new ClientDAC();
            List<ClientVO> result = dac.GetClientList();
            dac.Dispose();

            return result;
        }
    }
}
