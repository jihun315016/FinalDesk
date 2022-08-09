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
        /// 전체 창고 정보 가져오기
        /// </summary>
        /// <returns></returns>
        public List<WarehouseVO> GetAllWarehouse()
        {
            try
            {
                List<WarehouseVO> list = new List<WarehouseVO>();

                string sql = @"select Warehouse_Code, 
                                      Warehouse_Name, 
                                      Warehouse_Address,
                                      Warehouse_Type,
                                      CONVERT(VARCHAR(20), W.Create_Time, 20) Create_Time,
                                      W.Create_User_No,
									  U.User_Name AS Create_User_Name,
                                      CONVERT(VARCHAR(20), W.Update_Time, 20) Update_Time, 
									  UU.User_Name AS Update_User_Name,
                                      W.Update_User_No,
                                      W.Is_Delete
                               from TB_WAREHOUSE W
							   LEFT JOIN [dbo].[TB_USER] U ON W.Create_User_No=U.User_No
							   LEFT JOIN [dbo].[TB_USER] UU ON W.Update_User_No=UU.User_No
                               where W.Is_Delete='N'";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    list = DBHelpler.DataReaderMapToList<WarehouseVO>(cmd.ExecuteReader());
                }
                return list;
            }
            catch (Exception err)
            {
                return null;
            }
        }

        /// <summary>
        /// // 선택한 창고 따른 창고 상세정보 가져오기
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

        public List<PurchaseDetailVO> GetLotDetailList(string code)
        {
            try
            {
                string sql = @"select Lot_Code, Product_Name, Cur_Qty
                               from TB_MATERIAL_LOT ML
                               inner join TB_PRODUCT P on ML.Product_Code=P.Product_Code
                               where ML.Product_Code=@Product_Code";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Product_Code", code);

                    return DBHelpler.DataReaderMapToList<PurchaseDetailVO>(cmd.ExecuteReader());
                }
            }
            catch (Exception err)
            {
                return null;
            }
        }

        public WarehouseVO GetWarehouseInfoByCode(string code)
        {
            try
            {
                string sql = @"select Warehouse_Code, 
                                      Warehouse_Name, 
                                      Warehouse_Address,
                                      Warehouse_Type
                               from TB_WAREHOUSE W
                               where Warehouse_Code=@Warehouse_Code";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Warehouse_Code", code);

                    List<WarehouseVO> list = DBHelpler.DataReaderMapToList<WarehouseVO>(cmd.ExecuteReader());

                    if (list != null && list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
            catch (Exception err)
            {
                return null;
            }
        }

        /// <summary>
        /// 신규 등록할 창고ID 가져오기
        /// </summary>
        /// <returns></returns>
        public WarehouseVO GetLastID()
        {
            try
            {
                string sql = @"select TOP 1 Warehouse_Code
                               from [dbo].[TB_WAREHOUSE]
                               order by Warehouse_Code desc";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    List<WarehouseVO> list = DBHelpler.DataReaderMapToList<WarehouseVO>(cmd.ExecuteReader());

                    if (list != null && list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
            catch (Exception err)
            {
                return null;
            }
        }


        public bool RegisterWarehouse(WarehouseVO warehouse)
        {
            try
            {
                string sql = @"INSERT INTO TB_WAREHOUSE (Warehouse_Code, Warehouse_Name, Warehouse_Address, Warehouse_Type, Create_Time, Create_User_No)
                                    VALUES (@Warehouse_Code, @Warehouse_Name, @Warehouse_Address, @Warehouse_Type, @Create_Time, @Create_User_No)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Warehouse_Code", warehouse.Warehouse_Code);
                    cmd.Parameters.AddWithValue("@Warehouse_Name", warehouse.Warehouse_Name);
                    cmd.Parameters.AddWithValue("@Warehouse_Address", warehouse.Warehouse_Address);
                    cmd.Parameters.AddWithValue("@Warehouse_Type", warehouse.Warehouse_Type);
                    cmd.Parameters.AddWithValue("@Create_Time", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Create_User_No", warehouse.Create_User_No);


                    int iRowAffect = cmd.ExecuteNonQuery();
                    return (iRowAffect > 0);
                }

                return true;

            }
            catch (Exception err)
            {
                return false;
            }
        }


        public bool UpdateWarehouseInfo(WarehouseVO warehouse)
        {
            string sql = @"UPDATE TB_WAREHOUSE 
                           SET Warehouse_Name = @Warehouse_Name,
                               Warehouse_Address = @Warehouse_Address,
                               Warehouse_Type = @Warehouse_Type,
                               Update_Time = @Update_Time,
                               Update_User_No = @Update_User_No
                           WHERE Warehouse_Code= @Warehouse_Code";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Warehouse_Name", warehouse.Warehouse_Name);
                    cmd.Parameters.AddWithValue("@Warehouse_Address", warehouse.Warehouse_Address);
                    cmd.Parameters.AddWithValue("@Warehouse_Type", warehouse.Warehouse_Type);
                    cmd.Parameters.AddWithValue("@Update_Time", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Update_User_No", warehouse.Update_User_No);
                    cmd.Parameters.AddWithValue("@Warehouse_Code", warehouse.Warehouse_Code);

                    int iRowAffect = cmd.ExecuteNonQuery();
                    if (iRowAffect < 1)
                        throw new Exception("해당 창고 정보가 없습니다.");
                }
                return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }

        public bool DeleteWarehouseInfo(string warehouse)
        {
            string sql = @"UPDATE TB_WAREHOUSE 
                           SET Is_Delete = 'Y'
                           WHERE Warehouse_Code= @Warehouse_Code";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Warehouse_Code", warehouse);

                    int iRowAffect = cmd.ExecuteNonQuery();
                    if (iRowAffect < 1)
                        throw new Exception("해당 창고 정보가 없습니다.");
                }
                return true;
            }
            catch (Exception err)
            {
                return false;
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
        /// 창고에 보관할 상품 지정(등록)하기
        /// </summary>
        /// <param name="warehouseNo"></param>
        /// <param name="saveList"></param>
        /// <returns></returns>

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
