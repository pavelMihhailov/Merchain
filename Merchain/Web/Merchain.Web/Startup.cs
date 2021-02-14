namespace Merchain.Web
{
    using System;
    using System.Reflection;

    using CloudinaryDotNet;
    using Merchain.Common;
    using Merchain.Data;
    using Merchain.Data.Common;
    using Merchain.Data.Common.Repositories;
    using Merchain.Data.Models;
    using Merchain.Data.Repositories;
    using Merchain.Data.Seeding;
    using Merchain.Services.CloudinaryService;
    using Merchain.Services.Data;
    using Merchain.Services.Data.Interfaces;
    using Merchain.Services.Econt;
    using Merchain.Services.Interfaces;
    using Merchain.Services.Mapping;
    using Merchain.Services.Messaging;
    using Merchain.Web.ViewModels;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => false;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddControllersWithViews();

            services.AddDistributedSqlServerCache(
                options =>
                {
                    options.ConnectionString = this.configuration.GetConnectionString("DefaultConnection");
                    options.SchemaName = "dbo";
                    options.TableName = "Cache";
                });

            services.AddResponseCaching();

            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.IdleTimeout = TimeSpan.FromDays(2);
            });
            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });
            services.AddRazorPages();

            services.AddSingleton(this.configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            Account account = new Account()
            {
                Cloud = this.configuration["Cloudinary:Cloud"],
                ApiKey = this.configuration["Cloudinary:ApiKey"],
                ApiSecret = this.configuration["Cloudinary:ApiSecret"],
            };

            Cloudinary cloudinary = new Cloudinary(account);
            services.AddSingleton(cloudinary);

            SendGridEmailSender sendGridEmailSender =
                new SendGridEmailSender(this.configuration["SendGridEmail:ApiKey"]);
            services.AddSingleton(sendGridEmailSender);

            // Application services
            services.AddTransient<UserManager<ApplicationUser>>();
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<IProductsService, ProductsService>();
            services.AddTransient<IReviewsService, ReviewsService>();
            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IOrderItemService, OrderItemService>();
            services.AddTransient<IPromoCodesService, PromoCodesService>();
            services.AddTransient<IEcontService, EcontService>();
            services.AddTransient<CloudinaryService>();
            services.AddApplicationInsightsTelemetry();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();

                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseResponseCaching();
            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapControllerRoute(
                            name: "cart",
                            pattern: "cart/{action=Index}",
                            defaults: new { controller = "ShoppingCart", action = "Index", });
                    endpoints.MapControllerRoute(
                            name: "contactUs",
                            pattern: "contact-us",
                            defaults: new { controller = "Info", action = "ContactUs", });
                    endpoints.MapControllerRoute(
                            name: "product details",
                            pattern: "products/info",
                            defaults: new { controller = "Products", action = "Details", });
                    endpoints.MapControllerRoute(
                            name: "liked products",
                            pattern: "products/liked",
                            defaults: new { controller = "Products", action = "WishList", });
                    endpoints.MapControllerRoute(
                            name: "products admin",
                            pattern: "{area:exists}/products/{action=Index}",
                            defaults: new { controller = "Products", action = "Index", });
                    endpoints.MapControllerRoute(
                            name: "products",
                            pattern: "products/{action=Index}",
                            defaults: new { controller = "Products", action = "Index", });
                    endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                    endpoints.MapRazorPages();
                });
        }
    }
}
