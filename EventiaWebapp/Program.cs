using EventiaWebapp.Data;
using EventiaWebapp.Models;
using EventiaWebapp.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("default");

builder.Services.AddControllersWithViews();
// inside to addrazorpages
builder.Services.AddRazorPages();
//options =>
//{
//    options.Conventions.AllowAnonymousToAreaPage("/Identity", "/Login");
//    options.Conventions.AuthorizeAreaFolder("/Identity", "/Logout");
//    options.Conventions.AuthorizeAreaFolder("/Identity", "/RegisterConfirmation");
//}

builder.Services.AddScoped<EventsHandler>();
builder.Services.AddScoped<Database>();



builder.Services.AddDbContext<EventDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddControllersWithViews();

//if (builder.Environment.IsDevelopment())
//{
//    builder.Services.Configure<IdentityOptions>(options =>
//    {
//        // Default Password settings.
//        options.Password.RequireDigit = false;
//        options.Password.RequireLowercase = false;
//        options.Password.RequireNonAlphanumeric = false;
//        options.Password.RequireUppercase = false;
//        options.Password.RequiredLength = 6;
//        options.Password.RequiredUniqueChars = 1;
//    });
//}

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<EventDbContext>();

//from Dennis
//builder.Services.AddAuthorization(o => { o.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build(); });



var app = builder.Build();


app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
//from Dennis
//app.MapDefaultControllerRoute();

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
    //    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

    //    var eventsList = scope.ServiceProvider.GetRequiredService<EventsHandler>();

    //    var users = await userManager?.Users.ToListAsync()!;
    //    eventsList.Initialize(users);
}

app.Run();