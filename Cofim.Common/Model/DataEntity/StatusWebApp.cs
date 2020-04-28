using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cofim.Common.Model.DataEntity
{
    public class StatusWebApp
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = MessageCenter.webAppTextFieldRequired)]
        [MaxLength(50, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(300, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        public string Descripcion { get; set; }

        [Display(Name = "Orden")]
        [Required(ErrorMessage = MessageCenter.webAppTextFieldRequired)]
        public int Orden { get; set; }

      

    }//CLASS
}//NAMESPACE
