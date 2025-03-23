using EpitomelHotel.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EpitomelHotel.Models;
using System.Reflection.Emit;

namespace EpitomelHotel.Areas.Identity.Data;

public class EpitomelHotelDbContext : IdentityDbContext<ApplUser>
{
    public EpitomelHotelDbContext(DbContextOptions<EpitomelHotelDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Guest>().ToTable("Guest");
        builder.Entity<Payments>().ToTable("Payments");
        builder.Entity<BookingService>().ToTable("BookingService");
        builder.Entity<Bookings>().ToTable("Booking");
        builder.Entity<Rooms>().ToTable("Rooms");
        builder.Entity<Services>().ToTable("Service");
        builder.Entity<Status>().ToTable("Status");
        builder.Entity<Staff>().ToTable("Staff");
    }

public DbSet<EpitomelHotel.Models.Staff> Staff { get; set; } = default!;

public DbSet<EpitomelHotel.Models.Guest> Guest { get; set; } = default!;

public DbSet<EpitomelHotel.Models.Rooms> Rooms { get; set; } = default!;

public DbSet<EpitomelHotel.Models.Payments> Payments { get; set; } = default!;

public DbSet<EpitomelHotel.Models.Services> Services { get; set; } = default!;

public DbSet<EpitomelHotel.Models.Bookings> Bookings { get; set; } = default!;

public DbSet<EpitomelHotel.Models.BookingService> BookingService { get; set; } = default!;

public DbSet<EpitomelHotel.Models.Status> Status { get; set; } = default!;
}
