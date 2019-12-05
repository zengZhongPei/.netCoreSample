using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace JWTAuthSample.MyTokenValidators
{
    /// <summary>
    /// 自定义token验证
    /// </summary>
    public class MyTokenValidators : ISecurityTokenValidator
    {
        public bool CanValidateToken => true;

        public int MaximumTokenSizeInBytes { get; set; }

        public bool CanReadToken(string securityToken)
        {
            return true;
        }

        public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {
            //通用返回属性
            var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);

            //进行自定义验证，如果验证成功就往identity中添加相关权限和名称以及其他操作
            if (!string.IsNullOrEmpty(securityToken) && securityToken.Contains("456895"))
            {
                identity.AddClaims(new List<Claim>()
                {
                    new Claim("name","张三"),
                    new Claim("admin","true"),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType,"admin")
                });
            }

            validatedToken = null;
            var principal = new ClaimsPrincipal(identity);
            return principal;
        }
    }
}
