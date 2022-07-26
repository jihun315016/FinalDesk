using DESK_DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
