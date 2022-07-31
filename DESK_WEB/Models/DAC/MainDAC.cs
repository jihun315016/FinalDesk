using DESK_DTO;
using DESK_WEB.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace DESK_WEB.Models.DAC
{
    public class MainDAC : IDisposable
    {
        SqlConnection conn;

        public MainDAC()
        {
            string connStr = ConfigurationManager.ConnectionStrings["prjDB"].ConnectionString;
            conn = new SqlConnection(connStr);
            conn.Open();
        }

        public void Dispose()
        {
            if (conn != null && conn.State == System.Data.ConnectionState.Open)
                conn.Close();
        }

        /// <summary>
        /// Author : 강지훈
        /// 웹 메뉴 리스트 조회
        /// 웹 메뉴는 Parent_Function_No 칼럼이 -1인 항목으로 약속한다.
        /// </summary>
        public List<MenuVO> GetWebMenuList()
        {
            string sql = @"SELECT Function_No, Function_Name, Parent_Function_No, frmName
                            FROM TB_FUNCTION
                            WHERE Parent_Function_No = -1 ";

            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            List<MenuVO> list = DBHelpler.DataReaderMapToList<MenuVO>(reader);
            reader.Close();
            return list;
        }
    }
}   