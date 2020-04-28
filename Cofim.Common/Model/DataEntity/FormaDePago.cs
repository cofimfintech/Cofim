using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cofim.Common.Model.DataEntity
{
    public class FormaDePago
    {
        [Key]
        public int Id { get; set; }

        public int IdOpenPay { get; set; }

        [Display  (Name = "Tipo")]        
        [MaxLength(10, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        public string Tipo { get; set; }

        [Display  (Name = "Nombre")]
        [MaxLength(30, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        public string Nombre { get; set; }

        [Display  (Name = "Descripción")]
        [MaxLength(100, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        public string Descripcion { get; set; }


    }//CLASS
}//NAMESPACE
