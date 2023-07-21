using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using WebApiYINSA.Services;
using static System.Net.WebRequestMethods;

namespace WebApiYINSA.Controllers
{
	[ApiController]
	[Route("/api/documentos")]
	public class DocumentosController : ControllerBase
	{
		private readonly IDocumentosService documentosService;
		private readonly IWebHostEnvironment hostEnvironment;

		public DocumentosController(IDocumentosService documentosService, IWebHostEnvironment hostEnvironment)
        {
            this.documentosService= documentosService;
			this.hostEnvironment = hostEnvironment;
		}
		[HttpGet]
		[Route("/documento/{docentry}")]
		public ActionResult GetDocument(string docentry)
		{
			//var path = "https:\\localhost:7024\\Archivos\\" + docentry;
			//var path = "/Archivos/" + docentry;

			//var carpeta = "C:\\Users\\alma_\\Downloads";

			var path = "Archivos\\archivo.pdf" ;
			var carpeta = Path.Combine(hostEnvironment.ContentRootPath, "Descargas");
			var url = Path.Combine(hostEnvironment.ContentRootPath, path);

			//var carpeta = Server.MapPath("~/carpeta/");

			//intento 1
			/*using (var httpClient = new HttpClient())
			{
				var stream = await httpClient.GetStreamAsync(url);

				using var fileStream = File.Create(Path.Combine(carpeta, "unarchivo.txt"));
				stream.CopyTo(fileStream);

			}*/

			//Intento 2
			/*using (var httpClient = new HttpClient())
			{
				var stream = await httpClient.GetStreamAsync(url);

				using var fileStream = new FileStream(Path.Combine(carpeta, "archivo.txt"), FileMode.Create);
				await stream.CopyToAsync(fileStream);
			}*/

			//intento 3
			if (!Directory.Exists(carpeta))
			{
				Directory.CreateDirectory(carpeta);
			}

			//var httpClient = new HttpClient();
			
				//var archivo = await httpClient.GetByteArrayAsync(url);
				//byte[] archivo = System.IO.File.ReadAllBytes(url);
			var dest = Path.Combine(carpeta, "archivo.pdf");
			System.IO.File.Copy(url, dest, true);
				//WriteAllBytes(carpeta,archivo);
			

				return Ok(new { url = docentry });

		}

		[HttpGet]
		[Route("facturas/cliente")]
		public async Task<ActionResult> FacturasCliente(string id, DateTime inicio, DateTime fin)
		{
			var facturas = await documentosService.FacturasCliente(id,"PEN", inicio, fin);
			var res= Ok(facturas);

			if (res.Value == null) { return NoContent(); }
			return res;
		}
		[HttpGet]
		[Route("facturas/cliente/cancelados")]
		public async Task<ActionResult> FacturasCanCliente(string id, DateTime inicio, DateTime fin)
		{
			var facturas = await documentosService.FacturasCliente(id, "CAN", inicio, fin);
			var res = Ok(facturas);

			if (res.Value == null) { return NoContent(); }
			return res;
		}
		[HttpGet]
		[Route("facturas/cliente/finalizados")]
		public async Task<ActionResult> FacturasFinCliente(string id, DateTime inicio, DateTime fin)
		{
			var facturas = await documentosService.FacturasCliente(id, "CER", inicio, fin);
			var res = Ok(facturas);

			if (res.Value == null) { return NoContent(); }
			return res;
		}

		[HttpGet]
		[Route("notascredito/cliente")]
		public async Task<ActionResult> NotasCreditoCliente(string id,DateTime inicio, DateTime fin)
		{
			var notas = await documentosService.NotasCreditoCliente(id,"PEN", inicio, fin);
			var res= Ok(notas);
			if (res.Value == null) { return NoContent(); }
			return res;
		}

		[HttpGet]
		[Route("notascredito/cliente/cancelados")]
		public async Task<ActionResult> NotasCreditoCanCliente(string id, DateTime inicio, DateTime fin)
		{
			var notas = await documentosService.NotasCreditoCliente(id, "CAN", inicio, fin);
			var res = Ok(notas);
			if (res.Value == null) { return NoContent(); }
			return res;
		}

		[HttpGet]
		[Route("notascredito/cliente/finalizados")]
		public async Task<ActionResult> NotasCreditoFinCliente(string id, DateTime inicio, DateTime fin)
		{
			var notas = await documentosService.NotasCreditoCliente(id, "CER", inicio, fin);
			var res = Ok(notas);
			if (res.Value == null) { return NoContent(); }
			return res;
		}

		[HttpGet]
		[Route("facturas/proveedor")]
		public async Task<ActionResult> GetFacturasProveedor(string id, DateTime inicio, DateTime fin)
		{
			var facturas = await documentosService.FacturasProveedor(id, "PEN", inicio, fin);
			var res= Ok(facturas);
			if (res.Value == null) { return NoContent(); }
			return res;
		}

		[HttpGet]
		[Route("facturas/proveedor/cancelados")]
		public async Task<ActionResult> FacturasCanProveedor(string id, DateTime inicio, DateTime fin)
		{
			var facturas = await documentosService.FacturasProveedor(id, "CAN", inicio, fin);
			var res = Ok(facturas);
			if (res.Value == null) { return NoContent(); }
			return res;
		}

