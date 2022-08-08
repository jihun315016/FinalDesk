using DESK_WEB.Models;
using DESK_WEB.Models.DAC;
using DESK_WEB.Models.DTO;
using DESK_WEB.Utility;
using MvcPaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DESK_WEB.Controllers.api
{
    [RoutePrefix("api/Trade")]
    public class TradeApiController : ApiController
    {
        //https://localhost:44393/api/Trade/Purchase?startDate=2022-07-01&endDate=2022-08-04
        [Route("Purchase")]
        public IHttpActionResult GetPurchaseList(string startDate, string endDate, string keyword = "")
        {
            try
            {
                    TradeDAC dac = new TradeDAC();
                List<WebPurchaseVO> list = dac.GetPurchaseList(startDate, endDate, keyword);

                ResMessage<List<WebPurchaseVO>> result = new ResMessage<List<WebPurchaseVO>>()
                {
                    ErrCode = (list == null) ? -9 : 0,
                    ErrMsg = (list == null) ? "조회중 오류발생" : "S",
                    Data = list
                };

                return Ok(result);
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

                return Ok(new ResMessage()
                {
                    ErrCode = -9,
                    ErrMsg = "서비스 관리자에게 문의하시기 바랍니다."
                });
            }

            //try
            //{
            //    TradeDAC dac = new TradeDAC();
            //    List<WebPurchaseVO> list = dac.GetPurchaseList(startDate, endDate, keyword);

            //    ResMessage<List<WebPurchaseVO>> result = new ResMessage<List<WebPurchaseVO>>()
            //    {
            //        ErrCode = (list == null) ? -9 : 0,
            //        ErrMsg = (list == null) ? "조회중 오류발생" : "S",
            //        Data = list
            //    };

            //    return Ok(result);
            //}
            //catch (Exception err)
            //{
            //    LoggingMsgVO msg = new LoggingMsgVO()
            //    {
            //        Msg = err.Message,
            //        StackTrace = err.StackTrace,
            //        Source = err.Source
            //    };
            //    LoggingUtil.LoggingError(msg);

            //    return Ok(new ResMessage()
            //    {
            //        ErrCode = -9,
            //        ErrMsg = "서비스 관리자에게 문의하시기 바랍니다."
            //    });
            //}
        }
    }
}
