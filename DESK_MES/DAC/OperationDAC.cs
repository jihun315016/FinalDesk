using DESK_DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESK_MES.DAC
{
    public class OperationDAC : IDisposable
    {
        SqlConnection conn;

        public OperationDAC()
        {
            string connStr = ConfigurationManager.ConnectionStrings["prjDB"].ConnectionString;
            conn = new SqlConnection(connStr);
        }

        public void Dispose()
        {
            if (conn != null && conn.State == System.Data.ConnectionState.Open)
            {
                conn.Dispose();
            }
        }

        public bool SaveProcess(OperationVO oper)
        {
            string sql = @"INSERT INTO TB_OPERATION
                            (Operation_Name, Is_Check_Deffect, Is_Check_Inspect, Is_Check_Marerial, Create_Time, Create_User_No, Is_Delete)
                            VALUES
                            (@Operation_Name, @Is_Check_Deffect, @Is_Check_Inspect, @Is_Check_Marerial, CONVERT([char](19),getdate(),(20)), @Create_User_No, 'N') ";

            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.Parameters.AddWithValue("@Operation_Name", oper.Operation_Name);
                cmd.Parameters.AddWithValue("@Is_Check_Deffect", oper.Is_Check_Deffect);
                cmd.Parameters.AddWithValue("@Is_Check_Inspect", oper.Is_Check_Inspect);
                cmd.Parameters.AddWithValue("@Is_Check_Marerial", oper.Is_Check_Marerial);
                cmd.Parameters.AddWithValue("@Create_User_No", oper.Create_User_No);

                int iRow = cmd.ExecuteNonQuery();
                return iRow > 0;
            }
            catch (Exception err)
            {
                return false;
            }
        }
    }
}
