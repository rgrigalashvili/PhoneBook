using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Phonebook.BLL.Managers.Interfaces;
using Phonebook.BLL.Models;

namespace Phonebook.API.Controllers
{
    [Route("[controller]")]
    public class SecurityController : Controller
    {
        private readonly IUserManager userManager;
        public SecurityController(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        [Route("index")]
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Api is working fine!");
        }

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register(UserModel model)
        {
            return Ok(await userManager.AddUserAsync(model));
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var token = await userManager.Authenticate(model);
            if (token == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(token);
        }
    }
}