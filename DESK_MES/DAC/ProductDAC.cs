using DESK_DTO;
using DESK_MES.Utility;
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
        /// 모든 제품, 재공품, 원자재 리스트 조회
        /// </summary>
        /// <param name="code"></param>
        /// <param name="isBom"></param>
        /// <param name="operNo"></param>
        /// <returns></returns>
        public List<ProductVO> GetProductList(string code, bool isBom, int operNo)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"SELECT 
                        Product_Code, Product_Name, Product_Type, Is_Image, Price, Unit, c.Client_Code, Client_Name, p.Create_Time, p.Create_User_No
                        , u.User_Name Create_User_Name, p.Update_Time, p.Update_User_No, uu.User_Name Update_User_Name
                        FROM TB_PRODUCT p
                        LEFT JOIN TB_USER u ON p.Create_User_No = u.User_No
                        LEFT JOIN TB_USER uu ON p.Update_User_No = uu.User_No 
                        LEFT JOIN TB_Client c ON p.Client_Code = c.Client_Code ");

            if (!string.IsNullOrWhiteSpace(code))
                sb.Append(" WHERE Product_Code=@code ");

            if (isBom)
                sb.Append(" WHERE Product_Code NOT IN (SELECT Parent_Product_Code FROM TB_BOM) ");

            if (operNo > 0)
                sb.Append(" WHERE Product_Code IN (SELECT Product_Code FROM TB_PRODUCT_OPERATION_RELATION WHERE Operation_No=@Operation_No) ");

            string sql = sb.ToString();            
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@code", code);
            cmd.Parameters.AddWithValue("@Operation_No", operNo);
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
        /// BOM 등록된 제품 조회
        /// </summary>
        /// <returns></returns>
        public List<ProductVO> GetBomList()
        {
            string sql = @"SELECT 
                            Product_Code, Product_Name, Product_Type, Is_Image, Price, Unit, p.Create_Time, p.Create_User_No
                            , u.User_Name Create_User_Name, p.Update_Time, p.Update_User_No, uu.User_Name Create_User_Name
                            FROM TB_PRODUCT p
                            LEFT JOIN TB_USER u ON p.Create_User_No = u.User_No
                            LEFT JOIN TB_USER uu ON p.Update_User_No = uu.User_No 
                            WHERE Product_Code IN (
					                            SELECT * FROM
					                            (
						                            SELECT Parent_Product_Code Product_Code FROM TB_BOM 
						                            UNION
						                            SELECT Child_Product_Code Product_Code FROM TB_BOM 
					                            ) P
					                            GROUP BY Product_Code
				                            ) ";

            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            List<ProductVO> list = DBHelpler.DataReaderMapToList<ProductVO>(reader);
            reader.Close();
            return list;
        }

        /// <summary>
        /// Author : 강지훈
        /// BOM 정전개와 역전개 조회
        /// </summary>
        /// <returns></returns>
        public List<ProductVO> GetChildParentProductList(string code)
        {
            string sql = @"WITH Child_Products AS
                            (
	                            SELECT Child_Product_Code Product_Code, '자품목' Bom_Type, Qty
	                            FROM TB_BOM 
	                            WHERE Parent_Product_Code = @code
                            ),
                            Parent_Products AS
                            (
	                            SELECT Parent_Product_Code Product_Code, '모품목' Bom_Type, Qty
	                            FROM TB_BOM 
	                            WHERE Child_Product_Code = @code
                            )
                            SELECT c.Product_Code, Product_Name, c.Bom_Type, pd.Product_Type, Qty
                            FROM Child_Products c
                            JOIN TB_PRODUCT pd ON c.Product_Code = pd.Product_Code
                            UNION
                            SELECT p.Product_Code, Product_Name, p.Bom_Type, pd.Product_Type, Qty
                            FROM Parent_Products p
                            JOIN TB_PRODUCT pd ON p.Product_Code = pd.Product_Code ";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@code", code);
            SqlDataReader reader = cmd.ExecuteReader();
            List<ProductVO> list = DBHelpler.DataReaderMapToList<ProductVO>(reader);
            reader.Close();
            return list;
        }

        /// <summary>
        /// Author : 강지훈
        /// 정전개가 존재하는 BOM 리스트 조회
        /// </summary>
        /// <returns></returns>
        public List<ProductVO> GetBomParentList()
        {
            string sql = @"SELECT 
                            Product_Code, Product_Name, Product_Type, Is_Image, Price, Unit, p.Create_Time, p.Create_User_No
                            , u.User_Name Create_User_Name, p.Update_Time, p.Update_User_No, uu.User_Name Create_User_Name
                            FROM TB_PRODUCT p
                            LEFT JOIN TB_USER u ON p.Create_User_No = u.User_No
                            LEFT JOIN TB_USER uu ON p.Update_User_No = uu.User_No 
                            WHERE Product_Code IN (SELECT Parent_Product_Code Product_Code FROM TB_BOM ) ";

            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            List<ProductVO> list = DBHelpler.DataReaderMapToList<ProductVO>(reader);
            reader.Close();
            return list;
        }

        /// <summary>
        /// Author : 강지훈
        /// BOM 삭제
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool DeleteBom(string code)
        {
            string sql = "DELETE FROM TB_BOM WHERE Parent_Product_Code = @code ";
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.Parameters.AddWithValue("@code", code);
                int iRow = cmd.ExecuteNonQuery();
                return iRow > 0;
            }
            catch (Exception err)
            {
                LoggingMsgVO msg = new LoggingMsgVO()
                {
                    Msg = err.Message,
                    StackTrace = err.StackTrace,
                    Source = err.Source
                };
                LoggingUtil.LoggingError(msg);

                return false;
            }
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
                if (prd.Price == -1)
                    cmd.Parameters.AddWithValue("@Price", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Price", prd.Price);
                if (prd.Unit == -1)
                    cmd.Parameters.AddWithValue("@Unit", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Unit", prd.Unit);
                if (prd.Client_Code == null)
                    cmd.Parameters.AddWithValue("@Client_Code", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Client_Code", prd.Client_Code);
                cmd.Parameters.AddWithValue("@Create_User_No", userNo);
                cmd.Parameters.Add("@prd_code", System.Data.SqlDbType.NVarChar, 20);
                cmd.Parameters["@prd_code"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add("@err_msg", System.Data.SqlDbType.NVarChar, 1000);
                cmd.Parameters["@err_msg"].Direction = System.Data.ParameterDirection.Output;

                return (cmd.ExecuteNonQuery() > 0) ? cmd.Parameters["@prd_code"].Value.ToString() : String.Empty;
            }
            catch (Exception err)
            {
                LoggingMsgVO msg = new LoggingMsgVO()
                {
                    Msg = err.Message,
                    StackTrace = err.StackTrace,
                    Source = err.Source
                };
                LoggingUtil.LoggingError(msg);

                return String.Empty;
            }
        }

        /// <summary>
        /// Author : 강지훈
        /// 제품 수정 기능
        /// </summary>
        /// <param name="prd">수정될 제품 정보</param>
        /// <returns></returns>
        public bool UpdateProduct(ProductVO prd)
        {
            string sql = @"UPDATE TB_PRODUCT 
                            SET 
	                            Product_Name = @Product_Name, Product_Type = @Product_Type, Is_Image = @Is_Image, 
	                            Price = @Price, Unit = @Unit, Client_Code = @Client_Code,
	                            Update_Time = CONVERT(CHAR(19), GETDATE(), 20), Update_User_No = @Update_User_No
                            WHERE
	                            Product_Code = @Product_Code ";

            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Product_Name", prd.Product_Name);
                cmd.Parameters.AddWithValue("@Product_Type", prd.Product_Type);
                cmd.Parameters.AddWithValue("@Is_Image", prd.Is_Image);
                cmd.Parameters.AddWithValue("@Price", prd.Price);
                cmd.Parameters.AddWithValue("@Unit", prd.Unit);
                if (prd.Client_Code == null) 
                    cmd.Parameters.AddWithValue("@Client_Code", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Client_Code", prd.Client_Code);
                cmd.Parameters.AddWithValue("@Update_User_No", prd.Update_User_No);
                cmd.Parameters.AddWithValue("@Product_Code", prd.Product_Code);

                int iRow = cmd.ExecuteNonQuery();
                return iRow > 0;
            }
            catch (Exception err)
            {
                LoggingMsgVO msg = new LoggingMsgVO()
                {
                    Msg = err.Message,
                    StackTrace = err.StackTrace,
                    Source = err.Source
                };
                LoggingUtil.LoggingError(msg);

                return false;
            }
        }

        /// <summary>
        /// Author : 강지훈
        /// 제품 삭제 기능
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool DeleteProduct(string code)
        {
            string sql = "DELETE FROM TB_PRODUCT WHERE Product_Code=@Product_Code ";

            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Product_Code", code);
                int iRow = cmd.ExecuteNonQuery();
                return iRow > 0;
            }
            catch (Exception err)
            {
                LoggingMsgVO msg = new LoggingMsgVO()
                {
                    Msg = err.Message,
                    StackTrace = err.StackTrace,
                    Source = err.Source
                };
                LoggingUtil.LoggingError(msg);

                return false;
            }
        }

        /// <summary>
        /// Author : 강지훈
        /// BOM 등록 기능
        /// </summary>
        public bool SaveBom(List<BomVO> list, int userNo)
        {
            string sql = @"INSERT INTO TB_BOM
                            (Parent_Product_Code, Child_Product_Code, Qty, Create_Time, Create_User_No)
                            VALUES
                            (@Parent_Product_Code, @Child_Product_Code, @Qty, CONVERT([char](19),getdate(),(20)), @Create_User_No) ";

            SqlTransaction tran = conn.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@Parent_Product_Code", System.Data.SqlDbType.VarChar);
                cmd.Parameters.Add("@Child_Product_Code", System.Data.SqlDbType.VarChar);
                cmd.Parameters.Add("@Qty", System.Data.SqlDbType.VarChar);                
                cmd.Parameters.AddWithValue("@Create_User_No", userNo);

                cmd.Transaction = tran;

                foreach (BomVO item in list)
                {
                    cmd.Parameters["@Parent_Product_Code"].Value = item.Parent_Product_Code;
                    cmd.Parameters["@Child_Product_Code"].Value = item.Child_Product_Code;
                    cmd.Parameters["@Qty"].Value = item.Qty;
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();
                return true;
            }
            catch (Exception err)
            {
                LoggingMsgVO msg = new LoggingMsgVO()
                {
                    Msg = err.Message,
                    StackTrace = err.StackTrace,
                    Source = err.Source
                };
                LoggingUtil.LoggingError(msg);

                tran.Rollback();   
                return false;
            }
        }        
    }
}
