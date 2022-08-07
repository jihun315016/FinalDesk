using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace DESK_WEB.Models
{
    public class PopDAC
    {
        string strConn;

        public PopDAC()
        {
            strConn = WebConfigurationManager.ConnectionStrings["prjDB"].ConnectionString;
        }
        /// <summary>
        /// 유저 아이디로 로그인 진행 및 유저 내역 가져오기
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PopVO GetUserLogin(int id)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = new SqlConnection(strConn);
                    cmd.CommandText = @"USP_PopLogin";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(@"@_UsID", id);

                    cmd.Connection.Open();
                    List<PopVO> list = Helper.DataReaderMapToList<PopVO>(cmd.ExecuteReader());
                    cmd.Connection.Close();

                    return list[0];
                }
            }
            catch (Exception err)
            {
                return null;
            }
        }
        /// <summary>
        /// 유저 그룹 넘버로 작업지시 가져오기
        /// </summary>
        /// <param name="gCode"></param>
        /// <returns></returns>
        public List< PopVO> GetWorkList(int gCode)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = new SqlConnection(strConn);
                    cmd.CommandText = @"select Production_No, Work_Code,Work_Group_Code as User_Group_No,[User_Group_Name], Production_Equipment_Code,Equipment_Name, convert(nvarchar(20), Start_Due_Date,20)as Start_Due_Date, Work_State, Material_Lot_Input_State
                                        from [dbo].[TB_WORK] w inner join [dbo].[TB_USER_GROUP] ug on w.[Work_Group_Code]=ug.User_Group_No
                                                       inner join [dbo].[TB_EQUIPMENT] eq on w.Production_Equipment_Code = eq.[Equipment_No]
                                        where [Work_Group_Code] = @@Work_Group_Code
                                        order by Work_Code";
                                            cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Work_Group_Code", gCode);

                    cmd.Connection.Open();
                    List<PopVO> list = Helper.DataReaderMapToList<PopVO>(cmd.ExecuteReader());
                    cmd.Connection.Close();

                    return list;
                }
            }
            catch (Exception err)
            {
                return null;
            }
        }
    }
}