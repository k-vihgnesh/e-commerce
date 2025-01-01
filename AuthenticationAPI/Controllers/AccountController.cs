
using Jwtauthentication;
using Jwtauthentication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtTokenHandler _jwtTokenHandler;

        public AccountController(JwtTokenHandler jwtTokenHandler)
        {
            _jwtTokenHandler = jwtTokenHandler;
        }

        [HttpPost]
        [Route("login")]
        public ActionResult<AuthenticationResponse?> AuthenticateUser([FromBody] AuthendicationRequest authendicationRequest)
        {
            var response = _jwtTokenHandler.GenerateJwtToken(authendicationRequest);
            if (response == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            return response;
        }

    }
}
