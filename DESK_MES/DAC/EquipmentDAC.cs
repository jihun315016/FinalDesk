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
        /// 김준모/설비 등록
        /// </summary>
        /// <returns></returns>
        public bool InsertEquipment(EquipmentVO equi)
        {
            string sql = @"USP_EquipmentInsert";
            int iRowAffect;
            using (SqlTransaction tran = conn.BeginTransaction())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Transaction = tran;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Equipment_Name", equi.Equipment_Name);
                        cmd.Parameters.AddWithValue("@Create_User_No", equi.Create_User_No);
                        cmd.Parameters.AddWithValue("@Operation_Type_No", equi.Operation_Type_No);
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