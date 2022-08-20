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
                    List<PopVO> list = DBHelper.DataReaderMapToList<PopVO>(cmd.ExecuteReader());
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
        /// 유저 그룹 넘버로 작업지시 가져오기 List로
        /// </summary>
        /// <param name="gCode"></param>
        /// <returns></returns>
        public List<PopVO> GetWorkUcList(int gCode)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = new SqlConnection(strConn);
                    cmd.CommandText = @"select Production_No, Work_Code,Work_Group_Code as User_Group_No,[User_Group_Name], Production_Equipment_Code,Equipment_Name,Production_Operation_Code,Operation_Name, convert(nvarchar(20), Start_Due_Date,20)as Start_Due_Date,
Work_State,c1.Name as Work_State_Name,Work_Order_State,c2.Name as Work_Order_State_Name, Material_Lot_Input_State,c3.Name as Material_Lot_Input_State_Name
                                        from [dbo].[TB_WORK] w inner join [dbo].[TB_USER_GROUP] ug on w.[Work_Group_Code]=ug.User_Group_No
                                                       inner join [dbo].[TB_EQUIPMENT] eq on w.Production_Equipment_Code = eq.[Equipment_No]
													   inner join TB_OPERATION o on w.Production_Operation_Code = o.Operation_No
													   left join TB_COMMON_CODE c1 on w.Work_State = c1.Code
													   left join TB_COMMON_CODE c2 on w.Work_Order_State = c2.Code
													   left join TB_COMMON_CODE c3 on w.Material_Lot_Input_State = c3.Code
                                        where Work_Group_Code = @Work_Group_Code and Material_Lot_Input_State=8 and Work_State <> 3
                                        order by Work_Code";
                    cmd.Parameters.AddWithValue("@Work_Group_Code", gCode);

                    cmd.Connection.Open();
                    List<PopVO> list = DBHelper.DataReaderMapToList<PopVO>(cmd.ExecuteReader());
                    cmd.Connection.Close();

                    return list;
                }
            }
            catch (Exception err)
            {
                return null;
            }
        }

        /// <summary>
        /// WorkCode 로부터 WockDB 정보를 popVo 로 가져오기
        /// </summary>
        /// <param name="workCode"></param>
        /// <returns></returns>
        public PopVO GetWorkDetailList(string workCode)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = new SqlConnection(strConn);
                    cmd.CommandText = @"select Production_No, Work_Code, w.Product_Code,p1.Product_Name as Product_Name, Production_Operation_Code,Operation_Name,
                                        Production_Equipment_Code,Equipment_Name, Halb_Save_Warehouse_Code,
		                                        Input_Material_Code,p2.Product_Name, Input_Material_Qty, Halb_Material_Qty, Work_Group_Code as User_Group_No, [User_Group_Name],Production_Save_Warehouse_Code, Work_Plan_Qty, 
		                                        Work_Complete_Qty, convert(nvarchar(20),Work_Paln_Date,20)as Work_Paln_Date,
                                        convert(nvarchar(20),Start_Due_Date,20)as Start_Due_Date, convert(nvarchar(20),Start_Date,20)as Start_Date, 
                                        convert(nvarchar(20),Complete_Due_Date,20)as Complete_Due_Date, convert(nvarchar(20),Complete_Date,20)as Complete_Date, 
                                        convert(nvarchar(20),Work_Time,20)as Work_Time, Work_State,c1.Name as Work_State_Name,
                                        Work_Order_State,c2.Name as Work_Order_State_Name, Material_Lot_Input_State,c3.Name as Material_Lot_Input_State_Name,
                                        convert(nvarchar(20),w.Create_Time,20)as Create_Time, w.Create_User_No, 
                                        convert(nvarchar(20),w.Update_Time,20)as Update_Time, w.Update_User_No
                                        from [dbo].[TB_WORK] w left join TB_COMMON_CODE c1 on w.Work_State = c1.Code
													   left join TB_COMMON_CODE c2 on w.Work_Order_State = c2.Code
													   left join TB_COMMON_CODE c3 on w.Material_Lot_Input_State = c3.Code
													   inner join [dbo].[TB_USER_GROUP] ug on w.[Work_Group_Code]=ug.User_Group_No
                                                       inner join [dbo].[TB_EQUIPMENT] eq on w.Production_Equipment_Code = eq.[Equipment_No]
													   inner join TB_OPERATION o on w.Production_Operation_Code = o.Operation_No
													   left join TB_PRODUCT p1 on w.Product_Code = p1.Product_Code
													   left join TB_MATERIAL_LOT m on w.Input_Material_Code = m.Lot_Code
													   inner join TB_PRODUCT p2 on m.Product_Code = p2.Product_Code
                                        where Work_Code = @Work_Code";
                    cmd.Parameters.AddWithValue("@Work_Code", workCode);
                    cmd.Connection.Open();
                    List<PopVO> list = DBHelper.DataReaderMapToList<PopVO>(cmd.ExecuteReader());
                    cmd.Connection.Close();

                    return list[0];
                }
            }
            catch (Exception err)
            {
                return null;
            }
        }
        public List<PopVO> GetWorkGdvList(int gCode)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = new SqlConnection(strConn);
                    cmd.CommandText = @"select Production_No, Work_Code,Work_Group_Code as User_Group_No,[User_Group_Name], 
