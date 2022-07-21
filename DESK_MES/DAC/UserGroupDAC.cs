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

        /// <summary>
        /// 김준모/UserGroup 전체 List 가져오기
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// 김준모/권한 리스트불러오기(콤보박스용도)
        /// </summary>
        /// <returns></returns>
        public List<UserGroupVO> SelectAuthList()
        {
            string sql = @"select Auth_ID, Auth_Name, Auth_Desc,
                            (select top 1 [User_Group_No] 
                            from [dbo].[TB_USER_GROUP] 
                            group by [User_Group_No]
                            order by [User_Group_No] desc) as LastUser_No
                            from [dbo].[TB_AUTHORTY]";
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

        /// <summary>
        /// 김준모/유저 그룹 등록
        /// </summary>
        /// <param name="UserG"></param>
        /// <returns></returns>
        public bool InsertUserGroup(UserGroupVO UserG)
        {
            int iRowAffect;
            string sql = @"insert [dbo].[TB_USER_GROUP]
(User_Group_Name,User_Group_Type, Create_User_No)
values
(@User_Group_Name,@User_Group_Type, @Create_User_No)";

            using (SqlTransaction tran = conn.BeginTransaction())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Transaction = tran;
                        cmd.Parameters.AddWithValue("@User_Group_Name", UserG.User_Group_Name);
                        cmd.Parameters.AddWithValue("@User_Group_Type", UserG.User_Group_Type);
                        cmd.Parameters.AddWithValue("@Create_User_No", UserG.Create_User_No);
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
        /// 김준모/유저그룹 번호로 유저그룹 셀렉
        /// </summary>
        /// <param name="User_Group_No"></param>
        /// <returns></returns>
        public UserGroupVO SelectUserGroupCell(int User_Group_No)
        {
            string sql = @"select ug.User_Group_No,[User_Group_Name], User_Group_Type,[Auth_Name] as User_Group_TypeName, CONVERT(nvarchar(30),
ug.Create_Time,20) Create_Time, ug.Create_User_No,up.[User_Name] as Create_User_Name , CONVERT(nvarchar(30),ug.Update_Time,20) as Update_Time, ug.Update_User_No, u.[User_Name] as Update_User_Name
from TB_User_Group ug left join [dbo].[TB_USER] u on ug.Update_User_No = u.User_No
					left join [dbo].[TB_USER] up on ug.Create_User_No = up.User_No
					left join [dbo].[TB_AUTHORTY] a on ug.User_Group_Type = a.Auth_ID
where ug.User_Group_No = @User_Group_No";
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@User_Group_No", User_Group_No);
                    List<UserGroupVO> list = DBHelpler.DataReaderMapToList<UserGroupVO>(cmd.ExecuteReader());

                    return list[0];
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// 김준모/유저그룹 업데이트(수정)
        /// </summary>
        /// <param name="UserG"></param>
        /// <returns></returns>
        public bool UpdateUserGroup(UserGroupVO UserG)
        {
            int iRowAffect;
            string sql = @"update [dbo].[TB_USER_GROUP]
set
User_Group_Name=@User_Group_Name, User_Group_Type=@User_Group_Type, Update_Time =(CONVERT(char,getdate(),(20))), Update_User_No =@Update_User_No
where User_Group_No =@User_Group_No";

            using (SqlTransaction tran = conn.BeginTransaction())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Transaction = tran;
                        cmd.Parameters.AddWithValue("@User_Group_Name", UserG.User_Group_Name);
                        cmd.Parameters.AddWithValue("@User_Group_Type", UserG.User_Group_Type);
                        cmd.Parameters.AddWithValue("@Update_User_No", UserG.Update_User_No);
                        cmd.Parameters.AddWithValue("@User_Group_No", UserG.User_Group_No);
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
        public bool DeleteUserGroup(int User_Group_No)
        {
            int iRowAffect;
            string sql = @"delete from[dbo].[TB_USER_GROUP]
where User_Group_No =@User_Group_No";

            using (SqlTransaction tran = conn.BeginTransaction())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Transaction = tran;
                        
                        cmd.Parameters.AddWithValue("@User_Group_No", User_Group_No);
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
