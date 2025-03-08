using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RailwayManagementSystem.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<RMSContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RMSContext") ?? throw new InvalidOperationException("Connection string 'RMSContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

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


app.MapAreaControllerRoute(
    name: "Trains",
    areaName: "TrainsArea",
    pattern: "TrainsArea/{controller=Trains}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "Stations",
    areaName: "StationsArea",
    pattern: "StationsArea/{controller=Stations}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
