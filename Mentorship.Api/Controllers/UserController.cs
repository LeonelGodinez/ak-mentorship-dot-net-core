using Mentorship.Core.Aplication.DTO;
using Mentorship.Core.Aplication.Helpers;
using Mentorship.Core.Aplication.Interfaces;
using Mentorship.Core.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthorizeAttribute = Mentorship.Core.Aplication.Helpers.AuthorizeAttribute;

namespace Mentorship.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost("login")]
        public RestResponse login(AuthenticateRequest request)
        {
            var result = _userService.Authenticate(request);
            return result;

        }

        [Authorize]
        [HttpGet("users")]
        public IEnumerable<UserResult> GetUsers()
        {
            return _userService.GetUsers();
        }
    }
}