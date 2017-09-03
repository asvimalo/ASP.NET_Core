using Gec.EF.Db;
using Gec.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace Gec.Controllers.Api
{
    [Route("/api/auth")]
    public class AuthController : Controller
    {
        private GecContext _ctx;
        private ILogger<AuthController> _logger;
        private SignInManager<User> _signInManager;
        private UserManager<User> _userManager;
        private IPasswordHasher<User> _hasher;
        private IConfigurationRoot _config;

        public AuthController(GecContext ctx, 
            SignInManager<User> signInManager,
            IPasswordHasher<User> hasher,
            ILogger<AuthController> logger, 
            UserManager<User> userManager,
            IConfigurationRoot config)
        {
            _ctx = ctx;
            _signInManager = signInManager;
            _hasher = hasher;
            _logger = logger;
            _userManager = userManager;
            _config = config;
        }
        [HttpPost("login")]      
        public async Task<IActionResult> Login([FromBody] CredentialModel model)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
                if (result.Succeeded)
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception thrown while logging in: {ex}");
               
            }
            return BadRequest("Failed to login");
        }
        [HttpPost("token")]
        public async Task<IActionResult> CreateToken([FromBody] CredentialModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    if (_hasher.VerifyHashedPassword(user,user.PasswordHash,model.Password) == PasswordVerificationResult.Success)
                    {
                        var userClass = await _userManager.GetClaimsAsync(user);
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Jti, user.Email),
                            

                        }.Union(userClass);
                      //GET KEY FROM A CERTIFICATE OR SERVER
                      //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("VERILONGKEYTHATISSECURE"));
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));
                       
                        var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            issuer: _config["Token:Issuer"],
                            audience: _config["Token:Audience"],
                            claims: claims,
                            expires: DateTime.UtcNow.AddMinutes(15),
                            signingCredentials: creds
                            );

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo,
                        });
                    }
                }

            }
            catch (Exception ex)
            {

                _logger.LogError($"Exception thrown while creating JWT: {ex}");
            }
            return BadRequest("Failed to generate token");
        }
    }
}
