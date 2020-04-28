using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cofim.Web.Helpers
{
    public class MessageErrorHelper
    {
        public static string showIdentityResultError(IdentityResult result)
        {
            var re = result.Errors.Select(x => new { x.Code, x.Description }).ToArray();
            var error = "";
            foreach (var e in re)
            { error += e.Description ; }


            return error;

        }

        public static string showModelStateError(ModelStateDictionary ModelState)
        {
            string validationErrors = string.Join(","
                                                 , ModelState.Values.Where(E => E.Errors.Count > 0)
                                                                    .SelectMany(E => E.Errors)
                                                                    .Select(E => E.ErrorMessage)
                                                                    .ToArray());


            return validationErrors;

        }
    }//CLASS
}//Namespace