﻿using DESK_DTO;
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
            conn.Open();
        }

        public void Dispose()
        {
            if (conn != null && conn.State == System.Data.ConnectionState.Open)
            {
                conn.Dispose();
            }
        }

        public List<OperationVO> GetOperationList(int no)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"select 
	                            Operation_No, Operation_Name, Is_Check_Deffect, Is_Check_Inspect, Is_Check_Marerial, 
	                            o.Create_Time, o.Create_User_No, u.User_Name Create_User, o.Update_Time, o.Update_User_No, uu.User_Name Update_User 
                            from TB_OPERATION o
                            LEFT JOIN TB_USER u ON o.Create_User_No = u.User_No
                            LEFT JOIN TB_USER uu ON o.Update_User_No = uu.User_No ");

            if (no > 0)
                sb.Append(" WHERE Operation_No = @no ");

            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.AddWithValue("@no", no);
            SqlDataReader reader = cmd.ExecuteReader();
            List<OperationVO> list = DBHelpler.DataReaderMapToList<OperationVO>(reader);
            reader.Close();
            return list;
        }

        public bool SaveOperation(OperationVO oper)
        {
            string sql = @"INSERT INTO TB_OPERATION
                            (Operation_Name, Is_Check_Deffect, Is_Check_Inspect, Is_Check_Marerial, Create_Time, Create_User_No)
                            VALUES
                            (@Operation_Name, @Is_Check_Deffect, @Is_Check_Inspect, @Is_Check_Marerial, CONVERT([char](19),getdate(),(20)), @Create_User_No) ";

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
