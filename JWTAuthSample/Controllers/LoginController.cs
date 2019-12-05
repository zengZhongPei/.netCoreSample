using JWTAuthSample.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTAuthSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private JWTSeetings _jwtSeetings;

        public LoginController(IOptions<JWTSeetings> jwtSeetings)
        {
            _jwtSeetings = jwtSeetings.Value;
        }

        [HttpGet("index")]
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpGet("GetToken")]
        public IActionResult GetToken([FromQuery]AccountParam param)
        {
            if (!ModelState.IsValid)
                return BadRequest(new{msg="参数验证失败"});

            if (param.UserAccount!="17623028800" || param.VerificationCode!="4566")
                return BadRequest(new {msg = "当前账号不存在"});

           //开始颁发token
           var claims = new Claim[]
           {
               new Claim(ClaimTypes.Name,"17623028800"), 
               new Claim("admin","true"),
               new Claim(ClaimTypes.Role,"admin"),
           };
           var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSeetings.SecretKey));
           var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

           var token = new JwtSecurityToken(_jwtSeetings.Issue, _jwtSeetings.Audience, claims, DateTime.Now,
               DateTime.Now.AddSeconds(20), creds);
           return Ok(new{Token=new JwtSecurityTokenHandler().WriteToken(token)});
        }
    }
}