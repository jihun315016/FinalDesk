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
                                      convert(varchar(10), IncomingDue_date, 23) IncomingDue_date,
                                      Is_Incoming,
									  convert(varchar(10), P.Create_Time, 23) Create_Time,
                                      P.Create_User_No,
									  U.User_Name AS Create_User_Name,
                                      convert(varchar(10), P.Update_Time, 23) Update_Time,
                                      P.Update_User_No,
									  UU.User_Name AS Update_User_Name,
                                      convert(varchar(10), IncomingDue_date, 23) IncomingDue_date
                               from [dbo].[TB_PURCHASE] P
                               INNER JOIN [dbo].[TB_Client] C ON P.Client_Code=C.Client_Code
							   LEFT JOIN [dbo].[TB_USER] U ON C.Create_User_No=U.User_No
							   LEFT JOIN [dbo].[TB_USER] UU ON C.Update_User_No=UU.User_No";

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

        public List<PurchaseVO> GetClientList()
        {
            try
            {
                List<PurchaseVO> list = new List<PurchaseVO>();

                string sql = @"select Client_Code, Client_Name
                               from [dbo].[TB_Client]
                               where Client_Type='VEN'";

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

       

        public bool UpdatePurchaseInfo(PurchaseVO info)
        {
            string sql = @"update [dbo].[TB_PURCHASE]
                           set IncomingDue_date = @IncomingDue_date, 
                           Purchase_State = @Purchase_State,
                           Update_Time = Update_Time,
                           Update_User_No = Update_User_No
                           where Purchase_No = @Purchase_No";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IncomingDue_date", info.IncomingDue_date);
                    cmd.Parameters.AddWithValue("@Purchase_State", info.Purchase_State);
                    cmd.Parameters.AddWithValue("@Update_Time", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Update_User_No", info.Update_User_No);
                    cmd.Parameters.AddWithValue("@Purchase_No", info.Purchase_No);

                    int iRowAffect = cmd.ExecuteNonQuery();
                    if (iRowAffect < 1)
                        throw new Exception("해당 발주 정보가 없습니다.");
                }
                return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }


        public bool UpdatePurchaseOK(PurchaseVO order)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                try
                {
                    cmd.CommandText = @"Update [dbo].[TB_PURCHASE] set Purchase_State=@Purchase_State,
                                        Is_Incoming=@Is_Incoming,
                                        Update_Time=@Update_Time,
                                        Update_User_No=@Update_User_No
                                        where Purchase_No=@Purchase_No";

                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@Purchase_No", order.Purchase_No);
                    cmd.Parameters.AddWithValue("@Purchase_State", order.Purchase_State);
                    cmd.Parameters.AddWithValue("@Is_Incoming", order.Is_Incoming);
                    cmd.Parameters.AddWithValue("@Update_Time", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Update_User_No", order.Update_User_No);
                    cmd.ExecuteNonQuery();

                    return true;
                }
                catch (Exception err)
                {
                    return false;
                }
            }

        }
        public bool UpdatePurchaseCancle(PurchaseVO order)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                try
                {
                    cmd.CommandText = @"Update [dbo].[TB_PURCHASE] set Purchase_State=@Purchase_State,
                                            Update_Time=@Update_Time,
                                            Update_User_No=@Update_User_No
                                            where Purchase_No=@Purchase_No";

                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@Purchase_No", order.Purchase_No);
                    cmd.Parameters.AddWithValue("@Is_Incoming", order.Is_Incoming);
                    cmd.Parameters.AddWithValue("@Update_Time", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Update_User_No", order.Update_User_No);
                    cmd.ExecuteNonQuery();

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
