using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Mvc;
using ConsumingApiPortalDaTransparencia.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.IO;

namespace ConsumingApiPortalDaTransparencia.Controllers.api
{
    public class DepController : Controller
    {
        public ActionResult DepResume()
        {
            //IEnumerable<Deputados> depList = null;
            List<DepBasicDescription> depList = null;

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
                    var repositories = await JsonSerializer.DeserializeAsync<List<DepBasicDescription>>(await streamTask);
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
            return View(depList);
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