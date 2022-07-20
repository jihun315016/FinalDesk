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
    public class MenuDAC : IDisposable
    {
        SqlConnection conn;

        public MenuDAC()
        {
            string connstr = ConfigurationManager.ConnectionStrings["prjDB"].ConnectionString;
            conn = new SqlConnection(connstr);
            conn.Open();
        }

        public void Dispose()
        {
            if(conn != null && conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        public List<MenuVO> GetMenuList(int no)
        {
            string sql = @"WITH TB_FUNCTION_CTE AS
                           (
                           	SELECT Function_No, Function_Name, Parent_Function_No, frmName
                           	FROM TB_FUNCTION
                           	WHERE Function_No IN (SELECT Function_No FROM TB_USER_GROUP_FUNCTION_RELATION WHERE User_Group_No = @User_Group_No)
                           
                           	UNION ALL
                           
                           	SELECT P.Function_No, P.Function_Name, P.Parent_Function_No, P.frmName
                           	FROM TB_FUNCTION_CTE C
                           	JOIN TB_FUNCTION P ON C.Parent_Function_No = P.Function_No
                           )
                           SELECT DISTINCT Function_No, Function_Name, Parent_Function_No, frmName
                           FROM TB_FUNCTION_CTE
                           ORDER BY Parent_Function_No, Function_No";

            SqlCommand cmd = new SqlCommand(sql,conn);
            cmd.Parameters.AddWithValue("@User_Group_No", no);
            SqlDataReader reader = cmd.ExecuteReader();
            return DBHelpler.DataReaderMapToList<MenuVO>(reader);

        }

    }
}
