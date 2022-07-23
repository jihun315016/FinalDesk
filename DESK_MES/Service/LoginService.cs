using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DESK_DTO;

namespace DESK_MES
{
    public class LoginService
    {
        public UserVO GetUserInfo(int userNo, string pwd)
        {
            LoginDAC dac = new LoginDAC();
            UserVO userVO = dac.GetUserInfo(userNo, pwd);
            dac.Dispose();
            return userVO;
        }
    }
}
