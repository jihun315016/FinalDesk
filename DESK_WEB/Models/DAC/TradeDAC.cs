using DESK_WEB.Models.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace DESK_WEB.Models.DAC
{
    public class TradeDAC : IDisposable
    {
        SqlConnection conn;

        public TradeDAC()
        {
            string connStr = ConfigurationManager.ConnectionStrings["prjDB"].ConnectionString;
            conn = new SqlConnection(connStr);
            conn.Open();
        }

        public void Dispose()
        {
            if (conn != null && conn.State == System.Data.ConnectionState.Open)
                conn.Close();
        }

        /// <summary>
        /// Author : 강지훈
        /// 매입 현황 조회
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<WebPurchaseVO> GetPurchaseList(string startDate, string endDate, string keyword)
        {
            string sql = @"SELECT 
	                            pd.Purchase_No, p.Purchase_Date, p.Incoming_Date, Client_Name, 
	                            pd.Product_Code, Product_Name, TotalQty, Price, TotalPrice
                            FROM TB_PURCHASE_DETAIL pd
                            JOIN TB_PURCHASE p ON pd.Purchase_No = p.Purchase_No
                            JOIN TB_Client c ON p.Client_Code = c.Client_Code
                            JOIN TB_PRODUCT pr ON pd.Product_Code = pr.Product_Code
                            WHERE p.Is_Incoming = 'Y'
                            AND p.Purchase_Date >= @startDate
                            AND p.Purchase_Date <= @endDate
                            AND Client_Name LIKE @keyword 
                            ORDER BY Purchase_Date DESC ";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@startDate", startDate);
            cmd.Parameters.AddWithValue("@endDate", endDate);
            cmd.Parameters.AddWithValue("@keyword", $"%{keyword}%");
            SqlDataReader reader = cmd.ExecuteReader();
            List<WebPurchaseVO> list = DBHelper.DataReaderMapToList<WebPurchaseVO>(reader);
            reader.Close();
            return list;
        }

        /// <summary>
        /// Author : 강지훈
        /// 매출 현황 조회
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<WebOrderVO> GetOrderList(string startDate, string endDate, string keyword)
        {
            string sql = @"SELECT 
	                            od.Order_No, o.Order_Date, o.Release_Date, Client_Name, 
	                            od.Product_Code, Product_Name, TotalQty, Price, TotalPrice
                            FROM TB_ORDER_DETAIL od
                            JOIN TB_ORDER o ON od.Order_No = o.Order_No
                            JOIN TB_Client c ON o.Client_Code = c.Client_Code
                            JOIN TB_PRODUCT pr ON od.Product_Code = pr.Product_Code
                            WHERE o.Release_State = 'Y'
                            AND o.Order_Date >= @startDate
                            AND o.Order_Date <= @endDate
                            AND Client_Name LIKE @keyword 
                            ORDER BY Order_Date DESC ";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@startDate", startDate);
            cmd.Parameters.AddWithValue("@endDate", endDate);
            cmd.Parameters.AddWithValue("@keyword", $"%{keyword}%");
            SqlDataReader reader = cmd.ExecuteReader();
            List<WebOrderVO> list = DBHelper.DataReaderMapToList<WebOrderVO>(reader);
            reader.Close();
            return list;
        }

        /// <summary>
        /// 월별 매입 / 매출 조회
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public List<OrderPurchaseVO> GetOrderPurchaseList(int year, int month)
        {
            string sql = @"SELECT 
	                            p.Purchase_Date Date, SUM(TotalPrice) Total, '매입' Type
                            FROM TB_PURCHASE_DETAIL pd
                            JOIN TB_PURCHASE p ON pd.Purchase_No = p.Purchase_No
                            WHERE p.Is_Incoming = 'Y'
                            AND MONTH(Purchase_Date) = @Month
                            AND YEAR(Purchase_Date) = @year
                            GROUP BY Purchase_Date
                            UNION
                            SELECT 
	                            o.Order_Date Date, SUM(TotalPrice) Total, '매출' Type
                            FROM TB_ORDER_DETAIL od
                            JOIN TB_ORDER o ON od.Order_No = o.Order_No
                            WHERE o.Release_State = 'Y'
                            AND MONTH(Order_Date) = @month
                            AND YEAR(Order_Date) = @year
                            GROUP BY Order_Date ";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@month", month);
            cmd.Parameters.AddWithValue("@year", year);
            SqlDataReader reader = cmd.ExecuteReader();
            List<OrderPurchaseVO> list = DBHelper.DataReaderMapToList<OrderPurchaseVO>(reader);
            reader.Close();
            return list;
        }
    }
}