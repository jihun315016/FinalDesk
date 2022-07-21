using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DESK_DTO;
using System.Data.SqlClient;

namespace DESK_MES
{
    public class UserService
    {
        public List<UserVO> SelectUserList()
        {
            UserDAC db = new UserDAC();            
            List<UserVO> list = db.SelectUserList();
            db.Dispose();
            return list;
        }
    }
}
