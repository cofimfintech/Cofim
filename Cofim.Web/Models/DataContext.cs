
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

        public DbSet<FondosInversionMontosMinimos> FondosInversionMontosMinimos { get; set; }
        public DbSet<VectorPrecio>     VectorPrecios     { get; set; }                
        public DbSet<Usuario>          Usuarios          { get; set; }
        public DbSet<DatosFiscales>    DatosFiscales     { get; set; }
        public DbSet<StatusWebApp>     StatusWebApp      { get; set; }                        
        public DbSet<FormaDePago>      FormasDePago      { get; set; }
        public DbSet<TipoAdquirente>   TiposAdquirente   { get; set; }
        public DbSet<EtlProcessedFile> EtlProcessedFiles { get; set; }

    }//class

}//NameSpace
