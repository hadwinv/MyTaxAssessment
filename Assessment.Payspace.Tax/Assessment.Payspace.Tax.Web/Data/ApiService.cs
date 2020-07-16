using Assessment.Payspace.Tax.Web.Interface;
using Assessment.Payspace.Tax.Web.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Assessment.Payspace.Tax.Web.Data
{
    public class ApiService : IApiService
    {
        private AppSettings _appSettings { get; set; }

        public ApiService(IOptions<AppSettings> appSetting)
        {
            _appSettings = appSetting.Value;
        }

        public List<TaxType> GetTaxTypes()
        {
            string route = "api/tax/tax-types";

            return InvokeWebApi<List<TaxType>>(HttpMethod.Get, route, string.Empty);
        }

        public decimal CalculateTaxAmount(Request request)
        {
            string route = "/api/tax";

            return InvokeWebApi<decimal>(HttpMethod.Post, route, request);
        }

        private T InvokeWebApi<T>(HttpMethod httpMethod, string route, object request)
        {
            HttpContent httpContent = null;
            StringContent content = null;
            string response = string.Empty;
            bool valid = false;
            HttpResponseMessage result;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_appSettings.TaxService);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

                if (httpMethod == HttpMethod.Post)
                {
                    result = client.PostAsync(route, content).Result;
                }
                else if (httpMethod == HttpMethod.Put)
                {
                    result = client.PutAsync(route, content).Result;
                }
                else
                {
                    result = client.GetAsync(route).Result;
                }

                valid = result.IsSuccessStatusCode;

                if (valid)
                {
                    httpContent = result.Content;
                    // by calling .Result you are synchronously reading the result
                    response = httpContent.ReadAsStringAsync().Result;
                }
            }

            return JsonConvert.DeserializeObject<T>(response);
        }

    }
}
