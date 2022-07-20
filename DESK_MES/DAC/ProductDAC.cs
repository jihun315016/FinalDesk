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

        //public bool SaveProduct(ProductVO prd)
        //{
        //    return true;
        //}
    }
}
