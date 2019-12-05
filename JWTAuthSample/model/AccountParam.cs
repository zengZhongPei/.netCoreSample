using System.ComponentModel.DataAnnotations;

namespace JWTAuthSample.model
{
    public class AccountParam
    {
        [Required(AllowEmptyStrings = false,ErrorMessage = "参数账号必传")]
        public string UserAccount { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "参数验证码必传")]
        public string VerificationCode { get; set; }
    }
}
