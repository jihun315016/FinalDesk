﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DESK_WEB.Models;


namespace DESK_WEB.Controllers
{
    [RoutePrefix("api/Client")] 
    public class ClientController : ApiController
    {
        //https://localhost:44393/api/Client/Clients
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

        //http://localhost/api/Client/ClientByType/{type}
        [Route("ClientByType/{type}")]
        public IHttpActionResult GetClientListByType(string type)
        {
            try
            {
                ClientDAC db = new ClientDAC();
                List<ClientVO> list = db.GetClientListByType(type);
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
                return Ok(new ResMessage()
                {
                    ErrCode = -9,
                    ErrMsg = "서비스 관리자에게 문의하시기 바랍니다."
                });
            }
        }


        //https://localhost:44393/api/Client/{id}
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult ClientsInfo(string id)
        {
            ClientDAC db = new ClientDAC();
            ClientVO user = db.GetClientInfo(id);

            ResMessage<ClientVO> result = new ResMessage<ClientVO>()
            {
                ErrCode = (user == null) ? -9 : 0,
                ErrMsg = (user == null) ? "해당하는 정보가 없습니다." : "S",
                Data = user
            };

            return Ok(result);
        }

        //POST : https://localhost:44393/api/Client/SaveClient
        [HttpPost]
        [Route("SaveClient")]
        public IHttpActionResult SaveClient(ClientVO client)
        {
            try
            {
                ClientDAC db = new ClientDAC();
                bool flag = db.SaveClient(client);

                ResMessage result = new ResMessage()
                {
                    ErrCode = (!flag) ? -9 : 0,
                    ErrMsg = (!flag) ? "저장중 오류발생" : "S"
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

        //POST : https://localhost:44393/api/Client/UpdateClient
        [HttpPost]
        [Route("UpdateClient")]
        public IHttpActionResult UpdateClient(ClientVO client)
        {
            try
            {
                ClientDAC db = new ClientDAC();
                bool flag = db.UpdateClient(client);

                ResMessage result = new ResMessage()
                {
                    ErrCode = (!flag) ? -9 : 0,
                    ErrMsg = (!flag) ? "저장중 오류발생" : "S"
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


        //GET: https://localhost:44393/api/Client/DelClient/{no}
        [HttpGet]
        [Route("DelClient/{no}")]
        public IHttpActionResult DeleteClient(string no)
        {
            try
            {
                ClientDAC db = new ClientDAC();
                bool flag = db.DeleteClient(no);

                ResMessage result = new ResMessage()
                {
                    ErrCode = (!flag) ? -9 : 0,
                    ErrMsg = (!flag) ? "삭제중 오류발생" : "S"
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
