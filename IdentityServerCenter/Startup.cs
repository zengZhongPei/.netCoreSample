using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServerCenter.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IdentityServerCenter
{
    /// <summary>
    /// identity服务授权中心
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
            #region 新增identity的配置

            /*共4个步骤:  参考文献:http://docs.identityserver.io/en/aspnetcore2/quickstarts/1_client_credentials.html
               1.添加Nuget包:identityServer4
               2.添加Startup.cs配置类
               3.添加identityServerConfig配置类
               4.更改IdentityServer4配置。把identityServerConfig添加进去
               5.添加客户端配置
            */

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryApiResources(IdentityServerConfig.GeResources())//添加配置文件后把这个添加到配置里面去
                .AddInMemoryClients(IdentityServerConfig.GetClients());//添加配置文件后把这个添加到配置里面去


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

            app.UseIdentityServer();//放入管道
            app.UseHttpsRedirection();
            //app.UseMvc();
        }
    }
}
