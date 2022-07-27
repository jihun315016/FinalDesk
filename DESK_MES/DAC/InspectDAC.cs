using DESK_DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESK_MES.DAC
{
    public class InspectDAC : IDisposable
    {
        SqlConnection conn;

        public InspectDAC()
        {
            string connStr = ConfigurationManager.ConnectionStrings["prjDB"].ConnectionString;
            conn = new SqlConnection(connStr);
            conn.Open();
        }

        public void Dispose()
        {
            conn.Close();
        }

        /// <summary>
        /// Author : 강지훈
        /// 검사 데이터 항목 조회
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        public List<InspectItemVO> GetInspectItemList(int no)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"SELECT 
                         Inspect_No, Inspect_Name, Target, USL, LSL, 
                         i.Create_Time, i.Create_User_No, u.User_Name Create_User_Name, i.Update_Time, i.Update_User_No, uu.User_Name Update_User_Name 
                        FROM TB_INSPECT_ITEM i
                        LEFT JOIN TB_USER u ON i.Create_User_No = u.User_No
                        LEFT JOIN TB_USER uu ON i.Update_User_No = uu.User_No ");
            
            if (no > 0)
                sb.Append(" WHERE Inspect_No = @no ");

            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.AddWithValue("@no", no);
            SqlDataReader reader = cmd.ExecuteReader();
            List<InspectItemVO> list = DBHelpler.DataReaderMapToList<InspectItemVO>(reader);
            reader.Close();
            return list;
        }

        /// <summary>
        /// Author : 강지훈
        /// 검사 데이터 항목 저장
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool SaveInspectItem(InspectItemVO item)
        {
            string sql = @"INSERT INTO TB_INSPECT_ITEM
                            (Inspect_Name, Target, USL, LSL, Create_Time, Create_User_No)
                            VALUES
                            (@Inspect_Name, @Target, @USL, @LSL, CONVERT([char](19),getdate(),(20)), @Create_User_No) ";

            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.Parameters.AddWithValue("@Inspect_Name", item.Inspect_Name);
                cmd.Parameters.AddWithValue("@Target", item.Target);
                cmd.Parameters.AddWithValue("@USL", item.USL);
                cmd.Parameters.AddWithValue("@LSL", item.LSL);
                cmd.Parameters.AddWithValue("@Create_User_No", item.Create_User_No);

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
        /// 검사 데이터 항목 수정
        /// </summary>
        /// <param name="item"></param>
        public bool UpdateInspectItem(InspectItemVO item)
        {
            string sql = @"UPDATE TB_INSPECT_ITEM SET
                            Inspect_Name = @Inspect_Name, Target = @Target, USL = @USL, LSL = @LSL, 
                            Update_Time = CONVERT(CHAR(19), GETDATE(), 20), Update_User_No = @Update_User_No
                            WHERE Inspect_No = @Inspect_No ";

            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Inspect_Name", item.Inspect_Name);
                cmd.Parameters.AddWithValue("@Target", item.Target);
                cmd.Parameters.AddWithValue("@USL", item.USL);
                cmd.Parameters.AddWithValue("@LSL", item.LSL);
                cmd.Parameters.AddWithValue("@Update_User_No", item.Update_User_No);
                cmd.Parameters.AddWithValue("@Inspect_No", item.Inspect_No);

                int iRow = cmd.ExecuteNonQuery();
                return iRow > 0;
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
                return false;
            }
        }

        /// <summary>
        /// Author : 강지훈
        /// 검사 데이터 항목 삭제
        /// </summary>
        /// <param name="InspectNo"></param>
        /// <returns></returns>
        public bool DeleteInspectItem(int InspectNo)
        {
            string sql = "DELETE FROM TB_INSPECT_ITEM WHERE Inspect_No = @Inspect_No";

            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Inspect_No", InspectNo);
                int iRow = cmd.ExecuteNonQuery();
                return iRow > 0;
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
                return false;
            }
        }
    }
}
