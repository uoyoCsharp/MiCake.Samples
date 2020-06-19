using MiCake;
using MiCake.AspNetCore.Modules;
using MiCakeDemoApplication.Domain.UserBoundary.Aggregates;
using MiCakeDemoApplication.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

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

            services.AddCors(options =>
            {
                options.AddPolicy("uniAppOrigins", builder =>
                {
                    builder.WithOrigins("http://localhost:8080")
                    .AllowAnyHeader();
                });
            });

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
                .UseIdentity<User>()
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

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("uniAppOrigins");

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



    public class Demo : Microsoft.AspNetCore.Mvc.Filters.IAsyncResultFilter
    {
        public Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            throw new System.NotImplementedException();
        }
    }
}
