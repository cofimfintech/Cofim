using System;
using System.ComponentModel.DataAnnotations;


namespace Cofim.Common.Model.Request
{
    public class EmailRequest
    {
        [Required    (ErrorMessage = MessageCenter.webAppTextEmailRequired)]
        [EmailAddress(ErrorMessage = MessageCenter.webAppTextEmailInvalid) ]
        public String Email { get; set; }
    }
}
