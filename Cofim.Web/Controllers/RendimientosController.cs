using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Cofim.Common;
using Cofim.Common.Model;
using Cofim.Common.Model.DataEntity;
using Cofim.Common.Model.Request;
using Cofim.Common.Model.Response;
using Cofim.Common.Services;
using Cofim.Web.Helpers;
using Cofim.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Syncfusion.EJ2.Base;

namespace Cofim.Web.Controllers
{
    public class RendimientosController : Controller
    {
        private readonly ILogger<RendimientosController> _logger;
        private readonly DataContext  _dataContext;
        private readonly IMenuService _menuService;
        private readonly IUserHelper  _userHelper;

        public RendimientosController(DataContext dataContext, ILogger<RendimientosController> logger, IMenuService menuService, IUserHelper userHelper)
        {
            _dataContext = dataContext;
            _logger      = logger;
            _menuService = menuService;
            _userHelper  = userHelper;
        }

    public IActionResult Index()
        {
            if ( !User.Identity.IsAuthenticated )
               { return RedirectToAction("Login", "Account"); }

            ViewBag.MenuLeft  = _menuService.GenerateMenuWebAppLeftHeader (User.Identity.IsAuthenticated, _userHelper.GetRol((User.Identity as ClaimsIdentity)).FirstOrDefault(), "HomePrice");
            ViewBag.MenuRight = _menuService.GenerateMenuWebAppRightHeader(User.Identity.IsAuthenticated, User.Identity.Name);           
            
            var startdate = DateTime.Now.AddDays(MessageCenter.TREINTAYSEIS_MESES);
            FilterFondoInversion filterFI = new FilterFondoInversion
            {
                AdquirentesData     = new Items().AdquirentesLists(),
                AdquirentesSelected = "",
                FondosData          = _dataContext.FondosInversionMontosMinimos.Where(fi => fi.Operadora == "CI Fondos" || fi.Operadora == "BBVA Bancomer" || fi.Operadora == "SANTANDER").Where(fi => fi.Activo == MessageCenter.FONDO_ACTIVO).Select(fp => new Items { Id = fp.FondoSerie, Name = fp.FondoSerie, Category = fp.Operadora }).OrderBy(f=>f.Category).ToList(),
                FondosSelected      = "",
                StartDate           = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),                
                EndDate             = new DateTime(startdate.Year, startdate.Month, startdate.Day),            
            };
           
            return View(filterFI);

        }//index     


