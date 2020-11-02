using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductCompatibility.Data;
using ProductCompatibility.Data.Interfaces;
using Models = ProductCompatibility.Data.Models;
using ProductCompatibility.Data.Repository;
using Microsoft.AspNetCore.Http;
using ProductCompatibility.Migrations;

namespace ProductCompatibility
{
    public class Startup
    {
        public Startup(IWebHostEnvironment configuration)
        {            
            Configuration = (IConfigurationRoot)new ConfigurationBuilder().SetBasePath(configuration.ContentRootPath).AddJsonFile("appsettings.json").Build();
        }

        private IConfigurationRoot Configuration;        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {            
            services.AddDbContext<AppDBContent>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddTransient<IAllProducts, ProductRepository>();
            services.AddTransient<IAllCategories, CatogoryRepository>();

            services.AddTransient<IAllCompatibilities, CompatibilityRepository>();
            services.AddTransient<IAllProductsCompatibilities, ProductsCompatibilityRepository>();

            services.AddTransient<IAllOrders, OrderRepository>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(sp=> Models.ShopCart.GetCart(sp));

            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();                
            }
            else {
                app.UseExceptionHandler("/Home/Error");            
                app.UseHsts();
            }

            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            //app.UseHttpsRedirection();
            
            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "categoryFilter",
                    pattern: "Product/action/{category?}", 
                    defaults: new { Controller = "Product", action = "List" });
            });

            using (var scope = app.ApplicationServices.CreateScope()) {
                AppDBContent content = scope.ServiceProvider.GetRequiredService<AppDBContent>();
                DBObjects.Initial(content);
            }
        }
    }
}
