using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTAuthSample.model
{
    public class JWTSeetings
    {
        /// <summary>
        /// 谁颁发的授权
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// 谁使用的
        /// </summary>
        public string Issue { get; set; }

        /// <summary>
        /// 加密秘钥
        /// </summary>
        public string SecretKey { get; set; }
    }
}
