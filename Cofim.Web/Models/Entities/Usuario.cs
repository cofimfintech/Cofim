using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Cofim.Common;
using Cofim.Common.Model.DataEntity;

namespace Cofim.Web.Models.Entities
{
    public class Usuario 
    {               

        [Key]
        public int Id { get; set; }

        public UserLogin UserLogin { get; set; }


        [Display(Name = "Nombre")]
        [Required(ErrorMessage      = MessageCenter.webAppTextFieldRequired)]
        [MaxLength(30, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        public string FirstName { get; set; }

        [Display(Name = "Apellido")]
        [Required(ErrorMessage      = MessageCenter.webAppTextFieldRequired)]
        [MaxLength(30, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        public string LastName { get; set; }

        [Display(Name = "Teléfono Fijo")]
        [MaxLength(20, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        public string FixedPhone { get; set; }

        [JsonIgnore]
        [Display(Name = "Celular")]        
        public string CellPhone => $"{UserLogin.PhoneNumber} ";

        [JsonIgnore]
        [Display(Name = "Correo Electrónico")]
        public string Correo  => $"{UserLogin.Email} ";

        [JsonIgnore]
        [Display(Name = "Nombre del Usuario")] 
        public string FullName => $"{FirstName} {LastName}";

       

        [Display(Name = "Dirección Fiscal")]
        public DatosFiscales DatosFiscales { get; set; }

    }//CLASS
}//NAMESPACE
