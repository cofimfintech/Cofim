using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Cofim.Common;
using Cofim.Common.Model;
using Cofim.Common.Model.DataEntity;
using Cofim.Common.Services;
using Cofim.Web.Helpers;
using Cofim.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cofim.Web.Controllers
{

    public class PortalController : Controller
    {
        private readonly IUserHelper   _userHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IMailHelper   _mailHelper;
        private readonly IMenuService  _menuService;
        private readonly DataContext   _dataContext;

        public PortalController(IUserHelper userHelper, ICombosHelper combosHelper, IMailHelper mailHelper, IMenuService menuService, DataContext context)
        {
            _userHelper   = userHelper;
            _combosHelper = combosHelper;
            _mailHelper   = mailHelper;
            _menuService  = menuService;
            _dataContext  = context;
        }
        public IActionResult Index()
            {
                if (!User.Identity.IsAuthenticated)
                   { return RedirectToAction("Login", "Account"); }

                ViewBag.MenuLeft   = _menuService.GenerateMenuWebAppLeftHeader(User.Identity.IsAuthenticated, _userHelper.GetRol((User.Identity as ClaimsIdentity)).FirstOrDefault(), "HomePrice");
                ViewBag.MenuRight  = _menuService.GenerateMenuWebAppRightHeader(User.Identity.IsAuthenticated, User.Identity.Name);
                ViewBag.datasource = _dataContext.FormasDePago.ToList();

                spacingModel modelValue = new spacingModel { CellSpacing = new double[] { 10, 10 } };

                return View(modelValue);
            
            }

        public void test()
        {
            var personaFisica = true;
            var personaMoral  = true;
            var personaMoralNoContri = false;
            var sociedadesDeInversion = false;
            var montoMin      = 10000000;

            ICollection<FondosInversionMontosMinimos> fondos = _dataContext.FondosInversionMontosMinimos.Include(fi => fi.Precios).Where(fi => (fi.Activo == MessageCenter.FONDO_ACTIVO) && (fi.MontoMinimo >= montoMin) ).ToList();
            fondos = personaFisica == true ? fondos.Where(fi => fi.PersonaFisica == true).ToList() : fondos;
            fondos = personaMoral  == true ? fondos.Where(fi => fi.PersonaMoral  == true).ToList() : fondos;
            fondos = personaMoralNoContri == true ? fondos.Where(fi => fi.PersonaMoralNoContribuyente == true).ToList() : fondos;
            fondos = sociedadesDeInversion == true ? fondos.Where(fi => fi.SociedadesDeInversion == true).ToList() : fondos;

            foreach (var f in fondos)
                   Console.WriteLine($" FONDO: {f.FondoSerie} - {f.MontoMinimo} - {f.Precios.Count} - {f.PersonaFisica} - {f.PersonaMoral}  ");

            Console.WriteLine($" registros: {fondos.Count} ");


        }//test
    }
}