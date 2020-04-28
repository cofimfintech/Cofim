
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Cofim.Common.Model.DataEntity;
using Cofim.Web.Models.Entities;


namespace Cofim.Web.Models
{
    public class DataContext : IdentityDbContext<UserLogin>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<VectorPrecioBMV> VectorPreciosBMV { get; set; }

        public DbSet<FondoInversion>  FondosInversion { get; set; }
        
        public DbSet<Usuario>         Usuarios        { get; set; }
        public DbSet<DatosFiscales>   DatosFiscales   { get; set; }
        public DbSet<StatusWebApp>    StatusWebApp    { get; set; }                        
        public DbSet<FormaDePago>     FormasDePago    { get; set; }
    }//class
}//NameSpace
