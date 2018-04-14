using DNIC.AccountCenter.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DNIC.AccountCenter.Controllers
{
	public class ClientsController : Controller
	{
		private IResourceService resourceService;

		public ClientsController(IResourceService resourceService)
		{
			this.resourceService = resourceService;
		}
		public async Task<IActionResult> Index()
		{
			return await Task.FromResult(View(resourceService.GetAll()));
		}
	}
}
