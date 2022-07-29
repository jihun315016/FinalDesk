using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DESK_DTO;

namespace DESK_MES
{
    public class ClientDAC : IDisposable
    {
        SqlConnection conn;

        public ClientDAC()
        {
            string connstr = ConfigurationManager.ConnectionStrings["prjDB"].ConnectionString;
            conn = new SqlConnection(connstr);
            conn.Open();
        }
        public void Dispose()
        {
            if (conn != null && conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        public List<ClientVO> GetClientList() // 거래처 정보 가져오기
        {
            try
            {
                List<ClientVO> list = new List<ClientVO>();

                string sql = @"select Client_Code, 
                                           Client_Name, 
                                           Client_Type,
                                           Client_Number,
                                           Client_Phone,
                                           CONVERT(VARCHAR(20), C.Create_Time, 20) Create_Time,
                                           C.Create_User_No,
										   U.User_Name AS Create_User_Name,
                                           CONVERT(VARCHAR(20), C.Update_Time, 20) Update_Time,
                                           C.Update_User_No, 
										   UU.User_Name AS Update_User_Name,
                                           C.Is_Delete
                                    from TB_Client C
									LEFT JOIN [dbo].[TB_USER] U ON C.Create_User_No=U.User_No
									LEFT JOIN [dbo].[TB_USER] UU ON C.Update_User_No=UU.User_No
                                    where C.Is_Delete='N'";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    list = DBHelpler.DataReaderMapToList<ClientVO>(cmd.ExecuteReader());
                }
                return list;
            }
            catch (Exception err)
            {
                return null;
            }
        }
    }
}
