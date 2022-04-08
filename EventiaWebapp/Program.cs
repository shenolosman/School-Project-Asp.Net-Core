using EventiaWebapp.Data;
using EventiaWebapp.Models;
using EventiaWebapp.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("default");

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddScoped<EventsHandler>();
builder.Services.AddScoped<Database>();

builder.Services.AddDbContext<EventDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<EventDbContext>();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}");

app.MapControllerRoute(
    name: "conformation",
    pattern: "{controller=Event}/{action=JoinEvent}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<Database>();
    if (app.Environment.IsProduction())
    {
        await db.CreateIfNotExist();
    }

    if (app.Environment.IsDevelopment())
    {
        await db.CreateAndSeedIfNotExist();
    }
}

app.Run();