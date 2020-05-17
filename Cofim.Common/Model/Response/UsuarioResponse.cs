using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Cofim.Common.Model.DataEntity;

namespace Cofim.Common.Model.Response
{
    public class UsuarioResponse
    {
        public int Id { get; set; }
        
        [Display(Name = "Nombre")]        
        public string FirstName { get; set; }

        [Display(Name = "Apellido")]        
        public string LastName { get; set; }

        [Display(Name = "Teléfono Fijo")]        
        public string FixedPhone { get; set; }
        
        [Display(Name = "Celular")]
        public string CellPhone { get; set; }
        
        [Display(Name = "Correo Electrónico")]
        public string Correo { get; set; }

        [Display(Name = "Rol")]
        public string Rol { get; set; }

        [Display(Name = "Nombre del Usuario")]
        public string FullName => $"{FirstName} {LastName}";

        [Display(Name = "Dirección Fiscal")]
        public DatosFiscalesResponse DatosFiscalesResponse { get; set; }

        [Display(Name = "Formas de Pago")]
        public ICollection<FormaDePagoResponse> FormasDePago { get; set; }



    }//CLASS
}//NAMESPACE
