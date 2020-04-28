using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cofim.Common.Model.Request
{
    public class NewUserRequest
    {
        [Key]
        public int Id { get; set; }

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

        [Display(Name = "Correo Electrónico")]
        [Required(ErrorMessage = MessageCenter.webAppTextEmailRequired)]
        [EmailAddress(ErrorMessage = MessageCenter.webAppTextEmailInvalid)]
        public string Correo { get; set; }


        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = MessageCenter.webAppTextFieldRequired)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = MessageCenter.webAppTextFieldBetween)]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name = "Confirmar contraseña")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = MessageCenter.webAppTextFieldBetween)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }

        [Display(Name = "Registro como:")]
        [Required(ErrorMessage = MessageCenter.webAppTextFieldRequired)]
        [Range(0, int.MaxValue, ErrorMessage = MessageCenter.webAppTextFieldRequiredCombo)]
        public int RoleId { get; set; }

        public string Rol { get; set; }
    }
}
