using System;
using EasyUowApplication.EFCore;
using EasyUowApplication.Middleware;
using EasyUowApplication.Repositories;
using MiCake.EFCore.Easy.Extension;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;

namespace EasyUowApplication
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
            services.AddControllers();

            services.AddControllers();

            //将数据库链接字符串替换为您的数据库
            services.AddDbContext<UowAppDbContext>(options =>
            {
                options.UseMySql("Server=localhost;Database=uowexample;User=root;Password=a12345;", mySqlOptions => mySqlOptions
                    .ServerVersion(new ServerVersion(new Version(10, 5, 0), ServerType.MariaDb)));
            });
            services.AddTransient(typeof(ItineraryRepository));
            services.AddMiCake<UowMiCakeModule>();

            //Add Swagger
            services.AddSwaggerDocument(document => document.DocumentName = "MiCake Uow Easy");

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization(); 
            
            app.InitMiCake();

            app.UseMiddleware<UnitOfWorkMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOpenApi();
            app.UseSwaggerUi3();
        }
    }
}
