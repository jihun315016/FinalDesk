using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DESK_DTO;
using System.Data.SqlClient;

namespace DESK_MES
{
    public class UserGroupService
    {
        public List<UserGroupVO> SelectUserGroupList()
        {
            UserGroupDAC dac = new UserGroupDAC();
            List < UserGroupVO > list = dac.SelectUserGroupList();
            dac.Dispose();
            return list;

        }
    }
}
