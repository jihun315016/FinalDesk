using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DESK_DTO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DESK_MES
{
    public class UserGroupDAC : IDisposable
    {
        SqlConnection conn;

        public UserGroupDAC()
        {
            string connStr = ConfigurationManager.ConnectionStrings["prjDB"].ConnectionString;
            conn = new SqlConnection(connStr);
            conn.Open();
        }

        public void Dispose()
        {
            if (conn != null && conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }

        public List<UserGroupVO> SelectUserGroupList()
        {
            string sql = @"select ug.User_Group_No,[User_Group_Name], User_Group_Type,[Auth_Name] as User_Group_TypeName, CONVERT(nvarchar(30),
ug.Create_Time,20) Create_Time, ug.Create_User_No,up.[User_Name] as Create_User_Name , CONVERT(nvarchar(30),ug.Update_Time,20) as Update_Time, ug.Update_User_No, u.[User_Name] as Update_User_Name
from TB_User_Group ug left join [dbo].[TB_USER] u on ug.Update_User_No = u.User_No
					left join [dbo].[TB_USER] up on ug.Create_User_No = up.User_No
					left join [dbo].[TB_AUTHORTY] a on ug.User_Group_Type = a.Auth_ID";
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    List<UserGroupVO> list = DBHelpler.DataReaderMapToList<UserGroupVO>(cmd.ExecuteReader());

                    return list;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
    }
}
