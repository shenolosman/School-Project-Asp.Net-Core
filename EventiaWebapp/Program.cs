using EventiaWebapp.Service;
using Microsoft.EntityFrameworkCore;
//using EventsHandler = EventiaWebapp.Service.EventsHandler;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped<EventsHandler>();
builder.Services.AddScoped<Database>();
var connectionString = "Server=(LocalDB)\\MSSQLLocalDB;Database=EventiaWebappDB";
builder.Services.AddDbContext<EventDbContext>(options => options.UseSqlServer(connectionString));


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "event",
    pattern: "{controller=Event}/{action=JoinEvent}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<Database>();
    db.PrepDatabase();
}

app.Run();