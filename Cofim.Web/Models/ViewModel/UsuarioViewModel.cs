using System;
using Cofim.Common;
using System.ComponentModel.DataAnnotations;
using Cofim.Common.Model.Request;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Cofim.Web.Models.ViewModel
{
    public class UsuarioViewModel : NewUserRequest
    {
        
        public IEnumerable<SelectListItem> Roles { get; set; }

    }
}
