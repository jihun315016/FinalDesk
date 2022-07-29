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

        public ClientVO GetClientInfoByCode(string code)  // 선택 거래처 정보 가져오기
        {
            ClientDAC dac = new ClientDAC();
            ClientVO result = dac.GetClientInfoByCode(code);
            dac.Dispose();

            return result;
        }

        public ClientVO GetLastID() // 마지막 거래처코드 가져오기
        {
            ClientDAC dac = new ClientDAC();
            ClientVO result = dac.GetLastID();
            dac.Dispose();

            return result;
        }

        public bool RegisterClient(ClientVO client)
        {
            ClientDAC dac = new ClientDAC();
            bool result = dac.RegisterClient(client);
            dac.Dispose();

            return result;
        }

        public bool UpdateClientInfo(ClientVO client) 
        {
            ClientDAC dac = new ClientDAC();
            bool result = dac.UpdateClientInfo(client);
            dac.Dispose();

            return result;
        }

        public bool DeleteClientInfo(string code)
        {
            ClientDAC dac = new ClientDAC();
            bool result = dac.DeleteClientInfo(code);
            dac.Dispose();

            return result;
        }
    }
}
