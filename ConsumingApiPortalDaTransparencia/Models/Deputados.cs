using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsumingApiPortalDaTransparencia.Models
{
    public class Deputados
    {
        public List<DadosBasicosDep> dados { get; set; }
        public List<LinkDep> links { get; set; }
    }
}