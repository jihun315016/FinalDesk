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
    public class ManufactuerDAC : IDisposable
    {
        SqlConnection conn;

        public ManufactuerDAC()
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

        // 주문서 가져오기(combobox용)
        public List<ManufactureVO> GetOrderList()
        {
            try
            {
                List<ManufactureVO> list = new List<ManufactureVO>();

                string sql = @"select Order_No
                               from TB_ORDER
                               where Order_State= 'DT'";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    list = DBHelpler.DataReaderMapToList<ManufactureVO>(cmd.ExecuteReader());
                }
                return list;
            }
            catch (Exception err)
            {
                return null;
            }
        }

        public List<ManufactureVO> GetOrderProductListForManufacture(int orderNo)
        {
            try
            {
                List<ManufactureVO> list = new List<ManufactureVO>();

                string sql = @"select OD.Product_Code Product_Code, Product_Name
                               from TB_ORDER_DETAIL OD
                               INNER JOIN TB_PRODUCT P ON  OD.Product_Code=P.Product_Code
                               WHERE Order_No=@Order_No";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("Order_No", orderNo);
                    list = DBHelpler.DataReaderMapToList<ManufactureVO>(cmd.ExecuteReader());
                }
                return list;
            }
            catch (Exception err)
            {
                return null;
            }
        }

        public List<ManufactureVO> GetProductListByType(string type)
        {
            try
            {
                List<ManufactureVO> list = new List<ManufactureVO>();

                string sql = @"select Product_Code, Product_Name
                               from TB_PRODUCT
                               where Product_Type=@Product_Type";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("Product_Type", type);
                    list = DBHelpler.DataReaderMapToList<ManufactureVO>(cmd.ExecuteReader());
                }
                return list;
            }
            catch (Exception err)
            {
                return null;
            }
        }

        public bool RegisterManufacturePlan(ManufactureVO plan)
        {
            try
            {
                string sql = @"INSERT INTO TB_RPODUCTION (Order_No, Product_Code, Planned_Qty, Start_Date, Estimated_Date, Production_Plan_Status, Production_Plan_User_No, Create_Time, Create_User_No)
                               VALUES (@Order_No, @Product_Code, @Planned_Qty, @Start_Date, @Estimated_Date, @Production_Plan_Status, @Production_Plan_User_No, @Create_Time, @Create_User_No)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Order_No", plan.Order_No);
                    cmd.Parameters.AddWithValue("@Product_Code", plan.Product_Code);
                    cmd.Parameters.AddWithValue("@Planned_Qty", plan.Planned_Qty);
                    cmd.Parameters.AddWithValue("@Start_Date", plan.Start_Date);
                    cmd.Parameters.AddWithValue("@Estimated_Date", plan.Estimated_Date);
                    cmd.Parameters.AddWithValue("@Production_Plan_Status", plan.Production_Plan_Status);
                    cmd.Parameters.AddWithValue("@Production_Plan_User_No", plan.Production_Plan_User_No);
                    cmd.Parameters.AddWithValue("@Create_Time", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Create_User_No", plan.Create_User_No);

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

        public List<ManufactureVO> GetmanufactureList()
        {
            try
            {
                List<ManufactureVO> list = new List<ManufactureVO>();

                string sql = @"select Production_Code,
                               	      Order_No,
                               	      M.Product_Code,
                               	      Product_Name, 
                               	      Planned_Qty, 
                               	      Production_Qty, 
                               	      CONVERT(VARCHAR(20), Start_Date, 20) Start_Date,
                               	      CONVERT(VARCHAR(20), Estimated_Date, 20) Estimated_Date,
                               	      CONVERT(VARCHAR(20), Complete_Date, 20) Complete_Date,
                               	      Production_Plan_Status,
                               	      Production_Status,
                               	      Production_Plan_User_No,
                               	      UUU.User_Name AS Production_Plan_User_Name, 
                               	      CONVERT(VARCHAR(20), M.Create_Time, 20) Create_Time,
                               	      M.Create_User_No,
                               	      U.User_Name AS Create_User_Name,
                               	      CONVERT(VARCHAR(20), M.Update_Time, 20) Update_Time,
                               	      M.Update_User_No,
                               	      UU.User_Name AS Update_User_No
                               from TB_RPODUCTION M
                               LEFT JOIN [dbo].[TB_PRODUCT] P ON M.Product_Code=P.Product_Code
                               LEFT JOIN [dbo].[TB_USER] U ON M.Create_User_No=U.User_No
                               LEFT JOIN [dbo].[TB_USER] UU ON M.Update_User_No=UU.User_No
                               LEFT JOIN [dbo].[TB_USER] UUU ON M.Production_Plan_User_No=UUU.User_No";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    list = DBHelpler.DataReaderMapToList<ManufactureVO>(cmd.ExecuteReader());
                }
                return list;
            }
            catch (Exception err)
            {
                return null;
            }
        }

        public bool UpdateManufactureState(ManufactureVO plan)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                try
                {
                    cmd.CommandText = @"Update [dbo].[TB_RPODUCTION] set Production_Plan_Status=@Production_Plan_Status,
                                        Update_Time=@Update_Time,
                                        Update_User_No=@Update_User_No
                                        where Production_Code=@Production_Code";

                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@Production_Code", plan.Production_Code);
                    cmd.Parameters.AddWithValue("@Production_Plan_Status", plan.Production_Plan_Status);
                    cmd.Parameters.AddWithValue("@Update_Time", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Update_User_No", plan.Update_User_No);
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
