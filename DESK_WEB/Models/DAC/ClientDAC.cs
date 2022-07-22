using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace DESK_WEB.Models
{
    public class ClientDAC
    {
        string strConn;

        public ClientDAC()
        {
            strConn = WebConfigurationManager.ConnectionStrings["prjDB"].ConnectionString;
        }

        public List<ClientVO> GetAllClients()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select Client_Code, 
                                           Client_Name, 
                                           Client_Type,
                                           Client_Number,
                                           Client_Phone,
                                           CONVERT(VARCHAR(20), Create_Time, 20) Create_Time,
                                           Create_User_No,
                                           CONVERT(VARCHAR(20), Update_Time, 20) Update_Time,
                                           Update_User_No,
                                           Is_Delete
                                    from TB_Client
                                    where Is_Delete='N'";

                cmd.Connection.Open();
                List<ClientVO> list = Helper.DataReaderMapToList<ClientVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }

        public ClientVO GetClientInfo(string id)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select Client_Code, 
                                           Client_Name, 
                                           Client_Type,
                                           Client_Number,
                                           Client_Phone,
                                           CONVERT(VARCHAR(20), Create_Time, 20) Create_Time,
                                           Create_User_No,
                                           CONVERT(VARCHAR(20), Update_Time, 20) Update_Time,
                                           Update_User_No
                                    from TB_Client
                                    where Client_Code=@Client_Code";
                cmd.Parameters.AddWithValue("@Client_Code", id);

                cmd.Connection.Open();
                List<ClientVO> list = Helper.DataReaderMapToList<ClientVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                if (list != null && list.Count > 0)
                    return list[0];
                else
                    return null;
            }
        }

        /// <summary>
        /// Author : 강지훈
        /// 유형에 따른 거래처 리스트 조회
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<ClientVO> GetClientListByType(string type)
        {
            string sql = @"SELECT Client_Code, Client_Name
                            FROM TB_Client
                            WHERE Client_Type=@type ";

            SqlCommand cmd = new SqlCommand(sql, new SqlConnection(strConn));
            cmd.Parameters.AddWithValue("@type", type);
            cmd.Connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<ClientVO> list = Helper.DataReaderMapToList<ClientVO>(reader);
            reader.Close();
            cmd.Connection.Close();

            return list;
        }

        public bool SaveClient(ClientVO client)
        {
            using (SqlCommand cmd = new SqlCommand())            
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"INSERT INTO TB_Client (Client_Code, Client_Name, Client_Type, Client_Number, Client_Phone, Create_Time)
                                    VALUES (@Client_Code, @Client_Name, @Client_Type, @Client_Number, @Client_Phone, @Create_Time)";

                cmd.Parameters.AddWithValue("@Client_Code", client.Client_Code);
                cmd.Parameters.AddWithValue("@Client_Name", client.Client_Name);
                cmd.Parameters.AddWithValue("@Client_Type", client.Client_Type);
                cmd.Parameters.AddWithValue("@Client_Number", client.Client_Number);
                cmd.Parameters.AddWithValue("@Client_Phone", client.Client_Phone);
                cmd.Parameters.AddWithValue("@Create_Time", DateTime.Now);

                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }

        public bool UpdateClient(ClientVO client)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"UPDATE TB_Client SET Client_Name = @Client_Name,
                                                         Client_Type = @Client_Type,
                                                         Client_Phone = @Client_Phone
                                    WHERE Client_Code= @Client_Code";

                cmd.Parameters.AddWithValue("@Client_Code", client.Client_Code);
                cmd.Parameters.AddWithValue("@Client_Name", client.Client_Name);
                cmd.Parameters.AddWithValue("@Client_Type", client.Client_Type);
                cmd.Parameters.AddWithValue("@Client_Phone", client.Client_Phone);

                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }

        public bool DeleteClient(string clientNO)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"UPDATE TB_Client SET Is_Delete = 'Y'
                                    WHERE Client_Code= @Client_Code";

                cmd.Parameters.AddWithValue("@Client_Code", clientNO);

                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }
    }
}