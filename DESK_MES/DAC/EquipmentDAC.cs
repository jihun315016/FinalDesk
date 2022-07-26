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
            string sql = @"insert [dbo].[TB_EQUIPMENT]
                            (Equipment_Name, Create_User_No)
                            values
                            (@Equipment_Name,@Create_User_No)";
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Equipment_Name", equi.Equipment_Name);
                    cmd.Parameters.AddWithValue("@Create_User_No", equi.Create_User_No);
                    int iRowAffect = cmd.ExecuteNonQuery();
                    return iRowAffect > 0;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        /// <summary>
        /// 김준모/설비 변경
        /// </summary>
        /// <returns></returns>
        public bool UpdateEquipment(EquipmentVO equi)
        {
            string sql = @"update [dbo].[TB_EQUIPMENT]
                            set
                            Equipment_Name=@Equipment_Name, Is_Inoperative=@Is_Inoperative,Update_Time=(CONVERT([char](19),getdate(),(20))),
                            Update_User_No=@Update_User_No, Is_Delete =@Is_Delete
                            where Equipment_No = @Equipment_No";
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Equipment_Name", equi.Equipment_Name);
                    cmd.Parameters.AddWithValue("@Is_Inoperative", equi.Is_Inoperative);
                    cmd.Parameters.AddWithValue("@Update_User_No", equi.Update_User_No);
                    cmd.Parameters.AddWithValue("@Is_Delete", equi.Is_Delete);
                    cmd.Parameters.AddWithValue("@Equipment_No", equi.Equipment_No);
                    int iRowAffect = cmd.ExecuteNonQuery();
                    return iRowAffect > 0;
                }
            }
            catch (Exception err)
            {
                throw err;
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
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Create_User_No", equiNo);
                    int iRowAffect = cmd.ExecuteNonQuery();
                    return iRowAffect > 0;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
    }
}