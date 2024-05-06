using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdmin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSession(options => { options.IdleTimeout = TimeSpan.FromMinutes(30); });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<DropDown.ICommonDDL, DropDown.CommonDDL>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IImageWork, ImageWork>();
            services.AddTransient<ISessionData, SessionData>();

            //Database 
            services.AddSingleton<DBAccess.IDBAccess>(new DBAccess.MSSQLDataAccess(AppData.GetMSSQLDBCon()));

            //Account
            services.AddTransient<Account.Data.ISystemData, Account.Data.SystemData>();
            services.AddTransient<Account.Data.IUserData, Account.Data.UserData>();
            services.AddTransient<Account.Data.ITokenData, Account.Data.TokenData>();
            services.AddTransient<Account.Data.IEmailSettingData, Account.Data.EmailSettingData>();
            services.AddTransient<Account.Data.IAPIKeyData, Account.Data.APIKeyData>();

            //Catelog
            services.AddTransient<DropDown.ICatelogDDL, DropDown.CatelogDDL>();
            services.AddTransient<Catalog.Data.IBrandData, Catalog.Data.BrandData>();
            services.AddTransient<Catalog.Data.IItemTypeData, Catalog.Data.ItemTypeData>();
            services.AddTransient<Catalog.Data.ICategoryMainData, Catalog.Data.CategoryMainData>();
            services.AddTransient<Catalog.Data.ICategorySubData, Catalog.Data.CategorySubData>();
            services.AddTransient<Catalog.Data.IItemData, Catalog.Data.ItemData>();
            services.AddTransient<Catalog.Data.IImageData, Catalog.Data.ImageData>();
            services.AddTransient<Catalog.Data.IDiscountSchemaData, Catalog.Data.DiscountSchemaData>();
            services.AddTransient<Catalog.Data.IItemSizeData, Catalog.Data.ItemSizeData>();
            services.AddTransient<Catalog.Data.IIngredientsData, Catalog.Data.IngredientsData>();

            //Sales
            services.AddTransient<DropDown.ISalesDDL, DropDown.SalesDDL>();
            services.AddTransient<Sales.Data.ICountryData, Sales.Data.CountryData>();
            services.AddTransient<Sales.Data.ICustomerCategoryData, Sales.Data.CustomerCategoryData>();
            services.AddTransient<Sales.Data.ICustomerGroupData, Sales.Data.CustomerGroupData>();
            services.AddTransient<Sales.Data.ICustomerData, Sales.Data.CustomerData>();
            services.AddTransient<Sales.Data.ISaleOrderData, Sales.Data.SaleOrderData>();
            services.AddTransient<Sales.Data.IOrderItemData, Sales.Data.OrderItemData>();
            services.AddTransient<Sales.Data.IOrderActionData, Sales.Data.OrderActionData>();
            services.AddTransient<Sales.Data.IOrderPaymentData, Sales.Data.OrderPaymentData>();

            //Content
            services.AddTransient<Content.Data.ISlideBannerData, Content.Data.SlideBannerData>();
            services.AddTransient<Content.Data.IMetaTagData, Content.Data.MetaTagData>();
            services.AddTransient<Content.Data.ISectionTextData, Content.Data.SectionTextData>();
            services.AddTransient<Content.Data.ISettingsCategoryData, Content.Data.SettingsCategoryData>();
            services.AddTransient<Content.Data.ISettingsData, Content.Data.SettingsData>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "MyArea",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
