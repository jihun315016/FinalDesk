using DESK_DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DESK_POP
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
                Exception err = e.Exception;
                Debug.WriteLine($"예외 : {err.Message}");

                LoggingMsgVO msg = new LoggingMsgVO()
                {
                    Msg = err.Message,
                    StackTrace = err.StackTrace,
                    Source = err.Source
                };
                LoggingUtil.LoggingError(msg);

                MessageBox.Show("오류가 발생했습니다.");
            };

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                Exception err = ((Exception)e.ExceptionObject);
                Debug.WriteLine($"예외 : {err.Message}");

                LoggingMsgVO msg = new LoggingMsgVO()
                {
                    Msg = err.Message,
                    StackTrace = err.StackTrace,
                    Source = err.Source
                };
                LoggingUtil.LoggingError(msg);

                MessageBox.Show("오류가 발생했습니다.");
            };

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new POP_Main());
        }
    }
}
