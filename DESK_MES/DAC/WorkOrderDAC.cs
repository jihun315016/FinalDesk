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
        public List<WorkOrderVO> GetworkList(int code)
        {
            try
            {
                List<WorkOrderVO> list = new List<WorkOrderVO>();

                string sql = @"select Production_No, 
                                	  Work_Code, 
                                	  W.Product_Code AS Product_Code,
                                	  Product_Name,
                                	  Production_Operation_Code, 
                                	  Operation_Name AS Production_Operation_Name,
                                	  Production_Equipment_Code, 
                                	  Equipment_Name AS Production_Equipment_Name,
                                	  Halb_Save_Warehouse_Code,
                                	  WHA.Warehouse_Name AS Halb_Save_Warehouse_Name,
                                	  Input_Material_Code, 
                                	  Input_Material_Qty,
                                	  Halb_Material_Qty, 
                                	  Work_Group_Code,
                                	  User_Group_Name AS Work_Group_Name,
                                	  Production_Save_Warehouse_Code,
                                	  WHB.Warehouse_Name AS Production_Save_WareHouse_Name,
                                	  Work_Plan_Qty, 
                                	  Work_Complete_Qty,
                                	  convert(varchar(10), Work_Paln_Date, 23) Work_Paln_Date,
                                	  convert(varchar(10), Start_Due_Date, 23) Start_Due_Date,
                                	  convert(varchar(10), Start_Date, 23) Start_Date,
                                	  convert(varchar(10), Complete_Due_Date, 23) Complete_Due_Date,
                                	  convert(varchar(10), Complete_Date, 23) Complete_Date,
                                	  Work_Time, 
                                	  Work_Order_State, 
                                	  C.Name AS Work_Order_State_Name,
                                	  Material_Lot_Input_State,
                                	  CC.Name AS Material_Lot_Input_Name,
                                	  convert(varchar(10), W.Create_Time, 23) Create_Time,
                                	  W.Create_User_No,
                                      U.User_Name AS Create_User_Name,
                                      convert(varchar(10), W.Update_Time, 23) Update_Time,
                                      W.Update_User_No, 
                                	  UU.User_Name AS Update_User_Name,
                                	  Work_State
                               from TB_WORK W
                               left join TB_PRODUCT P on W.Product_Code=P.Product_Code
                               LEFT JOIN [dbo].[TB_USER] U ON W.Create_User_No=U.User_No
                               LEFT JOIN [dbo].[TB_USER] UU ON W.Update_User_No=UU.User_No
                               LEFT JOIN TB_OPERATION O ON W.Production_Operation_Code=O.Operation_No
                               LEFT JOIN TB_EQUIPMENT E ON W.Production_Equipment_Code=E.Equipment_No
                               LEFT JOIN TB_WAREHOUSE WHA ON W.Halb_Save_Warehouse_Code=WHA.Warehouse_Code
                               LEFT JOIN TB_WAREHOUSE WHB ON W.Halb_Save_Warehouse_Code=WHB.Warehouse_Code
                               LEFT JOIN TB_USER_GROUP UG ON W.Work_Group_Code=UG.User_Group_No
                               LEFT JOIN TB_COMMON_CODE C ON W.Work_Order_State=C.Code
                               LEFT JOIN TB_COMMON_CODE CC ON W.Material_Lot_Input_State=CC.Code
                               WHERE Production_No=@Production_No";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("Production_No", code);
                    list = DBHelpler.DataReaderMapToList<WorkOrderVO>(cmd.ExecuteReader());
                }
                return list;
            }
            catch (Exception err)
            {
                return null;
            }
        }


        public List<ProductVO> GetBomListForRegisert(string code) // BOM 정보 가져오기(등록용)
        {
            try
            {
                List<ProductVO> list = new List<ProductVO>();

                string sql = @"WITH BOM AS
                               (
                                    SELECT Product_Code, Product_Name, '1' as Qty
                                    FROM [dbo].[TB_PRODUCT]
                                    WHERE Product_Code = @Product_Code
                                    UNION ALL
                                    SELECT B.Child_Product_Code AS Product_Code, P.Product_Name, Qty
                                    FROM TB_BOM B
                                    INNER JOIN [dbo].[TB_PRODUCT] P ON B.Child_Product_Code=P.Product_Code
                                    WHERE Parent_Product_Code = @Product_Code
                               )
                               select * from BOM";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("Product_Code", code);
                    list = DBHelpler.DataReaderMapToList<ProductVO>(cmd.ExecuteReader());
                }
                return list;
            }
            catch (Exception err)
            {
                return null;
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
                               FROM TB_USER_GROUP
                               where User_Group_Type=103";

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
                                           Work_Order_State,                                             
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
                                       @Work_Order_State,
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
                    cmd.Parameters.Add("@Work_Order_State", System.Data.SqlDbType.Int);
                    cmd.Parameters.AddWithValue("@Create_Time", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Create_User_No", System.Data.SqlDbType.Int);


                    for (int i = 0; i < workIDList.Count; i++)
                    {
                        cmd.Parameters["@Work_Code"].Value = workIDList[i];
                        cmd.Parameters["@Production_No"].Value = workList[i].Production_No;
                        cmd.Parameters["@Product_Code"].Value = workList[i].Product_Code;
                        cmd.Parameters["@Production_Operation_Code"].Value = workList[i].Production_Operation_Code;
                        cmd.Parameters["@Production_Equipment_Code"].Value = workList[i].Production_Equipment_Code;
                        if (string.IsNullOrEmpty(workList[i].Input_Material_Code))
                            cmd.Parameters["@Input_Material_Code"].Value = DBNull.Value;
                        else
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
                        cmd.Parameters["@Work_Order_State"].Value = workList[i].Work_Order_State;
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

        public bool UpdateAllWorkOrderState(WorkOrderVO list) // 작업 상태 일괄 변경
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                try
                {
                    cmd.CommandText = @"UPDATE [dbo].[TB_WORK] SET Work_Order_State=@Work_Order_State,
                                                                   Material_Lot_Input_State=@Material_Lot_Input_State
                                                               WHERE Production_No = @Production_No";

                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@Production_No", list.Production_No);
                    cmd.Parameters.AddWithValue("@Work_Order_State", list.Work_Order_State);
                    cmd.Parameters.AddWithValue("@Material_Lot_Input_State", list.Material_Lot_Input_State);
                    cmd.ExecuteNonQuery();

                    return true;
                }
                catch (Exception err)
                {
                    return false;
                }
            }
        }

        public bool UpdateEachWorkOrderState(WorkOrderVO list) // 작업 상태 개별 변경
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                try
                {
                    cmd.CommandText = @"UPDATE [dbo].[TB_WORK] SET Work_Order_State=@Work_Order_State,
                                                                   Material_Lot_Input_State=@Material_Lot_Input_State
                                                               WHERE Work_Code = @Work_Code";

                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@Work_Code", list.Work_Code);
                    cmd.Parameters.AddWithValue("@Work_Order_State", list.Work_Order_State);
                    cmd.Parameters.AddWithValue("@Material_Lot_Input_State", list.Material_Lot_Input_State);
                    cmd.ExecuteNonQuery();

                    return true;
                }
                catch (Exception err)
                {
                    return false;
                }
            }
        }

        public bool InputAllMaterial(WorkOrderVO list, List<WorkOrderVO> workList)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    cmd.CommandText = @"UPDATE [dbo].[TB_WORK] 
                                        SET Material_Lot_Input_State=@Material_Lot_Input_State
                                        WHERE Production_No = @Production_No";

                    cmd.Connection = conn;
                    cmd.Transaction = trans;
                    cmd.Parameters.AddWithValue("@Production_No", list.Production_No);
                    cmd.Parameters.AddWithValue("@Material_Lot_Input_State", list.Material_Lot_Input_State);
                    cmd.ExecuteNonQuery();

                    // 반제품 수량 다운
                    // CHANGE_QUANTITY <= 현재수량 - 주문 수량
                    cmd.Parameters.Clear();
                    cmd.CommandText = @"UPDATE [dbo].[TB_WAREHOUSE_PRODUCT_RELATION]
                                        SET Product_Quantity = Product_Quantity - @Halb_Material_Qty
                                        WHERE Product_Code = @Product_Code";

                    cmd.Parameters.Add("@Product_Code", System.Data.SqlDbType.NVarChar);
                    cmd.Parameters.Add("@Halb_Material_Qty", System.Data.SqlDbType.Int);

                    foreach (WorkOrderVO item in workList)
                    {
                        cmd.Parameters["@Product_Code"].Value = item.Product_Code;
                        cmd.Parameters["@Halb_Material_Qty"].Value = item.Halb_Material_Qty;
                        cmd.ExecuteNonQuery();
                    }

                    // 원자재 수량 다운
                    // CHANGE_QUANTITY <= 현재수량 - 주문 수량
                    cmd.Parameters.Clear();
                    cmd.CommandText = @"UPDATE [dbo].[TB_MATERIAL_LOT]
                                        SET Cur_Qty = Cur_Qty - @Input_Material_Qty
                                        WHERE Lot_Code = @Lot_Code";

                    cmd.Parameters.Add("@Lot_Code", System.Data.SqlDbType.NVarChar);
                    cmd.Parameters.Add("@Input_Material_Qty", System.Data.SqlDbType.Int);

                    foreach (WorkOrderVO item in workList)
                    {
                        cmd.Parameters["@Lot_Code"].Value = item.Input_Material_Code;
                        cmd.Parameters["@Input_Material_Qty"].Value = item.Input_Material_Qty;
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

        public bool InputEachMaterial(WorkOrderVO list)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    cmd.CommandText = @"UPDATE [dbo].[TB_WORK] 
                                        SET Material_Lot_Input_State=@Material_Lot_Input_State
                                        WHERE Work_Code = @Work_Code";

                    cmd.Connection = conn;
                    cmd.Transaction = trans;
                    cmd.Parameters.AddWithValue("@Work_Code", list.Work_Code);
                    cmd.Parameters.AddWithValue("@Material_Lot_Input_State", list.Material_Lot_Input_State);
                    cmd.ExecuteNonQuery();

                    // 반제품 수량 다운
                    // CHANGE_QUANTITY <= 현재수량 - 주문 수량
                    cmd.Parameters.Clear();
                    cmd.CommandText = @"UPDATE [dbo].[TB_WAREHOUSE_PRODUCT_RELATION]
                                        SET Product_Quantity = Product_Quantity - @Halb_Material_Qty
                                        WHERE Product_Code = @Product_Code";

                    cmd.Parameters.AddWithValue("@Product_Code", list.Product_Code);
                    cmd.Parameters.AddWithValue("@Halb_Material_Qty", list.Halb_Material_Qty);
                    cmd.ExecuteNonQuery();

                    // 원자재 수량 다운
                    // CHANGE_QUANTITY <= 현재수량 - 주문 수량
                    cmd.Parameters.Clear();
                    cmd.CommandText = @"UPDATE [dbo].[TB_MATERIAL_LOT]
                                        SET Cur_Qty = Cur_Qty - @Input_Material_Qty
                                        WHERE Lot_Code = @Lot_Code";

                    cmd.Parameters.AddWithValue("@Lot_Code", list.Input_Material_Code);
                    cmd.Parameters.AddWithValue("@Input_Material_Qty", list.Input_Material_Qty);
                    cmd.ExecuteNonQuery();

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
    }
}
