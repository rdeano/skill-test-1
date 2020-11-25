using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using skill_test_1.Interfaces;
using skill_test_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace skill_test_1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IRepositoryWrapper _repoWrapper;
        private IEmailRepository _emailRepository;

        public UserController(IRepositoryWrapper repoWrapper, IEmailRepository emailRepository)
        {
            _repoWrapper = repoWrapper;
            _emailRepository = emailRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {

            return await _repoWrapper.User.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var user = await _repoWrapper.User.Get(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(User user)
        {
            try
            {
                var res = await _repoWrapper.User.Update(user);
                return Ok();
            }
            catch(Exception ex)
            {
                return this.Content(ex.ToString());
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(User user)
        {
            try {
                await _repoWrapper.User.Add(user);

                string from = "adminservices@unifrutti.com.ph";
                string to = user.Email;
                string subject = "Welcome to Skill Test App";
                string content = "Your new account has been created. <br />";

                _emailRepository.sendEmail(from, to, subject, content);

                return Ok();
            }
            catch (Exception ex)
            {
                return this.Content(ex.ToString());
            }
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(User user)
        {
            try
            {
                var res = await _repoWrapper.User.Delete(user.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return this.Content(ex.ToString());
            }
        }

        [HttpPost("validateuser")]
        public AuthenticateResponse Validateuser(AuthenticateResponse user)
        {
            AuthenticateResponse res = _repoWrapper.User.Authenticate(user);
            return res;
        }


        [HttpGet("testAuthJWT")]
        [Authorize]
        public async Task<ActionResult<User>> testAuthJWT(int id)
        {
            var user = await _repoWrapper.User.Get(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }


    }
}
