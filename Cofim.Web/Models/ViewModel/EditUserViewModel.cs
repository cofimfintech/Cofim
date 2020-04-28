using System.ComponentModel.DataAnnotations;
using Cofim.Common;

namespace Cofim.Web.Models.ViewModel
{
    public class EditUserViewModel
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = MessageCenter.webAppTextFieldRequired)]
        [MaxLength(30, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        public string FirstName { get; set; }

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = MessageCenter.webAppTextFieldRequired)]
        [MaxLength(30, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        public string LastName { get; set; }


        [Required(ErrorMessage = MessageCenter.webAppTextFieldRequired)]
        [Display(Name = "Celular")]
        [Phone(ErrorMessage = MessageCenter.webAppTextPhoneInvalid)]
        public string CellPhone { get; set; }

        [Display(Name = "Contraseña Actual")]
        [Required(ErrorMessage = MessageCenter.webAppTextFieldRequired)]        
        [MaxLength(30, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = MessageCenter.webAppTextFieldBetween)]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Display(Name = "Nueva Contraseña")]
        [Required(ErrorMessage = MessageCenter.webAppTextFieldRequired)]      
        [StringLength(20, MinimumLength = 6, ErrorMessage = MessageCenter.webAppTextFieldBetween)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "Confirmar contraseña")]
        [Required(ErrorMessage = MessageCenter.webAppTextFieldRequired)]        
        [StringLength(20, MinimumLength = 6, ErrorMessage = MessageCenter.webAppTextFieldBetween)]
        [Compare("NewPassword")]
        [DataType(DataType.Password)]
        public string Confirm { get; set; }

    }//Class

}//NameSpace

