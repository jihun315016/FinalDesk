using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DESK_DTO;

namespace DESK_MES
{
    public class WarehouseDAC : IDisposable
    {
        SqlConnection conn;

        public WarehouseDAC()
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

        public List<WarehouseProductVO> GetWarehouseDetailList(string code) // 선택한 창고 따른 주문 상세 정보 가져오기(주문현황)
        {
            try
            {
                string sql = @"select WP.Product_Code as Product_Code, 
                                      WP.Warehouse_Code as Warehouse_Code, 
                                      Product_Quantity,
                                      Safe_Stock,
                                      CONVERT(VARCHAR(20), WP.Create_Time, 20) Create_Time,
                                      WP.Create_User_No as Create_User_No
                               from TB_WAREHOUSE_PRODUCT_RELATION WP 
                               INNER JOIN TB_PRODUCT P ON WP.Product_Code = P.Product_Code
                               WHERE Warehouse_Code = @Warehouse_Code";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Warehouse_Code", code);

                    return DBHelpler.DataReaderMapToList<WarehouseProductVO>(cmd.ExecuteReader());
                }
            }
            catch (Exception err)
            {
                return null;
            }

        }
    }
}
