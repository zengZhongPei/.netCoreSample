using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ClientCredentialsApi
{
    /// <summary>
    /// ClientCredentials授权模块拉取授权中心进行授权。从而访问api接口
    /// </summary>
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
            #region 配置identity授权认证 参考文献:http://docs.identityserver.io/en/aspnetcore2/quickstarts/1_client_credentials.html

            //添加授权认证
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//使用jwt认证模式
                .AddIdentityServerAuthentication(options => //配置IdentityServer认证
                {
                    options.ApiName = "api"; //此处的值需要在授权中心的resource中存在
                    options.Authority = "https://localhost:44396/";//授权颁发者的地址此。此处就是identityServerCenter的项目运行地址
                    //options.RequireHttpsMetadata = false;//是否启用https
                });

            #endregion
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
