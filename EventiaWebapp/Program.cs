using EventHandler = EventiaWebapp.Service.EventHandler;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSingleton<EventHandler>();
var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "joinevent",
    pattern: "Event/JoinEvent/{id:int?}",
    new { controller = "Event", action = "JoinEvent" });

app.Run();
