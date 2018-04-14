using DNIC.AccountCenter.Filters;
using DNIC.AccountCenter.Services;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DNIC.IdentityServer4.Admin.Controllers.Api
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
