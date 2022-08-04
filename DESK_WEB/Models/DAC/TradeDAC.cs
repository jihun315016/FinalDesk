using DESK_WEB.Models.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

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

        public List<WebPurchaseVO> GetPurchaseList(string startDate, string endDate, string keyword = "")
        {
            string sql = @"SELECT 
	                            pd.Purchase_No, p.Purchase_Date, p.Incoming_Date, Client_Name, 
	                            pd.Product_Code, Product_Name, TotalQty, Price, TotalPrice
                            FROM TB_PURCHASE_DETAIL pd
                            JOIN TB_PURCHASE p ON pd.Purchase_No = p.Purchase_No
                            JOIN TB_Client c ON p.Client_Code = c.Client_Code
                            JOIN TB_PRODUCT pr ON pd.Product_Code = pr.Product_Code
                            WHERE p.Is_Incoming = 'Y'
                            AND p.Purchase_Date <= @startDate
                            AND p.Purchase_Date >= @endDate
                            AND Client_Name LIKE %keywork ";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@startDate", startDate);
            cmd.Parameters.AddWithValue("@endDate", endDate);
            cmd.Parameters.AddWithValue("@keywork", $"%{keyword}%");
            SqlDataReader reader = cmd.ExecuteReader();
            List<WebPurchaseVO> list = DBHelper.DataReaderMapToList<WebPurchaseVO>(reader);
            return list;
        }
    }
}