Production_Equipment_Code,Equipment_Name,Production_Operation_Code,Operation_Name, 
convert(nvarchar(20), Start_Due_Date,20)as Start_Due_Date,c1.Name as Work_State_Name,convert(nvarchar(20), Start_Date,20)as Start_Date,convert(nvarchar(20), Complete_Date,20)as  Complete_Date,
convert(nvarchar(20), w.Update_Time,20)as Update_Time, w.Update_User_No,User_Name as Update_User_Name
                                        from [dbo].[TB_WORK] w inner join [dbo].[TB_USER_GROUP] ug on w.[Work_Group_Code]=ug.User_Group_No
                                                       inner join [dbo].[TB_EQUIPMENT] eq on w.Production_Equipment_Code = eq.[Equipment_No]
													   inner join TB_OPERATION o on w.Production_Operation_Code = o.Operation_No
													   left join TB_COMMON_CODE c1 on w.Work_State = c1.Code
													   left join TB_COMMON_CODE c2 on w.Work_Order_State = c2.Code
													   left join TB_COMMON_CODE c3 on w.Material_Lot_Input_State = c3.Code
													   left join TB_USER u on w.Update_User_No = u.User_No
                                        where Work_Group_Code = @Work_Group_Code and Work_State = 3
                                        order by Work_Code";
                    cmd.Parameters.AddWithValue("@Work_Group_Code", gCode);

                    cmd.Connection.Open();
                    List<PopVO> list = DBHelper.DataReaderMapToList<PopVO>(cmd.ExecuteReader());
                    cmd.Connection.Close();

                    return list;
                }
            }
            catch (Exception err)
            {
                return null;
            }
        }
        /// <summary>
        /// 머신 작동
        /// </summary>
        /// <param name="work"></param>
        /// <returns></returns>
        public PopVO GetStartEquiment(string work)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = new SqlConnection(strConn);
                    cmd.CommandText = @"USP_EquimentStart";

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Work_Code", work);
                    cmd.Parameters.Add(new SqlParameter("@Eq_YN", SqlDbType.Int)).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@Eq_MSG", SqlDbType.NVarChar, 50)).Direction = ParameterDirection.Output;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();

                    PopVO workList = new PopVO
                    {
                        Msg_YN = Convert.ToInt32(cmd.Parameters["@Eq_YN"].Value),
                        Msg = cmd.Parameters["@Eq_MSG"].Value.ToString()
                    };
                    return workList;
                }
            }
            catch (Exception err)
            {
                return null;
            }
        }
    }
}