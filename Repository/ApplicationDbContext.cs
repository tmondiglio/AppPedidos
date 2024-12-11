using Model;
using Microsoft.EntityFrameworkCore;


namespace Repository
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoItem> PedidoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de Cliente
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente);
                entity.Property(e => e.Cuit).IsRequired();
                entity.Property(e => e.RazonSocial).IsRequired();
                entity.HasMany<Pedido>()
                      .WithOne()
                      .HasForeignKey(p => p.IdCliente)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            
            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(e => e.IdPedido);
                entity.Property(e => e.ContactName).IsRequired();
                entity.Property(e => e.FechaPedido).IsRequired();
                entity.Property(e => e.FechaEntrega).IsRequired();
              
                entity.HasMany<PedidoItem>()
                      .WithOne()
                      .HasForeignKey(pi => pi.IdPedido)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PedidoItem>(entity =>
            {
                entity.HasKey(e => new { e.IdPedido, e.IdItem });
                entity.Property(e => e.Producto).IsRequired();
            });
        }
    }
}