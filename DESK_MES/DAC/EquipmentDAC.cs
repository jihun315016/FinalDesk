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
    public class EquipmentDAC : IDisposable
    {
        SqlConnection conn;
        public EquipmentDAC()
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
        /// 김준모/설비 전체 조회
        /// </summary>
        /// <returns></returns>
        public List<EquipmentVO> SelectEquipmentAllList()
        {
            string sql = @"USP_EquipmentSelectAll";
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    List<EquipmentVO> list = DBHelpler.DataReaderMapToList<EquipmentVO>(cmd.ExecuteReader());

                    return list;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        public EquipmentVO SelectEquipmentNoList(int eqNo)
        {
            string sql = @"USP_EquipmentSelectNo";
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Equipment_No", eqNo);
                    List<EquipmentVO> list = DBHelpler.DataReaderMapToList<EquipmentVO>(cmd.ExecuteReader());

                    return list[0];
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        public List<EquipmentVO> SelectOperationTypeList()
        {
            string sql = @"select Code, Catagory, Name from TB_COMMON_CODE";
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    List<EquipmentVO> list = DBHelpler.DataReaderMapToList<EquipmentVO>(cmd.ExecuteReader());

                    return list;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Author : 강지훈
        /// 주어진 공정에 등록된 설비 리스트 조회
        /// </summary>
        /// <param name="operNo"></param>
        /// <returns></returns>
        public List<EquipmentVO> SelectEquipmentByOperation(int operNo)
        {
            string sql = @"SELECT e.Equipment_No, Equipment_Name, Is_Inoperative, 
                                e.Create_Time, u.User_Name Create_User_Name,
                                e.Update_Time, uu.User_Name Update_User_Name 
                            FROM TB_EQUIPMENT e
                            JOIN (SELECT Equipment_No, Operation_No FROM TB_EQUIPMENT_OPERATION_RELATION WHERE Operation_No = @Operation_No) eo 
                            ON e.Equipment_No = eo.Equipment_No
                            LEFT JOIN TB_USER u ON e.Create_User_No = u.User_No
                            LEFT JOIN TB_USER uu ON e.Update_User_No = uu.User_No
                            WHERE e.Is_Delete = 'N' ";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Operation_No", operNo);
            SqlDataReader reader = cmd.ExecuteReader();
            List<EquipmentVO> list = new List<EquipmentVO>();
            while (reader.Read())
            {
                list.Add(new EquipmentVO()
                {
                    Equipment_No = Convert.ToInt32(reader["Equipment_No"]),
                    Equipment_Name = reader["Equipment_Name"].ToString(),
                    Is_Inoperative = reader["Is_Inoperative"].ToString(),
                    Create_Time = Convert.ToDateTime(reader["Create_Time"]).ToShortDateString(),
                    Create_User_Name = reader["Create_User_Name"].ToString(),
                    Update_Time = reader["Update_Time"] == DBNull.Value ? null : Convert.ToDateTime(reader["Update_Time"]).ToShortDateString(),
                    Update_User_Name = reader["Update_User_Name"] == DBNull.Value ? null : reader["Update_User_Name"].ToString()
                });
            }

            return list;
        }

        /// <summary>
        /// 김준모/설비 등록
        /// </summary>
        /// <returns></returns>
        public bool InsertEquipment(EquipmentVO equi)
        {
            string sql = @"insert [dbo].[TB_EQUIPMENT]
                            (Equipment_Name, Create_User_No,Output_Qty)
                            values
                            (@Equipment_Name,@Create_User_No,@Output_Qty)";
            int iRowAffect;
            using (SqlTransaction tran = conn.BeginTransaction())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Transaction = tran;
                        cmd.Parameters.AddWithValue("@Equipment_Name", equi.Equipment_Name);
                        cmd.Parameters.AddWithValue("@Create_User_No", equi.Create_User_No);
                        cmd.Parameters.AddWithValue("@Output_Qty", equi.Output_Qty);
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
        /// 김준모/설비 변경
        /// </summary>
        /// <returns></returns>
        public bool UpdateEquipment(EquipmentVO equi)
        {
            string sql = @"USP_EquipmentUpdate";
            int iRowAffect;
            using (SqlTransaction tran = conn.BeginTransaction())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Transaction = tran;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Equipment_No", equi.Equipment_No);
                        cmd.Parameters.AddWithValue("@Equipment_Name", equi.Equipment_Name);
                        cmd.Parameters.AddWithValue("@Is_Inoperative", equi.Is_Inoperative);
                        cmd.Parameters.AddWithValue("@Update_User_No", equi.Update_User_No);
                        cmd.Parameters.AddWithValue("@Inoperative_Reason", equi.Inoperative_Reason);
                        cmd.Parameters.AddWithValue("@Action_History", equi.Action_History);
                        cmd.Parameters.AddWithValue("@Output_Qty", equi.Output_Qty);
                        cmd.Parameters.AddWithValue("@Is_Inoperative_Date", Convert.ToDateTime(equi.Is_Inoperative_Date));
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
        /// 김준모/설비 삭제
        /// </summary>
        /// <returns></returns>
        public bool DeleteEquipment(int equiNo)
        {
            string sql = @"update [dbo].[TB_EQUIPMENT]
                            set
                            Is_Delete = 'Y'
                            where Equipment_No = @Equipment_No";
            int iRowAffect;
            using (SqlTransaction tran = conn.BeginTransaction())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Create_User_No", equiNo);
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