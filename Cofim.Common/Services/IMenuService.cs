using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cofim.Common.Model;

namespace Cofim.Common.Services
{
    public interface IMenuService
    {        
        List<Menu> GenerateMenuWebAppRightHeader(bool IsAuthenticated, string name);

        List<Menu> GenerateMenuWebAppLeftHeader(bool IsAuthenticated, string rol, string section);
    }//Class

}//NameSpace