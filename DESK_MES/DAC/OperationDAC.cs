using DESK_DTO;
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

                            SELECT Operation_No, ior.Inspect_No, i.Inspect_Name, i.Target, i.USL, i.LSL
                            FROM TB_INSPECT_OPERATION_RELEATION ior
                            JOIN TB_INSPECT_ITEM i ON ior.Inspect_No = i.Inspect_No ";

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
        /// 하나의 공정에 대한 검사 데이터 항목 리스트 조회
        /// </summary>
        /// <param name="operNo"></param>
        /// <returns></returns>
        public DataTable GetInspectListByOperation(int operNo)
        {
            string sql = @"SELECT Inspect_No 
                            FROM TB_INSPECT_OPERATION_RELEATION
                            WHERE Operation_No = @Operation_No ";

            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.SelectCommand.Parameters.AddWithValue("@Operation_No", operNo);
            da.Fill(dt);
            return dt;
        }

        /// <summary>
        /// Author : 강지훈
        /// 하나의 공정에 대한 설비 리스트 조죄
        /// </summary>
        /// <param name="operNo"></param>
        /// <returns></returns>
        public DataTable GetEquipmentListByOperation(int operNo)
        {
            string sql = @"SELECT Equipment_No 
                            FROM TB_EQUIPMENT_OPERATION_RELATION
                            WHERE Operation_No = @Operation_No ";

            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.SelectCommand.Parameters.AddWithValue("@Operation_No", operNo);
            da.Fill(dt);
            return dt;
        }

        /// <summary>
        /// Author : 강지훈
        /// 공정에 대한 검사 데이터 항목 관계 설정
        /// </summary>
        /// <param name="operNo"></param>
        /// <param name="inspectList"></param>
        /// <returns></returns>
        public bool SaveOIRelation(int operNo, List<InspectItemVO> inspectList)
        {
            SqlTransaction tran = conn.BeginTransaction();
            SqlCommand cmd = new SqlCommand();
            cmd.Transaction = tran;

            try
            {
                cmd.Connection = conn;
                // 이미 등록된 항목 삭제
                cmd.CommandText = @"DELETE FROM TB_INSPECT_OPERATION_RELEATION
                                    WHERE Operation_No = @Operation_No ";

                cmd.Parameters.AddWithValue("@Operation_No", operNo);
                cmd.ExecuteNonQuery();

                // 검사 데이터 항목 추가
                cmd.CommandText = @"INSERT INTO TB_INSPECT_OPERATION_RELEATION
                                    (Inspect_No, Operation_No)
                                    VALUES
                                    (@Inspect_No, @Operation_No) ";
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
        /// 공정에 대한 품목 관계 설정
        /// </summary>
        /// <param name="operNo"></param>
        /// <param name="productList"></param>
        /// <returns></returns>
        public bool SaveOPRelation(int operNo, List<ProductVO> productList)
        {
            SqlTransaction tran = conn.BeginTransaction();
            SqlCommand cmd = new SqlCommand();
            cmd.Transaction = tran;

            try
            {
                cmd.Connection = conn;
                cmd.CommandText = @"DELETE FROM TB_PRODUCT_OPERATION_RELATION 
                                    WHERE Operation_No=@Operation_No ";

                cmd.Parameters.AddWithValue("@Operation_No", operNo);
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"INSERT INTO TB_PRODUCT_OPERATION_RELATION
                                    (Product_Code, Operation_No)
                                    VALUES
                                    (@Product_Code, @Operation_No) ";

                cmd.Parameters.Add("@Product_Code", SqlDbType.NVarChar);

                foreach (ProductVO item in productList)
                {
                    cmd.Parameters["@Product_Code"].Value = item.Product_Code;
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
        /// 공정에 대한 설비 관계 설정
        /// </summary>
        /// <param name="operNo"></param>
        /// <param name="equipmentList"></param>
        /// <returns></returns>
        public bool SaveOERelation(int operNo, List<EquipmentVO> equipmentList)
        {
            SqlTransaction tran = conn.BeginTransaction();
            SqlCommand cmd = new SqlCommand();
            cmd.Transaction = tran;

            try
            {
                cmd.Connection = conn;
                // 이미 등록된 항목 삭제
                cmd.CommandText = @"DELETE FROM TB_EQUIPMENT_OPERATION_RELATION
                                    WHERE Operation_No = @Operation_No ";

                cmd.Parameters.AddWithValue("@Operation_No", operNo);
                cmd.ExecuteNonQuery();

                // 설비 추가
                cmd.CommandText = @"INSERT INTO TB_EQUIPMENT_OPERATION_RELATION
                                    (Equipment_No, Operation_No)
                                    VALUES
                                    (@Equipment_No, @Operation_No) ";
                cmd.Parameters.Add("@Equipment_No", SqlDbType.Int);

                foreach (EquipmentVO item in equipmentList)
                {
                    cmd.Parameters["@Equipment_No"].Value = item.Equipment_No;
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
        /// 공정과 관련된 검사 데이터 항목 삭제
        /// </summary>
        /// <param name="operNo"></param>
        /// <returns></returns>
        public bool DeleteOIIetm(int operNo)
        {
            string sql = @"DELETE FROM TB_INSPECT_OPERATION_RELEATION
                            WHERE Operation_No = @Operation_No ";

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

        /// <summary>
        /// Author : 강지훈
        /// 공정과 관련된 설비 삭제
        /// </summary>
        /// <param name="operNo"></param>
        /// <returns></returns>
        public bool DeleteEOItem(int operNo)
        {
            string sql = @"DELETE FROM TB_EQUIPMENT_OPERATION_RELATION
                            WHERE Operation_No = @Operation_No ";

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
