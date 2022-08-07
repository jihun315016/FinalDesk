using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DESK_WEB.Models;

namespace DESK_WEB.Controllers
{
    [RoutePrefix("api/Pop")] //여기 코드쓰려면 앞에 이거 붙여라
    public class PopController : ApiController
    {
        //https://localhost:44393/api/Pop/{id}
        [HttpGet]               //이 메서드? 쓰려면 타입을 이걸루 해라
        [Route("Login/{id}")]         // 이 메서드 쓰려면 뒤에 input값 적어라
        public IHttpActionResult GetUserLogin(int id)
        {
            PopDAC db = new PopDAC(); //대충 DB에서 사람 있는지 조회하는 코드
            PopVO user =  db.GetUserLogin(id);

            ResMessage<PopVO> result = new ResMessage<PopVO>
            {
                ErrCode = (user == null) ? -9:0,
                ErrMsg = (user==null)? "ID와 일치하는 정보가 없습니다.":"S",
                Data = user
            };
            return Ok(result);
        }
    }
}
