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

        /// <summary>
        /// 창고 저장용 품목 목록 가져오기
        /// </summary>
        /// <returns></returns>
        public List<ProductVO> GetProductListForWarehouse()
        {
            try
            {
                string sql = @"select Product_Code, 
                                      Product_Name, 
                                      Product_Type, 
                                      Unit 
                               from [dbo].[TB_PRODUCT]";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    return DBHelpler.DataReaderMapToList<ProductVO>(cmd.ExecuteReader());
                }
            }
            catch (Exception err)
            {
                return null;
            }
        }

        /// <summary>
        /// // 선택한 창고 따른 상세정보 가져오기
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public List<WarehouseProductVO> GetWarehouseDetailList(string code) 
        {
            try
            {
                string sql = @"select WP.Product_Code as Product_Code, 
                                      WP.Warehouse_Code as Warehouse_Code,
                                      Warehouse_Name, 
                                      Product_Quantity,
                                      Product_Name,
                                      Safe_Stock,
                                      CONVERT(VARCHAR(20), WP.Create_Time, 20) Create_Time,
                                      WP.Create_User_No as Create_User_No
                               from TB_WAREHOUSE_PRODUCT_RELATION WP
							   INNER JOIN TB_WAREHOUSE W ON WP.Warehouse_Code = W.Warehouse_Code
                               INNER JOIN TB_PRODUCT P ON WP.Product_Code = P.Product_Code
                               WHERE WP.Warehouse_Code =@Warehouse_Code";

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
        /// <summary>
        /// 창고 전체 정보 가져오기
        /// </summary>
        /// <returns></returns>
        public List<WarehouseVO> GetAllWarehouse()
        {
            try
            {
                string sql = @"select Warehouse_Code, 
                                           Warehouse_Name, 
                                           Warehouse_Address,
                                           Warehouse_Type,
                                           CONVERT(VARCHAR(20), Create_Time, 20) Create_Time,
                                           Create_User_No,
                                           CONVERT(VARCHAR(20), Update_Time, 20) Update_Time, 
                                           Update_User_No,
                                           Is_Delete
                                    from TB_WAREHOUSE"; ;

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    return DBHelpler.DataReaderMapToList<WarehouseVO>(cmd.ExecuteReader());
                }
            }
            catch (Exception err)
            {
                return null;
            }
        }

        

        //public bool SaveWarehouse(WarehouseVO warehouse)
        //{
        //    using (SqlCommand cmd = new SqlCommand())
        //    {
        //        cmd.Connection = new SqlConnection(strConn);
        //        cmd.CommandText = @"INSERT INTO TB_WAREHOUSE (Warehouse_Code, Warehouse_Name, Warehouse_Address, Warehouse_Type, Create_Time)
        //                            VALUES (@Warehouse_Code, @Warehouse_Name, @Warehouse_Address, @Warehouse_Type, @Create_Time)";

        //        cmd.Parameters.AddWithValue("@Warehouse_Code", warehouse.Warehouse_Code);
        //        cmd.Parameters.AddWithValue("@Warehouse_Name", warehouse.Warehouse_Name);
        //        cmd.Parameters.AddWithValue("@Warehouse_Address", warehouse.Warehouse_Address);
        //        cmd.Parameters.AddWithValue("@Warehouse_Type", warehouse.Warehouse_Type);
        //        cmd.Parameters.AddWithValue("@Create_Time", DateTime.Now);

        //        cmd.Connection.Open();
        //        int iRowAffect = cmd.ExecuteNonQuery();
        //        cmd.Connection.Close();

        //        return (iRowAffect > 0);
        //    }
        //}

        //public bool UpdateWarehouse(WarehouseVO warehouse)
        //{
        //    using (SqlCommand cmd = new SqlCommand())
        //    {
        //        cmd.Connection = new SqlConnection(strConn);
        //        cmd.CommandText = @"UPDATE TB_WAREHOUSE SET Warehouse_Name = @Warehouse_Name,
        //                                                    Warehouse_Address = @Warehouse_Address,
        //                                                    Warehouse_Type = @Warehouse_Type
        //                            WHERE Warehouse_Code= @Warehouse_Code";

        //        cmd.Parameters.AddWithValue("@Warehouse_Code", warehouse.Warehouse_Code);
        //        cmd.Parameters.AddWithValue("@Warehouse_Name", warehouse.Warehouse_Name);
        //        cmd.Parameters.AddWithValue("@Warehouse_Address", warehouse.Warehouse_Address);
        //        cmd.Parameters.AddWithValue("@Warehouse_Type", warehouse.Warehouse_Type);

        //        cmd.Connection.Open();
        //        int iRowAffect = cmd.ExecuteNonQuery();
        //        cmd.Connection.Close();

        //        return (iRowAffect > 0);
        //    }
        //}

        //public bool DeleteWarehouse(string warehouseNO)
        //{
        //    using (SqlCommand cmd = new SqlCommand())
        //    {
        //        cmd.Connection = new SqlConnection(strConn);
        //        cmd.CommandText = @"UPDATE TB_WAREHOUSE SET Is_Delete = 'Y'
        //                            WHERE Warehouse_Code= @Warehouse_Code";

        //        cmd.Parameters.AddWithValue("@Warehouse_Code", warehouseNO);

        //        cmd.Connection.Open();
        //        int iRowAffect = cmd.ExecuteNonQuery();
        //        cmd.Connection.Close();

        //        return (iRowAffect > 0);
        //    }
        //}
        public bool SaveProductInWarehouse(string warehouseNo, List<WarehouseProductVO> saveList)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                try
                {
                    cmd.CommandText = @"INSERT INTO TB_WAREHOUSE_PRODUCT_RELATION (Warehouse_Code, Product_Code, Product_Quantity, Safe_Stock)
                                        VALUES(@Warehouse_Code, @Product_Code, @Product_Quantity, @Safe_Stock)";

                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@Warehouse_Code", warehouseNo);
                    cmd.Parameters.Add("@Product_Code", System.Data.SqlDbType.NVarChar);
                    cmd.Parameters.Add("@Product_Quantity", System.Data.SqlDbType.Int);
                    cmd.Parameters.Add("@Safe_Stock", System.Data.SqlDbType.Int);

                    foreach (WarehouseProductVO item in saveList)
                    {
                        cmd.Parameters["@Product_Code"].Value = item.Product_Code;
                        cmd.Parameters["@Product_Quantity"].Value = item.Product_Quantity;
                        cmd.Parameters["@Safe_Stock"].Value = item.Safe_Stock;
                        cmd.ExecuteNonQuery();
                    }

                    return true;
                }
                catch (Exception err)
                {
                    return false;
                }
            }
        }
    }
}
