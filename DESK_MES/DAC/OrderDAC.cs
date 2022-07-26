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
    public class OrderDAC : IDisposable
    {
        SqlConnection conn;

        public OrderDAC()
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

        public List<OrderVO> GetOrderList() // 주문 정보 가져오기
        {
            try
            {
                List<OrderVO> list = new List<OrderVO>();

                string sql = @"select Order_No, 
                               	      OD.Client_Code,
                               	      Client_Name,
                                      convert(varchar(10), Order_Date, 23) Order_Date,
                               	      Order_State,
                                      convert(varchar(10), Release_Date, 23) Release_Date,
                               	      Release_State,
                                      convert(varchar(10), OD.Create_Time, 23) Create_Time,
                               	      OD.Create_User_No,
                                      convert(varchar(10), OD.Update_Time, 23) Update_Time,
                               	      OD.Update_User_No
                               from [dbo].[TB_ORDER] OD
                               INNER JOIN [dbo].[TB_Client] C ON OD.Client_Code=C.Client_Code";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    list = DBHelpler.DataReaderMapToList<OrderVO>(cmd.ExecuteReader());
                }
                return list;
            }
            catch (Exception err)
            {
                return null;
            }
        }

        public List<OrderDetailVO> GetOrderDetailList(int no)
        {
            try
            {
                // 카테고리, 주문번호, 제품번호, 제품이름, 주문 수량, 제품단가, 합계
                string sql = @"select Order_No,
                                      OD.Product_Code AS Product_Code,
                                      Product_Name,
                                      TotalPrice,
                                      Qty_PerUnit,
                                      TotalQty
                               from[dbo].[TB_ORDER_DETAIL] OD
                               INNER JOIN [dbo].[TB_PRODUCT] P ON OD.Product_Code=P.Product_Code
                               WHERE Order_No=@Order_No";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Order_No", no);

                    return DBHelpler.DataReaderMapToList<OrderDetailVO>(cmd.ExecuteReader());
                }
            }
            catch (Exception err)
            {
                return null;
            }
        } 

        public OrderVO GetOrderListByOrderNO(int no)
        {
            try
            {                
                string sql = @"select Order_No, 
                               	      OD.Client_Code,
                               	      Client_Name,
                                      convert(varchar(10), Order_Date, 23) Order_Date,
                               	      Order_State,
                                      convert(varchar(10), Release_Date, 23) Release_Date,
                               	      Release_State,
                                      convert(varchar(10), OD.Create_Time, 23) Create_Time,
                               	      OD.Create_User_No,
                                      convert(varchar(10), OD.Update_Time, 23) Update_Time,
                               	      OD.Update_User_No
                               from [dbo].[TB_ORDER] OD
                               INNER JOIN [dbo].[TB_Client] C ON OD.Client_Code=C.Client_Code
                               where Order_No=@Order_No";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Order_No", no);

                    List<OrderVO> list = DBHelpler.DataReaderMapToList<OrderVO>(cmd.ExecuteReader());

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
        public List<ProductVO> GetProductListForOrder()
        {
            try
            {
                string sql = @"select Product_Code, 
                                      Product_Name, 
                                      Product_Type, 
                                      Price,
                                      Unit
                               from [dbo].[TB_PRODUCT]
                               where Product_Type= 'FERT'";

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

        public List<OrderDetailVO> GetOrderDetailList(string code) 
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

                    return DBHelpler.DataReaderMapToList<OrderDetailVO>(cmd.ExecuteReader());
                }
            }
            catch (Exception err)
            {
                return null;
            }
        }

        public bool RegisterOrder(OrderVO order, List<OrderDetailVO> orderList) // 주문 등록
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    cmd.CommandText = @"insert into [dbo].[TB_ORDER] (Client_Code, Order_Date, Release_Date, Release_State, Create_Time)
                                        values (@Client_Code, @Order_Date, @Release_Date, @Release_State, @Create_Time); SELECT @@IDENTITY";

                    cmd.Connection = conn;
                    cmd.Transaction = trans;
                    cmd.Parameters.AddWithValue("@Client_Code", order.Client_Code);
                    cmd.Parameters.AddWithValue("@Order_Date", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Release_Date", order.Release_Date);
                    cmd.Parameters.AddWithValue("@Release_State", order.Release_State);
                    cmd.Parameters.AddWithValue("@Create_Time", DateTime.Now);

                    int newOrderID = Convert.ToInt32(cmd.ExecuteScalar());

                    cmd.Parameters.Clear();
                    cmd.CommandText = @"insert into [dbo].[TB_ORDER_DETAIL] (Order_No, Product_Code, TotalPrice, Qty_PerUnit, TotalQty)
                                        values (@Order_No, @Product_Code, @TotalPrice, @Qty_PerUnit, @TotalQty)";

                    cmd.Parameters.AddWithValue("@Order_No", newOrderID);
                    cmd.Parameters.Add("@Product_Code", System.Data.SqlDbType.NVarChar);
                    cmd.Parameters.Add("@TotalPrice", System.Data.SqlDbType.Int);
                    cmd.Parameters.Add("@Qty_PerUnit", System.Data.SqlDbType.Int);
                    cmd.Parameters.Add("@TotalQty", System.Data.SqlDbType.Int);

                    foreach (OrderDetailVO item in orderList)
                    {
                        cmd.Parameters["@Product_Code"].Value = item.Product_Code;
                        cmd.Parameters["@TotalPrice"].Value = item.TotalPrice;
                        cmd.Parameters["@Qty_PerUnit"].Value = item.Qty_PerUnit;
                        cmd.Parameters["@TotalQty"].Value = item.TotalPrice;

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

        public bool UpdateOrderInfo(OrderVO order) // 주문 수정
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                try
                {
                    cmd.CommandText = @"Update [dbo].[TB_ORDER] set Release_Date=@Release_Date, 
                                                                    Order_State=@Order_State
                                        where Order_No=@Order_No";

                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@Order_No", order.Order_No);
                    cmd.Parameters.AddWithValue("@Release_Date", order.Release_Date);
                    cmd.Parameters.AddWithValue("@Order_State", order.Order_State);
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
