using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RailwayManagementSystem.Data;
using RailwayManagementSystem.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<RMSContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RMSContext") ?? throw new InvalidOperationException("Connection string 'RMSContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<LoginService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

//app.MapControllerRoute(
//    name: "Home",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapControllerRoute(
//    name: "Stations",
//    pattern: "{controller=Stations}/{action=Index}/{id?}");

//app.MapControllerRoute(
//    name: "Trains",
//    pattern: "{controller=Trains}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "login",
    pattern: "{controller=Home}/{action=Login}/{id?}");
app.MapControllerRoute(
    name: "logout",
    pattern: "{controller=Home}/{action=Logout}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
