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
    [RoutePrefix("api/Work")]
    public class WebWorkController : ApiController
    {
        // https://localhost:44393/api/Work/inoperative
        [Route("inoperative")]
        public IHttpActionResult GetInoperativeEquipmentList(string startDate, string endDate, string keyword = "")
        {
            WorkDAC dac = new WorkDAC();
            List<InoperativeEquipmentVO> list = dac.GetInoperativeEquipmentList(startDate, endDate, keyword);

            try
            {
                ResMessage<List<InoperativeEquipmentVO>> res = new ResMessage<List<InoperativeEquipmentVO>>()
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
    }
}