		[HttpGet]
		[Route("facturas/proveedor/finalizados")]
		public async Task<ActionResult> FacturasFinProveedor(string id, DateTime inicio, DateTime fin)
		{
			var facturas = await documentosService.FacturasProveedor(id, "CER", inicio, fin);
			var res = Ok(facturas);
			if (res.Value == null) { return NoContent(); }
			return res;
		}

		[HttpGet]
		[Route("notascredito/proveedor")]
		public async Task<ActionResult> NotasCreditoProveedor(string id, DateTime inicio, DateTime fin)
		{
			var notas = await documentosService.NotasCreditoProveedor(id,"PEN", inicio, fin);
			var res= Ok(notas);
			if (res.Value == null) { return NoContent(); }
			return res;
		}

		[HttpGet]
		[Route("notascredito/proveedor/cancelados")]
		public async Task<ActionResult> NotasCreditoCanProveedor(string id, DateTime inicio, DateTime fin)
		{
			var notas = await documentosService.NotasCreditoProveedor(id, "CAN", inicio, fin);
			var res = Ok(notas);
			if (res.Value == null) { return NoContent(); }
			return res;
		}

		[HttpGet]
		[Route("notascredito/proveedor/finalizados")]
		public async Task<ActionResult> NotasCreditoFinProveedor(string id, DateTime inicio, DateTime fin)
		{
			var notas = await documentosService.NotasCreditoProveedor(id, "CER", inicio, fin);
			var res = Ok(notas);
			if (res.Value == null) { return NoContent(); }
			return res;
		}
		[HttpGet]
		[Route("pedidos")]
		public async Task<ActionResult> PedidosCliente(string id, DateTime inicio, DateTime fin)
		{
			var query =await documentosService.PedidosCliente(id,"PEN",inicio,fin);
			var res = Ok(query);
			if (res.Value == null) { return NoContent(); }
			return res;
		}
		[HttpGet]
		[Route("pedidos/finalizados")]
		public async Task<ActionResult> PedidosFinalizados(string id, DateTime inicio, DateTime fin)
		{
			var query = await documentosService.PedidosCliente(id, "CER", inicio, fin);
			var res = Ok(query);
			if (res.Value == null) { return NoContent(); }
			return res;
		}
		[HttpGet]
		[Route("pedidos/cancelados")]
		public async Task<ActionResult> PedidosCancelados(string id, DateTime inicio, DateTime fin)
		{
			var query = await documentosService.PedidosCliente(id, "CAN", inicio, fin);
			var res = Ok(query);
			if (res.Value == null) { return NoContent(); }
			return res;
		}
		[HttpGet]
		[Route("compras")]
		public async Task<ActionResult> Compras(string id, DateTime inicio, DateTime fin)
		{
			var query = await documentosService.ComprasProveedor(id, "PEN", inicio, fin);
			var res = Ok(query);
			if (res.Value == null) { return NoContent(); }
			return res;
		}
		[HttpGet]
		[Route("compras/finalizados")]
		public async Task<ActionResult> ComprasFinalizados(string id, DateTime inicio, DateTime fin)
		{
			var query = await documentosService.ComprasProveedor(id, "CER", inicio, fin);
			var res = Ok(query);
			if (res.Value == null) { return NoContent(); }
			return res;
		}
		[HttpGet]
		[Route("compras/cancelados")]
		public async Task<ActionResult> ComprasCancelados(string id, DateTime inicio, DateTime fin)
		{
			var query = await documentosService.ComprasProveedor(id, "CAN", inicio, fin);
			var res = Ok(query);
			if (res.Value == null) { return NoContent(); }
			return res;
		}
		[HttpGet]
		[Route("estadocuenta/cliente")]
		public async Task<ActionResult> EstadoCuentaCliente(string id, DateTime inicio, DateTime fin)
		{
			var query = await documentosService.EdoCuentaCliente(id, inicio, fin);
			var res = Ok(query);
			if (res.Value == null) { return NoContent(); }
			return res;
		}
		[HttpGet]
		[Route("estadocuenta/proveedor")]
		public async Task<ActionResult> EstadoCuentaproveedor(string id, DateTime inicio, DateTime fin)
		{
			var query = await documentosService.EdoCuentaProveedor(id, inicio, fin);
			var res = Ok(query);
			if (res.Value == null) { return NoContent(); }
			return res;
		}
		[HttpGet]
		[Route("estadocuenta/facturas/cliente")]
		public async Task<ActionResult> FacturasPenCliente(string id, DateTime inicio, DateTime fin)
		{
			var query = await documentosService.CuentasaPagarCliente(id, inicio, fin);
			var res = Ok(query);
			if (res.Value == null) { return NoContent(); }
			return res;
		}
		[HttpGet]
		[Route("estadocuenta/facturas/proveedor")]
		public async Task<ActionResult> FacturasPenProveedor(string id, DateTime inicio, DateTime fin)
		{
			var query = await documentosService.CuentasaCobrarProveedor(id, inicio, fin);
			var res = Ok(query);
			if (res.Value == null) { return NoContent(); }
			return res;
		}
	}
}
