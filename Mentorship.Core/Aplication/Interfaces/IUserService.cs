using Mentorship.Core.Aplication.DTO;
using Mentorship.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentorship.Core.Aplication.Interfaces
{
    public interface IUserService
    {
        public List<UserResult> GetUsers();
        public RestResponse Authenticate(AuthenticateRequest model);

        public string GetName();
    }

}