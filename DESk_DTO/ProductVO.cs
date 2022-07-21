using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESK_DTO
{
    public class ProductVO
    {
        public string Product_Code { get; set; }
        public string Product_Name { get; set; }
        public string Product_Type { get; set; }
        public int Is_Image { get; set; }
        public int Price { get; set; }
        public int Unit { get; set; }
        public DateTime Create_Time { get; set; }
        public int Create_User_No { get; set; }
        public DateTime Update_Time { get; set; }
        public int Update_User_No { get; set; }
        public string Is_Delete { get; set; }

        public string Create_User_Name { get; set; }
        public string Update_User_Name { get; set; }
    }
}
