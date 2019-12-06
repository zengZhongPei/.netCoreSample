using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;

namespace IdentityServerCenter.Config
{
    /// <summary>
    ///  identityServer 配置文件
    /// </summary>
    public class IdentityServerConfig
    {
        public static IEnumerable<ApiResource> GeResources()
        {
            return  new List<ApiResource>()
            {
                new ApiResource("api","my api") //支持可访问的resource
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return  new List<Client>()
            {
                new Client()
                {
                 ClientId="client",
                 AllowedGrantTypes =GrantTypes.ClientCredentials,//设置为客户端凭证模式
                 ClientSecrets=new List<Secret>()
                 {
                     new Secret("secret".Sha256())//设置客户端凭证模式秘钥
                 },
                 AllowedScopes = new List<string>(){"api"} //允许访问的resource
                }
            };
        }
    }
}
