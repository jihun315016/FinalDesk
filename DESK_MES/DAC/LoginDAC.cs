using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DESK_DTO;

namespace DESK_MES
{
    public class LoginDAC : IDisposable
    {
        SqlConnection conn;

        public LoginDAC()
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

        public UserVO GetUserInfo(int userNo, string pwd)
        {
            string sql = @"SELECT [User_No], [User_Name], U.[User_Group_No], [User_Group_Name]
                           FROM [dbo].[TB_USER] U
                           JOIN [dbo].[TB_USER_GROUP] UG ON U.[User_Group_No] = UG.User_Group_No
                           WHERE [User_No] = @User_No
                           AND [User_Pwd] = @User_Pwd";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@User_No", userNo);
            cmd.Parameters.AddWithValue("@User_Pwd", pwd);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                UserVO user = new UserVO()
                {
                    User_No = Convert.ToInt32(reader["User_No"]),
                    User_Name = reader["User_Name"].ToString(),
                    User_Group_No = Convert.ToInt32(reader["User_Group_No"]),
                    User_Group_Name = reader["User_Group_Name"].ToString()
                };

                return user;
            }
            return null;
        }
    }
}
