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

        public List<PurchaseDetailVO> GetProductionSaveWarehouse(string code) // 생산품 저장 창고 콤보박스
        {
            try
            {
                List<PurchaseDetailVO> list = new List<PurchaseDetailVO>();

                string sql = @"SELECT W.Warehouse_Code, Warehouse_Name 
                               FROM TB_WAREHOUSE W
                               inner join TB_WAREHOUSE_PRODUCT_RELATION WP on w.Warehouse_Code=WP.Warehouse_Code
                               where Product_Code=@Product_Code";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("Product_Code", code);
                    list = DBHelpler.DataReaderMapToList<PurchaseDetailVO>(cmd.ExecuteReader());
                }
                return list;
            }
            catch (Exception err)
            {
                return null;
            }
        }

        public List<PurchaseDetailVO> GetOutputWarehouse() // 자재 투입 창고 콤보박스
        {
            try
            {
                List<PurchaseDetailVO> list = new List<PurchaseDetailVO>();

                string sql = @"SELECT Warehouse_Code, Warehouse_Name 
                               FROM TB_WAREHOUSE
                               WHERE Warehouse_Type='자재'";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    list = DBHelpler.DataReaderMapToList<PurchaseDetailVO>(cmd.ExecuteReader());
                }
                return list;
            }
            catch (Exception err)
            {
                return null;
            }
        }

        public List<UserGroupVO> GetWorkGroupList() // 작업 담당팀 창고 콤보박스
        {
            try
            {
                List<UserGroupVO> list = new List<UserGroupVO>();

                string sql = @"SELECT User_Group_No, User_Group_Name 
                               FROM TB_USER_GROUP";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    list = DBHelpler.DataReaderMapToList<UserGroupVO>(cmd.ExecuteReader());
                }
                return list;
            }
            catch (Exception err)
            {
                return null;
            }
        }

        public List<PurchaseDetailVO> GetInputWarehouse(string no) // 반제품 보관 창고 콤보박스
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

                string sql = @"select Lot_Code, Product_Name
                               from [dbo].[TB_MATERIAL_LOT] ML
                               INNER join [dbo].[TB_PRODUCT] P on ML.Product_Code=P.Product_Code
                               WHERE Warehouse_Code=@Warehouse_Code";
                               
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("Warehouse_Code", no);
                    list = DBHelpler.DataReaderMapToList<PurchaseDetailVO>(cmd.ExecuteReader());
                }
                return list;
            }
            catch (Exception err)
            {
                return null;
            }
        }


        //public bool RegisterWorkOrderList(List<WorkOrderVO> workList, List<string> workIDList) //작업지시 등록
        //{
        //    using (SqlCommand cmd = new SqlCommand())
        //    {
        //        SqlTransaction trans = conn.BeginTransaction();
        //        try
        //        {
        //            cmd.CommandText = @"INSERT INTO [dbo].[TB_MATERIAL_LOT] (Lot_Code, Product_Code, Client_Code, Lot_Time, Lot_Qty, Cur_Qty, Warehouse_Code, Create_Time)
        //                                VALUES (@Lot_Code, @Product_Code, @Client_Code, @Lot_Time, @Lot_Qty, @Cur_Qty, @Warehouse_Code, @Create_Time)";

        //            cmd.Parameters.Add("@Lot_Code", System.Data.SqlDbType.NVarChar);
        //            cmd.Parameters.Add("@Product_Code", System.Data.SqlDbType.NVarChar);
        //            cmd.Parameters.AddWithValue("@Client_Code", System.Data.SqlDbType.NVarChar);
        //            cmd.Parameters.AddWithValue("@Lot_Time", DateTime.Now);
        //            cmd.Parameters.Add("@Lot_Qty", System.Data.SqlDbType.Int);
        //            cmd.Parameters.Add("@Cur_Qty", System.Data.SqlDbType.Int);
        //            cmd.Parameters.AddWithValue("@Warehouse_Code", System.Data.SqlDbType.NVarChar);
        //            cmd.Parameters.AddWithValue("@Create_Time", DateTime.Now);

        //            for (int i = 0; i < workIDList.Count; i++)
        //            {
        //                //cmd.Parameters["@Lot_Code"].Value = workIDList[i];
        //                //cmd.Parameters["@Product_Code"].Value = workList[i].Product_Code;
        //                //cmd.Parameters["@Client_Code"].Value = workList[i].Client_Code;
        //                //cmd.Parameters["@Warehouse_Code"].Value = workList[i].Warehouse_Code;
        //                //cmd.Parameters["@Lot_Qty"].Value = workList[i].Lot_Qty;
        //                //cmd.Parameters["@Cur_Qty"].Value = workList[i].Cur_Qty;

        //                cmd.ExecuteNonQuery();
        //            }

        //            trans.Commit();
        //            return true;
        //        }
        //        catch (Exception err)
        //        {
        //            trans.Rollback();
        //            return false;
        //        }
        //    }
        //}
    }
}
