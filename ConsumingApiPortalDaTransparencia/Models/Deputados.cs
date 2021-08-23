using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsumingApiPortalDaTransparencia.Models
{
    public class Deputados
    {
        [JsonProperty("dados")]
        public List<DadosBasicosDep> dados { get; set; }
        
        [JsonProperty("links")]
        public List<LinkDep> links { get; set; }
    }
}