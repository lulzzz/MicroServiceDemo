using System.Collections.Generic;
using System.Threading.Tasks;
using GracefulTear.Core.Services.Client;
using GracefulTear.Core.Services.Resource;
using GracefulTear.Web.Filters;
using GracefulTear.Web.Services;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;

namespace GracefulTear.Web.Controllers.Api
{
	[NoCache]
	[Route("api/[controller]")]
	//[Authorize(Roles = "superadmin")]
	public class ClientsController : Controller
	{
		private readonly IClientService clientService;

		public ClientsController(IClientService clientService)
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
