using System.Threading.Tasks;
using GracefulTear.Core.Services.Resource;
using GracefulTear.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace GracefulTear.Web.Controllers
{
	public class ClientsController : Controller
	{
		public async Task<IActionResult> Index()
		{
			return await Task.FromResult(View());
		}
	}
}
