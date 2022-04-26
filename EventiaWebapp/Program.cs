using EventiaWebapp.Data;
using EventiaWebapp.Models;
using EventiaWebapp.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("default");
builder.Services.AddScoped<Database>();
builder.Services.AddScoped<EventsHandler>();
builder.Services.AddScoped<AdminsHandler>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<EventDbContext>(options =>
{
    options.UseSqlServer(connectionString);
    options.EnableSensitiveDataLogging();
});
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.ConfigureApplicationCookie(o =>
{
    //o.Cookie.HttpOnly = true;
    //o.Cookie.Name = "EventiaCookie";
    //o.Cookie.SameSite = SameSiteMode.Strict;
    //o.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    //o.ExpireTimeSpan = TimeSpan.FromDays(30);
    //o.LoginPath = new PathString("/Home/Index"); //redirect to that path without permission 
});
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<EventDbContext>();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapDefaultControllerRoute();
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