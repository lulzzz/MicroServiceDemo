using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain;
using Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Service
{
    public class AccountService: IAccountService
	{
        private HttpContext _httpcontext;

        public AccountService(IHttpContextAccessor contextAccessor)
        {
            _httpcontext = contextAccessor.HttpContext;
        }
        public async Task LoginAsync(string userName, string password)
        {
            var claimsIdentity = new ClaimsIdentity(new[] {new Claim(ClaimTypes.Name, userName)}, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(claimsIdentity);
            await _httpcontext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new Microsoft.AspNetCore.Authentication.AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddHours(24),
                IsPersistent = true,
                AllowRefresh = false
            });
        }
    }
}
