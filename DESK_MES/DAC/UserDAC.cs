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
    public class UserDAC : IDisposable
    {
        SqlConnection conn;
        public UserDAC()
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
        public List<UserVO> SelectUserList()
        {
            string sql = @"with userr as 
(
select u.User_No, u.User_Name, u.User_Pwd, u.User_Group_No, CONVERT(nvarchar(30),
u.Create_Time,20) AS Create_Time, u.Create_User_No, u.User_Name as Create_User_Name, CONVERT(nvarchar(30),u.Update_Time,20) as Update_Time, u.Update_User_No,u.Is_Delete
from [dbo].[TB_USER] u
--where u.Is_Delete ='N'
)
select u.User_No, u.User_Name, u.User_Pwd, u.User_Group_No, [User_Group_Name],[Auth_Name], u.Create_Time, u.Create_User_No, us.User_Name as Create_User_Name, u.Update_Time, u.Update_User_No,ur.User_Name as Update_User_Name,u.Is_Delete
from userr u left join [dbo].[TB_USER] us on u.Create_User_No = us.User_No
			left join [dbo].[TB_USER] ur on u.Update_User_No = ur.User_No
			left join [dbo].[TB_USER_GROUP] ug on u.User_Group_No = ug.User_Group_No
			left join [dbo].[TB_AUTHORTY] a on ug.User_Group_Type = a.Auth_ID";
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                   List<UserVO> list = DBHelpler.DataReaderMapToList<UserVO>(cmd.ExecuteReader());

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
