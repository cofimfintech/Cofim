using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cofim.Common.Model.DataEntity
{
    public class TipoAdquirente
    {
        [Key]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Adquirente")]
        [Required(ErrorMessage      = MessageCenter.webAppTextFieldRequired)]
        [MaxLength(40, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        public string Adquirente { get; set; }

        [Display(Name = "NombreCorto")]
        [Required(ErrorMessage = MessageCenter.webAppTextFieldRequired)]
        [MaxLength(10, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        public string NombreCorto { get; set; }
    }
}
