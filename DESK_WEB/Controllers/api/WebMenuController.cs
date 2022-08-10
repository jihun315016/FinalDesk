using DESK_WEB.Models;
using DESK_WEB.Models.DAC;
using DESK_WEB.Models.DTO;
using DESK_WEB.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DESK_WEB.Controllers.api
{
    [RoutePrefix("api/Menu")]
    public class WebMenuController : ApiController
    {
        // https://localhost:44393/api/Menu/Menu
        [Route("Menu")]
        public IHttpActionResult GetWebList()
        {
            MainDAC dac = new MainDAC();
            List<MenuVO> list = dac.GetWebMenuList();
            try
            {
                ResMessage<List<MenuVO>> res = new ResMessage<List<MenuVO>>()
                {
                    ErrCode = list == null ? -9 : 0,
                    ErrMsg = list == null ? "조회 중 오류 발생" : "S",
                    Data = list
                };

                return Ok(res);
            }
            catch (Exception err)
            {
                LoggingMsgVO msg = new LoggingMsgVO()
                {
                    Msg = err.Message,
                    StackTrace = err.StackTrace,
                    Source = err.Source
                };
                LoggingUtil.LoggingError(msg);

                ResMessage res = new ResMessage()
                {
                    ErrCode = -9,
                    ErrMsg = "서비스 관리자에게 문의하시기 바랍니다."
                };

                return Ok(res);
            }
        }

        [HttpPost]
        [Route("login")]
        public IHttpActionResult CheckLogin(UserVO user)
        {
            MainDAC dac = new MainDAC();
            try
            {
                bool result = dac.CheckLogin(user);
                ResMessage<bool> res = new ResMessage<bool>()
                {
                    ErrCode = result ? -9 : 0,
                    ErrMsg = result ? "조회 중 오류 발생" : "S",
                    Data = result
                };
                return Ok(res);
            }
            catch (Exception err)
            {
                LoggingMsgVO msg = new LoggingMsgVO()
                {   
                    Msg = err.Message,
                    StackTrace = err.StackTrace,
                    Source = err.Source
                };
                LoggingUtil.LoggingError(msg);

                ResMessage res = new ResMessage()
                {
                    ErrCode = -9,
                    ErrMsg = "서비스 관리자에게 문의하시기 바랍니다."
                };

                return Ok(res);
            }
        }
    }
}
