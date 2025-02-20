using Microsoft.EntityFrameworkCore;

using PRUEBA2_MAEV.DataBase.Models;
using System.Reflection.Emit;

namespace PRUEBA2_MAEV.DataBase


{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<PagoModel> pagoModel{ get; set; }
        public DbSet<LiquidacionModel> liquidacionModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar LiquidacionModel como un tipo sin clave
            modelBuilder.Entity<LiquidacionModel>().HasNoKey();
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        { }
        //public DbSet<HistoricoOperacion> Operaciones { get; set; }
    }
}
