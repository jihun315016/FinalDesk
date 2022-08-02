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
    public class ManufactuerDAC : IDisposable
    {
        SqlConnection conn;

        public ManufactuerDAC()
        {
            string connStr = ConfigurationManager.ConnectionStrings["prjDB"].ConnectionString;
            conn = new SqlConnection(connStr);
            conn.Open();
        }

        public void Dispose()
        {
            if (conn != null && conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        // 주문서 가져오기(combobox용)
        public List<ManufactureVO> GetOrderList()
        {
            try
            {
                List<ManufactureVO> list = new List<ManufactureVO>();

                string sql = @"select Order_No
                               from TB_ORDER
                               where Order_State= 'DT'";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    list = DBHelpler.DataReaderMapToList<ManufactureVO>(cmd.ExecuteReader());
                }
                return list;
            }
            catch (Exception err)
            {
                return null;
            }
        }

        public List<ManufactureVO> GetOrderProductListForManufacture(int orderNo)
        {
            try
            {
                List<ManufactureVO> list = new List<ManufactureVO>();

                string sql = @"select OD.Product_Code, Product_Name
                               from TB_ORDER_DETAIL OD
                               INNER JOIN TB_PRODUCT P ON  OD.Product_Code=P.Product_Code
                               WHERE Order_No=@Order_No";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("Order_No", orderNo);
                    list = DBHelpler.DataReaderMapToList<ManufactureVO>(cmd.ExecuteReader());
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
