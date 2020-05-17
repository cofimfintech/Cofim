using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Cofim.Web.Helpers;
using Cofim.Web.Models;
using Cofim.Web.Models.Entities;
using Cofim.Common;
using Cofim.Common.Model.Request;
using Cofim.Common.Model.Response;
using Cofim.Common.Model.DataEntity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Cofim.Web.Models.ViewModel;
using System.Security.Claims;
using System.Collections;
using System.Collections.Generic;

namespace Cofim.Web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
    private readonly DataContext    _dataContext;
    private readonly IConfiguration _configuration;
    private readonly IUserHelper    _userHelper;
    private readonly IMailHelper    _mailHelper;
    private readonly ICombosHelper  _combosHelper;

    public AccountController(DataContext dataContext, IConfiguration configuration, IUserHelper userHelper, IMailHelper mailHelper, ICombosHelper combosHelper)
    {
        _dataContext   = dataContext;
        _configuration = configuration;
        _userHelper    = userHelper;
        _mailHelper    = mailHelper;
        _combosHelper  = combosHelper;
    }

    [Route("CreateToken")]
    [HttpPost]
    public async Task<IActionResult> CreateToken([FromBody] LoginTARequest model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userHelper.GetUserByEmailAsync(model.Email);
                if (user == null)
                   {    return NotFound(MessageCenter.webApplabelEmailNotFound + " " + model.Email);  }
                else
                    {
                        var result = await _userHelper.ValidatePasswordAsync(user, model.Password);

                        if (result.Succeeded)
                           { return Created(string.Empty, _userHelper.BuildToken(model)); }
                        else
                           { return BadRequest(MessageCenter.webApplabelLoginFail);       }
                    }
        }

        return BadRequest();
    }

    

    [Route("GetUsuarioByEmail")]
    [HttpPost]
   /*[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]*/
    public async Task<IActionResult> GetUserByEmailAsync([FromBody]EmailRequest request)
    {
        if (!ModelState.IsValid)
           { return BadRequest(MessageErrorHelper.showModelStateError(ModelState)); }
            
        var usuario = await _dataContext.Usuarios.Include(c => c.UserLogin)                                                   
                                                 .Include(c => c.DatosFiscales)
                                                 .FirstOrDefaultAsync(u => u.UserLogin.NormalizedEmail == request.Email.ToUpper());
        if ( usuario == null )
           { return NotFound(request); }

        
        IList<string> roles = await _userHelper.GetRolesByUserAsync(usuario?.UserLogin);        

        return Ok(ToUsuarioResponse( usuario
                                    , roles.FirstOrDefault().ToString()                                    
                                    , await _dataContext.FormasDePago.ToListAsync()
                                    )
                );

    }//GetUsuarioByEmail

    // ******************************APP ******************************************/
    [Route("RegisterUser")]
    [HttpPost]
    public async Task<IActionResult> RegisterUserAsync([FromBody] NewUserRequest request)
    {
        if (!ModelState.IsValid)
           { return BadRequest(new Resp<object>{ IsSuccess = false, Message = MessageErrorHelper.showModelStateError(ModelState) }); }

        var user  = await _userHelper.GetUserByEmailAsync(request.Correo);
        if (user != null)
           { return BadRequest(new Resp<object> { IsSuccess = false, Message = MessageCenter.appTextEmailUsed }); }

        var newUser = new UsuarioViewModel {  Correo    = request.Correo
                                            , CellPhone = request.CellPhone
                                            , FirstName = request.FirstName
                                            , LastName  = request.LastName
                                            , RoleId    = request.RoleId
                                            , Rol       = _combosHelper.GetComboRolesByValue(request.RoleId.ToString())
                                            , Password  = request.Password                                               
                                            , PasswordConfirm = request.PasswordConfirm                                               
                                            };

        var addUserResult  = await _userHelper.AddUsuario(newUser);         
        if (addUserResult != IdentityResult.Success)
           { return BadRequest(new Resp<object> { IsSuccess = false, Message = MessageErrorHelper.showIdentityResultError(addUserResult) }); }       

        var userLogin = await _userHelper.GetUserByEmailAsync(request.Correo);
        var myToken   = await _userHelper.GenerateEmailConfirmationTokenAsync(userLogin);
        var tokenLink = Url.Action("ConfirmEmail", "Account", new { userid = userLogin.Id,
                                                                    token  = myToken
                                                                  }, protocol: HttpContext.Request.Scheme);
            
        _mailHelper.SendEmailAccountConfirmation(request.Correo, tokenLink);
        
        return Ok(new Resp<object>{ IsSuccess = true, Message = MessageCenter.commonMessageEmailInst });

    }    

    [HttpPost]
    [Route("RecoverPassword")]
    public async Task<IActionResult> RecoverPassword([FromBody] EmailRequest request)
    {
	    if (!ModelState.IsValid)
	       { return BadRequest(new Resp<object> { IsSuccess = false, Message = "Bad request" }); }

	    var user = await _userHelper.GetUserByEmailAsync(request.Email);
	    if (user == null)
	       { return BadRequest(new Resp<object> { IsSuccess = false, Message = MessageCenter.commonMessageRecoverNoEmail }); }

	    var myToken   = await _userHelper.GeneratePasswordResetTokenAsync(user);
	    var tokenLink = Url.Action("ResetPassword", "Account", new { token = myToken }, protocol: HttpContext.Request.Scheme);
        _mailHelper.SendEmailRecoverPwd(request.Email, tokenLink);
            
	    return Ok(new Resp<object> { IsSuccess = true, Message = MessageCenter.commonMessageRecoverEmail });
    }




    [Route("GetFondosInversionByFilter")]
    [HttpPost]
    /*[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]*/
    public async Task<IActionResult> GetFondosInversionByFilter([FromBody]FilterFondoInversion request)
    {
        if ( !ModelState.IsValid )
           { return BadRequest(MessageErrorHelper.showModelStateError(ModelState)); }

            var personaFisica = true;
            var personaMoral = true;
            var personaMoralNoContri = false;
            var sociedadesDeInversion = false;
            var montoMin = 10000000;

            ICollection<FondosInversionMontosMinimos> fondos = await _dataContext.FondosInversionMontosMinimos.Include(fi => fi.Precios).Where(fi => (fi.Activo == MessageCenter.FONDO_ACTIVO) && (fi.MontoMinimo >= montoMin)).ToListAsync();
            fondos = personaFisica == true ? fondos.Where(fi => fi.PersonaFisica == true).ToList() : fondos;
            fondos = personaMoral == true ? fondos.Where(fi => fi.PersonaMoral == true).ToList() : fondos;
            fondos = personaMoralNoContri == true ? fondos.Where(fi => fi.PersonaMoralNoContribuyente == true).ToList() : fondos;
            fondos = sociedadesDeInversion == true ? fondos.Where(fi => fi.SociedadesDeInversion == true).ToList() : fondos;


            return Ok();

    }//GetFondosInversionByFilter


    // ******************************APP ******************************************/





    private UsuarioResponse ToUsuarioResponse(Usuario usuario, string rol, List<FormaDePago> formasDePago)
    {
    return new UsuarioResponse
                { Id         = usuario.Id
                , FirstName  = usuario.FirstName
                , LastName   = usuario.LastName
                , FixedPhone = usuario.FixedPhone
                , CellPhone  = usuario.UserLogin?.PhoneNumber
                , Correo     = usuario.UserLogin?.Email
                , Rol        = rol                
                , FormasDePago = formasDePago?.Select(fp => new FormaDePagoResponse { Id = fp.Id, Descripcion = fp.Descripcion, IdOpenPay = fp.IdOpenPay, Nombre=fp.Nombre, Tipo=fp.Tipo  }).ToList()
    };
    }
           
    
    private DatosFiscalesResponse ToDatosFiscalesResponse(DatosFiscales DatosFiscales)
    {
        if (DatosFiscales == null)
            return new DatosFiscalesResponse();
        else
            return new DatosFiscalesResponse{Rfc               = DatosFiscales.Rfc
                                            ,Calle             = DatosFiscales.Calle
                                            ,NumeroExterior    = DatosFiscales.NumeroExterior
                                            ,NumeroInterior    = DatosFiscales.NumeroInterior
                                            ,Colonia           = DatosFiscales.Colonia
                                            ,AlcaldiaMunicipio = DatosFiscales.AlcaldiaMunicipio
                                            ,Estado            = DatosFiscales.Estado
                                            ,CodigoPostal      = DatosFiscales.CodigoPostal
                                            };
    }

    }//Class Account

}//NameSpace