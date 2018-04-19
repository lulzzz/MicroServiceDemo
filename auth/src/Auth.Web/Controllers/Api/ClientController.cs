using System.Collections.Generic;
using System.Threading.Tasks;
using Auth.Core.Services.Client;
using Auth.Core.Services.Resource;
using Auth.Web.Filters;
using Auth.Web.Services;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Web.Controllers.Api
{
	[NoCache]
	[Route("api/[controller]")]
	//[Authorize(Roles = "superadmin")]
	public class ClientController : Controller
	{
		private readonly IClientService clientService;

		public ClientController(IClientService clientService)
		{
			this.clientService = clientService;
		}

		[HttpGet]
		public async Task<IEnumerable<Client>> GetAll()
		{
			return await clientService.GetAll();
		}
	}
}
