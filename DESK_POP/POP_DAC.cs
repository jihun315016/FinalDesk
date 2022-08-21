using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DESK_DTO;

namespace DESK_POP
{
    public class POP_DAC : IDisposable
    {
        SqlConnection conn;

        public POP_DAC()
        {
            string connstr = ConfigurationManager.ConnectionStrings["prjDB"].ConnectionString;
            conn = new SqlConnection(connstr);
            conn.Open();
        }
        public void Dispose()
        {
            if (conn != null && conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        public PopVO GetWorkQty(string workC)
        {
            try
            {
                List<PopVO> list = new List<PopVO>();

                string sql = @"select [Working_Qty]
                                from [dbo].[TB_WORK]
                                where Work_Code=@Work_Code";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Work_Code", workC);
                    list = DBHelpler.DataReaderMapToList<PopVO>(cmd.ExecuteReader());
                }
                return list[0];
            }
            catch (Exception err)
            {
                return null;
            }
        }
        public bool Getupdate(string workC)
        {
            try
            {
                List<PopVO> list = new List<PopVO>();

                string sql = @"update [dbo].[TB_WORK]
                                set [Work_State] = 3
                                where [Work_Code] = @Work_Code";
                int row;
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Work_Code", workC);
                    row = cmd.ExecuteNonQuery();
                }
                return row > 0;
            }
            catch (Exception err)
            {
                return false;
            }
        }
    }
}
