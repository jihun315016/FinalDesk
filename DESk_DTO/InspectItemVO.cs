using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESK_DTO
{
    public class InspectItemVO
    {
        public int Inspect_No { get; set; }
        public string Inspect_Name { get; set; }
        public int Target { get; set; }
        public int USL { get; set; }
        public int LSL { get; set; }
        public DateTime Create_Time { get; set; }
        public int Create_User_No { get; set; }
        public DateTime Update_Time { get; set; }
        public int Update_User_No { get; set; }

        public string Create_User_Name { get; set; }
        public string Update_User_Name { get; set; }
    }
}
