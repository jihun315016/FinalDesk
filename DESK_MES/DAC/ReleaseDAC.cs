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
    public class ReleaseDAC : IDisposable
    {
        SqlConnection conn;

        public ReleaseDAC()
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

        public List<ReleaseVO> GetReleaseList() // 출고와 관련된 주문 정보 가져오기(주문 확정인 것들만)
        {
            try
            {
                List<ReleaseVO> list = new List<ReleaseVO>();

                string sql = @"select Order_No, 
                               	      OD.Client_Code,
                               	      Client_Name,
                                      convert(varchar(10), Order_Date, 23) Order_Date,
                                      Order_State,
                                      convert(varchar(10), Release_Date, 23) Release_Date,
                               	      Release_State,
                                      convert(varchar(10), Release_OK_Date, 23) Release_OK_Date,
									  OD.Create_User_No,
									  U.User_Name AS Create_User_Name,
                                      convert(varchar(10), OD.Update_Time, 23) Update_Time,
                               	      OD.Update_User_No, 
									  UU.User_Name AS Update_User_Name
                               from [dbo].[TB_ORDER] OD
                               LEFT JOIN [dbo].[TB_Client] C ON OD.Client_Code=C.Client_Code
							   LEFT JOIN [dbo].[TB_USER] U ON OD.Create_User_No=U.User_No
							   LEFT JOIN [dbo].[TB_USER] UU ON OD.Update_User_No=UU.User_No
                               where Order_State='DT'";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    list = DBHelpler.DataReaderMapToList<ReleaseVO>(cmd.ExecuteReader());
                }
                return list;
            }
            catch (Exception err)
            {
                return null;
            }
        }

        public bool RegisterRelease(ReleaseVO release, List<OrderDetailVO> orderList, List<string> idlist)  
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    cmd.CommandText = @"UPDATE [dbo].[TB_ORDER]
                                        SET Release_OK_Date = @Release_OK_Date,
                                            Release_State = @Release_State,
                                            Update_Time = @Update_Time,
                                            Update_User_No = @Update_User_No
                                        WHERE Order_No = @Order_No";

                    cmd.Connection = conn;
                    cmd.Transaction = trans;
                    cmd.Parameters.AddWithValue("@Order_No", release.Order_No);
                    cmd.Parameters.AddWithValue("@Release_OK_Date", release.Release_OK_Date);
                    cmd.Parameters.AddWithValue("@Release_State", release.Release_State);
                    cmd.Parameters.AddWithValue("@Update_Time", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Update_User_No", release.Update_User_No);
                    cmd.ExecuteNonQuery();

                    // ITEM 수량 업데이트
                    // CHANGE_QUANTITY <= 현재수량 - 주문 수량
                    cmd.Parameters.Clear();
                    cmd.CommandText = @"UPDATE [dbo].[TB_WAREHOUSE_PRODUCT_RELATION]
                                        SET Product_Quantity = Product_Quantity - @Product_Quantity
                                        WHERE Product_Code = @Product_Code";

                    cmd.Parameters.Add("@Product_Code", System.Data.SqlDbType.NVarChar);
                    cmd.Parameters.Add("@Product_Quantity", System.Data.SqlDbType.Int);

                    foreach (OrderDetailVO item in orderList)
                    {
                        cmd.Parameters["@Product_Code"].Value = item.Product_Code;
                        cmd.Parameters["@Product_Quantity"].Value = item.TotalQty;
                        cmd.ExecuteNonQuery();
                    }

                    cmd.Parameters.Clear();
                    cmd.CommandText = @"UPDATE TB_ORDER_DETAIL
                                        SET BarcodeID = @BarcodeID
                                        WHERE Order_No = @Order_No";

                    cmd.Parameters.Add("@BarcodeID", System.Data.SqlDbType.NVarChar);
                    cmd.Parameters.AddWithValue("@Order_No", release.Order_No);

                    for (int i = 0; i < idlist.Count; i++)
                    {
                        cmd.Parameters["@BarcodeID"].Value = idlist[i];
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

        public DataTable GetBoxOutputList()
        {
            string sql = @"select BarcodeID, OD.Product_Code, Product_Name, Unit, TotalQty, convert(varchar(10), Release_OK_Date, 23) Release_OK_Date
                           from [dbo].[TB_ORDER_DETAIL] OD
                           inner join [dbo].[TB_ORDER] O ON OD.Order_No=O.Order_No
                           inner join [dbo].[TB_PRODUCT] P ON OD.Product_Code=P.Product_Code
                           where Release_State= 'Y'
                           order by BarcodeID desc";
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.SelectCommand.Connection.Close();
            return dt;
        }

        public DataTable GetPrintBoxOutputLabel(string selList)
        {
            string sql = @"select BarcodeID, OD.Product_Code, Product_Name, Unit, TotalQty, convert(varchar(10), Release_OK_Date, 23) Release_OK_Date
                           from [dbo].[TB_ORDER_DETAIL] OD
                           inner join [dbo].[TB_ORDER] O ON OD.Order_No=O.Order_No
                           inner join [dbo].[TB_PRODUCT] P ON OD.Product_Code=P.Product_Code where BarcodeID in (" + selList + ")";

            SqlDataAdapter da = new SqlDataAdapter(sql,conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.SelectCommand.Connection.Close();
            return dt;
        }

        public OrderDetailVO GetLastID() // 새롭게 등록할 바코드ID 가져오기
        {
            try
            {
                string sql = @"select TOP(1) BarcodeID
                               from TB_ORDER_DETAIL
                               ORDER BY BarcodeID DESC";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    List<OrderDetailVO> list = DBHelpler.DataReaderMapToList<OrderDetailVO>(cmd.ExecuteReader());

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
    }
}
