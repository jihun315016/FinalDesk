using DESK_DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace DESK_WEB.Controllers.api
{
    public class ImageUploadController : ApiController
    {
        public IHttpActionResult Post()
        {
            try
            {
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    foreach (string file in HttpContext.Current.Request.Files)
                    {
                        string path = HttpContext.Current.Server.MapPath("/Files/");

                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);

                        var f = HttpContext.Current.Request.Files[file];
                        f.SaveAs($"{path}{f.FileName}");
                    }

                    ResMessage res = new ResMessage()
                    {
                        ErrCode = 0,
                        ErrMsg = "S"
                    };
                    return Ok(res);
                }
                else
                {
                    ResMessage res = new ResMessage()
                    {
                        ErrCode = -9,
                        ErrMsg = "이미지가 없습니다."
                    };

                    return Ok(res);
                }
            }
            catch (Exception err)
            {
                ResMessage res = new ResMessage()
                {
                    ErrCode = -9,
                    ErrMsg = "오류가 발생했습니다."
                };

                return Ok(res);
            }
        }
    }
}
