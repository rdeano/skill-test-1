using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using skill_test_1.Data;
using skill_test_1.Interfaces;
using skill_test_1.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace skill_test_1.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private IConfiguration _config;
        private stest1Context _context;

        public UserRepository(stest1Context context, IConfiguration config) : base(context)
        {
            _config = config;
            _context = context;
        }

        public AuthenticateResponse Authenticate(AuthenticateResponse model)
        {
            var user = _context.Users.Where(a => a.Email == model.Email && a.Password == model.Password).FirstOrDefault();
            // return null if user not found
            if (user == null)
            {
                return null;
            }
            string token = this.GenerateJSONWebToken(user);
            model.FirstName = user.Firstname;
            model.LastName = user.Lastname;
            model.Id = user.Id;
            model.Token = token;
            return model;
        }

        public string GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim("id", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(_config["JWT:issuer"],
                _config["JWT:issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
