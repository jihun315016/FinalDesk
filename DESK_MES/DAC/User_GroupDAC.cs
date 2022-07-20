using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DESK_DTO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DESK_MES.DAC
{
    public class User_GroupDAC
    {
        string StrConn;

        public User_GroupDAC()
        {
            StrConn = ConfigurationManager.ConnectionStrings["prjDB"].ConnectionString;
        }

        public List<User_GroupVO> GetUser_GroupList()
        {
            using (SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection(StrConn),
                CommandText = @"select User_Group_No, User_Group_Type, Create_Time, Create_User_No, Update_Time, Update_User_No
from [dbo].[TB_USER_GROUP]",
            })
            {
                cmd.Connection.Open();
                //List<User_GroupVO> list = //Helper.cmd.ExecuteReader();
                //    cmd.Connection.Close();
                return null;// list;
            }

        }
    }
}
