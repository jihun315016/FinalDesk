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

        /// <summary>
        /// Author : 강지훈
        /// 로그인 체크 후 결과 반환
        /// </summary>
        /// <param name="userNo"></param>
        /// <param name="userPwd"></param>
        /// <returns></returns>
        public bool CheckLogin(string userNo, string userPwd)
        {
            string sql = @"SELECT count(*)
                            FROM TB_USER
                            WHERE User_No=@User_No AND User_Pwd=@User_Pwd AND User_Group_No='1001' ";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@User_No", userNo);
            cmd.Parameters.AddWithValue("@User_Pwd", userPwd);
            int iRow = Convert.ToInt32(cmd.ExecuteScalar());
            return iRow > 0;
        }
    }
}   