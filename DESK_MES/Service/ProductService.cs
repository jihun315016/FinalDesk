using DESK_DTO;
using DESK_MES.DAC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DESK_MES.Service
{
    class ProductService
    {
        string baseUrl = ConfigurationManager.AppSettings["apiURL"];

        public List<CodeCountVO> GetProductType()
        {
            ProductDAC dac = new ProductDAC();
            List<CodeCountVO> list = dac.GetProductType();
            dac.Dispose();
            return list;
        }

        public string SaveProduct(string code, int userNo, ProductVO prd)
        {
            ProductDAC dac = new ProductDAC();            
            string savePrdMsg = dac.SaveProduct(code, userNo, prd);
            dac.Dispose();
            return savePrdMsg;
        }

        /// <summary>
        /// Author : 강지훈
        /// 제품 등록 시 이미지가 존재하는 경우 이미지 등록
        /// </summary>
        /// <param name="path">등록할 이미지 경로</param>
        /// <param name="fileName">등록할 이미지 이름 -> 제품 코드</param>
        /// <returns></returns>
        public bool SaveProductImage(string path, string fileName)
        {
            FileStream fs = File.Open(path, FileMode.Open);
            
            MultipartFormDataContent content = new MultipartFormDataContent();
            Debug.WriteLine($"{fileName}{new FileInfo(path).Extension}");
            // Web API에 f1이라는 이름으로 넘어가고, Hello.png라는 이름으로 저장된다.
            content.Add(new StreamContent(fs), "f1", $"{fileName}{new FileInfo(path).Extension}");

            string url = $"{baseUrl}api/ImageUpload";

            HttpClient client = new HttpClient();
            HttpResponseMessage res = client.PostAsync(url, content).Result;

            string data = res.Content.ReadAsStringAsync().Result;
            ResMessage result = JsonConvert.DeserializeObject<ResMessage>(data);

            if (res.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                Debug.WriteLine(result.ErrMsg);
                return false;
            }
        }
    }
}
