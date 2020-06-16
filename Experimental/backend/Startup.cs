using MiCake;
using MiCake.Identity;
using MiCakeDemoApplication.Domain.UserBoundary.Aggregates;
using MiCakeDemoApplication.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;

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

            services.AddDistributedMemoryCache();
            services.AddWeChatAndJwtBearer(Configuration);

            //Add EFCore
            services.AddDbContext<MyDbContext>(options =>
            {
                options.UseSqlite("Data Source=micake.db");
            });

            services.AddMiCakeWithDefault<MyDbContext, MyEntryModule>(miCakeAspNetConfig: s =>
                {
                    // s.UseCustomModel();  打开将使用自定义格式模型
                    s.DataWrapperOptions.IsDebug = true;
                })
                .AddIdentityCore<User>(jwtOptions =>
                {
                    jwtOptions.Audience = Configuration["JwtConfig:Audience"];
                    jwtOptions.Issuer = Configuration["JwtConfig:Issuer"];
                    jwtOptions.ExpirationMinutes = int.Parse(Configuration["JwtConfig:ExpireDay"]) * 24 * 60;
                    jwtOptions.SecurityKey = Encoding.Default.GetBytes(Configuration["JwtConfig:SecurityKey"]);
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

            app.UseAuthentication();
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
