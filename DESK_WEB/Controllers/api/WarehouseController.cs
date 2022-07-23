using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http;
using DESK_WEB.Models;

namespace DESK_WEB.Controllers
{
    [RoutePrefix("api/Warehouse")]
    public class WarehouseController : ApiController
    {
        //https://localhost:44393/api/Warehouse/Warehouse
        [Route("Warehouse")]
        public IHttpActionResult GetAllWarehouse()
        {
            try
            {
                WarehouseDAC db = new WarehouseDAC();
                List<WarehouseVO> list = db.GetAllWarehouse();

                ResMessage<List<WarehouseVO>> result = new ResMessage<List<WarehouseVO>>()
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

        //https://localhost:44393/api/Warehouse/{id}
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult WarehouseInfo(string id)
        {
            WarehouseDAC db = new WarehouseDAC();
            WarehouseVO user = db.GetWarehouseInfo(id);

            ResMessage<WarehouseVO> result = new ResMessage<WarehouseVO>()
            {
                ErrCode = (user == null) ? -9 : 0,
                ErrMsg = (user == null) ? "해당하는 정보가 없습니다." : "S",
                Data = user
            };

            return Ok(result);
        }

        //POST : https://localhost:44393/api/Warehouse/SaveWarehouse
        [HttpPost]
        [Route("SaveWarehouse")]
        public IHttpActionResult SaveWarehouse(WarehouseVO warehouse)
        {
            try
            {
                WarehouseDAC db = new WarehouseDAC();
                bool flag = db.SaveWarehouse(warehouse);

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

        //POST : https://localhost:44393/api/Warehouse/UpdateWarehouse
        [HttpPost]
        [Route("UpdateWarehouse")]
        public IHttpActionResult UpdateWarehouse(WarehouseVO warehouse)
        {
            try
            {
                WarehouseDAC db = new WarehouseDAC();
                bool flag = db.UpdateWarehouse(warehouse);

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

        //GET: https://localhost:44393/api/Warehouse/DelWarehouse/{no}
        [HttpGet]
        [Route("DelWarehouse/{no}")]
        public IHttpActionResult DeleteWarehouse(string no)
        {
            try
            {
                WarehouseDAC db = new WarehouseDAC();
                bool flag = db.DeleteWarehouse(no);

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
