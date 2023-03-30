using System.Data.SqlClient;
using WebApiYINSA.Database;
using WebApiYINSA.Models;

namespace WebApiYINSA.Services
{
	public interface IDocumentosService
	{
		Task<List<Documento>> FacturasGeneral(DateTime inicio, DateTime fin);
		Task<List<Documento>> FacturasPorUsuario(string id, DateTime inicio, DateTime fin);
		Task<List<Documento>> NotasCreditoGeneral(DateTime inicio, DateTime fin);
		Task<List<Documento>> NotasCreditoPorUsuario(string id, DateTime inicio, DateTime fin);
		Task<List<Documento>> PedidosCanceladosCliente(string id);
		Task<List<Documento>> PedidosCliente(string id);
		Task<List<Documento>> PedidosFinalizadosCliente(string id);
	}
	public class DocumentosService: IDocumentosService
	{
		private readonly IDBConnection connection;

		public DocumentosService(IDBConnection connection) {
			this.connection = connection;
		}
		public async Task<List<Documento>> FacturasGeneral(DateTime inicio, DateTime fin)
		{
			List<SqlParameter> parametros = new()
			{
				 new SqlParameter("@fechaInicio ", inicio),
				 new SqlParameter("@fechaFin", fin)
			};
			string resp = await connection.QueryParameterSP("YINSA_PRUEBA.dbo.FacturasGeneral", parametros);

			var lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Documento>>(resp);

			return lst;
		}
		public async Task<List<Documento>> NotasCreditoGeneral(DateTime inicio, DateTime fin)
		{
			List<SqlParameter> parametros = new()
			{
				 new SqlParameter("@fechaInicio ", inicio),
				 new SqlParameter("@fechaFin", fin)
			};
			string resp = await connection.QueryParameterSP("YINSA_PRUEBA.dbo.NotasCreditoGeneral", parametros);

			var lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Documento>>(resp);

			return lst;
		}
		public async Task<List<Documento>> FacturasPorUsuario(string id,DateTime inicio, DateTime fin)
		{
			List<SqlParameter> parametros = new() 
			{
				 new SqlParameter("@idUsuario", id),
				 new SqlParameter("@fechaInicio ", inicio),
				 new SqlParameter("@fechaFin", fin)
			};
			string resp = await connection.QueryParameterSP("YINSA_PRUEBA.dbo.FacturasPorUsuario", parametros);

			var lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Documento>>(resp);

			return lst;
		}

		public async Task<List<Documento>> NotasCreditoPorUsuario(string id, DateTime inicio, DateTime fin)
		{
			List<SqlParameter> parametros = new()
			{
				 new SqlParameter("@idUsuario", id),
				 new SqlParameter("@fechaInicio ", inicio),
				 new SqlParameter("@fechaFin", fin)
			};
			string resp = await connection.QueryParameterSP("YINSA_PRUEBA.dbo.NotasCreditoUsuario", parametros);

			var lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Documento>>(resp);

			return lst;
		}

		//modificar, generalizar
		public async Task<List<Documento>> PedidosCliente(string id)
		{
			List<SqlParameter> parametros = new()
			{
				 new SqlParameter("@idUsuario", id),

			};
			string resp = await connection.QueryParameterSP("YINSA_PRUEBA.dbo.PedidosCliente", parametros);

			var lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Documento>>(resp);

			return lst;
		}
		public async Task<List<Documento>> PedidosFinalizadosCliente(string id)
		{
			List<SqlParameter> parametros = new()
			{
				 new SqlParameter("@idUsuario", id),
			};
			string resp = await connection.QueryParameterSP("YINSA_PRUEBA.dbo.PedidosFinalizadosCliente", parametros);

			var lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Documento>>(resp);

			return lst;
		}
		public async Task<List<Documento>> PedidosCanceladosCliente(string id)
		{
			List<SqlParameter> parametros = new()
			{
				 new SqlParameter("@idUsuario", id)
			};
			string resp = await connection.QueryParameterSP("YINSA_PRUEBA.dbo.PedidosCanceladosCliente", parametros);

			var lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Documento>>(resp);

			return lst;
		}
	}
}
