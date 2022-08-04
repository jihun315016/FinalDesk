using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace DESK_WEB.Models
{
    public class PopDAC
    {
        string strConn;

        public PopDAC()
        {
            strConn = WebConfigurationManager.ConnectionStrings["prjDB"].ConnectionString;
        }
        public PopVO GetUserLogin(int id)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = new SqlConnection(strConn);
                    cmd.CommandText = @"USP_PopLogin";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(@"@_UsID", id);

                    cmd.Connection.Open();
                    List<PopVO> list = DBHelper.DataReaderMapToList<PopVO>(cmd.ExecuteReader());
                    cmd.Connection.Close();

                    return list[0];
                }
            }
            catch (Exception err)
            {
                return null;
            }
        }
    }
}