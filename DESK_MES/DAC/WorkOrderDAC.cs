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
        public List<PurchaseDetailVO> GetMetarialList(string no) // 창고에 보관된 자재 목록 가져오기
        {
            try
            {
                List<PurchaseDetailVO> list = new List<PurchaseDetailVO>();

                string sql = @"select W.Product_Code, Product_Name
                                      from [dbo].[TB_WAREHOUSE_PRODUCT_RELATION] W
                                      inner join [dbo].[TB_PRODUCT] P on W.Product_Code=P.Product_Code
                                      where Warehouse_Code=@Warehouse_Code";
                               
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

        public List<PurchaseDetailVO> GetMetarialLotList(string no) // 창고에 보관된 자재의 자재 lot목록 가져오기
        {
            try
            {
                List<PurchaseDetailVO> list = new List<PurchaseDetailVO>();

                string sql = @"select Lot_Code
                               from [dbo].[TB_MATERIAL_LOT] L
                               inner join [dbo].[TB_PRODUCT] P on L.Product_Code = P.Product_Code
                               WHERE L.Product_Code=@Product_Code";

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

        public WorkOrderVO GetLastID() // 새롭게 등록할 작업id 가져오기
        {
            try
            {
                string sql = @"select TOP(1) [Work_Code]
                               from  [dbo].[TB_WORK]
                               ORDER BY [Work_Code] DESC";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    List<WorkOrderVO> list = DBHelpler.DataReaderMapToList<WorkOrderVO>(cmd.ExecuteReader());

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


        public bool RegisterWorkOrderList(List<WorkOrderVO> workList, List<string> workIDList) //작업지시 등록
        {
            try
            {
                string sql = @"INSERT INTO [dbo].[TB_WORK]
                                          (Production_No, 
                                           Work_Code, 
                                           Product_Code, 
                                           Production_Operation_Code, 
                                           Production_Equipment_Code, 
                                           Input_Material_Code, 
                                           Input_Material_Qty, 
                                           Halb_Material_Qty, 
                                           Work_Group_Code, 
                                           Halb_Save_Warehouse_Code, 
                                           Production_Save_Warehouse_Code,
                                           Work_Plan_Qty,
                                           Work_Paln_Date, 
                                           Start_Due_Date,
                                           Complete_Due_Date,
                                           Work_State, 
                                           Material_Lot_Input_State, 
                                           Create_Time,
                                           Create_User_No)
                               VALUES (@Production_No, 
                                       @Work_Code, 
                                       @Product_Code, 
                                       @Production_Operation_Code, 
                                       @Production_Equipment_Code, 
                                       @Input_Material_Code, 
                                       @Input_Material_Qty, 
                                       @Halb_Material_Qty, 
                                       @Work_Group_Code, 
                                       @Halb_Save_Warehouse_Code, 
                                       @Production_Save_Warehouse_Code,
                                       @Work_Plan_Qty,
                                       @Work_Paln_Date, 
                                       @Start_Due_Date,
                                       @Complete_Due_Date,
                                       @Work_State, 
                                       @Material_Lot_Input_State, 
                                       @Create_Time,
                                       @Create_User_No)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@Production_No", System.Data.SqlDbType.Int);
                    cmd.Parameters.Add("@Work_Code", System.Data.SqlDbType.NVarChar);
                    cmd.Parameters.Add("@Product_Code", System.Data.SqlDbType.NVarChar);
                    cmd.Parameters.Add("@Production_Operation_Code", System.Data.SqlDbType.Int);
                    cmd.Parameters.Add("@Production_Equipment_Code", System.Data.SqlDbType.Int);
                    cmd.Parameters.Add("@Input_Material_Code", System.Data.SqlDbType.NVarChar);
                    cmd.Parameters.Add("@Input_Material_Qty", System.Data.SqlDbType.Int);
                    cmd.Parameters.Add("@Halb_Material_Qty", System.Data.SqlDbType.Int);
                    cmd.Parameters.Add("@Work_Group_Code", System.Data.SqlDbType.Int);
                    cmd.Parameters.Add("@Halb_Save_Warehouse_Code", System.Data.SqlDbType.NVarChar);
                    cmd.Parameters.Add("@Production_Save_Warehouse_Code", System.Data.SqlDbType.NVarChar);
                    cmd.Parameters.Add("@Work_Plan_Qty", System.Data.SqlDbType.Int);
                    cmd.Parameters.Add("@Work_Paln_Date", System.Data.SqlDbType.DateTime);
                    cmd.Parameters.Add("@Start_Due_Date", System.Data.SqlDbType.DateTime);
                    cmd.Parameters.Add("@Complete_Due_Date", System.Data.SqlDbType.DateTime);
                    cmd.Parameters.Add("@Work_State", System.Data.SqlDbType.NVarChar);
                    cmd.Parameters.Add("@Material_Lot_Input_State", System.Data.SqlDbType.NVarChar);
                    cmd.Parameters.AddWithValue("@Create_Time", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Create_User_No", System.Data.SqlDbType.Int);


                    for (int i = 0; i < workIDList.Count; i++)
                    {
                        cmd.Parameters["@Work_Code"].Value = workIDList[i];
                        cmd.Parameters["@Production_No"].Value = workList[i].Production_No;
                        cmd.Parameters["@Product_Code"].Value = workList[i].Product_Code;
                        cmd.Parameters["@Production_Operation_Code"].Value = workList[i].Production_Operation_Code;
                        cmd.Parameters["@Production_Equipment_Code"].Value = workList[i].Production_Equipment_Code;
                        cmd.Parameters["@Input_Material_Code"].Value = workList[i].Input_Material_Code;
                        cmd.Parameters["@Input_Material_Qty"].Value = workList[i].Input_Material_Qty;
                        cmd.Parameters["@Halb_Material_Qty"].Value = workList[i].Halb_Material_Qty;
                        cmd.Parameters["@Work_Group_Code"].Value = workList[i].Work_Group_Code;
                        cmd.Parameters["@Halb_Save_Warehouse_Code"].Value = workList[i].Halb_Save_Warehouse_Code;
                        cmd.Parameters["@Production_Save_Warehouse_Code"].Value = workList[i].Production_Save_WareHouse_Code;
                        cmd.Parameters["@Work_Plan_Qty"].Value = workList[i].Work_Plan_Qty;
                        cmd.Parameters["@Work_Paln_Date"].Value = workList[i].Work_Paln_Date;
                        cmd.Parameters["@Start_Due_Date"].Value = workList[i].Start_Due_Date;
                        cmd.Parameters["@Complete_Due_Date"].Value = workList[i].Complete_Due_Date;
                        cmd.Parameters["@Work_State"].Value = workList[i].Work_State;
                        cmd.Parameters["@Material_Lot_Input_State"].Value = workList[i].Material_Lot_Input_State;
                        cmd.Parameters["@Create_User_No"].Value = workList[i].Create_User_No;

                        cmd.ExecuteNonQuery();
                    }


                    
                    return true;
                }

                return true;

            }
            catch (Exception err)
            {
                return false;
            }
        }
    }
}
