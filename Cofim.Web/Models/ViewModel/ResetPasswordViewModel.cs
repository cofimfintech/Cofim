using System.ComponentModel.DataAnnotations;
using Cofim.Common;

namespace Cofim.Web.Models.ViewModel
{
    public class ResetPasswordViewModel
    {
        [Display(Name = "Correo Electrónico")]
        [Required(ErrorMessage = MessageCenter.webAppTextFieldRequired)]
        public string Email { get; set; }

        [Display(Name = "Nueva Contraseña")]
        [Required(ErrorMessage = MessageCenter.webAppTextFieldRequired)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = MessageCenter.webAppTextFieldBetween)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirmar contraseña")]
        [Required(ErrorMessage = MessageCenter.webAppTextFieldRequired)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = MessageCenter.webAppTextFieldBetween)]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = MessageCenter.webAppTextFieldRequired)]
        public string Token { get; set; }

    }//Class
}//Namespace
