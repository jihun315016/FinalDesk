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
    }
}
