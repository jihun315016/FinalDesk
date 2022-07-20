using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESK_DTO
{
    public class ResMessage
    {
        // 성공:0,   실패:-9
        public int ErrCode { get; set; } 

        // 성공:"S"  실패:에러메세지
        public string ErrMsg { get; set; }
    }

    public class ResMessage<T>
    {
        // 성공:0,   실패:-9
        public int ErrCode { get; set; }

        // 성공:"S"  실패:에러메세지
        public string ErrMsg { get; set; }

        // 전달하고 싶은 데이터
        public T Data { get; set; }
    }
}
