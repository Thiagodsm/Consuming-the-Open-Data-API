using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Mvc;
using ConsumingApiPortalDaTransparencia.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using X.PagedList;

namespace ConsumingApiPortalDaTransparencia.Controllers.api
{
    public class DepController : Controller
    {
        //Hosted web API REST Service base url
        string Baseurl = "https://dadosabertos.camara.leg.br/api/v2/";
        public async Task<ActionResult> DepResume(int page = 1)
        {
            //IEnumerable<Deputados> depList = null;
            Deputados depList = new Deputados();

            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("deputados");
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    depList = JsonConvert.DeserializeObject<Deputados> (EmpResponse);
                    //depList = JsonConvert.DeserializeObject<IEnumerable<Deputados>>(EmpResponse);
                }
                //IEnumerable<Deputados> depIEnumerable = new[] { depList };
                //returning the employee list to view
                //return View(depIEnumerable);
                var deputados = depList.dados.OrderBy(p => p.id).ToPagedList(page, 10);
                return View(deputados);
                //return View(depList);
            }
            #region t2
            /*
            using (var handler = new HttpClientHandler())
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri("https://dadosabertos.camara.leg.br/api/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                var responseTask = client.GetAsync("deputados");
                responseTask.Wait();
                //HttpRequestMessage responseTask = await client.GetAsync("deputados");

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var streamTask = client.GetStreamAsync("deputados");
                    //var repositories = await JsonSerializer.DeserializeAsync<List<DepBasicDescription>>(await streamTask);
                    //var readTask = result.Content.ReadAsAsync<List<Deputados>>();
                    //readTask.Wait();
                    //depList = readTask.Result;
                }
                else // web api sent error response
                {
                    // log response status here...
                    depList = null;
                    //depList = Enumerable.Empty<Deputados>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact the administrator.");
                }
            }
            */
            #endregion

        }
        #region t
        /*
        public async Task<ActionResult> DepResume()
        {
            //IEnumerable<Deputados> depList = null;
            List<DepBasicDescription> depList = null;
            //Deputados depList = new Deputados();
            var serializer = new JsonSerializer();

            var client = new HttpClient();
            //client.DefaultRequestHeaders.Add("chave-api-dados", "1d5r8yt963h2v4g5h6j3k138sbfiec21");

            using (Stream streamTask = await client.GetStreamAsync("https://dadosabertos.camara.leg.br/api/v2/deputados"))
            {
                //streamTask.Wait();
                //depList = await JsonSerializer.DeserializeAsync<List<DepBasicDescription>>(await streamTask);

                //object o = DeserializeFromStream(streamTask);
                //depList = o as List<DepBasicDescription>;
            }
        */

        /*
        //using (var client = new HttpClient())

        using (var handler = new HttpClientHandler())
        using (var client = new HttpClient(handler))
        {
            client.BaseAddress = new Uri("https://dadosabertos.camara.leg.br/api/v2/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // HTTP GET
            //var responseTask = client.GetAsync("deputados");
            HttpRequestMessage responseTask = await client.GetAsync("deputados");
            //responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IEnumerable<Deputados>>();
                readTask.Wait();
                depList = readTask.Result;
            }
            else // web api sent error response
            {
                // log response status here...
                depList = null;
                //depList = Enumerable.Empty<Deputados>();
                ModelState.AddModelError(string.Empty, "Server error. Please contact the administrator.");
            }
        }

        return View(depList); 
    }



    public static object DeserializeFromStream(Stream stream)
    {
        var serializer = new JsonSerializer();

        using (var sr = new StreamReader(stream))
        using (var jsonTextReader = new JsonTextReader(sr))
        {
            return serializer.Deserialize(jsonTextReader);
        }
    }
        */
        #endregion
        // GET: Deputados
    }
}