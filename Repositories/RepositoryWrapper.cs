using Microsoft.Extensions.Configuration;
using skill_test_1.Data;
using skill_test_1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace skill_test_1.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private stest1Context _context;
        private IConfiguration _config;
        private IUserRepository _user;

        public RepositoryWrapper(stest1Context context, IConfiguration config)
        {
            this._context = context;
            this._config = config;
        }

        public IUserRepository User
        {
            get { 
                if (_user == null)
                {
                    _user = new UserRepository(_context,_config);
                }

                return _user;
            }
        }



    }
}
