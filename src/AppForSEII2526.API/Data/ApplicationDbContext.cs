using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<BonoBocadillo> BonoBocadillos{get; set;}
    public DbSet<BonosComprados> BonosComprados { get; set; }
    public DbSet<CompraBono> CompraBono { get; set; }
    public DbSet<TipoBocadillo> TipoBocadillo { get; set; }

    public DbSet<Bocadillo> Bocadillos { get; set; }
    public DbSet<Resenya> Resenyas { get; set; }
    public DbSet<ResenyaBocadillo> ResenyaBocadillos { get; set; }
    public DbSet<TipoPan> TipoPanes { get; set; }  
    }
