using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cofim.Common.Model.DataEntity
{
    public class FondosInversionMontosMinimos
    {
        /*fondoKey 0420FondoSerie*/
        [Key]
        [Required (ErrorMessage = MessageCenter.webAppTextFieldRequired)]
        [MaxLength(34, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        public string FondoKey { get; set; }

        [Display      (Name = "Fecha")]
        [Required     (ErrorMessage          = MessageCenter.webAppTextFieldRequired)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        [Display(Name = "FechaLocal")]
        public DateTime FechaLocal => Fecha.ToLocalTime();

        [Display  (Name = "Operadora")]
        [Required (ErrorMessage      = MessageCenter.webAppTextFieldRequired)]
        [MaxLength(50, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        public string Operadora { get; set; }

        [Display  (Name = "Fondo")]
        [Required (ErrorMessage      = MessageCenter.webAppTextFieldRequired)]
        [MaxLength(20, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        public string Fondo { get; set; }

        [Display  (Name = "Serie")]
        [Required (ErrorMessage      = MessageCenter.webAppTextFieldRequired)]
        [MaxLength(10, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        public string Serie { get; set; }

        [Display  (Name = "FondoSerie")]        
        [Required (ErrorMessage      = MessageCenter.webAppTextFieldRequired)]
        [MaxLength(30, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        public string FondoSerie { get; set; }

        [Display  (Name = "MontoMinimo")]
        [Required (ErrorMessage      = MessageCenter.webAppTextFieldRequired)]
        [MaxLength(10, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        [DisplayFormat(DataFormatString = "{0:C1}", ApplyFormatInEditMode = false)]
        [Column(TypeName = "decimal(18,1)")]
        public decimal MontoMinimo { get; set; }

        [Display  (Name = "MontoMinimoTipo")]
        [Required (ErrorMessage     = MessageCenter.webAppTextFieldRequired)]
        [MaxLength(2, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        public char MontoMinimoTipo { get; set; }

        [Display(Name = "Divisa")]
        [Required(ErrorMessage     = MessageCenter.webAppTextFieldRequired)]
        [MaxLength(4, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        public string Divisa { get; set; }

        [Display  (Name = "Activo")]
        [Required (ErrorMessage = MessageCenter.webAppTextFieldRequired)]
        [MaxLength(2, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        public char Activo { get; set; }

        [Display(Name = "PersonaFisica")]        
        public bool PersonaFisica { get; set; }

        [Display(Name = "PersonaMoral")]
        public bool PersonaMoral { get; set; }

        [Display(Name = "PersonaMoralNoContribuyente")]
        public bool PersonaMoralNoContribuyente { get; set; }

        [Display(Name = "SociedadesDeInversion")]
        public bool SociedadesDeInversion { get; set; }

        [Required(ErrorMessage = MessageCenter.webAppTextFieldRequired)]
        [MaxLength(20, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        public string IdLoadFile { get; set; }

        /**************RELATIONSHIP*****************/
        [Display(Name = "Precios")]
        public ICollection<VectorPrecio> Precios { get; set; }

    }//Class
}//namepace
