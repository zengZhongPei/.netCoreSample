using JWTAuthSample.model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthSample
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
            services.Configure<JWTSeetings>(Configuration.GetSection("jwtSeetings"));
            
            //在配置文件中获取jwt相关加密参数
            var jwtseetings = new JWTSeetings();
            Configuration.Bind("jwtSeetings",jwtseetings);
            services.AddAuthentication(options =>
            {
                //认证方式的配置.这里配置的是 jwt验证方式
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                #region 默认配置jwt相关参数验证.请求token参数放到headers中，并且参数名为:"Authorization"。token前面加上前缀："bearer "
                
                //options.TokenValidationParameters = new TokenValidationParameters()
                //{
                //    ValidAudience = jwtseetings.Audience,
                //    ValidIssuer = jwtseetings.Issue,
                //    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtseetings.SecretKey))//对称加密
                //};

                #endregion

                #region 自定义jwt验证。参数名可以任意定义

                //清理以前的验证参数
                options.SecurityTokenValidators.Clear();
                options.SecurityTokenValidators.Add(new MyTokenValidators.MyTokenValidators());
                
                options.Events = new JwtBearerEvents()
                {
                    //通过这个属性重新设定token的值
                    OnMessageReceived = context =>
                    {
                        var token = context.Request.Headers["mytoken"];
                        context.Token = token.FirstOrDefault();
                        return Task.CompletedTask;
                    }
                };

                #endregion
            });
            services.AddAuthorization(option =>
            {
                option.AddPolicy("admin",policy=>policy.RequireClaim("admin"));
            });
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

            app.UseAuthentication();//开启授权管道
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
