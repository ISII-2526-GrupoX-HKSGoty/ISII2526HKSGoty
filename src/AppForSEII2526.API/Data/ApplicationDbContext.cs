using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AppForSEII2526.Models;

namespace AppForSEII2526.API.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options) {
    public DbSet<Bocadillo> Bocadillos { get; set; }
    public DbSet<Compra> Compras { get; set; }
    public DbSet<CompraBocadillo> ComprarBocadillos { get; set; }
    public DbSet<TipoPan> tipoPan { get; set; }

    public DbSet<BonoBocadillo> BonoBocadillos{get; set;}
    public DbSet<BonosComprados> BonosComprados { get; set; }
    public DbSet<CompraBono> CompraBono { get; set; }
    public DbSet<TipoBocadillo> TipoBocadillo { get; set; }

    public DbSet<Resenya> Resenyas { get; set; }
    public DbSet<ResenyaBocadillo> ResenyaBocadillos { get; set; }

    public DbSet<TipoProducto> TipoProductos { get; set; }

    public DbSet<Producto> Productos { get; set; }
    public DbSet<Producto_Compra> Productos_Compras { get; set; }

    public DbSet<Compra_Producto> Compra_Productos { get; set; }

}
