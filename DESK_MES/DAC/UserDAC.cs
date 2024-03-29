﻿using System;
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
        /// <summary>
        /// 김준모/유저검색
        /// </summary>
        /// <returns></returns>
        public List<UserVO> SelectUserList()
        {
            string sql = @"with userr as 
                            (
                            select u.User_No, u.User_Name, u.User_Pwd, u.User_Group_No, CONVERT(nvarchar(30),
                            u.Create_Time,20) AS Create_Time, u.Create_User_No, u.User_Name as Create_User_Name, CONVERT(nvarchar(30),u.Update_Time,20) as Update_Time, u.Update_User_No,u.Is_Delete
                            from [dbo].[TB_USER] u
                            --where u.Is_Delete ='N'
                            )
                            select u.User_No, u.User_Name, u.User_Pwd, u.User_Group_No, [User_Group_Name],[Auth_Name], u.Create_Time, u.Create_User_No,
                            us.User_Name as Create_User_Name, u.Update_Time, u.Update_User_No,ur.User_Name as Update_User_Name,u.Is_Delete
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
        /// <summary>
        /// 김준모/유저 등록
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool InsertUser(UserVO user)
        {
            int iRowAffect;
            string sql = @"USP_InsertUser";

            using (SqlTransaction tran = conn.BeginTransaction())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Transaction = tran;
                        cmd.Parameters.AddWithValue("@User_Name", user.User_Name);
                        cmd.Parameters.AddWithValue("@User_Pwd", user.User_Pwd);
                        cmd.Parameters.AddWithValue("@Create_User_No", user.Create_User_No);
                        cmd.Parameters.AddWithValue("@User_Group_No", user.User_Group_No);
                        iRowAffect = cmd.ExecuteNonQuery();
                    }
                    tran.Commit();
                    return iRowAffect > 0;
                }
                catch (Exception err)
                {
                    tran.Rollback();
                    throw err;
                }
            }
        }
        /// <summary>
        /// 김준모/유저 변경
        /// </summary>
        /// <param name="UserG"></param>
        /// <returns></returns>
        public bool UpdateUser(UserVO UserG)
        {
            int iRowAffect;
            string sql = @"update [dbo].[TB_USER]
                            set
                            User_Name=@User_Name, User_Pwd=@User_Pwd, User_Group_No=@User_Group_No, Update_Time=(CONVERT(char,getdate(),(20))),
                            Update_User_No =@Update_User_No,Is_Delete =@Is_Delete
                            where User_No =@User_No";

            using (SqlTransaction tran = conn.BeginTransaction())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Transaction = tran;
                        cmd.Parameters.AddWithValue("@User_Name", UserG.User_Name);
                        cmd.Parameters.AddWithValue("@User_Pwd", UserG.User_Pwd);
                        cmd.Parameters.AddWithValue("@User_Group_No", UserG.User_Group_No);
                        cmd.Parameters.AddWithValue("@Update_User_No", UserG.Update_User_No);
                        cmd.Parameters.AddWithValue("@Is_Delete", UserG.Is_Delete);
                        cmd.Parameters.AddWithValue("@User_No", UserG.User_No);

                        iRowAffect = cmd.ExecuteNonQuery();
                    }
                    tran.Commit();
                    return iRowAffect > 0;
                }
                catch (Exception err)
                {
                    tran.Rollback();
                    throw err;
                }
            }
        }
        /// <summary>
        /// 김준모/유저 삭제
        /// </summary>
        /// <param name="UserG"></param>
        /// <returns></returns>
        public bool DeleteUser(UserVO UserG)
        {
            int iRowAffect;
            string sql = @"update [dbo].[TB_USER]
                            set
                            Is_Delete =@Is_Delete
                            where User_No =@User_No";

            using (SqlTransaction tran = conn.BeginTransaction())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Transaction = tran;
                        cmd.Parameters.AddWithValue("@Is_Delete", UserG.Is_Delete);
                        cmd.Parameters.AddWithValue("@User_No", UserG.User_No);

                        iRowAffect = cmd.ExecuteNonQuery();
                    }
                    tran.Commit();
                    return iRowAffect > 0;
                }
                catch (Exception err)
                {
                    tran.Rollback();
                    throw err;
                }
            }
        }
    }
}
