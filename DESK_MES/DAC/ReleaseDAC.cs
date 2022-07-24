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
    public class ReleaseDAC : IDisposable
    {
        SqlConnection conn;

        public ReleaseDAC()
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

        public List<ReleaseVO> GetReleaseList() // 출고와 관련된 주문 정보 가져오기(주문 확정인 것들만)
        {
            try
            {
                List<ReleaseVO> list = new List<ReleaseVO>();

                string sql = @"select Order_No, 
                               	      OD.Client_Code,
                               	      Client_Name,
                                      convert(varchar(10), Order_Date, 23) Order_Date,
                                      Order_State,
                                      convert(varchar(10), Release_Date, 23) Release_Date,
                               	      Release_State,
                                      convert(varchar(10), Release_OK_Date, 23) Release_OK_Date
                               from [dbo].[TB_ORDER] OD
                               INNER JOIN [dbo].[TB_Client] C ON OD.Client_Code=C.Client_Code
                               where Order_State='DT'";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    list = DBHelpler.DataReaderMapToList<ReleaseVO>(cmd.ExecuteReader());
                }
                return list;
            }
            catch (Exception err)
            {
                return null;
            }
        }
    }
}
