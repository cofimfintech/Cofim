using Cofim.Web.Helpers;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cofim.Common;
using Cofim.Common.Model.Request;
using Cofim.Web.Models.ViewModel;
using Cofim.Common.Model.Response;
using Cofim.Web.Models;
using Cofim.Web.Models.Entities;
using Cofim.Common.Services;
using System.Security.Claims;

namespace Cofim.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper   _userHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IMailHelper   _mailHelper;
        private readonly IMenuService  _menuService;
        private readonly DataContext   _dataContext;

        public AccountController(IUserHelper userHelper, ICombosHelper combosHelper, IMailHelper mailHelper, IMenuService menuService, DataContext context)
        {
            _userHelper   = userHelper;
            _combosHelper = combosHelper;
            _mailHelper   = mailHelper;
            _menuService  = menuService;
            _dataContext  = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
               { return RedirectToAction("Index", "Portal"); }

            ViewBag.MenuLeft    = _menuService.GenerateMenuWebAppLeftHeader(User.Identity.IsAuthenticated, _userHelper.GetRol((User.Identity as ClaimsIdentity)).FirstOrDefault(), "PortalLogin");
            ViewBag.MenuRight   = _menuService.GenerateMenuWebAppRightHeader(User.Identity.IsAuthenticated, User.Identity.Name);            

            return View();
        }

        public IActionResult NotAuthorized()
        {
            ViewBag.Message = MessageCenter.webAppMessageNotAuthorized;
            

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Login(LoginTARequest model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                       { return Redirect(Request.Query["ReturnUrl"].First()); }
                                        
                    return RedirectToAction("Index", "Portal");
                }
            }

            ModelState.AddModelError(string.Empty, MessageCenter.webApplabelLoginFail);
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            var view = (User.Identity.IsAuthenticated) ? new UsuarioViewModel { Roles = _combosHelper.GetComboRoles() } : new UsuarioViewModel { Roles = _combosHelper.GetComboRolUser() };

            ViewBag.Title     = MessageCenter.webAppTitlePageRegisterUser;
            ViewBag.MenuLeft  = _menuService.GenerateMenuWebAppLeftHeader(User.Identity.IsAuthenticated, _userHelper.GetRol((User.Identity as ClaimsIdentity)).FirstOrDefault(), "PortalLogin");
            ViewBag.MenuRight = _menuService.GenerateMenuWebAppRightHeader(User.Identity.IsAuthenticated, User.Identity.Name);            
            
            
            return View(view);
        }


        [HttpPost]       
        public async Task<IActionResult> Register(UsuarioViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var rol       = _combosHelper.GetComboRoles().Where(r => r.Value == viewModel.RoleId.ToString()).Select(r => r.Text);
                viewModel.Rol = rol.FirstOrDefault().ToString();

                var addUserResult = await _userHelper.AddUsuario(viewModel);                
                if (addUserResult == IdentityResult.Success)                
                    { 
                    var userLogin = await _userHelper.GetUserByEmailAsync(viewModel.Correo);
                    await _dataContext.Usuarios.AddAsync(new Usuario {  DatosFiscales = null, FirstName = viewModel.FirstName, LastName= viewModel.LastName, UserLogin = userLogin });
                    await _dataContext.SaveChangesAsync();
                    //ToDo: implementar el guardado en sesión del TOKEN
                    /*TokenResponse token = _userHelper.BuildToken(new LoginTARequest { Email = view.Correo, Password = view.Password, RememberMe = false });                    
                    var result = await _userHelper.LoginAsync(new LoginTARequest { Email =view.Correo, Password = view.Password, RememberMe= false });
                    if (result.Succeeded)
                       { return RedirectToAction("Index", "Home"); }
                    */
                    var defaultToken = await _userHelper.GenerateEmailConfirmationTokenAsync(userLogin);
                    var tokenLink = Url.Action("ConfirmEmail", "Account", new { userid = userLogin.Id,
                                                                                token = defaultToken
                                                                              }
                                                , protocol: HttpContext.Request.Scheme);

                    _mailHelper.SendEmailAccountConfirmation(viewModel.Correo, tokenLink);                        

                    ViewBag.Message = MessageCenter.commonMessageEmailInst;
                    
                     return View(viewModel);
                }

                ModelState.AddModelError(string.Empty, MessageErrorHelper.showIdentityResultError(addUserResult) );
                viewModel.Rol = rol.FirstOrDefault().ToString();
                return View(viewModel);

            }

            return View(viewModel);
        }

        //TODO: arreglar la vista para tener dos vistar parciales /In PartailView //PartailView.cshtml https://stackoverflow.com/questions/5410055/using-ajax-beginform-with-asp-net-mvc-3-razor
        public async Task<IActionResult> ChangeUser()
        {
            if ( !User.Identity.IsAuthenticated )
               { return RedirectToAction("Login", "Account"); }

            var user = await _userHelper.GetUsuarioTAByEmailAsync(User.Identity.Name);
            if (user == null)
               { return NotFound(); }

            var view = new EditUserViewModel{ FirstName = user.FirstName,
                                              LastName  = user.LastName,
                                              CellPhone = user.CellPhone
                                            };
            ViewBag.Title     = MessageCenter.webAppTitlePageEditUser;
            ViewBag.MenuLeft  = _menuService.GenerateMenuWebAppLeftHeader(User.Identity.IsAuthenticated, _userHelper.GetRol((User.Identity as ClaimsIdentity)).FirstOrDefault(), "PortalLogin");
            ViewBag.MenuRight = _menuService.GenerateMenuWebAppRightHeader(User.Identity.IsAuthenticated, User.Identity.Name);
            

            return View(view);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeUser(EditUserViewModel viewModel)
        {
            ViewBag.Title = MessageCenter.webAppTitlePageEditUser;
            if (ModelState.IsValid)
            {
                var usuario       = await _userHelper.GetUsuarioTAByEmailAsync(User.Identity.Name);
                usuario.FirstName = viewModel.FirstName;
                usuario.LastName  = viewModel.LastName;

                usuario.UserLogin.PhoneNumber          = viewModel.CellPhone;
                usuario.UserLogin.PhoneNumberConfirmed = false;

                _userHelper.UpdateUsuarioTAB(usuario);

                return RedirectToAction("Index", "Home");
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(EditUserViewModel viewModel)
        {
            ViewBag.Title = MessageCenter.webAppTitlePageEditUser;
            if (ModelState.IsValid)
            {
                var usuario = await _userHelper.GetUsuarioTAByEmailAsync(User.Identity.Name);
                var result  = await _userHelper.ChangePasswordAsync(usuario.UserLogin, viewModel.OldPassword, viewModel.NewPassword);
                if (result.Succeeded)
                   { return RedirectToAction("ChangeUser"); }

               ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description); 
                
            }

            return View(viewModel);
        }


        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
               { return NotFound(); }

            var user =  await _userHelper.GetUserByIdAsync(userId);
            if (user == null)
               { return NotFound(); }

            var result = await _userHelper.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
               { return NotFound(); }

            ViewBag.MessageConfirm = MessageCenter.commonMessageEmailConfirm;
            ViewBag.MenuLeft       = _menuService.GenerateMenuWebAppLeftHeader(User.Identity.IsAuthenticated, _userHelper.GetRol((User.Identity as ClaimsIdentity)).FirstOrDefault(), "PortalLogin");
            ViewBag.MenuRight      = _menuService.GenerateMenuWebAppRightHeader(User.Identity.IsAuthenticated, User.Identity.Name);            

            return View();
        }





        public IActionResult RecuperarContrasena()
        {

            ViewBag.Title     = MessageCenter.commonTitlePageRecoverPwd;
            ViewBag.MenuLeft  = _menuService.GenerateMenuWebAppLeftHeader(User.Identity.IsAuthenticated, _userHelper.GetRol((User.Identity as ClaimsIdentity)).FirstOrDefault(), "PortalLogin");
            ViewBag.MenuRight = _menuService.GenerateMenuWebAppRightHeader(User.Identity.IsAuthenticated, User.Identity.Name);            

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RecuperarContrasena(EmailRequest viewModel)
        {
            ViewBag.Title = MessageCenter.commonTitlePageRecoverPwd;
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(viewModel.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, MessageCenter.commonMessageRecoverNoEmail);
                    return View(viewModel);
                }

                var myToken   = await _userHelper.GeneratePasswordResetTokenAsync(user);
                var tokenLink = Url.Action("ResetPassword", "Account", new { token = myToken }, protocol: HttpContext.Request.Scheme);

                _mailHelper.SendEmailRecoverPwd(viewModel.Email, tokenLink);               

                ViewBag.Message = MessageCenter.commonMessageRecoverEmail;

                return View();

            }

            return View(viewModel);
        }

        public IActionResult ResetPassword(string token)
        {
            ViewBag.MenuLeft  = _menuService.GenerateMenuWebAppLeftHeader(User.Identity.IsAuthenticated, _userHelper.GetRol((User.Identity as ClaimsIdentity)).FirstOrDefault(), "PortalLogin");
            ViewBag.MenuRight = _menuService.GenerateMenuWebAppRightHeader(User.Identity.IsAuthenticated, User.Identity.Name);
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel viewModel)
        {
            var user  = await _userHelper.GetUserByEmailAsync(viewModel.Email);
            if (user != null)
            {
                var result = await _userHelper.ResetPasswordAsync(user, viewModel.Token, viewModel.Password);
                if (result.Succeeded)
                {
                    ViewBag.Message = MessageCenter.commonMessagePassReset;
                    return View();
                }

                ViewBag.Message = MessageCenter.commonMessageErrorPassReset;
                return View(viewModel);
            }

            ViewBag.Message = MessageCenter.commonMessageRecoverNoEmail;

            return View(viewModel);
        }



    }//Class
}//NameSpace