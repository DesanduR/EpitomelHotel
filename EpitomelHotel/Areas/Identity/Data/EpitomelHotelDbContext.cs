using EpitomelHotel.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EpitomelHotel.Models;

namespace EpitomelHotel.Areas.Identity.Data;

public class EpitomelHotelDbContext : IdentityDbContext<ApplUser>
{
    public EpitomelHotelDbContext(DbContextOptions<EpitomelHotelDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

public DbSet<EpitomelHotel.Models.Staff> Staff { get; set; } = default!;

public DbSet<EpitomelHotel.Models.Guest> Guest { get; set; } = default!;

public DbSet<EpitomelHotel.Models.Rooms> Rooms { get; set; } = default!;

public DbSet<EpitomelHotel.Models.Payments> Payments { get; set; } = default!;
}
