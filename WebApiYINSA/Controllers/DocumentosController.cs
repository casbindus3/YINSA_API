using Microsoft.AspNetCore.Mvc;
using WebApiYINSA.Services;

namespace WebApiYINSA.Controllers
{
	[ApiController]
	[Route("yinsa/api/documentos")]
	public class DocumentosController : ControllerBase
	{
		private readonly IDocumentosService documentosService;

		public DocumentosController(IDocumentosService documentosService)
        {
            this.documentosService= documentosService;
        }


		[HttpGet]
		[Route("facturas")]
		public async Task<ActionResult> GetFacturas(DateTime inicio, DateTime fin)
		{
			var facturas = await documentosService.FacturasGeneral( inicio, fin);
			return Ok(facturas);
		}

		[HttpGet]
		[Route("facturas/usuario")]
		public async Task<ActionResult> GetFacturasUsuario(string id, DateTime inicio, DateTime fin)
		{
			var facturas = await documentosService.FacturasPorUsuario(id, inicio, fin);
			return Ok(facturas);
		}

		[HttpGet]
		[Route("notascredito")]
		public async Task<ActionResult> GetNotasCredito(DateTime inicio, DateTime fin)
		{
			var notas = await documentosService.NotasCreditoGeneral(inicio, fin);
			return Ok(notas);
		}

		[HttpGet]
		[Route("notascredito/usuario")]
		public async Task<ActionResult> GetNotasCreditoUsuario(string id,DateTime inicio, DateTime fin)
		{
			var notas = await documentosService.NotasCreditoPorUsuario(id,inicio, fin);
			return Ok(notas);
		}
		[HttpGet]
		[Route("pedidos/usuario")]
		public async Task<ActionResult> GetPedidosCliente(string id)
		{
			var res = await documentosService.PedidosCliente(id);
			return Ok(res);
		}
		[HttpGet]
		[Route("pedidos/finalizados/usuario")]
		public async Task<ActionResult> GetPedidosFinalizados(string id)
		{
			var res = await documentosService.PedidosFinalizadosCliente(id);
			return Ok(res);
		}

	}
}
