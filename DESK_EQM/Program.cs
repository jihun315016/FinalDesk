using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using DESK_DTO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DESK_EQM
{
    public class Program
    {
        static PopVO wkList;
        static void Main(string[] args)
        {
            //넘겨받은 해당 값으로 DB에 조회            
            string wId = "WORK_20220806_0006"; // args[0]; //추후에 이걸로 교체
            bool flag = true; // 작업이 있는지 유무
            DAC dac = new DAC();
            wkList = dac.SelectQtyList(wId); //작업내역확인
            Service srv = new Service(wkList);


            if (wkList != null) // 작업이 있는 설비
            {
                Console.WriteLine($"작업 : {wId}");
                Console.WriteLine($"작업 시작 : {DateTime.Now}");
                srv.Starttimer(wId); //시작하기
                Console.ReadLine();
            }
            else              // 작업이 없는 설비
            {
                Console.WriteLine($"해당 작업내역 없음");
                Console.ReadLine();
            }
        }
    }
    public class Service
    {
        static string workCode; //가져온 작업 코드
        static Timer searchSave;
        PopVO workdate;
        DAC dac;
        int qty;
        public Service(PopVO work)
        {
            workdate = work;
            qty = workdate.Working_Qty;
            dac = new DAC();
        }
        public string MyProperty { get; set; }

        /// <summary>
        /// 타이머 시작
        /// </summary>
        /// <param name="code"></param>
        public void Starttimer(string code)
        {
            workCode = code;
            searchSave = new Timer(1000);
            searchSave.Elapsed += SearchSave_Elapsed;
            searchSave.AutoReset = true;
            searchSave.Enabled = true; //시작
            //searchSave.Start();
        }
        public void Stoptimer(string code)          //이거 필요한가?
        {
            //DB 에 저장
        }
        /// <summary>
        /// 초당 DB 업로드, 숫자 더하기, 화면표시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchSave_Elapsed(object sender, ElapsedEventArgs e)
        {

            //db에 저장
            if (dac.UpdateQty(workCode, qty))
            {
                Console.WriteLine($"작업 : {workCode} | {qty} | {DateTime.Now}");

                qty += workdate.Output_Qty;
            }
            else
            {
                searchSave.Enabled = false;
            }

        }
    }
    public class DAC : IDisposable
    {
        SqlConnection conn;
        public int Working_Qty { get; set; }
        public int Output_Qty { get; set; }
        public DAC()
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
        /// <summary>
        /// 해당 설비번호, 설비명, 1초당 출력값, 진행 값
        /// </summary>
        /// <param name="workCode"></param>
        /// <returns></returns>
        public PopVO SelectQtyList(string workCode)
        {
            string sql = @"select Production_Equipment_Code,Equipment_Name,Output_Qty,Working_Qty
from TB_WORK w left join TB_EQUIPMENT e on w.Production_Equipment_Code = e.Equipment_No
where Work_Code=@Work_Code";
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Work_Code", workCode);
                    List<PopVO> list = DBHelpler.DataReaderMapToList<PopVO>(cmd.ExecuteReader());

                    return list[0];
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        /// <summary>
        /// 현재 갯수를 저장
        /// true = 있음/ false 없음/ 정지
        /// </summary>
        /// <param name="UserG"></param>
        /// <returns></returns>
        public bool UpdateQty(string workCode, int qty)
        {
            int iRowAffect;
            string sql = @"USP_EQMUpdate";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Work_Code", workCode);
                    cmd.Parameters.AddWithValue("@Working_Qty", qty);

                    iRowAffect = Convert.ToInt32(cmd.ExecuteScalar());
                }
                return iRowAffect > 0;
            }
            catch (Exception err)
            {
                throw err;
            }

        }
    }
}
