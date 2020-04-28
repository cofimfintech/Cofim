using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Cofim.Common.Model.Request;
using Cofim.Common.Model.Response;
using Cofim.Web.Models.Entities;
using Cofim.Web.Models.ViewModel;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Collections.Generic;
using Cofim.Web.Models;
using Microsoft.EntityFrameworkCore;




/*** 
 * Implementación del manejo de los usuarios, INYECCIÓN DE DEPENDENCIAS en el starup
 * ***/
namespace Cofim.Web.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly UserManager<UserLogin>    _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<UserLogin>  _signInManager;
        private readonly IConfiguration            _configuration;
        private readonly DataContext               _dataContext;

        public UserHelper(UserManager<UserLogin>     userManager
                         , RoleManager<IdentityRole> roleManager
                         , SignInManager<UserLogin>  signInManager
                         , IConfiguration            configuration
                         , DataContext               context)
                        {
                            _userManager   = userManager;
                            _roleManager   = roleManager;
                            _signInManager = signInManager;
                            _configuration = configuration;
                            _dataContext   = context;
                        }


        public async Task<IdentityResult> AddUsuario (UsuarioViewModel view)
        {
            var result = await AddUserAsync(new UserLogin { UserName = view.Correo, Email = view.Correo, PhoneNumber = view.CellPhone }, view.Password);
            if (result != IdentityResult.Success)
            {                                
                return IdentityResult.Failed(result.Errors.ToArray() );
            }

            var newUserLogin = await GetUserByEmailAsync(view.Correo);            
            await AddUserToRoleAsync(newUserLogin, view.Rol);

            return IdentityResult.Success;
        }

        public TokenResponse BuildToken(LoginTARequest userInfo)
        {
            var claims = new[]{ new Claim(JwtRegisteredClaimNames.Sub, userInfo.Email           ),
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                              };

            var key         = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["CofimSettings:JwtSecretKey"]));
            var credentials = new SigningCredentials  (key, SecurityAlgorithms.HmacSha256);
            var expiration  = DateTime.UtcNow.AddHours(12);

            JwtSecurityToken token = new JwtSecurityToken(issuer  : _configuration["CofimSettings:JwtIssuer"],
                                                          audience: _configuration["CofimSettings:Jwtaudience"],
                                                          claims  : claims,
                                                          expires : expiration,
                                                          signingCredentials: credentials
                                                          );

            return new TokenResponse{ Token        = new JwtSecurityTokenHandler().WriteToken(token)
                                    , Expiration   = expiration
                                    , Valid        = token.ValidTo
                                    , TokenExpired = true
                                    };
        }


        public async Task<IdentityResult> AddUserAsync(UserLogin user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> UpdateUserAsync(UserLogin user)
        {
            return await _userManager.UpdateAsync(user);
        }


        public async Task AddUserToRoleAsync(UserLogin user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task CheckRoleAsync(string roleName)
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = roleName });
            }
        }

        public List<string> GetRol(ClaimsIdentity identity)
        {
            return identity.Claims
                           .Where (c => c.Type == ClaimTypes.Role)
                           .Select(c => c.Value)
                           .ToList();
        }

        public async Task<UserLogin> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);            
        }
        
        public async Task<bool> IsUserInRoleAsync(UserLogin user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<SignInResult> LoginAsync(LoginTARequest model)
        {
            return await _signInManager.PasswordSignInAsync( model.Email,
                                                             model.Password,
                                                             model.RememberMe,
                                                             false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<SignInResult> ValidatePasswordAsync(UserLogin user, string password)
        {
            return await _signInManager.CheckPasswordSignInAsync(user, password, false);
        }

        public async Task<IdentityResult> ChangePasswordAsync(UserLogin user, string oldPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }


        public async Task<Usuario> GetUsuarioTAByEmailAsync(string email)
        {            
            return await _dataContext.Usuarios.Include(c => c.UserLogin)
                                              .FirstOrDefaultAsync(u => u.UserLogin.NormalizedEmail == email.ToUpper());
        }

        public async Task<IList<string>> GetRolesByUserAsync(UserLogin user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        

        public async void UpdateUsuarioTAB(Usuario usuario)
        {
            await _userManager.UpdateAsync(usuario.UserLogin);
            _dataContext.Update(usuario);                    
            await _dataContext.SaveChangesAsync();
        }



        public async Task<IdentityResult> ConfirmEmailAsync(UserLogin user, string token)
        {
            return await _userManager.ConfirmEmailAsync(user, token);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(UserLogin user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<UserLogin> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }



        public async Task<string> GeneratePasswordResetTokenAsync(UserLogin user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> ResetPasswordAsync(UserLogin user, string token, string password)
        {
            return await _userManager.ResetPasswordAsync(user, token, password);
        }


    }//class
}//NameSpace
