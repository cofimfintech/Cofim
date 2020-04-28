
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Cofim.Common.Model.Response;

namespace Cofim.Web.Helpers
{
    public interface ICombosHelper
    {        

        IEnumerable<SelectListItem> GetComboRoles();

        IEnumerable<SelectListItem> GetComboRolUser();

        string GetComboRolesByValue(string value);

        

    }//Class
}//Namespace
