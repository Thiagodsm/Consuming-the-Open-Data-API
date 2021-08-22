using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using ConsumingApiPortalDaTransparencia.Models;

namespace ConsumingApiPortalDaTransparencia.Controllers.api
{
    public class DeputadosController : Controller
    {
        // GET: Deputados
        public ActionResult Index()
        {
            IEnumerable<Deputados> deputados = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://dadosabertos.camara.leg.br/api/v2/deputados");

                // HTTP GET
                var responseTask = client.GetAsync("deputados");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<Deputados>>();

                    deputados = readTask.Result;
                }
                else // web api sent error response
                {
                    // log response status here...
                    deputados = Enumerable.Empty<Deputados>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact the administrator.");
                }
            }

            return View(deputados);
        }
    }
}