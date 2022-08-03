using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESK_DTO
{
    public class WorkOrderVO
    {
        public int Production_No { get; set; }
        public string Work_Code { get; set; }
        public string Product_Code { get; set; }
        public string Product_Name { get; set; }
        public int Production_Operation_Code { get; set; }
        public string Production_Operation_Name { get; set; }
        public int Production_Equipment_Code { get; set; }
        public string Production_Equipment_Name { get; set; }
        public string Output_Warehouse_Code { get; set; }
        public string Output_Warehouse_Name { get; set; }
        public string Input_Material_Code { get; set; }
        public string Input_Material_Name { get; set; }
        public int Bom_Qty { get; set; }
        public int Input_Material_Qty { get; set; }
        public int Production_Qty { get; set; }
        public int Work_Group_Code { get; set; }
        public string Work_Group_Name { get; set; }
        public string Input_Warehouse_Code { get; set; }
        public string Input_Warehouse_Name { get; set; }
        public int Planned_Qty { get; set; }
        public int Work_Plan_Qty { get; set; }
        public int Work_Complete_Qty { get; set; }
        public string Work_Date { get; set; }
        public string Start_Due_Date { get; set; }
        public string Start_Date { get; set; }
        public string Complete_Due_Date { get; set; }
        public string Complete_Date { get; set; }
        public string Work_Time { get; set; }
        public string Work_State { get; set; }
        public string Material_Lot_Input_State { get; set; }
        public string Create_Time { get; set; }
        public int Create_User_No { get; set; }
        public string Create_User_Name { get; set; }
        public string Update_Time { get; set; }
        public int Update_User_No { get; set; }
        public string Update_User_Name { get; set; }
    }
}