        public IActionResult LoadDataDirectos()
        {
            try
            {   var draw                = HttpContext.Request.Form["draw"].FirstOrDefault();                
                var start               = Request.Form["start"].FirstOrDefault(); // Skip number of Rows count                  
                var length              = Request.Form["length"].FirstOrDefault(); // Paging Length 10,20                  
                var sortColumn          = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();// Sort Column Name  
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();                // Sort Column Direction (asc, desc)  
                var searchValue         = Request.Form["search[value]"].FirstOrDefault();                // Search Value from (Search box)  
                int pageSize            = length != null ? Convert.ToInt32(length) : 0;                //Paging Size (10, 20, 50,100)  
                int skip                = start  != null ? Convert.ToInt32(start)  : 0;
                int recordsTotal        = 0;
                Console.WriteLine($"DataTable pageSize: {pageSize} - skip: {skip} - searchValue: {searchValue}");

                FilterFondoInversion model = new FilterFondoInversion
                {
                    MontoMinimo         = Convert.ToDecimal(HttpContext.Request.Form["montoMinimo"].FirstOrDefault(), new CultureInfo("en-US") ),
                    StartDate           = DateTime.ParseExact(HttpContext.Request.Form["StartDate"].FirstOrDefault(), "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    AdquirentesSelected = HttpContext.Request.Form["AdquirentesSelected"].FirstOrDefault(),
                    FondosSelected      = HttpContext.Request.Form["FondosSelected"].FirstOrDefault(),                    
                };
                Console.WriteLine($"My Mapping: {model.MontoMinimo} - {model.StartDate} - {model.AdquirentesSelected} - {model.FondosSelected} ");
             
                string whereDateR = $" AND VP.Fecha='{model?.StartDate.ToString("yyyy-MM-dd")}'";
                string[] fondosSelected = model?.FondosSelected == null ? new string[0] : model?.FondosSelected.Split(',');
                string whereFondos = "";
                foreach (var f in fondosSelected)
                {
                    if (f == "") { break; }
                    whereFondos += $",'{f}'";
                }
                whereFondos = (whereFondos == "" ? "" : $" AND FI.FondoSerie IN ({whereFondos.Remove(0, 1)})");

                string[] adquirentesSelected = model?.AdquirentesSelected == null ? new string[0] : model?.AdquirentesSelected.Split(',');
                string whereAdq = "";
                foreach (var ad in adquirentesSelected)
                {
                    whereAdq += ad switch
                    {
                        "PF" => $" OR FI.PersonaFisica = 1",
                        "PM" => $" OR FI.PersonaMoral = 1",
                        "PMNC" => $" OR FI.PersonaMoralNoContribuyente = 1",
                        "SI" => $" OR FI.SociedadesDeInversion = 1",
                        _ => "",
                    };
                }
                whereAdq = (whereAdq == "" ? "" : $" AND ( {whereAdq.Remove(0, 3)} )");

                string QueryRendimientos = $" SELECT * FROM FondosInversionMontosMinimos FI " +
                                           $" WHERE FI.Activo ='{MessageCenter.FONDO_ACTIVO}' AND FI.MontoMinimo >= {model.MontoMinimo} {whereFondos} {whereAdq}";

                Console.WriteLine($"QUERY: {QueryRendimientos}");
                //Paging   
                var fondos = _dataContext.FondosInversionMontosMinimos.FromSqlRaw(QueryRendimientos)
                                                                      .Include(fi => fi.Precios).Where(p => p.Precios.Any(vp => vp.Fecha == model.StartDate))                                                                      
                                                                      .ToList<FondosInversionMontosMinimos>();
                Console.WriteLine($"TOTAL: {fondos.Count}");
               
                List<RendimientoResp> fondosRendimiento = fondos?.Select(fi => new RendimientoResp
                {
                    Operadora = fi.Operadora,
                    Fondo = fi.Fondo,
                    Serie = fi.Serie,
                    FondoSerie = fi.FondoSerie,
                    RendiDirCuatroDias        = fi?.Precios?.FirstOrDefault(vp => vp.Fecha == model.StartDate && vp.FondoKey == fi.FondoKey)?.RendiDirCuatroDias,
                    RendiDirSieteDias         = fi?.Precios?.FirstOrDefault(vp => vp.Fecha == model.StartDate && vp.FondoKey == fi.FondoKey)?.RendiDirSieteDias,
                    RendiDirUnMes             = fi?.Precios?.FirstOrDefault(vp => vp.Fecha == model.StartDate && vp.FondoKey == fi.FondoKey)?.RendiDirUnMes,
                    RendiDirTresMeses         = fi?.Precios?.FirstOrDefault(vp => vp.Fecha == model.StartDate && vp.FondoKey == fi.FondoKey)?.RendiDirTresMeses,
                    RendiDirSeisMeses         = fi?.Precios?.FirstOrDefault(vp => vp.Fecha == model.StartDate && vp.FondoKey == fi.FondoKey)?.RendiDirSeisMeses,
                    RendiDirNueveMeses        = fi?.Precios?.FirstOrDefault(vp => vp.Fecha == model.StartDate && vp.FondoKey == fi.FondoKey)?.RendiDirNueveMeses,
                    RendiDirDoceMeses         = fi?.Precios?.FirstOrDefault(vp => vp.Fecha == model.StartDate && vp.FondoKey == fi.FondoKey)?.RendiDirDoceMeses,
                    RendiDirDiesiOchoMeses    = fi?.Precios?.FirstOrDefault(vp => vp.Fecha == model.StartDate && vp.FondoKey == fi.FondoKey)?.RendiDirDiesiOchoMeses,
                    RendiDirVeintiCuatroMeses = fi?.Precios?.FirstOrDefault(vp => vp.Fecha == model.StartDate && vp.FondoKey == fi.FondoKey)?.RendiDirVeintiCuatroMeses,
                    RendiDirTreintaySeisMeses = fi?.Precios?.FirstOrDefault(vp => vp.Fecha == model.StartDate && vp.FondoKey == fi.FondoKey)?.RendiDirTreintaySeisMeses,
                    RendiAnuCuatroDias        = fi?.Precios?.FirstOrDefault(vp => vp.Fecha == model.StartDate && vp.FondoKey == fi.FondoKey)?.RendiAnuCuatroDias,
                    RendiAnuSieteDias         = fi?.Precios?.FirstOrDefault(vp => vp.Fecha == model.StartDate && vp.FondoKey == fi.FondoKey)?.RendiAnuSieteDias,
                    RendiAnuUnMes             = fi?.Precios?.FirstOrDefault(vp => vp.Fecha == model.StartDate && vp.FondoKey == fi.FondoKey)?.RendiAnuUnMes,
                    RendiAnuTresMeses         = fi?.Precios?.FirstOrDefault(vp => vp.Fecha == model.StartDate && vp.FondoKey == fi.FondoKey)?.RendiAnuTresMeses,
                    RendiAnuSeisMeses         = fi?.Precios?.FirstOrDefault(vp => vp.Fecha == model.StartDate && vp.FondoKey == fi.FondoKey)?.RendiAnuSeisMeses,
                    RendiAnuNueveMeses        = fi?.Precios?.FirstOrDefault(vp => vp.Fecha == model.StartDate && vp.FondoKey == fi.FondoKey)?.RendiAnuNueveMeses,
                    RendiAnuDoceMeses         = fi?.Precios?.FirstOrDefault(vp => vp.Fecha == model.StartDate && vp.FondoKey == fi.FondoKey)?.RendiAnuDoceMeses,
                    RendiAnuDiesiOchoMeses    = fi?.Precios?.FirstOrDefault(vp => vp.Fecha == model.StartDate && vp.FondoKey == fi.FondoKey)?.RendiAnuDiesiOchoMeses,
                    RendiAnuVeintiCuatroMeses = fi?.Precios?.FirstOrDefault(vp => vp.Fecha == model.StartDate && vp.FondoKey == fi.FondoKey)?.RendiAnuVeintiCuatroMeses,
                    RendiAnuTreintaySeisMeses = fi?.Precios?.FirstOrDefault(vp => vp.Fecha == model.StartDate && vp.FondoKey == fi.FondoKey)?.RendiAnuTreintaySeisMeses,
                }).ToList();
                /*               
                //Sorting  
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                   { rendimientosData = rendimientosData.OrderBy(sortColumn + " " + sortColumnDirection); }
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                   { rendimientosData = rendimientosData.Where<RendimientoResp>(m => m.Operadora == searchValue ); }
                 */
                                
                recordsTotal = fondosRendimiento.Count(); //total number of rows counts   
                //Paging   
                var data = fondosRendimiento.Skip(skip).Take(pageSize).ToList();
                //Returning Json Data  
                return Json(new { draw, recordsFiltered = recordsTotal, recordsTotal, data });

            }
            catch (Exception e) { Console.WriteLine($"POST LoadData: {e.Message}");
                                  return Json(new { draw = 0, recordsFiltered = 0, recordsTotal = 0, data = new List<RendimientoResp>() });
                                }

        }//LoadData

        [HttpPost]
        public ActionResult ChartDataDirectos()
        {
            List<ChartDataStackingLineResp> data = new List<ChartDataStackingLineResp>
            {
                new ChartDataStackingLineResp { X = "4 Días", Y = 90, Y1 = 40, Y2 = 70, Y3 = 120 },
                new ChartDataStackingLineResp { X = "7 Días", Y = 80, Y1 = 90, Y2 = 110, Y3 = 70 },
                new ChartDataStackingLineResp { X = "1 Mes", Y = 50, Y1 = 80, Y2 = 120, Y3 = 50 },
                new ChartDataStackingLineResp { X = "3 Meses", Y = 70, Y1 = 30, Y2 = 60, Y3 = 180 },
                new ChartDataStackingLineResp { X = "6 Meses", Y = 30, Y1 = 80, Y2 = 80, Y3 = 30 },
                new ChartDataStackingLineResp { X = "9 Meses", Y = 10, Y1 = 40, Y2 = 30, Y3 = 270 },
                new ChartDataStackingLineResp { X = "12 Meses", Y = 100, Y1 = 30, Y2 = 70, Y3 = 40 },
                new ChartDataStackingLineResp { X = "18 Meses", Y = 55, Y1 = 95, Y2 = 55, Y3 = 75 },
                new ChartDataStackingLineResp { X = "24 Meses", Y = 20, Y1 = 50, Y2 = 40, Y3 = 65 },
                new ChartDataStackingLineResp { X = "36 Meses", Y = 40, Y1 = 20, Y2 = 80, Y3 = 95 },                
            };

            List<SeriesStackingLineResp> series = new List<SeriesStackingLineResp>
            {
                new SeriesStackingLineResp { Type="StackingLine100", DataSource=data, Marker=new MarkerClass{ Visible=true }, DashArray="5, 1", XName="x", Width=2, YName="y" , Name="SAURORT B1"   },
                new SeriesStackingLineResp { Type="StackingLine100", DataSource=data, Marker=new MarkerClass{ Visible=true }, DashArray="5, 1", XName="x", Width=2, YName="y1", Name="SAURORT B2"  },
                new SeriesStackingLineResp { Type="StackingLine100", DataSource=data, Marker=new MarkerClass{ Visible=true }, DashArray="5, 1", XName="x", Width=2, YName="y2", Name="SAURORT B3"  },
                new SeriesStackingLineResp { Type="StackingLine100", DataSource=data, Marker=new MarkerClass{ Visible=true }, DashArray="5, 1", XName="x", Width=2, YName="y3", Name="VALUEV5 B" },
            };

            return Json(series);
        }


        public IActionResult LoadDataAnual()
        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault(); // Skip number of Rows count                  
                var length = Request.Form["length"].FirstOrDefault(); // Paging Length 10,20                  
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();// Sort Column Name  
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();                // Sort Column Direction (asc, desc)  
                var searchValue = Request.Form["search[value]"].FirstOrDefault();                // Search Value from (Search box)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;                //Paging Size (10, 20, 50,100)  
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                List<RendimientoResp> fondosRendimiento = new List<RendimientoResp>();
                var data = fondosRendimiento.Skip(skip).Take(pageSize).ToList();

                //Returning Json Data  
                return Json(new { draw, recordsFiltered = recordsTotal, recordsTotal, data });
            }
            catch (Exception e)
            {
                Console.WriteLine($"POST LoadData: {e.Message}");
                return Json(new { draw = 0, recordsFiltered = 0, recordsTotal = 0, data = new List<RendimientoResp>() });
            }
        }


        [HttpPost]
        public ActionResult ChartDataAnual()
        {
            List<ChartDataStackingLineResp> data = new List<ChartDataStackingLineResp>
            {
                new ChartDataStackingLineResp { X = "4 Días", Y = 90, Y1 = 40, Y2 = 70, Y3 = 120 },
                new ChartDataStackingLineResp { X = "7 Días", Y = 80, Y1 = 90, Y2 = 110, Y3 = 70 },
                new ChartDataStackingLineResp { X = "1 Mes", Y = 50, Y1 = 80, Y2 = 120, Y3 = 50 },
                new ChartDataStackingLineResp { X = "3 Meses", Y = 70, Y1 = 30, Y2 = 60, Y3 = 180 },
                new ChartDataStackingLineResp { X = "6 Meses", Y = 30, Y1 = 80, Y2 = 80, Y3 = 30 },
                new ChartDataStackingLineResp { X = "9 Meses", Y = 10, Y1 = 40, Y2 = 30, Y3 = 270 },
                new ChartDataStackingLineResp { X = "12 Meses", Y = 100, Y1 = 30, Y2 = 70, Y3 = 40 },
                new ChartDataStackingLineResp { X = "18 Meses", Y = 55, Y1 = 95, Y2 = 55, Y3 = 75 },
                new ChartDataStackingLineResp { X = "24 Meses", Y = 20, Y1 = 50, Y2 = 40, Y3 = 65 },
                new ChartDataStackingLineResp { X = "36 Meses", Y = 40, Y1 = 20, Y2 = 80, Y3 = 95 },
            };

            List<SeriesStackingLineResp> series = new List<SeriesStackingLineResp>
            {
                new SeriesStackingLineResp { Type="StackingLine100", DataSource=data, Marker=new MarkerClass{ Visible=true }, DashArray="5, 1", XName="x", Width=2, YName="y" , Name="SAURORT B1"   },
                new SeriesStackingLineResp { Type="StackingLine100", DataSource=data, Marker=new MarkerClass{ Visible=true }, DashArray="5, 1", XName="x", Width=2, YName="y1", Name="SAURORT B2"  },
                new SeriesStackingLineResp { Type="StackingLine100", DataSource=data, Marker=new MarkerClass{ Visible=true }, DashArray="5, 1", XName="x", Width=2, YName="y2", Name="SAURORT B3"  },
                new SeriesStackingLineResp { Type="StackingLine100", DataSource=data, Marker=new MarkerClass{ Visible=true }, DashArray="5, 1", XName="x", Width=2, YName="y3", Name="VALUEV5 B" },
            };

            return Json(series);
        }


    }//class
}//namespace