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
    class ProductDAC : IDisposable
    {

        SqlConnection conn;

        public ProductDAC()
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

        public List<ProductVO> GetAllProductList()
        {
            string sql = @"SELECT 
                            Product_Code, Product_Name, Product_Type, Is_Image, Price, Unit, p.Create_Time, p.Create_User_No, 
                            u.User_Name Create_User_Name, p.Update_Time, p.Update_User_No, uu.User_Name Create_User_Name, p.Is_Delete 
                            FROM TB_PRODUCT p
                            JOIN TB_USER u ON p.Create_User_No = u.User_No
                            LEFT JOIN TB_USER uu ON p.Update_User_No = uu.User_No ";

            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            List<ProductVO> list = DBHelpler.DataReaderMapToList<ProductVO>(reader);
            reader.Close();
            return list;
        }

        /// <summary>
        /// Author : 강지훈
        /// 제품 유형 조회
        /// </summary>
        /// <returns></returns>
        public List<CodeCountVO> GetProductType()
        {
            string sql = @"SELECT Code, Category 
                            FROM TB_CODE_COUNT 
                            WHERE Category IN ('완제품', '재공품', '원자재') ";

            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            List<CodeCountVO> list = DBHelpler.DataReaderMapToList<CodeCountVO>(reader);
            reader.Close();
            return list;
        }

        /// <summary>
        /// Author : 강지훈
        /// 제품 등록 기능
        /// 제품 유형에 따라 다른 코드로 저장된다.
        /// </summary>
        /// <param name="code">저장된 제품 코드</param>
        /// <param name="userNo">등록 사용자 번호</param>
        /// <param name="prd">제품 정보</param>
        /// <returns></returns>
        public string SaveProduct(string code, int userNo, ProductVO prd)
        {
            SqlCommand cmd = new SqlCommand("SP_SaveProduct", conn);

            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@code", code);
                cmd.Parameters.AddWithValue("@Product_Name", prd.Product_Name);
                cmd.Parameters.AddWithValue("@Product_Type", prd.Product_Type);
                cmd.Parameters.AddWithValue("@Is_Image", prd.Is_Image);
                cmd.Parameters.AddWithValue("@Price", prd.Price);
                cmd.Parameters.AddWithValue("@Unit", prd.Unit);
                cmd.Parameters.AddWithValue("@Is_Delete", prd.Is_Delete);
                cmd.Parameters.AddWithValue("@Create_User_No", userNo);
                cmd.Parameters.Add("@prd_code", System.Data.SqlDbType.NVarChar, 20);
                cmd.Parameters["@prd_code"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add("@err_msg", System.Data.SqlDbType.NVarChar, 1000);
                cmd.Parameters["@err_msg"].Direction = System.Data.ParameterDirection.Output;

                return (cmd.ExecuteNonQuery() > 0) ? cmd.Parameters["@prd_code"].Value.ToString() : String.Empty;                    
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                Console.WriteLine($"{Environment.NewLine}DB error : {cmd.Parameters["@err_msg"].Value}");
                return String.Empty;
            }
        }
    }
}
