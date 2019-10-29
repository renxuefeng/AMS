using AMS.BLL.MenuApp;
using AMS.BLL.RoleApp;
using AMS.BLL.UserApp;
using AMS.Common.Configuration;
using AMS.DAL.IRepositories;
using AMS.DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMS.Helpers
{
    public static class StartupHelper
    {
        /// <summary>
        /// 注入配置文件
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureRootConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            var rootConfiguration = configuration.GetSection(nameof(RootConfiguration)).Get<RootConfiguration>();
            services.AddSingleton(rootConfiguration);
            var audienceConfiguration = configuration.GetSection(nameof(AudienceConfiguration)).Get<AudienceConfiguration>();
            services.AddSingleton(audienceConfiguration);

            services.ConfigureRepositories();
            services.ConfigureServices();
            return services;
        }
        /// <summary>
        /// 注入仓储
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IRoleRepository, RoleRepository>();
            services.AddSingleton<IMenuRepository, MenuRepository>();
            return services;
        }
        /// <summary>
        /// 注入服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<IUserInfoAppService, UserInfoAppService>();
            services.AddSingleton<IRoleAppService, RoleAppService>();
            services.AddSingleton<IMenuInfoAppService, MenuInfoAppService>();
            return services;
        }
        /// <summary>
        /// 配置支持语言种类
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureLocalization(this IApplicationBuilder app)
        {
            //var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            //app.UseRequestLocalization(options.Value);
            var supportedCultures = new[] { "en","zh" };
            app.UseRequestLocalization(options =>
                options
                    .AddSupportedCultures(supportedCultures)
                    .AddSupportedUICultures(supportedCultures)
                    .SetDefaultCulture(supportedCultures[1])
            );
        }

        public static void AddLogging(this IApplicationBuilder app, ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }
    }
}
