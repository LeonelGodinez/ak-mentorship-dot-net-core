using Mentorship.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentorship.Core.Aplication.Interfaces
{
    public interface IUserRepository
    {
        public List<UserResult> GetUsers();
        public UserResult login(User user);
    }
}
