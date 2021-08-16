using CinemaSite.Data.Database;
using CinemaSite.Data.Interfaces;
using CinemaSite.Data.Models;
using CinemaSite.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSite
{
    public class Startup
    {
        public IConfigurationRoot _configurationRoot;

        public Startup(IWebHostEnvironment hostEnvironment)
        {
            _configurationRoot = new ConfigurationBuilder().SetBasePath(hostEnvironment.ContentRootPath).AddJsonFile("dbSettings.json").Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(_configurationRoot.GetConnectionString("DefaultConnection"))
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            services.AddTransient<IAllGenres, GenresRepository>();
            services.AddTransient<IAllReviews, ReviewsRepository>();
            services.AddTransient<IAllFilms, FilmsRepository>();
            services.AddTransient<IAllUsers, UsersRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseStatusCodePages();
            app.UseRouting();
            app.UseSession();
            app.UseCors();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
            using (var scope = app.ApplicationServices.CreateScope())
            {
                AppDbContext content = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                DbObjects.Initial(content);
            }
        }

    }
}
