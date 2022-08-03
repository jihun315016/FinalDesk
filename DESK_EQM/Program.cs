using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace DESK_EQM
{
    public class Program
    {
        static void Main(string[] args)
        {
            //넘겨받은 해당 값으로 DB에 조회
            //
            
        }
    }
    public class Service
    {
        string workCode;
        Timer timerCount;
        Timer searchSave;
        public Service(string code)
        {
            workCode = code;
        }
        private void StartSearch(string code)
        {
            searchSave = new Timer(1000);
            searchSave.Elapsed += SearchSave_Elapsed;
            searchSave.AutoReset = true;
            searchSave.Enabled = true; //시작
        }

        private void SearchSave_Elapsed(object sender, ElapsedEventArgs e)
        {
            //DB에서 설비 켜져있는지 조회

            //현재 재고량 저장
        }

        private void StartCount(string code)
        {
            int dbNum = 0;
            if (dbNum==0) //코드로 DB 조회 = 설비 정지중 + 새로운 작업
            {
                timerCount = new Timer(1000);
                timerCount.Elapsed += Timer1_Elapsed;
                timerCount.AutoReset = true;
                timerCount.Enabled = true; //시작
                //DB에 현재 시간 찍기//
            }
            else if (dbNum == 1) //코드 조회 = 설비 진행중
            {

            }

        }

        private void Timer1_Elapsed(object sender, ElapsedEventArgs e)
        {
            //초당 갯수 증가

            

            //DB 찍기
        }
    }
}
