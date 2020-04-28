using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cofim.Common.Model.Response
{
    public class DatosFiscalesResponse
    {
        [Display(Name = "Registro Federal de Contribuyente")]        
        public string Rfc { get; set; }

        [Display(Name = "Calle")]        
        public string Calle { get; set; }

        [Display(Name = "Número Exterior")]        
        public int NumeroExterior { get; set; }

        [Display(Name = "Número Interior")]        
        public string NumeroInterior { get; set; }

        [Display(Name = "Colonia")]        
        public string Colonia { get; set; }

        [Display(Name = "Alcaldia/Municipio")]        
        public string AlcaldiaMunicipio { get; set; }

        [Display(Name = "Estado")]        
        public string Estado { get; set; }

        [Display(Name = "Código Postal")]        
        public int CodigoPostal { get; set; }
    }//CLASS
}//NAMESPACE
