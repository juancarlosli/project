using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebServer.ViewModels;


namespace WebServer.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        
        public IConfiguration Configuration { get; set; }

        public AuthController(IConfiguration configuration)
        {
            Configuration = configuration;
        }



        [HttpPost]
        public IActionResult GetToken([FromBody]UserViewModel credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (credentials.UserName != "admin")
            {
                return Unauthorized();
            }

            var token = Security.JwtTokenBuilder.GetSecuredToken(Configuration);

            return Ok(new {
                Token = String.Concat("Bearer ", Security.JwtTokenBuilder.WriteToken(token))
            });
        }

    }
}
