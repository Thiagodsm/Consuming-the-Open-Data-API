using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsumingApiPortalDaTransparencia.Models
{
    public class DepBasicDescription
    {
        public string email { get; set; }
        public int id { get; set; }
        public int idLegislatura { get; set; }
        public string nome { get; set; }
        public string siglaPartido { get; set; }
        public string siglaUf { get; set; }
        public string uri { get; set; }
        public string uriPartido { get; set; }
        public string urlFoto { get; set; }
        public string href { get; set; }
        public string rel { get; set; }
        public string type { get; set; }
    }
}