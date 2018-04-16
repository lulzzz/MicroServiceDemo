using System.Collections.Generic;
using System.Threading.Tasks;
using GracefulTear.Web.Filters;
using GracefulTear.Web.Services;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;

namespace GracefulTear.Web.Controllers.Api
{
	[NoCache]
	[Route("api/[controller]")]
	//[Authorize(Roles = "superadmin")]
	public class ResourceController : Controller
	{
		private readonly IResourceService _apiResourceService;

		public ResourceController(IResourceService apiResourceService)
		{
			_apiResourceService = apiResourceService;
		}

		[HttpGet]
		public async Task<IEnumerable<Client>> GetAll()
		{
			return await _apiResourceService.GetAll();
		}
	}
}
