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
        //https://localhost:44393/api/Pop/Login/{id}
        //https://localhost:44393/api/Pop/Login/10002
        //http://localhost/api/Pop/Login/{id}
        //http://localhost/api/Pop/Login/10002
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
        //https://localhost:44393/api/Pop/Uc/{id}
        //https://localhost:44393/api/Pop/Uc/1002
        //http://localhost/api/Pop/Uc/{id}
        //http://localhost/api/Pop/Uc/1002
        [HttpGet]               //이 메서드? 쓰려면 타입을 이걸루 해라
        [Route("Uc/{id}")]         // 이 메서드 쓰려면 뒤에 input값 적어라
        public IHttpActionResult GetPopUc(int id)
        {
            PopDAC db = new PopDAC(); //대충 DB에서 사람 있는지 조회하는 코드
            List<PopVO> list = db.GetWorkUcList(id);

            ResMessage<List<PopVO>> result = new ResMessage<List<PopVO>>
            {
                ErrCode = (list == null) ? -9 : 0,
                ErrMsg = (list == null) ? "ID와 일치하는 작업지시가 없습니다." : "S",
                Data = list
            };
            return Ok(result);
        }
        //https://localhost:44393/api/Pop/Detail/{id}
        //https://localhost:44393/api/Pop/Detail/WORK_20220806_0006
        //http://localhost/api/Pop/Detail/{id}
        //http://localhost/api/Pop/Detail/WORK_20220806_0006
        [HttpGet]                      // 이 메서드? 쓰려면 타입을 이걸루 해라
        [Route("Detail/{id}")]         // 이 메서드 쓰려면 뒤에 input값 적어라
        public IHttpActionResult GetPopWorkDetail(string id)
        {
            PopDAC db = new PopDAC(); //대충 DB에서 사람 있는지 조회하는 코드
            PopVO list = db.GetWorkDetailList(id);

            ResMessage<PopVO> result = new ResMessage<PopVO>
            {
                ErrCode = (list == null) ? -9 : 0,
                ErrMsg = (list == null) ? "Code와 일치하는 작업이 없습니다." : "S",
                Data = list
            };
            return Ok(result);
        }
        //https://localhost:44393/api/Pop/Gdv/{id}
        //https://localhost:44393/api/Pop/Gdv/1006
        //http://localhost/api/Pop/Gdv/{id}
        //http://localhost/api/Pop/Gdv/1006
        [HttpGet]               //이 메서드? 쓰려면 타입을 이걸루 해라
        [Route("Gdv/{id}")]         // 이 메서드 쓰려면 뒤에 input값 적어라
        public IHttpActionResult GetPopWorkGdvList(int id)
        {
            PopDAC db = new PopDAC(); //대충 DB에서 사람 있는지 조회하는 코드
            List<PopVO> list = db.GetWorkGdvList(id);

            ResMessage<List<PopVO>> result = new ResMessage<List<PopVO>>
            {
                ErrCode = (list == null) ? -9 : 0,
                ErrMsg = (list == null) ? "Code와 일치하는 작업이 없습니다." : "S",
                Data = list
            };
            return Ok(result);
        }
    }
}
