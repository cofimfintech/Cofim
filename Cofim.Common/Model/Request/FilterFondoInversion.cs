using Cofim.Common.Model.DataEntity;
using Cofim.Common.Model.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cofim.Common.Model.Request
{
    public class FilterFondoInversion
    {        
        public decimal MontoMinimo { get; set; }       
        public string Divisa       { get; set; }        
        public char   Activo       { get; set; }

        public string AdquirentesSelected { get; set; }
        public List<Items> AdquirentesData  { get; set; }

        public string FondosSelected { get; set; }
        public List<Items> FondosData  { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate   { get; set; }


        public bool PersonaFisica  { get; set; }
        public bool PersonaMoral   { get; set; }
        public bool PersonaMoralNoContribuyente { get; set; }
        public bool SociedadesDeInversion { get; set; }
        public ICollection<RendimientoResp> Fondos { get; set; }

    }//CLASS

}//Namespace

