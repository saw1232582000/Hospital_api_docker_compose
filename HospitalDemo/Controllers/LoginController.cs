using HospitalDemo.Data;
using HospitalDemo.Models.RefreshToken;
using HospitalDemo.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace HospitalDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly HospitalDbContext dbContext;
        private IConfiguration _config;
        public LoginController(HospitalDbContext dbContext, IConfiguration config)
        {
            this.dbContext = dbContext;
            _config = config;
                
        }
        [NonAction]
        public async Task<TokenResponse> TokenAuthenticate(string user, Claim[] claims)
        {
            var access_token= GenerateAccessToken(claims);

            var response= new TokenResponse() { access_token = access_token, refresh_token = await GenerateRefreshToken(user) };
            return response;
        }

        [NonAction]
        private string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenHandler = new JwtSecurityTokenHandler();
            //var exdate = DateTime.Now;
           // var exdate2 = DateTime.Now.AddMinutes(1);
            var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(1),
            signingCredentials: credentials);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            var access_token = tokenString;
            return access_token;
        }
        [NonAction]
        public async Task<string> GenerateRefreshToken(string username)
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                string refreshtoken = Convert.ToBase64String(randomNumber);
                var token = await dbContext.RefreshToken.FirstOrDefaultAsync(t => t.userName == username);
                if (token != null)
                {
                    token.refresh_token = refreshtoken;
                    dbContext.RefreshToken.Update(token);
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    dbContext.RefreshToken.Add(new refreshToken()
                    {
                        Token_id = new Random().Next().ToString(),
                        userName = username,
                        refresh_token = refreshtoken,
                        isActive = true
                    });
                    await dbContext.SaveChangesAsync();

                }
                return refreshtoken;
            }

        }

        // [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task< IActionResult> Login([FromBody]UserLogin_Request_Model userlogin)
        {
            var user = dbContext.User.FirstOrDefault(u => u.username == userlogin.username && u.password == userlogin.password); 
            if (user == null)
            {
                return Unauthorized();
            }
            // var token = GenerateToken(userlogin);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.username),
                new Claim(ClaimTypes.Role,user.role)
                
            };
            var accesstoken = GenerateAccessToken(claims);
           var response=new TokenResponse(){ access_token=accesstoken,refresh_token=await GenerateRefreshToken(userlogin.username) };
           
            return Ok(response);
           
            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            //var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //var tokenHandler = new JwtSecurityTokenHandler();

            //var token = new JwtSecurityToken(
            //_config["Jwt:Issuer"],
            //_config["Jwt:Audience"],
            //claims,
            //expires: DateTime.Now.AddMinutes(10),
            //signingCredentials: credentials);
            //var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            //return Ok(new { access_token = tokenString });
           // Response.Headers.Add("Authorization", "Bearer " + tokenString);
           // return Ok();
        }

        [HttpPost]
        [Route("refreshToken")]
        public async Task<IActionResult> refToken([FromBody] TokenResponse tokenresponse)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            SecurityToken securityToken;
            var principal = tokenhandler.ValidateToken(tokenresponse.access_token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(tokenkey),
                ValidateIssuer = false,
                ValidateAudience = false,

            }, out securityToken);
            var token = securityToken as JwtSecurityToken;
            if (token != null && !token.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
            {
                return Unauthorized("invalid token format");
            }
            
            var username = principal.Identity?.Name;
            if(username == null)
            {
                return Unauthorized("unknow username");
            }

            //var user = await dbContext.RefreshToken.FirstOrDefaultAsync(item => item.userName == username && item.refresh_token == tokenresponse.refresh_token);
            //if (user == null)
            //{
            //    return NotFound("no user in database");
            //}
            var response = TokenAuthenticate(username, principal.Claims.ToArray()).Result;
            return Ok(response);
        }


    }
}
