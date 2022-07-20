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

        public bool SaveProductImage(string path)
        {
            FileStream fs = File.Open(path, FileMode.Open);
            string[] temp = path.Split('\\');
            string fileName = temp[temp.Length - 1];

            Debug.WriteLine($"{path} | {fileName}");
            
            MultipartFormDataContent content = new MultipartFormDataContent();

            // Web API에 f1이라는 이름으로 넘어가고, Hello.png라는 이름으로 저장된다.
            content.Add(new StreamContent(fs), "f1", "Hello.png");

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
