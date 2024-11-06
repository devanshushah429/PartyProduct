using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using DatabaseServices;
using Rotativa.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IInvoicesService, InvoicesService>();
builder.Services.AddScoped<IInvoiceWiseProductsService, InvoiceWiseProductsService>();
builder.Services.AddScoped<IPartyWiseProductsService, PartyWiseProductsService>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<IPartiesService, PartiesService>();
builder.Services.AddScoped<IProductRatesService, ProductRatesService>();

builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

app.UseRotativa();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
