using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESK_POP
{
    class TextBoxUtil
    {
        /// <summary>
        /// Author : 강지훈
        /// IsRequired 속성이 True지만 입력되지 않은 Textbox를 검사한다.
        /// </summary>
        /// <param name="txts">검사할 ccTextBox 배열</param>
        /// <returns>
        /// isRequired 처리된 ccTextBox가 모두 입력되었다면 빈 문자열 반환하고
        /// 입력되지 않은 컨트롤이 있다면 필수 입력을 알리는 문자열을 반환한다.
        /// </returns>
        public static string IsRequiredCheck(ccTextBox[] txts)
        {
            StringBuilder sb = new StringBuilder();

            foreach (ccTextBox txt in txts)
            {
                if (txt.isRequired)
                {
                    if (string.IsNullOrEmpty(txt.Text) || txt.Text.Equals(txt.PlaceHolder))
                        sb.Append($"[{txt.Tag}] ");
                }
            }

            if (sb.Length > 0)            
                sb.Append($"{Environment.NewLine}필수 입력입니다.");
            return sb.ToString();
        }
    }
}
