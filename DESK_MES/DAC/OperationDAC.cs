﻿using DESK_DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESK_MES.DAC
{
    public class OperationDAC : IDisposable
    {
        SqlConnection conn;

        public OperationDAC()
        {
            string connStr = ConfigurationManager.ConnectionStrings["prjDB"].ConnectionString;
            conn = new SqlConnection(connStr);
            conn.Open();
        }

        public void Dispose()
        {
            if (conn != null && conn.State == System.Data.ConnectionState.Open)
            {
                conn.Dispose();
            }
        }

        /// <summary>
        /// Author : 강지훈
        /// 공정 리스트 조회
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        public List<OperationVO> GetOperationList(int no)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"SELECT 
	                        Operation_No, Operation_Name, Is_Check_Deffect, Is_Check_Inspect, Is_Check_Marerial, 
	                        o.Create_Time, o.Create_User_No, u.User_Name Create_User_Name, o.Update_Time, o.Update_User_No, uu.User_Name Update_User_Name 
                        FROM TB_OPERATION o
                        LEFT JOIN TB_USER u ON o.Create_User_No = u.User_No
                        LEFT JOIN TB_USER uu ON o.Update_User_No = uu.User_No ");

            if (no > 0)
                sb.Append(" WHERE Operation_No = @no ");

            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.AddWithValue("@no", no);
            SqlDataReader reader = cmd.ExecuteReader();
            List<OperationVO> list = DBHelpler.DataReaderMapToList<OperationVO>(reader);
            reader.Close();
            return list;
        }

        public DataSet GetOIRelation()
        {
            string sql = @"SELECT 
	                            Operation_No, Operation_Name, Is_Check_Deffect, Is_Check_Inspect, Is_Check_Marerial, 
	                            o.Create_Time, o.Create_User_No, u.User_Name Create_User_Name, o.Update_Time, o.Update_User_No, uu.User_Name Update_User_Name 
                            FROM TB_OPERATION o
                            LEFT JOIN TB_USER u ON o.Create_User_No = u.User_No
                            LEFT JOIN TB_USER uu ON o.Update_User_No = uu.User_No
                            WHERE Is_Check_Inspect = 'Y' ;

                            SELECT 
                                Inspect_No, Inspect_Name, Target, USL, LSL, 
                                i.Create_Time, i.Create_User_No, u.User_Name Create_User_Name, i.Update_Time, i.Update_User_No, uu.User_Name Update_User_Name 
                            FROM TB_INSPECT_ITEM i
                            LEFT JOIN TB_USER u ON i.Create_User_No = u.User_No
                            LEFT JOIN TB_USER uu ON i.Update_User_No = uu.User_No ";

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            da.Fill(ds);
            return ds;
        }

        /// <summary>
        /// Author : 강지훈
        /// 공정 등록 기능
        /// </summary>
        /// <param name="oper"></param>
        /// <returns></returns>
        public bool SaveOperation(OperationVO oper)
        {
            string sql = @"INSERT INTO TB_OPERATION
                            (Operation_Name, Is_Check_Deffect, Is_Check_Inspect, Is_Check_Marerial, Create_Time, Create_User_No)
                            VALUES
                            (@Operation_Name, @Is_Check_Deffect, @Is_Check_Inspect, @Is_Check_Marerial, CONVERT([char](19),getdate(),(20)), @Create_User_No) ";

            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.Parameters.AddWithValue("@Operation_Name", oper.Operation_Name);
                cmd.Parameters.AddWithValue("@Is_Check_Deffect", oper.Is_Check_Deffect);
                cmd.Parameters.AddWithValue("@Is_Check_Inspect", oper.Is_Check_Inspect);
                cmd.Parameters.AddWithValue("@Is_Check_Marerial", oper.Is_Check_Marerial);
                cmd.Parameters.AddWithValue("@Create_User_No", oper.Create_User_No);

                int iRow = cmd.ExecuteNonQuery();
                return iRow > 0;
            }
            catch (Exception err)
            {
                return false;
            }
        }

        /// <summary>
        /// Author : 강지훈
        /// 공정에 대한 검사 데이터 항목 관계 설정
        /// </summary>
        /// <param name="operNo"></param>
        /// <param name="userNo"></param>
        /// <param name="inspectList"></param>
        /// <returns></returns>
        public bool SaveOIRelation(int operNo, int userNo, List<InspectItemVO> inspectList)
        {
            string sql = @"INSERT INTO TB_INSPECT_OPERATION_RELEATION
                        (Inspect_No, Operation_No, Create_Time, Create_User_No)
                        VALUES
                        (@Inspect_No, @Operation_No, CONVERT([char](19),getdate(),(20)), @Create_User_No) ";

            SqlTransaction tran = conn.BeginTransaction();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Transaction = tran;

            try
            {
                cmd.Parameters.AddWithValue("@Operation_No", operNo);
                cmd.Parameters.AddWithValue("@Create_User_No", userNo);
                cmd.Parameters.Add("@Inspect_No", SqlDbType.Int);

                foreach (InspectItemVO item in inspectList)
                {
                    cmd.Parameters["@Inspect_No"].Value = item.Inspect_No;
                    cmd.ExecuteNonQuery();
                }

                tran.Commit();
                return true;
            }
            catch (Exception err)
            {
                tran.Rollback();
                return false;
            }
        }

        /// <summary>
        /// Author : 강지훈
        /// 공정 수정 기능
        /// </summary>
        /// <param name="oper"></param>
        /// <returns></returns>
        public bool UpdateOperation(OperationVO oper)
        {
            string sql = @"UPDATE TB_OPERATION 
                            SET 
	                            Operation_Name = @Operation_Name, Is_Check_Deffect = @Is_Check_Deffect, 
	                            Is_Check_Inspect = @Is_Check_Inspect, Is_Check_Marerial = @Is_Check_Marerial,
                                Update_Time = CONVERT(CHAR(19), GETDATE(), 20), Update_User_No = @Update_User_No
                            WHERE Operation_No = @Operation_No ";

            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.Parameters.AddWithValue("@Operation_Name", oper.Operation_Name);
                cmd.Parameters.AddWithValue("@Is_Check_Deffect", oper.Is_Check_Deffect);
                cmd.Parameters.AddWithValue("@Is_Check_Inspect", oper.Is_Check_Inspect);
                cmd.Parameters.AddWithValue("@Is_Check_Marerial", oper.Is_Check_Marerial);
                cmd.Parameters.AddWithValue("@Update_User_No", oper.Update_User_No);
                cmd.Parameters.AddWithValue("@Operation_No", oper.Operation_No);

                int iRow = cmd.ExecuteNonQuery();
                return iRow > 0;
            }
            catch (Exception err)
            {
                return false;
            }
        }

        /// <summary>
        /// 공정 삭제 기능
        /// </summary>
        /// <param name="operNo"></param>
        /// <returns></returns>
        public bool DeleteOperation(int operNo)
        {
            string sql = "DELETE FROM TB_OPERATION WHERE Operation_No = @Operation_No ";

            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.Parameters.AddWithValue("@Operation_No", operNo);
                int iRow = cmd.ExecuteNonQuery();
                return iRow > 0;
            }
            catch (Exception err)
            {
                return false;
            }
        }
    }
}
