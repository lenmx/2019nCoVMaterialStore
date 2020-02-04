using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using nCoVMSApi.Entity.Models;
using nCoVMSApi.Filters;

namespace nCoVMSApi
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
            #region Cors

            services.AddCors(options =>
            {
                options.AddPolicy("nCoVMSApi", policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                        //.AllowCredentials();
                });
            });

            #endregion

            #region DB Options

            services.AddDbContext<nCoVMSDBContext>(ops =>
            {
                ops.UseSqlServer(Configuration.GetConnectionString("nCoVMSConnection"));
            });

            #endregion

            //            #region redis
            //            services.AddDistributedRedisCache(options =>
            //            {
            //                options.Configuration = Configuration["RedisConfig:Configuration"];
            //                options.InstanceName = Configuration["RedisConfig:InstanceName"];
            //            });

            //            string redisConnctionString = Configuration["RedisConfig:ConfigurationIn"];
            //#if DEBUG
            //            redisConnctionString = Configuration["RedisConfig:ConfigurationOut"];
            //#endif
            //            RedisHelper.Initialization(new CSRedis.CSRedisClient(redisConnctionString));

            //            #endregion

            #region JWT

            string baseUrl = Configuration["AuthConfig:BaseUrl"].ToLower();
            string securityKey = Configuration["AuthConfig:SecurityKey"];
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidAudience = baseUrl,
                            ValidIssuer = baseUrl,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey))
                        };
                    });

            #endregion

            #region MVC Options

            services.AddControllers();
            services.AddMvc(ops =>
            {
                // 全局过滤器
                ops.Filters.Add(typeof(CustomExceptionFilterAttribute));
                ops.Filters.Add(typeof(MonitorTicketFilterAttribute));
            }).AddNewtonsoftJson(ops =>
            {
                // json 返回值时间格式化
                ops.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseCors("nCoVMSApi");

            #region 日志配置

            //loggerFactory.AddConsole();
            //loggerFactory.AddDebug();
            //loggerFactory.AddNLog();

            #endregion

            #region Auth

            app.UseAuthentication();

            #endregion

            //访问静态文件
            app.UseStaticFiles();
            var filePath = AppDomain.CurrentDomain.BaseDirectory + "uploads";
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "uploads")),
                RequestPath = "/uploads"
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
