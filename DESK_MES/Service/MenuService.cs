using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DESK_DTO;
using DESK_MES;
using System.Configuration;


namespace DESK_MES
{
    class MenuService
    {
        public List<MenuVO> GetMenuList(int no)
        {
            MenuDAC dac = new MenuDAC();
            List<MenuVO> list = dac.GetMenuList(no);
            dac.Dispose();
            return list;
        }
    }
}
