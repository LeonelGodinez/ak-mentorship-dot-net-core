using Mentorship.Core.Aplication.DTO;
using Mentorship.Core.Aplication.Helpers;
using Mentorship.Core.Aplication.Interfaces;
using Mentorship.Core.Domain;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mentorship.Core.Aplication.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _repository;
        private readonly AppSettings _appSettings;


        public UserService(IOptions<AppSettings> appSettings, IUserRepository repository) {
            _repository = repository;
            _appSettings = appSettings.Value;
        }

        public List<UserResult> GetUsers() {
            var result = _repository.GetUsers();
            return result;
        }

        public RestResponse Authenticate(AuthenticateRequest model) {
            User user = new User() { FirstName = model.Username, Password = model.Password };
            var result = _repository.login(user);
            if (result == null) return new RestResponse() { Success = false, Data = new { Message = "UserName or password incorrect" } };
            user.Id = result.id;
            user.FirstName = result.firstName;
            user.LastName = result.lastName;
            var token = generateJwtToken(user);
            return new RestResponse(){ Success = true, Data = new { Token = token } };
        }

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = ne w SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GetName() {
            return "Leo";
        }
    }
}
