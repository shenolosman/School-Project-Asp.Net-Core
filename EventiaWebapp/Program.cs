using EventiaWebapp.Data;
using EventiaWebapp.Models;
using EventiaWebapp.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped<EventsHandler>();
builder.Services.AddScoped<Database>();

var connectionString = builder.Configuration.GetConnectionString("default");

builder.Services.AddDbContext<EventDbContext>(options => options.UseSqlServer(connectionString));


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

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
        await db.RecreateAndSeed();
    }
    var userManager = scope.ServiceProvider
        .GetRequiredService<UserManager<User>>();

    //var eventsList = scope.ServiceProvider
    //    .GetRequiredService<EventHandler>();

    //eventsList.Initialize(userManager.Users.ToList());
}

app.Run();