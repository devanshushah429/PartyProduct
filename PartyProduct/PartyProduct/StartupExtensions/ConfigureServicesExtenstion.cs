using DatabaseServices;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;

namespace PartyProduct.StartupExtensions
{
    public static class ConfigureServicesExtenstion
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews();

            services.AddScoped<IInvoicesService, InvoicesService>();
            services.AddScoped<IInvoiceWiseProductsService, InvoiceWiseProductsService>();
            services.AddScoped<IPartyWiseProductsService, PartyWiseProductsService>();
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<IPartiesService, PartiesService>();
            services.AddScoped<IProductRatesService, ProductRatesService>();

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<AppDbContext>()

                .AddDefaultTokenProviders()

                .AddUserStore<UserStore<ApplicationUser, ApplicationRole, AppDbContext, Guid>>()

                .AddRoleStore<RoleStore<ApplicationRole, AppDbContext, Guid>>();
            services.AddDbContext<AppDbContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddHttpLogging(options =>
            {
                options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestProperties | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponsePropertiesAndHeaders;
            });
            return services;
        }
    }
}
