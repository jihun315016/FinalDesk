using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Configuration;
using DESK_DTO;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;
using System.Net;

namespace DESK_MES
{
    public class ServiceHelper : IDisposable
    {
        HttpClient client = new HttpClient();

        public string BaseServiceURL { get; set; }
        public ServiceHelper(string routePrefix)
        {
            BaseServiceURL = $"{ConfigurationManager.AppSettings["apiURL"]}{routePrefix}";

            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void Dispose()
        {
            client.Dispose();
        }

        // Get
        public ResMessage GetAsyncNon(string path)
        {
            string url = $"{BaseServiceURL}/{path}";

            HttpResponseMessage res = client.GetAsync(url).Result;
            if (res.IsSuccessStatusCode)
            {
                string mss = res.Content.ReadAsStringAsync().Result;
                ResMessage result = JsonConvert.DeserializeObject<ResMessage>(mss);

                return result;
            }

            return null;
        }

        public T GetAsyncT<T>(string path)
        {
            string url = $"{BaseServiceURL}/{path}";

            T obj = default(T);

            HttpResponseMessage res = client.GetAsync(url).Result;
            if (res.IsSuccessStatusCode)
            {
                string mss = res.Content.ReadAsStringAsync().Result;
                T result = JsonConvert.DeserializeObject<T>(mss);

                return result;
            }

            return obj;
        }

        // Get + Message<T>
        public ResMessage<T> GetAsync<T>(string path)
        {
            string url = $"{BaseServiceURL}/{path}";

            HttpResponseMessage res = client.GetAsync(url).Result;
            if (res.IsSuccessStatusCode)
            {
                string mss = res.Content.ReadAsStringAsync().Result;
                ResMessage<T> result = JsonConvert.DeserializeObject<ResMessage<T>>(mss);

                // AutoGenerateColumns = true 일때
                return result;
            }

            return null;
        }

        // Post
        public ResMessage PostAsyncNon<T>(string path, T t)
        {
            string url = $"{BaseServiceURL}/{path}";

            HttpResponseMessage res = client.PostAsJsonAsync(url, t).Result;
            if (res.IsSuccessStatusCode)
            {
                ResMessage result = JsonConvert.DeserializeObject<ResMessage>(res.Content.ReadAsStringAsync().Result);

                return result;
            }
            else
            {
                return null;
            }
        }

        // Post + Message<T>
        public ResMessage<R> PostAsync<T, R>(string path, T t)
        {
            string url = $"{BaseServiceURL}/{path}";

            HttpResponseMessage res = client.PostAsJsonAsync(url, t).Result;
            if (res.IsSuccessStatusCode)
            {
                ResMessage<R> result = JsonConvert.DeserializeObject<ResMessage<R>>(res.Content.ReadAsStringAsync().Result);

                return result;
            }
            else
            {
                return null;
            }
        }

        public ResMessage ServerFileUpload(string localFileName)
        {
            // localFileName : local에서 선택한 파일명
            // uploadFileName : Server에 업로드할 파일명
            string uploadFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + new FileInfo(localFileName).Extension;

            FileStream fs = File.Open(localFileName, FileMode.Open);
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StreamContent(fs), "file1", uploadFileName);
            content.Add(new StringContent(new FileInfo(localFileName).Name), "file1_orgName");

            string url = $"{BaseServiceURL}";
            HttpResponseMessage res = client.PostAsync(url, content).Result;

            if (res.IsSuccessStatusCode)
            {
                ResMessage result = JsonConvert.DeserializeObject<ResMessage>(res.Content.ReadAsStringAsync().Result);

                return result;
            }
            else
            {
                return null;
            }
        }

        public ResMessage LocalFileDownLoad(string fileUrl, string localFileName)
        {            
            try
            {
                string dawnloadPath = Application.StartupPath + "/Downloads/";
                if (!Directory.Exists(dawnloadPath))
                {
                    Directory.CreateDirectory(dawnloadPath);
                }

                WebClient web = new WebClient();
                web.DownloadFile(fileUrl, dawnloadPath + localFileName);

                return new ResMessage
                {
                    ErrCode = 0,
                    ErrMsg = "S"
                };
            }
            catch(Exception err)
            {
                return new ResMessage
                {
                    ErrCode = -9,
                    ErrMsg = err.Message
                };
            }
        }
    }
}
