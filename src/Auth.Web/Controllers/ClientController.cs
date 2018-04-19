using System.Threading.Tasks;
using Auth.Core.Services.Resource;
using Auth.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Web.Controllers
{
	public class ClientController : Controller
	{
		public async Task<IActionResult> Index()
		{
			return await Task.FromResult(View());
		}
	}
}
