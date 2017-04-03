using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace ApiIntegracaoERPConcillius.Classes
{
    public class RequisicaoHttp
    {
        public static HttpResponseMessage Get(string urlBase, string v)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromMinutes(10);

                return client.GetAsync(v).Result;
                
            }
        }

        public static HttpResponseMessage Post(string urlBase, string rotaInscricao, object objeto)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromMinutes(10);

                var TNFeJson = JsonConvert.SerializeObject(objeto);
                var httpContent = new StringContent(TNFeJson, Encoding.UTF8, "application/json");

                return client.PostAsync(rotaInscricao, httpContent).Result;

            }
        }
    }
}