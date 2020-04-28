using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cofim.Common.Model.DataEntity
{
    public class VectorPrecioBMV
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Fecha")]
        [Required(ErrorMessage = MessageCenter.webAppTextFieldRequired)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        [Display(Name = "FechaLocal")]
        public DateTime FechaLocal => Fecha.ToLocalTime();

        [Display(Name = "Nombre Corto Fondo Inversion")]
        [Required(ErrorMessage      = MessageCenter.webAppTextFieldRequired)]
        [MaxLength(30, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        public string NombreCortoFondoInv { get; set; }

        [Display(Name = "PrecioBMV")]
        [Required(ErrorMessage     = MessageCenter.webAppTextFieldRequired)]
        [MaxLength(7, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Column(TypeName = "decimal(18,4)")]
        public decimal PrecioBMV { get; set; }

        [Display(Name = "Rendimiento Diario")]        
        [MaxLength(7, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Column(TypeName = "decimal(18,4)")]
        public decimal RendimientoDiario { get; set; }

    }//Class
}//Namespace
