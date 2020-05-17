using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cofim.Common.Model;
using Cofim.Common.Model.DataEntity;

namespace Cofim.Common.Services
{
    public class MenuService : IMenuService
    {
        
        public static List<Menu> GenerateMenuApp( string rol)
        {
          var menu = new List<Menu>
            {
                new Menu{ Icon     = "ic_action_home",
                          PageName = "TramitesPage",
                          Title    = "Tramites"
                        }
                /*,new Menu{ Icon = "ic_action_list_alt",
                           PageName = "ContractsPage",
                           Title = "Contracts"
                         }
               */
                ,new Menu{Icon     = "ic_action_person",
                          PageName = "ModifyUserPage",
                          Title    = "Perfil"
                         }
                ,new Menu{ Icon     = "ic_action_map",
                           PageName = "MapPage",
                           Title    = "Map"
                }
                ,new Menu{ Icon     = "ic_action_exit_to_app",
                           PageName = "LoginPage",
                           Title    = "Cerrar Sesión"
                        }
            };

            return menu;
        }

        public List<Menu> GenerateMenuWebAppRightHeader(bool IsAuthenticated, string name)
        {
            if (IsAuthenticated)
                return new List<Menu>
                {
                    new Menu{ Icon       = "fa fa-user",
                              PageName   = "ChangeUser",
                              Controller = "Account",
                              Title      = name
                            },
                    new Menu{ Icon       = "fa fa-sign-out",
                              PageName   = "Logout",
                              Controller = "Account",
                              Title      = "Salir"
                            }
                };
            else
                return new List<Menu>
                {
                    new Menu()
                };
        }

        public List<Menu> GenerateMenuWebAppLeftHeader(bool IsAuthenticated, string rol, string sectionActive)
        {
            if (IsAuthenticated && rol == RolesWebApp.Admin)
            return new List<Menu>
            {
                new Menu{ Icon       = "fa fa-users",
                          PageName   = "Index",
                          Controller = "Usuarios",
                          Title      = "Usuarios"
                        },
                new Menu{ Icon       = "fa fa-upload",
                          PageName   = "Index",
                          Controller = "Etl",
                          Title      = "Carga Info"
                        },
                new Menu{ Icon       = "fa fa-briefcase",
                          PageName   = "Index",
                          Controller = "Portafolio",
                          Title      = "Portafolios"
                        },
                new Menu{ Icon       = "fa fa-chart-bar",
                          PageName   = "Index",
                          Controller = "Rendimientos",
                          Title      = "Rendimientos"
                        }
            };            

           

            if (IsAuthenticated && rol == RolesWebApp.Usuario)
                return new List<Menu>
                {
                    new Menu{ Icon   = "fa fa-briefcase",
                          PageName   = "Index",
                          Controller = "Portafolios",
                          Title      = "Mi Portafolio"
                        },
                new Menu{ Icon       = "fa fa-chart-bar",
                          PageName   = "Index",
                          Controller = "Rendimientos",
                          Title      = "Rendimientos"
                        }
                };

            return new List<Menu> {  new Menu{ Icon       = "fa fa-home",
                                               PageName   = "Index",
                                               Controller = "Home",
                                               Title      = "Inicio",
                                               ActiveClass= sectionActive=="HomeIndex" ? "active" :""
                                             }
                                   , new Menu{ Icon       = "fa fa-id-card",
                                               PageName   = "Price",
                                               Controller = "Home",
                                               Title      = "Precios",
                                               ActiveClass= sectionActive=="HomePrice" ? "active" :""
                                             }
                                   , new Menu{ Icon       = "fa fa-user",
                                               PageName   = "Login",
                                               Controller = "Account",
                                               Title      = "Portal",
                                               ActiveClass= sectionActive=="PortalLogin" ? "active" :""
                                             }
                                  };
        }

    }//Class

}//NameSpace
