using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DESK_MES
{
    static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += (s, e) =>
            {
                string msg = e.Exception.Message;
                Debug.WriteLine($"예외 : {msg}");
                // TODO : WEB api에 예외 메세지 전송
            };

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                string msg = ((Exception)e.ExceptionObject).Message;
                Debug.WriteLine($"예외 : {msg}");
                // TODO : WEB api에 예외 메세지 전송
            };

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}
