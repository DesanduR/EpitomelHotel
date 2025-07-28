using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EpitomelHotel.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("EpitomelHotelDbContextConnection")
                       ?? throw new InvalidOperationException("Connection string 'EpitomelHotelDbContextConnection' not found.");

builder.Services.AddDbContext<EpitomelHotelDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<EpitomelHotelDbContext>();

// Add session service BEFORE building the app
builder.Services.AddSession();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Add authentication middleware before authorization
app.UseAuthentication();

// Add session middleware AFTER routing but BEFORE authorization
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplUser>>();

    string adminEmail = "admin@epitomelhotel.com";
    string adminPassword = "Password123!";

    if (await userManager.FindByEmailAsync(adminEmail) == null)
    {
        var user = new ApplUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            Firstname = "Test",
            Lastname = "Admin",
            Phone = "1234567890"
        };

        await userManager.CreateAsync(user, adminPassword);
        await userManager.AddToRoleAsync(user, "Admin");
    }
}

app.Run();
