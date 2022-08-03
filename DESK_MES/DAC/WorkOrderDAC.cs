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
    public class WorkOrderDAC : IDisposable
    {
        SqlConnection conn;

        public WorkOrderDAC()
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

        public List<OperationVO> GetOperationList(string code) // 공정 콤보박스
        {
            try
            {
                List<OperationVO> list = new List<OperationVO>();

                string sql = @"select po.Operation_No as Operation_No, Operation_Name
                               from [dbo].[TB_PRODUCT_OPERATION_RELATION] PO
                               inner join [dbo].[TB_OPERATION] O on po.Operation_No=o.Operation_No
                               where Product_Code=@Product_Code";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("Product_Code", code);
                    list = DBHelpler.DataReaderMapToList<OperationVO>(cmd.ExecuteReader());
                }
                return list;
            }
            catch (Exception err)
            {
                return null;
            }
        }

        public List<EquipmentVO> GetProcessList(int operationNo) // 설비 콤보박스
        {
            try
            {
                List<EquipmentVO> list = new List<EquipmentVO>();

                string sql = @"SELECT EO.Equipment_No AS Equipment_No, Equipment_Name
                               FROM TB_EQUIPMENT_OPERATION_RELATION EO
                               INNER JOIN [dbo].[TB_EQUIPMENT] E ON EO.Equipment_No=E.Equipment_No
                               WHERE Operation_No = @Operation_No";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("Operation_No", operationNo);
                    list = DBHelpler.DataReaderMapToList<EquipmentVO>(cmd.ExecuteReader());
                }
                return list;
            }
            catch (Exception err)
            {
                return null;
            }
        }

        public List<PurchaseDetailVO> GetOutputWarehouse(string no) // 자재창고 콤보박스
        {
            try
            {
                List<PurchaseDetailVO> list = new List<PurchaseDetailVO>();

                string sql = @"select WP.Warehouse_Code Warehouse_Code, Warehouse_Name
                               from [dbo].[TB_WAREHOUSE_PRODUCT_RELATION] WP
                               INNER JOIN [dbo].[TB_WAREHOUSE] W on WP.Warehouse_Code=W.Warehouse_Code
                               WHERE Product_Code=@Product_Code";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("Product_Code", no);
                    list = DBHelpler.DataReaderMapToList<PurchaseDetailVO>(cmd.ExecuteReader());
                }
                return list;
            }
            catch (Exception err)
            {
                return null;
            }
        }
        public List<PurchaseDetailVO> GetMetarialLotList(string no) // 자재LOT 콤보박스
        {
            try
            {
                List<PurchaseDetailVO> list = new List<PurchaseDetailVO>();

                string sql = @"select Lot_Code, P.Product_Name as Product_Name
                               from [dbo].[TB_WAREHOUSE_PRODUCT_RELATION] WP
                               left join [dbo].[TB_MATERIAL_LOT] ML on ML.Warehouse_Code=WP.Warehouse_Code
                               left join [dbo].[TB_PRODUCT] P on ML.Product_Code=P.Product_Code
                               where WP.Warehouse_Code='WRHS_0001'";
                               
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    //cmd.Parameters.AddWithValue("Warehouse_Code", no);
                    list = DBHelpler.DataReaderMapToList<PurchaseDetailVO>(cmd.ExecuteReader());
                }
                return list;
            }
            catch (Exception err)
            {
                return null;
            }
        }
    }
}
