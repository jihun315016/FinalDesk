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

        /// <summary>
        /// 전체 주문 목록 가져오기
        /// </summary>
        /// <returns></returns>
        public List<OrderVO> GetOrderList() 
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
									  U.User_Name AS Create_User_Name,
                                      convert(varchar(10), OD.Update_Time, 23) Update_Time,
                               	      OD.Update_User_No, 
									  UU.User_Name AS Update_User_Name
                               from [dbo].[TB_ORDER] OD
                               LEFT JOIN [dbo].[TB_Client] C ON OD.Client_Code=C.Client_Code
							   LEFT JOIN [dbo].[TB_USER] U ON OD.Create_User_No=U.User_No
							   LEFT JOIN [dbo].[TB_USER] UU ON OD.Update_User_No=UU.User_No";

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

        /// <summary>
        /// 선택한 주문 상세정보 가져오기
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        public List<OrderDetailVO> GetOrderDetailList(int no)
        {
            try
            {
                // 카테고리, 주문번호, 제품번호, 제품이름, 주문 수량, 제품단가, 합계
                string sql = @"select Order_No,
                                      OD.Product_Code AS Product_Code,
                                      Product_Name,
                                      Product_Type,
                                      Price,
                                      Unit,
                                      Qty_PerUnit,
                                      TotalQty,
                                      TotalPrice
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

        /// <summary>
        /// 선택한 주문 목록 가져오기
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 주문을 위한 완제품 목록 가져오기
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 주문 거래처 가져오기(콤보박스)
        /// </summary>
        /// <returns></returns>
        public List<OrderVO> GetClientList()
        {
            try
            {
                List<OrderVO> list = new List<OrderVO>();

                string sql = @"select Client_Code, Client_Name
                               from [dbo].[TB_Client]
                               where Client_Type='CUS'";

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
        /// <summary>
        /// 주문 등록하기
        /// </summary>
        /// <param name="order"></param>
        /// <param name="orderList"></param>
        /// <returns></returns>
        public bool RegisterOrder(OrderVO order, List<OrderDetailVO> orderList) // 주문 등록
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    cmd.CommandText = @"insert into [dbo].[TB_ORDER] (Client_Code, Order_Date, Release_Date, Release_State, Create_Time, Create_User_No)
                                        values (@Client_Code, @Order_Date, @Release_Date, @Release_State, @Create_Time, @Create_User_No); SELECT @@IDENTITY";

                    cmd.Connection = conn;
                    cmd.Transaction = trans;
                    cmd.Parameters.AddWithValue("@Client_Code", order.Client_Code);
                    cmd.Parameters.AddWithValue("@Order_Date", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Release_Date", order.Release_Date);
                    cmd.Parameters.AddWithValue("@Release_State", order.Release_State);
                    cmd.Parameters.AddWithValue("@Create_Time", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Create_User_No", order.Create_User_No);

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

        /// <summary>
        /// 주문 수정
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool UpdateOrderInfo(OrderVO order) 
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                try
                {
                    cmd.CommandText = @"Update [dbo].[TB_ORDER] set Release_Date=@Release_Date, 
                                                                    Order_State=@Order_State,
                                                                    Update_Time=@Update_Time,
                                                                    Update_User_No=@Update_User_No
                                        where Order_No=@Order_No";

                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@Order_No", order.Order_No);
                    cmd.Parameters.AddWithValue("@Release_Date", order.Release_Date);
                    cmd.Parameters.AddWithValue("@Order_State", order.Order_State);
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
