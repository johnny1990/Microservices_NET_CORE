using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using WebApi.Helpers;
using WebApi.Models;
using System.Linq;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly JwtSettings jwtSettings;

        public AuthorizationController(JwtSettings jwtSettings)
        {
            this.jwtSettings = jwtSettings;
        }

        private IEnumerable<Users> logins = new List<Users>()
        {
            new Users()
            {
                Id = 1,
                UserName ="Admin",
                Password="Admin",
            },
            new Users()
            {
                Id = 2,
                UserName ="User",
                Password="User",
            }
        };

        /// <summary>
        /// Generate an Access token
        /// </summary>
        /// <param name="userLogins"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GenerateToken(UserLogins userLogins)
        {
            try
            {
                var Token = new UserTokens();
                var Valid = logins.Any(x => x.UserName.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase));
                if (Valid)
                {
                    var user = logins.FirstOrDefault(x => x.UserName.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase));
                    Token = JwtHelpers.GenTokenkey(new UserTokens()
                    {
                        UserName = user.UserName,

                    }, jwtSettings);
                }
                else
                {
                    return BadRequest($"wrong password");
                }
                return Ok(Token);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
