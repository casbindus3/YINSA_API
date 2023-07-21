using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApiYINSA.Models;
using WebApiYINSA.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace WebApiYINSA.Controllers
{
	[ApiController]
	[Route("/api/productos")]
	//[Authorize]
	public class ProductosController: ControllerBase
	{
		private readonly IProductosService productosService;

		public ProductosController(IProductosService productosService)
		{
			this.productosService = productosService;
		}

		[HttpGet]
		public async Task<ActionResult> ObtenerProductos()
		{
			var productos = await productosService.ObtenerProductos();
			var respuesta = Ok(productos);
			return respuesta;
		}

		[HttpGet]
		[Route("productos/categoria")]
		public async Task<ActionResult> ProductosCat(int id)
		{
			var productos = await productosService.ProductByCat(id);
			var respuesta = Ok(productos);
			return respuesta;
		}
	}
}
