using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace skill_test_1.Interfaces
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
    }
}
