using DESK_WEB.Models.DTO;
using DESK_WEB.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace DESK_WEB.Models.DAC
{
    public class WorkDAC : IDisposable
    {
        SqlConnection conn;

        public WorkDAC()
        {
            string connStr = WebConfigurationManager.ConnectionStrings["prjDB"].ConnectionString;
            conn = new SqlConnection(connStr);
            conn.Open();
        }

        public void Dispose()
        {
            if (conn != null && conn.State == System.Data.ConnectionState.Open)
                conn.Close();
        }

        /// <summary>
        /// Author : 강지훈
        /// 설비 비가동 내역 조회
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<InoperativeEquipmentVO> GetInoperativeEquipmentList(string startDate, string endDate, string keyword)
        {
            string sql = @"SELECT 
	                            ie.Equipment_No, Equipment_Name, Inoperative_Start_Time, Inoperative_End_Time, 
	                            DATEDIFF(MINUTE, Inoperative_Start_Time, Inoperative_End_Time) MinuiteDiff, User_Name, 
	                            Inoperative_Reason, Action_History
                            FROM TB_INOPERATIVE_EQUIPMENT ie
                            JOIN TB_EQUIPMENT e ON ie.Equipment_No = e.Equipment_No
                            JOIN TB_USER u ON ie.Inoperative_User_No = u.User_No 
                            WHERE Inoperative_Start_Time >= @startDate
                            AND Inoperative_Start_Time <= @endDate
                            AND Equipment_Name LIKE @keyword 
                            ORDER BY Inoperative_Start_Time DESC ";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@startDate", startDate);
            cmd.Parameters.AddWithValue("@endDate", endDate);
            cmd.Parameters.AddWithValue("@keyword", $"%{keyword}%");
            SqlDataReader reader = cmd.ExecuteReader();
            List<InoperativeEquipmentVO> list = DBHelpler.DataReaderMapToList<InoperativeEquipmentVO>(reader);
            reader.Close();
            return list;
        }
    }
}