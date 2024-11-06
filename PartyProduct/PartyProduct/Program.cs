using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using DatabaseServices;
using Rotativa.AspNetCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using PartyProduct.StartupExtensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.ConfigureServices(builder.Configuration);
// Add services to the container.

var app = builder.Build();

app.UseRotativa();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
