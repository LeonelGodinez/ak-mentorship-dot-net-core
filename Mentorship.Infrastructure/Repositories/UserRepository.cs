using Mentorship.Core.Aplication.Interfaces;
using Mentorship.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentorship.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository 
    {
        private DataContext _dataContext;

        public UserRepository(DataContext dataContext) {
            _dataContext = dataContext;
        }

        public List<UserResult> GetUsers() { 
            var query = _dataContext.Users.FromSqlRaw("Select * from User;").ToList();
            return query;
        }

        public UserResult login(User user) {
            var query = _dataContext.Users.FromSqlRaw($"call login('{user.FirstName}', '{user.Password}')").ToList();
            return query.FirstOrDefault();
        }

    }
}
