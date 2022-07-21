﻿using DESK_DTO;
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

        public List<ProductVO> GetProductList(string code = "")
        {
            ProductDAC dac = new ProductDAC();
            List<ProductVO> list = dac.GetProductList(code);
            dac.Dispose();
            return list;
        }

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
        /// <param name="uploadName">등록할 이미지 경로</param>
        /// <param name="uploadName">등록할 이미지 이름 -> 제품 코드</param>
        /// <returns></returns>
        public bool SaveProductImage(string uploadName, string file)
        {
            FileStream fs = File.Open(file, FileMode.Open);
            
            MultipartFormDataContent content = new MultipartFormDataContent();
            Debug.WriteLine($"{uploadName}{new FileInfo(file).Extension}");
            
            content.Add(new StreamContent(fs), "f1", $"{uploadName}{new FileInfo(uploadName).Extension}");

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
