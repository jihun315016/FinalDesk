using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;


namespace DESK_WEB.Models
{
    public class WarehouseDAC
    {
        string strConn;

        public WarehouseDAC()
        {
            strConn = WebConfigurationManager.ConnectionStrings["prjDB"].ConnectionString;
        }

        public List<WarehouseVO> GetAllWarehouse()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select Warehouse_Code, 
                                           Warehouse_Name, 
                                           Warehouse_Address,
                                           Warehouse_Type,
                                           CONVERT(VARCHAR(20), Create_Time, 20) Create_Time,
                                           Create_User_No,
                                           CONVERT(VARCHAR(20), Update_Time, 20) Update_Time, 
                                           Update_User_No,
                                           Is_Delete
                                    from TB_WAREHOUSE";

                cmd.Connection.Open();
                List<WarehouseVO> list = Helper.DataReaderMapToList<WarehouseVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }
        public WarehouseVO GetWarehouseInfo(string id)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select Warehouse_Code, 
                                           Warehouse_Name, 
                                           Warehouse_Address,
                                           Warehouse_Type,
                                           CONVERT(VARCHAR(20), Create_Time, 20) Create_Time, 
                                           Create_User_No, 
                                           CONVERT(VARCHAR(20), Update_Time, 20) Update_Time, 
                                           Update_User_No, 
                                           Is_Delete
                                    from TB_WAREHOUSE
                                    where Warehouse_Code=@Warehouse_Code";
                cmd.Parameters.AddWithValue("@Warehouse_Code", id);

                cmd.Connection.Open();
                List<WarehouseVO> list = Helper.DataReaderMapToList<WarehouseVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                if (list != null && list.Count > 0)
                    return list[0];
                else
                    return null;
            }
        }

        public bool SaveWarehouse(WarehouseVO warehouse)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"INSERT INTO TB_WAREHOUSE (Warehouse_Code, Warehouse_Name, Warehouse_Address, Warehouse_Type, Create_Time)
                                    VALUES (@Warehouse_Code, @Warehouse_Name, @Warehouse_Address, @Warehouse_Type, @Create_Time)";

                cmd.Parameters.AddWithValue("@Warehouse_Code", warehouse.Warehouse_Code);
                cmd.Parameters.AddWithValue("@Warehouse_Name", warehouse.Warehouse_Name);
                cmd.Parameters.AddWithValue("@Warehouse_Address", warehouse.Warehouse_Address);
                cmd.Parameters.AddWithValue("@Warehouse_Type", warehouse.Warehouse_Type);
                cmd.Parameters.AddWithValue("@Create_Time", DateTime.Now);

                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }

        public bool UpdateWarehouse(WarehouseVO warehouse)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"UPDATE TB_WAREHOUSE SET Warehouse_Name = @Warehouse_Name,
                                                            Warehouse_Address = @Warehouse_Address,
                                                            Warehouse_Type = @Warehouse_Type
                                    WHERE Warehouse_Code= @Warehouse_Code";

                cmd.Parameters.AddWithValue("@Warehouse_Code", warehouse.Warehouse_Code);
                cmd.Parameters.AddWithValue("@Warehouse_Name", warehouse.Warehouse_Name);
                cmd.Parameters.AddWithValue("@Warehouse_Address", warehouse.Warehouse_Address);
                cmd.Parameters.AddWithValue("@Warehouse_Type", warehouse.Warehouse_Type);

                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }

        public bool DeleteWarehouse(string warehouseNO)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"UPDATE TB_WAREHOUSE SET Is_Delete = 'Y'
                                    WHERE Warehouse_Code= @Warehouse_Code";

                cmd.Parameters.AddWithValue("@Warehouse_Code", warehouseNO);

                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }
    }
}