using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBankSystem.Core.Entities;
using OnlineBankSystem.Core.Repositories;
using OnlineBankSystem.Infrastructure;
using OnlineBankSystem.Infrastructure.Repositories;
using OnlineBankSystem.Services.Interfaces;
using OnlineBankSystem.Services.Models;
using OnlineBankSystem.Services.Services;
using Twilio;

namespace OnlineBankSystem.Web
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<TwilioOptions>(Configuration.GetSection("Twilio"));
            services.Configure<FixerOptions>(Configuration.GetSection("Fixer"));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services
                .Configure<CookieTempDataProviderOptions>(options => { options.Cookie.IsEssential = true; });

            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
                {
                    options.Password.RequireNonAlphanumeric = false;
                    options.SignIn.RequireConfirmedEmail = false;
                    options.SignIn.RequireConfirmedAccount = true;
                    options.SignIn.RequireConfirmedPhoneNumber = true;

                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.AllowedForNewUsers = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                options.SlidingExpiration = true;
                options.LoginPath = "/account/login";
                options.LogoutPath = "/account/logout";
            });

            services
                .AddResponseCompression(options => options.EnableForHttps = true);

            services.AddScoped<ISmsService, SmsService>();
            services.AddScoped<ICardHelper, CardHelper>();
            services.AddScoped<ICryptoHelper, CryptoHelper>();
            services.AddScoped<IAccountHelper, AccountHelper>();
            services.AddScoped<ITransactionHelper, TransactionHelper>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ICardRepository, CardRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ITransactionStatusRepository, TransactionStatusRepository>();
            services.AddScoped<IAccountStatusRepository, AccountStatusRepository>();
            services.AddScoped<ICardStatusRepository, CardStatusRepository>();
            services.AddScoped<ICountryCurrencyCodeRepository, CountryCurrencyCodeRepository>();
            services.AddScoped<IDepartamentRepository, DepartamentRepository>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<ICountryCurrencyCodeService, CountryCurrencyCodeService>();
            services.AddScoped<IAccountStatusService, AccountStatusService>();
            services.AddScoped<ICardStatusService, CardStatusService>();
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IDepartamentService, DepartamentService>();

            services.AddRazorPages();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            var cultureInfo = new CultureInfo("en-US");

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
