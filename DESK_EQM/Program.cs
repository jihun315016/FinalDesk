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
            bool flag = true; // 작업이 있는지 유무
            Service srv = new Service();

            if (flag == true) // 작업이 있는 설비
            {
                Console.WriteLine($"작업 : 작업설비명");
                Console.WriteLine($"작업 시작 : {DateTime.Now}");
                srv.Starttimer("작업 코드"); //시작하기
                Console.ReadLine();
            }
            else              // 작업이 없는 설비
            {
                
            }
        }
    }
    public class Service
    {
        static string workCode; //가져온 작업 코드
        static  Timer timerCount; //삭제 예정
        static Timer searchSave;
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
            if (true) //설비 켜져있으면
            {
                //db에 저장

                Console.WriteLine($"작업숫자 표시 : {DateTime.Now}");

                //숫자 ++
            }
            else
            {
                //db 저장
                //타이머 stop
                //종료
            }
        }
    }
}
