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
using X.PagedList;
using ConsumingApiPortalDaTransparencia.Services;

namespace ConsumingApiPortalDaTransparencia.Controllers
{
    public class DepController : Controller
    {
        //Hosted web API REST Service base url
        readonly string Baseurl = "https://dadosabertos.camara.leg.br/api/v2/";
        public async Task<ActionResult> DepResume(string selectedLetter)
        {
            //IEnumerable<Deputados> depList = null;
            Deputados depList = new Deputados();

            ContactApi service = new ContactApi();

            depList = await service.MakeRequest(depList, "deputados");

            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllDeputados using HttpClient
                HttpResponseMessage Res = await client.GetAsync("deputados");
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var response = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Deputados list
                    depList = JsonConvert.DeserializeObject<Deputados> (response);
                }
                var viewModel = new AlphabeticCustomerPagingViewModel { SelectedLetter = selectedLetter };

                viewModel.FirstLetters = depList.dados
                .GroupBy(c => c.nome.Substring(0, 1))
                .Select(x => x.Key.ToUpper())
                .ToList();

                if (string.IsNullOrEmpty(selectedLetter) || selectedLetter == "Todos")  
                {  
                    viewModel.depName = depList.dados 
                        .Select(c => c.nome)  
                        .ToList();

                    viewModel.listaDeputados = depList.dados;
                }  
                else  
                {  
                    if (selectedLetter == "0-9")  
                    {  
                        var numbers = Enumerable.Range(0, 10).Select(i => i.ToString());  
                        viewModel.depName = depList.dados
                            .Where(p => numbers.Contains(p.nome.Substring(0, 1)))  
                            .Select(p => p.nome)  
                            .ToList(); 
                        
                        viewModel.listaDeputados = depList.dados
                            .Where(p => numbers.Contains(p.nome.Substring(0, 1)))
                            .Select(p => p)
                            .ToList();

                    }  
                    else  
                    {  
                        viewModel.depName = depList.dados  
                            .Where(p => p.nome.StartsWith(selectedLetter))  
                            .Select(p => p.nome)  
                            .ToList(); 
                        
                        viewModel.listaDeputados = depList.dados
                            .Where(p => p.nome.StartsWith(selectedLetter))
                            .Select(p => p)
                            .ToList();
                    }  
                }
                return View(viewModel);
            }
        }

        
        public async Task<ActionResult> DepDetails(int? id)
        {
            DeputadosInfoDetalhada info = new DeputadosInfoDetalhada();

            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllDeputados using HttpClient
                HttpResponseMessage Res = await client.GetAsync($"deputados/{id.ToString()}");
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var response = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Deputados list
                    info = JsonConvert.DeserializeObject<DeputadosInfoDetalhada>(response);
                }
            }
                return PartialView(info);
        }
    }
}


#region esse funciona
/*
 namespace ConsumingApiPortalDaTransparencia.Controllers
{
    //public class DepController : Controller

    public class DepController : Controller
    {
        //Hosted web API REST Service base url
        readonly string Baseurl = "https://dadosabertos.camara.leg.br/api/v2/";
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
                var deputados = depList.dados.OrderBy(p => p.nome).ToPagedList(page, 10);
                return View(deputados);
                //return View(depList);
            }
        }
    }
}
 
 */
#endregion