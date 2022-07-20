using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DESK_DTO;
using System.Data;
using System.Data.SqlClient;

namespace DESK_MES
{
    public class MenuDAC : IDisposable
    {
        SqlConnection conn;

        public MenuDAC()
        {
            //string connstr = 
        }

        public void Dispose()
        {
            if(conn != null && conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }
}
