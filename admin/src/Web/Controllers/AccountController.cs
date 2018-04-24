using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

		[HttpGet]
		[Authorize]
		public IActionResult Login() => Redirect("/");

		[HttpGet]
        [Authorize]
        public async Task<IActionResult> Info()
		{
			var user = User;

            var model = new {Name = User.Claims.FirstOrDefault(x=>x.Type == "name").Value,Roles=new []{ User.Claims.FirstOrDefault(x=>x.Type == ClaimTypes.Role).Value} };

            return Ok(model);
        }

    }
}
