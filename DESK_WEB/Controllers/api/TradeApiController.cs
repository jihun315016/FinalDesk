using DESK_WEB.Models;
using DESK_WEB.Models.DAC;
using DESK_WEB.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DESK_WEB.Controllers.api
{
    public class TradeApiController : ApiController
    {
        public IHttpActionResult GetPurchaseList()
        {
            string startDate = "2022-07-01";
            string endDate = "2022-08-04";
            try
            {
                TradeDAC dac = new TradeDAC();
                List<WebPurchaseVO> list = dac.GetPurchaseList(startDate, endDate);

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
                return Ok(new ResMessage()
                {
                    ErrCode = -9,
                    ErrMsg = "서비스 관리자에게 문의하시기 바랍니다."
                });
            }
        }
    }
}
