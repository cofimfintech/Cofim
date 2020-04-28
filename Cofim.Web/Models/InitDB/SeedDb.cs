using System.Linq;
using System.Threading.Tasks;
using Cofim.Web.Helpers;
using Cofim.Web.Models.Entities;
using Cofim.Common.Model.DataEntity;
using Cofim.Common;

namespace Cofim.Web.Models.InitDB
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
                    {
                        _context = context;
                        _userHelper = userHelper;
                    }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();            
            await CheckFormasDePagoAsync();
            await CheckRoles();
            await CheckUserAsync( "Eduardo", "Admin"    , "ems@convivere.mx"              , "350 634 2747", "admin123" , RolesWebApp.Admin);
            await CheckUserAsync( "Alvaro" , "Admin"    , "alvarosolares1@hotmail.com"    , "350 634 2747", "admin123" , RolesWebApp.Admin);
            await CheckUserAsync( "User"   , "User"     , "eduardo.munoz.siller@gmail.com", "350 634 2747", "user123"  , RolesWebApp.Usuario);
            await CheckUserAsync( "Edu"    , "Finanzas" , "eduardo_m81@hotmail.com"       , "350 634 2747", "gestor123", RolesWebApp.EducacionFinanciera);            
        }


        private async Task CheckRoles()
        {
            await _userHelper.CheckRoleAsync(RolesWebApp.Admin);
            await _userHelper.CheckRoleAsync(RolesWebApp.Usuario);
            await _userHelper.CheckRoleAsync(RolesWebApp.EducacionFinanciera);            
        }
              

       
        private async Task CheckFormasDePagoAsync()
        {
            if (!_context.FormasDePago.Any())
            {
                _context.FormasDePago.Add(new FormaDePago { Tipo = MessageCenter.FormaDePagoCard , IdOpenPay = 6, Nombre = "Tarjeta Crédito/Débito", Descripcion = "Pago con tarjeta de crédito ó débito" });                
                _context.FormasDePago.Add(new FormaDePago { Tipo = "-"    , IdOpenPay = 0, Nombre = "Seleccione un método de Pago", Descripcion = "-" });
                await _context.SaveChangesAsync();
            }

        }

        private async Task CheckUserAsync(string firstName, string lastName, string email, string phone, string pwd, string role)
        {
            var userLogin = await _userHelper.GetUserByEmailAsync(email);
            if (userLogin == null)
            {
                userLogin = new UserLogin { Email = email, PhoneNumber = phone, UserName = email };
                var user  = new Usuario   { FirstName = firstName, LastName = lastName, UserLogin = userLogin };

                await _userHelper.AddUserAsync(userLogin, pwd);
                await _userHelper.AddUserToRoleAsync(userLogin, role);

                _context.Usuarios.Add(user);
                await _context.SaveChangesAsync();

                var defaultToken = await _userHelper.GenerateEmailConfirmationTokenAsync(userLogin);
                await _userHelper.ConfirmEmailAsync(userLogin, defaultToken);
            }
        }

        }//class
}//namespace
