using System.Collections.Generic;
using System.Threading.Tasks;
using Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;

namespace Admin.Controllers
{

    public class AccountController : Controller
    {
        private AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        public class Users
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromBody]Users user)
        {
            await _accountService.LoginAsync(user.UserName,user.Password);

            return Ok();
        }

		[HttpGet]
		[Authorize]
		public IActionResult Login2() => Redirect("http://www.baidu.com");

		[HttpGet]
        [Authorize]
        public async Task<IActionResult> Info()
        {
            var model = new {Name = "小明",Roles=new []{"admin"}};

            return Ok(model);
        }

    }
}
