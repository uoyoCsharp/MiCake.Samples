using MiCake;
using MiCakeDemoApplication.MiCakeFeatures;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MiCakeDemoApplication
{
    public class Startup
    {
        private IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            #region addFileProvider
            var physicalProvider = _env.ContentRootFileProvider;
            services.AddSingleton(physicalProvider);
            #endregion

            services.AddDbContext<MyDbContext>(options =>
            {
                options.UseSqlite("Data Source=micake.db");
            });

            services.AddMiCakeWithDefault<MyDbContext, MyEntryModule>(miCakeAspNetConfig: s =>
            {
                // s.UseCustomModel();  打开将使用自定义格式模型
                s.DataWrapperOptions.IsDebug = true;
            })
            .Build();

            //Add Swagger
            services.AddSwaggerDocument(document => document.DocumentName = "MiCake Demo Application");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.StartMiCake();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOpenApi();
            app.UseSwaggerUi3();
        }
    }
}
