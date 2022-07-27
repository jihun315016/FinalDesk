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
    public class PurchaseDAC : IDisposable
    {
        SqlConnection conn;

        public PurchaseDAC()
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

        public List<PurchaseVO> GetPurchaseList() // 발주 정보 가져오기
        {
            try
            {
                List<PurchaseVO> list = new List<PurchaseVO>();

                string sql = @"select Purchase_No, 
                                      P.Client_Code,
                                      Client_Name,
                                      convert(varchar(10), Purchase_Date, 23) Purchase_Date,
                                      Purchase_State,
                                      convert(varchar(10), Incoming_Date, 23) Incoming_Date,
                                      Is_Incoming,
                                      convert(varchar(10), P.Create_Time, 23) Create_Time,
                                      P.Create_User_No,
                                      convert(varchar(10), P.Update_Time, 23) Update_Time,
                                      P.Update_User_No,
                                      convert(varchar(10), IncomingDue_date, 23) IncomingDue_date
                               from [dbo].[TB_PURCHASE] P
                               INNER JOIN [dbo].[TB_Client] C ON P.Client_Code=C.Client_Code";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    list = DBHelpler.DataReaderMapToList<PurchaseVO>(cmd.ExecuteReader());
                }
                return list;
            }
            catch (Exception err)
            {
                return null;
            }
        }
        public PurchaseVO GetPurchaseInfoByPurchaseCode(int no) // 선택한 발주 정보 가져오기
        {
            try
            {
                string sql = @"select Purchase_No, 
                                      P.Client_Code,
                                      Client_Name,
                                      convert(varchar(10), Purchase_Date, 23) Purchase_Date,
                                      Purchase_State,
                                      convert(varchar(10), Incoming_Date, 23) Incoming_Date,
                                      Is_Incoming,
                                      convert(varchar(10), P.Create_Time, 23) Create_Time,
                                      P.Create_User_No,
                                      convert(varchar(10), P.Update_Time, 23) Update_Time,
                                      P.Update_User_No,
                                      convert(varchar(10), IncomingDue_date, 23) IncomingDue_date
                               from [dbo].[TB_PURCHASE] P
                               INNER JOIN [dbo].[TB_Client] C ON P.Client_Code=C.Client_Code
                               where Purchase_No=@Purchase_No";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Purchase_No", no);

                    List<PurchaseVO> list = DBHelpler.DataReaderMapToList<PurchaseVO>(cmd.ExecuteReader());

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


        public List<PurchaseDetailVO> GetPurchaseDetailList(int no)
        {
            try
            {
                string sql = @"select Purchase_No, 
                                      PD.Product_Code as Product_Code,
                              	      Product_Name,
                              	      Product_Type,
                              	      Price,
                               	      Unit,
                                      TotalQty,
                                      Qty_PerUnit,
                                      TotalPrice
                              from [dbo].[TB_PURCHASE_DETAIL] PD
                              INNER JOIN [dbo].[TB_PRODUCT] P ON PD.Product_Code=P.Product_Code
                              where Purchase_No=@Purchase_No";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Purchase_No", no);

                    return DBHelpler.DataReaderMapToList<PurchaseDetailVO>(cmd.ExecuteReader());
                }
            }
            catch (Exception err)
            {
                return null;
            }
        } 


        public List<ProductVO> GetProductListForPurchase()
        {
            try
            {
                string sql = @"select Product_Code, 
                                      Product_Name, 
                                      Product_Type, 
                                      Price,
                                      Unit
                               from [dbo].[TB_PRODUCT]
                               where Product_Type= 'ROH'";

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

        public bool RegisterPurchase(PurchaseVO purchase, List<PurchaseDetailVO> purchaseList) // 발주 등록
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    cmd.CommandText = @"insert into [dbo].[TB_PURCHASE] (Client_Code, Purchase_Date, IncomingDue_date, Create_Time)
                                        values (@Client_Code, @Purchase_Date, @IncomingDue_date, @Create_Time); SELECT @@IDENTITY";

                    cmd.Connection = conn;
                    cmd.Transaction = trans;
                    cmd.Parameters.AddWithValue("@Client_Code", purchase.Client_Code);
                    cmd.Parameters.AddWithValue("@Purchase_Date", DateTime.Now);
                    cmd.Parameters.AddWithValue("@IncomingDue_date", purchase.IncomingDue_date);
                    cmd.Parameters.AddWithValue("@Create_Time", DateTime.Now);

                    int newPurchaseID = Convert.ToInt32(cmd.ExecuteScalar());

                    cmd.Parameters.Clear();
                    cmd.CommandText = @"insert into [dbo].[TB_PURCHASE_DETAIL] (Purchase_No, Product_Code, TotalQty, Qty_PerUnit, TotalPrice)
                                        values (@Purchase_No, @Product_Code, @TotalQty, @Qty_PerUnit, @TotalPrice)";

                    cmd.Parameters.AddWithValue("@Purchase_No", newPurchaseID);
                    cmd.Parameters.Add("@Product_Code", System.Data.SqlDbType.NVarChar);
                    cmd.Parameters.Add("@TotalQty", System.Data.SqlDbType.Int);
                    cmd.Parameters.Add("@Qty_PerUnit", System.Data.SqlDbType.Int);
                    cmd.Parameters.Add("@TotalPrice", System.Data.SqlDbType.Int);

                    foreach (PurchaseDetailVO item in purchaseList)
                    {
                        cmd.Parameters["@Product_Code"].Value = item.Product_Code;
                        cmd.Parameters["@TotalPrice"].Value = item.TotalPrice;
                        cmd.Parameters["@Qty_PerUnit"].Value = item.Qty_PerUnit;
                        cmd.Parameters["@TotalQty"].Value = item.TotalQty;

                        cmd.ExecuteNonQuery();
                    }

                    trans.Commit();
                    return true;
                }
                catch (Exception err)
                {
                    trans.Rollback();
                    return false;
                }
            }
        }

        public PurchaseDetailVO GetLastID() // 새롭게 등록할 LotID 가져오기
        {
            try
            {
                string sql = @"select TOP(1) [Lot_Code]
                               from [dbo].[TB_MATERIAL_LOT]   
                               ORDER BY [Lot_Code] DESC";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    List<PurchaseDetailVO> list = DBHelpler.DataReaderMapToList<PurchaseDetailVO>(cmd.ExecuteReader());

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
        public PurchaseDetailVO GetIncomingProductInfo(string no) // 입고하는 품목 정보 가져오기
        {
            try
            {
                string sql = @"select PD.Product_Code as Product_Code,
                              	      P.Product_Name as Product_Name,
                                      TotalQty
                              from [dbo].[TB_PURCHASE_DETAIL] PD
                              INNER JOIN [dbo].[TB_PRODUCT] P ON PD.Product_Code=P.Product_Code
                              where PD.Product_Code=@Product_Code";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Product_Code", no);

                    List<PurchaseDetailVO> list = DBHelpler.DataReaderMapToList<PurchaseDetailVO>(cmd.ExecuteReader());

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

        public List<PurchaseDetailVO> GetEqualWarehouse(string no)
        {
            try
            {
                string sql = @"select WP.Warehouse_Code, Warehouse_Name
                               from [dbo].[TB_WAREHOUSE] W
                               inner join [dbo].[TB_WAREHOUSE_PRODUCT_RELATION] WP on W.Warehouse_Code=WP.Warehouse_Code
                               inner join [dbo].[TB_PRODUCT] P on  WP.Product_Code=P.Product_Code
                               where WP.Product_Code=@Product_Code";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Product_Code", no);
                    return DBHelpler.DataReaderMapToList<PurchaseDetailVO>(cmd.ExecuteReader());
                }
            }
            catch (Exception err)
            {
                return null;
            }
        }

        public bool RegisterIncomingPurchase(PurchaseVO purchase, List<string> lotIDList, List<PurchaseDetailVO> purchaseList) // 발주 등록
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    // 발주 상세 정보를 업데이트
                    // 자재Lot를 생성 
                    cmd.CommandText = @"UPDATE [dbo].[TB_PURCHASE] SET Incoming_Date=@Incoming_Date, Is_Incoming=@Is_Incoming
                                        WHERE Purchase_No=@Purchase_No";

                    cmd.Connection = conn;
                    cmd.Transaction = trans;
                    cmd.Parameters.AddWithValue("@Purchase_No", purchase.Purchase_No);
                    cmd.Parameters.AddWithValue("@Incoming_Date", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Is_Incoming", purchase.Is_Incoming);
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = @"INSERT INTO [dbo].[TB_MATERIAL_LOT] (Lot_Code, Product_Code, Client_Code, Lot_Time, Lot_Qty, Cur_Qty, Warehouse_Code, Create_Time)
                                        VALUES (@Lot_Code, @Product_Code, @Client_Code, @Lot_Time, @Lot_Qty, @Cur_Qty, @Warehouse_Code, @Create_Time)";

                    cmd.Parameters.Add("@Lot_Code", System.Data.SqlDbType.NVarChar);
                    cmd.Parameters.Add("@Product_Code", System.Data.SqlDbType.NVarChar);
                    cmd.Parameters.AddWithValue("@Client_Code", System.Data.SqlDbType.NVarChar);
                    cmd.Parameters.AddWithValue("@Lot_Time", DateTime.Now);
                    cmd.Parameters.Add("@Lot_Qty", System.Data.SqlDbType.Int);
                    cmd.Parameters.Add("@Cur_Qty", System.Data.SqlDbType.Int);
                    cmd.Parameters.AddWithValue("@Warehouse_Code", System.Data.SqlDbType.NVarChar);
                    cmd.Parameters.AddWithValue("@Create_Time", DateTime.Now);

                    for(int i = 0; i < lotIDList.Count; i++)
                    {
                        cmd.Parameters["@Lot_Code"].Value = lotIDList[i];
                        cmd.Parameters["@Product_Code"].Value = purchaseList[i].Product_Code;
                        cmd.Parameters["@Client_Code"].Value = purchaseList[i].Client_Code;
                        cmd.Parameters["@Warehouse_Code"].Value = purchaseList[i].Warehouse_Code;
                        cmd.Parameters["@Lot_Qty"].Value = purchaseList[i].Lot_Qty;
                        cmd.Parameters["@Cur_Qty"].Value = purchaseList[i].Cur_Qty;

                        cmd.ExecuteNonQuery();
                    }

                    cmd.Parameters.Clear();
                    cmd.CommandText = @"update [dbo].[TB_WAREHOUSE_PRODUCT_RELATION] set Product_Quantity=@Product_Quantity
                                        where Product_Code=@Product_Code";

                    cmd.Parameters.Add("@Product_Code", System.Data.SqlDbType.NVarChar);
                    cmd.Parameters.Add("@Product_Quantity", System.Data.SqlDbType.Int);

                    foreach (PurchaseDetailVO item in purchaseList)
                    {
                        cmd.Parameters["@Product_Code"].Value = item.Product_Code;
                        cmd.Parameters["@Product_Quantity"].Value = item.Lot_Qty;

                        cmd.ExecuteNonQuery();
                    }

                    trans.Commit();
                    return true;
                }
                catch (Exception err)
                {
                    trans.Rollback();
                    return false;
                }
            }
        }
    }
}
