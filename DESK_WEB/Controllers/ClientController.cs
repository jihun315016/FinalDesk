using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DESK_WEB.Models;
using DESK_DTO;

namespace DESK_WEB.Controllers
{
    [RoutePrefix("api/Client")]
    public class ClientController : ApiController
    {
        //https://localhost:44393/api/Client/GetAllClients
        [Route("Client")]
        public IHttpActionResult GetAllClients()
        {
            try
            {
                ClientDAC db = new ClientDAC();
                List<ClientVO> list = db.GetAllClients();

                ResMessage<List<ClientVO>> result = new ResMessage<List<ClientVO>>()
                {
                    ErrCode = (list == null) ? -9 : 0,
                    ErrMsg = (list == null) ? "조회중 오류발생" : "S",
                    Data = list
                };

                return Ok(result);
            }
            catch (Exception err)
            {
                System.Diagnostics.Debug.WriteLine(err.Message);

                return Ok(new ResMessage()
                {
                    ErrCode = -9,
                    ErrMsg = "서비스 관리자에게 문의하시기 바랍니다."
                });
            }

        }

    }
}
