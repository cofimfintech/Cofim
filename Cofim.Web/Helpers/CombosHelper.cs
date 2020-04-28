using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Cofim.Common.Model.DataEntity;
using Cofim.Common;
using Cofim.Web.Models;
using Cofim.Common.Model.Response;

namespace Cofim.Web.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _dataContext;
        public CombosHelper(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<SelectListItem> GetComboRoles()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem { Value =  MessageCenter.commonMessageChooseValue, Text = MessageCenter.commonMessageChoose},
                new SelectListItem { Value = "0", Text = RolesWebApp.Admin     },
                new SelectListItem { Value = "1", Text = RolesWebApp.Usuario },
                new SelectListItem { Value = "2", Text = RolesWebApp.EducacionFinanciera    },                
            };

            return list;
        }

        public IEnumerable<SelectListItem> GetComboRolUser()
        {
            var list = new List<SelectListItem>{ new SelectListItem { Value = "3", Text = RolesWebApp.Usuario} };

            return list;
        }

        public IEnumerable<SelectListItem> GetComboStatusTA()
        {
            var list = _dataContext.StatusWebApp.Select(s => new SelectListItem
            {
                Text  = s.Nombre,
                Value = s.Id.ToString()
            }).OrderBy(v => v.Value).ToList();

            list.Insert(0, new SelectListItem{ Text  = MessageCenter.commonMessageChoose,
                                               Value = MessageCenter.commonMessageChooseValue
                                            });

            return list;
        }      


        public string GetComboRolesByValue(string value)
        {
            var roles = GetComboRoles();

            return roles.Where(r => r.Value == value).Select(r => r.Text).FirstOrDefault();

        }

    }//Class
}//Namespace
