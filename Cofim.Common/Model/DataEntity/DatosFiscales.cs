using System.ComponentModel.DataAnnotations;
using Cofim.Common;

namespace Cofim.Common.Model.DataEntity
{
    public class DatosFiscales
    {        
        [Key]
        [Display(Name = "Registro Federal de Contribuyente")]
        [Required(ErrorMessage      = MessageCenter.webAppTextFieldRequired)]
        [MaxLength(30, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        public string Rfc { get; set; }

        [Display(Name = "Calle")]
        [Required(ErrorMessage      = MessageCenter.webAppTextFieldRequired)]
        [MaxLength(50, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        public string Calle { get; set; }

        [Display(Name = "Número Exterior")]
        [Required(ErrorMessage      = MessageCenter.webAppTextFieldRequired)]
        [MaxLength(6, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        public int NumeroExterior { get; set; }

        [Display(Name = "Número Interior")]
        [Required(ErrorMessage      = MessageCenter.webAppTextFieldRequired)]
        [MaxLength(20, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        public string NumeroInterior { get; set; }

        [Display(Name = "Colonia")]
        [Required(ErrorMessage      = MessageCenter.webAppTextFieldRequired)]
        [MaxLength(50, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        public string Colonia { get; set; }

        [Display(Name = "Alcaldia/Municipio")]
        [Required(ErrorMessage      = MessageCenter.webAppTextFieldRequired)]
        [MaxLength(60, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        public string AlcaldiaMunicipio { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage      = MessageCenter.webAppTextFieldRequired)]
        [MaxLength(50, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        public string Estado { get; set; }

        [Display(Name = "Código Postal")]
        [Required(ErrorMessage     = MessageCenter.webAppTextFieldRequired)]
        [MaxLength(6, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        public int CodigoPostal { get; set; }

        [Display(Name = "Domicilio Fiscal")]
        public string DomicilioFiscal => $"{Calle} {NumeroExterior} {NumeroInterior} {Colonia} {AlcaldiaMunicipio} {Estado} CP {CodigoPostal}";
    }
}
