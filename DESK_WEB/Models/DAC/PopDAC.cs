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
                                        where [Work_Group_Code] = @Work_Group_Code and Material_Lot_Input_State=8 and Work_State <> 3
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
                    cmd.CommandText = @"select Production_No, Work_Code, Product_Code, Production_Operation_Code, Production_Equipment_Code, Halb_Save_Warehouse_Code,
		                                        Input_Material_Code, Input_Material_Qty, Halb_Material_Qty, Work_Group_Code, Production_Save_Warehouse_Code, Work_Plan_Qty, 
		                                        Work_Complete_Qty, convert(nvarchar(20),
Work_Paln_Date,20)as Work_Paln_Date, convert(nvarchar(20),
Start_Due_Date,20)as Start_Due_Date, convert(nvarchar(20),
Start_Date,20)as Start_Date, convert(nvarchar(20),
Complete_Due_Date,20)as Complete_Due_Date, convert(nvarchar(20),
Complete_Date,20)as Complete_Date, convert(nvarchar(20),
Work_Time,20)as Work_Time, Work_State, 
		                                        Material_Lot_Input_State, convert(nvarchar(20),
Create_Time,20)as Create_Time, Create_User_No, convert(nvarchar(20),
Update_Time,20)as Update_Time, Update_User_No
                                        from [dbo].[TB_WORK]
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
    }
}