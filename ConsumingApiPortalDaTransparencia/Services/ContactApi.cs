using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Mvc;
using ConsumingApiPortalDaTransparencia.Models;
using ConsumingApiPortalDaTransparencia.ViewModels;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ConsumingApiPortalDaTransparencia.Services
{
    public class ContactApi
    {
        public async Task<dynamic> MakeRequest(Object objectType, string path)
        {
            //Hosted web API REST Service base url
            string Baseurl = "https://dadosabertos.camara.leg.br/api/v2/";
            object objectResponse = null;

            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllDeputados using HttpClient
                HttpResponseMessage Res = await client.GetAsync(path);
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var response = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the object
                    objectResponse = JsonConvert.DeserializeObject(response);
                }
            }
            return objectResponse;
        }
    }
}
          