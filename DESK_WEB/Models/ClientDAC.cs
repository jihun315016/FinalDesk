using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using DESK_DTO;

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
                                           CONVERT(VARCHAR(20), Create_Time, 20) Create_Time,
                                           Create_User_No,
                                           CONVERT(VARCHAR(20), Update_Time, 20) Update_Time,
                                           Update_User_No 
                                    from TB_Client";

                cmd.Connection.Open();
                List<ClientVO> list = Helper.DataReaderMapToList<ClientVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }
    }
}