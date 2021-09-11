using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsumingApiPortalDaTransparencia.Models
{
    public class DeputadosDespesas
    {
        public int ano { get; set; }
        public string cnpjCpfFornecedor { get; set; }
        public int codDocumento { get; set; }
        public int codLote { get; set; }
        public int codTipoDocumento { get; set; }
        public string dataDocumento { get; set; }
        public int mes { get; set; }
        public string nomeFornecedor { get; set; }
        public string numDocumento { get; set; }
        public string numRessarcimento { get; set; }
        public int parcela { get; set; }
        public string tipoDespesa { get; set; }
        public string tipoDocumento { get; set; }
        public string urlDocumento { get; set; }
        public int valorDocumento { get; set; }
        public int valorGlosa { get; set; }
        public int valorLiquido { get; set; }
    }
}