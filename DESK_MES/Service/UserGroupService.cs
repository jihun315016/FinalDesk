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
        public List<UserGroupVO> SelectAuthList()
        {
            UserGroupDAC dac = new UserGroupDAC();
            List<UserGroupVO> list = dac.SelectAuthList();
            dac.Dispose();
            return list;
        }
        public bool InsertUserGroup(UserGroupVO UserG)
        {
            UserGroupDAC dac = new UserGroupDAC();
            bool result = dac.InsertUserGroup(UserG);
            dac.Dispose();
            return result;
        }
        public UserGroupVO SelectUserGroupCell(int selectU)
        {
            UserGroupDAC dac = new UserGroupDAC();
            UserGroupVO list = dac.SelectUserGroupCell(selectU);
            dac.Dispose();
            return list;
        }
        public bool UpdateUserGroup(UserGroupVO UserG)
        {
            UserGroupDAC dac = new UserGroupDAC();
            bool result = dac.UpdateUserGroup(UserG);
            dac.Dispose();
            return result;
        }
        public bool DeleteUserGroup(int User_Group_No)
        {
            UserGroupDAC dac = new UserGroupDAC();
            bool result = dac.DeleteUserGroup(User_Group_No);
            dac.Dispose();
            return result;
        }
        public List<UserGroupVO> SelectGroupNameList()
        {
            UserGroupDAC dac = new UserGroupDAC();
            List<UserGroupVO> list = dac.SelectGroupNameList();
            dac.Dispose();
            return list;
        }
    }
}
