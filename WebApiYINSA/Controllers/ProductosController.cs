using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApiYINSA.Models;
using WebApiYINSA.Services;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace WebApiYINSA.Controllers
{
	[ApiController]
	[Route("yinsa/api/productos")]
	public class ProductosController: ControllerBase
	{
		private readonly IProductosService productosService;

		public ProductosController(IProductosService productosService)
		{
			this.productosService = productosService;
		}

		[HttpGet]
		//[Route("ObtenerProductos")]
		public async Task<ActionResult> ObtenerProductos()
		{
			var productos = await productosService.ObtenerProductos();
			var respuesta = Ok(productos);
			return respuesta;
		}
	}
}
