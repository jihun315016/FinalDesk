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

        /// <summary>
        /// 전체 거래처 정보 가져오기
        /// </summary>
        /// <returns></returns>
        public List<ClientVO> GetClientList()
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

        public ClientVO GetClientInfoByCode(string code)
        {
            try
            {
                string sql = @"select Client_Code, 
                                      Client_Name, 
                                      Client_Type,
                                      Client_Number,
                                      Client_Phone
                               from TB_Client
                               where Client_Code=@Client_Code";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Client_Code", code);

                    List<ClientVO> list = DBHelpler.DataReaderMapToList<ClientVO>(cmd.ExecuteReader());

                    if (list != null && list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
            catch (Exception err)
            {
                return null;
            }
        }


        /// <summary>
        /// 신규 등록할 거래처ID 가져오기
        /// </summary>
        /// <returns></returns>
        public ClientVO GetLastID() 
        {
            try
            {
                string sql = @"select TOP 1 Client_Code 
                               from [dbo].[TB_Client] 
                               order by Client_Code desc";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    List<ClientVO> list = DBHelpler.DataReaderMapToList<ClientVO>(cmd.ExecuteReader());

                    if (list != null && list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
            catch (Exception err)
            {
                return null;
            }
        }

        /// <summary>
        /// 거래처 신규 등록
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public bool RegisterClient(ClientVO client)
        {
            try
            {
                //  ITEM_QUANTITY , ITEM_SAFETY_STOCK
                string sql = @"INSERT INTO TB_Client (Client_Code, Client_Name, Client_Type, Client_Number, Client_Phone, Create_Time, Create_User_No)
                                    VALUES (@Client_Code, @Client_Name, @Client_Type, @Client_Number, @Client_Phone, @Create_Time, @Create_User_No)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Client_Code", client.Client_Code);
                    cmd.Parameters.AddWithValue("@Client_Name", client.Client_Name);
                    cmd.Parameters.AddWithValue("@Client_Type", client.Client_Type);
                    cmd.Parameters.AddWithValue("@Client_Number", client.Client_Number);
                    cmd.Parameters.AddWithValue("@Client_Phone", client.Client_Phone);
                    cmd.Parameters.AddWithValue("@Create_Time", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Create_User_No", client.Create_User_No);
 

                    int iRowAffect = cmd.ExecuteNonQuery();
                    return (iRowAffect > 0);
                }

                return true;

            }
            catch (Exception err)
            {
                return false;
            }
        }

        /// <summary>
        /// 거래처 정보 수정
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public bool UpdateClientInfo(ClientVO client)
        {
            string sql = @"UPDATE TB_Client 
                           SET Client_Name = @Client_Name,
                               Client_Type = @Client_Type,
                               Client_Phone = @Client_Phone,
                               Update_Time = @Update_Time,
                               Update_User_No = @Update_User_No
                           WHERE Client_Code= @Client_Code";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Client_Name", client.Client_Name);
                    cmd.Parameters.AddWithValue("@Client_Type", client.Client_Type);
                    cmd.Parameters.AddWithValue("@Client_Phone", client.Client_Phone);
                    cmd.Parameters.AddWithValue("@Update_Time", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Update_User_No", client.Update_User_No);
                    cmd.Parameters.AddWithValue("@Client_Code", client.Client_Code);

                    int iRowAffect = cmd.ExecuteNonQuery();
                    if (iRowAffect < 1)
                        throw new Exception("해당 거래처 정보가 없습니다.");
                }
                return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }

        /// <summary>
        /// 거래처 정보 삭제( N => Y 변경)
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public bool DeleteClientInfo(string client)
        {
            string sql = @"UPDATE TB_Client 
                           SET Is_Delete = 'Y'
                           WHERE Client_Code= @Client_Code";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Client_Code", client);

                    int iRowAffect = cmd.ExecuteNonQuery();
                    if (iRowAffect < 1)
                        throw new Exception("해당 거래처 정보가 없습니다.");
                }
                return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }
    }
}
