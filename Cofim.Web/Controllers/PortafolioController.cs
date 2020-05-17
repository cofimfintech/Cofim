using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Cofim.Common.Services;
using Cofim.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cofim.Web.Controllers
{
    public class PortafolioController : Controller
    {
        private readonly ILogger<PortafolioController> _logger;
        private readonly IMenuService _menuService;
        private readonly IUserHelper _userHelper;

        public PortafolioController(ILogger<PortafolioController> logger, IMenuService menuService, IUserHelper userHelper)
        {
            _logger      = logger;
            _menuService = menuService;
            _userHelper  = userHelper;
        }


        public IActionResult Index()
        {

            if (!User.Identity.IsAuthenticated)
            { return RedirectToAction("Login", "Account"); }

            ViewBag.MenuLeft = _menuService.GenerateMenuWebAppLeftHeader(User.Identity.IsAuthenticated, _userHelper.GetRol((User.Identity as ClaimsIdentity)).FirstOrDefault(), "HomePrice");
            ViewBag.MenuRight = _menuService.GenerateMenuWebAppRightHeader(User.Identity.IsAuthenticated, User.Identity.Name);

            return View();
        }
    }
}