using skill_test_1.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using skill_test_1.Models;

namespace skill_test_1.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        AuthenticateResponse Authenticate(AuthenticateResponse model);

        string GenerateJSONWebToken(User user); 
    }
}
