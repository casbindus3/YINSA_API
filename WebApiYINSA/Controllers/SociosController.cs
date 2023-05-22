using Microsoft.AspNetCore.Mvc;
using WebApiYINSA.Models;
using WebApiYINSA.Services;

namespace WebApiYINSA.Controllers
{
	[ApiController]
	[Route("/api/socios")]
	public class SociosController : ControllerBase
	{
		private readonly ISociosService sociosService;

		public SociosController(ISociosService sociosService)
        {
            this.sociosService = sociosService;
        }
        [HttpGet]
		[Route("socio")]
		public async Task<ActionResult> GetSocio(string id)
		{
			var socio= await sociosService.GetSocio(id);
			return Ok(socio);
		}
		[HttpGet]
		[Route("clientes")]
		public async Task<ActionResult> GetClientes()
		{
			var clientes = await sociosService.GetClientes();
			return Ok(clientes);
		}
		[HttpGet]
		[Route("proveedores")]
		public async Task<ActionResult> GetProveedores()
		{
			var proveedores = await sociosService.GetProveedores();
			return Ok(proveedores);
		}

		
	}
}
