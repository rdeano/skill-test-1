using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace skill_test_1.Models
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
        public string Token { get; set; }



    }
}
