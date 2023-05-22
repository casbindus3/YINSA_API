using System.Data.SqlClient;
using WebApiYINSA.Database;
using WebApiYINSA.Models;

namespace WebApiYINSA.Services
{
	public interface IDocumentosService
	{
		Task<List<Documento>> ComprasProveedor(string id, string estat, DateTime inicio, DateTime fin);
		Task<List<Documento>> PedidosCliente(string id, string estat, DateTime inicio, DateTime fin);
		Task<List<Documento>> FacturasCliente(string id,string estatus ,DateTime inicio, DateTime fin);
		Task<List<Documento>> FacturasProveedor(string id, string estatus, DateTime inicio, DateTime fin);
		Task<List<Documento>> NotasCreditoCliente(string id, string estatus, DateTime inicio, DateTime fin);
		Task<List<Documento>> NotasCreditoProveedor(string id, string estatus, DateTime inicio, DateTime fin);
		Task<List<Documento>> EdoCuentaCliente(string id, DateTime inicio, DateTime fin);
		Task<List<Documento>> EdoCuentaProveedor(string id, DateTime inicio, DateTime fin);
		Task<List<Documento>> CuentasaCobrarProveedor(string id, DateTime inicio, DateTime fin);
		Task<List<Documento>> CuentasaPagarCliente(string id, DateTime inicio, DateTime fin);
	}
	public class DocumentosService: IDocumentosService
	{
		private readonly IDBConnection connection;

		public DocumentosService(IDBConnection connection) {
			this.connection = connection;
		}

		public async Task<List<Documento>> FacturasCliente(string id, string estatus,DateTime inicio, DateTime fin)
		{
			List<SqlParameter> parametros = new() 
			{
				 new SqlParameter("@idUsuario", id),
				 new SqlParameter("@estat", estatus),
				 new SqlParameter("@fechaInicio ", inicio),
				 new SqlParameter("@fechaFin", fin),
				 new SqlParameter("@tipous", "C"),
				 new SqlParameter("tipodoc", "FACT"),
			};
			//string resp = await connection.QueryParameterSP("YINSA_PRUEBA.dbo.FacturasPorUsuario", parametros);
			string resp = await connection.QueryParameterSP("YINSA_PRUEBA.dbo.DocumentosUsuario", parametros);
			 var lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Documento>>(resp);

			return lst;
		}
		public async Task<List<Documento>> FacturasProveedor(string id, string estatus, DateTime inicio, DateTime fin)
		{
			List<SqlParameter> parametros = new()
			{
				 new SqlParameter("@idUsuario", id),
				 new SqlParameter("@estat", estatus),
				 new SqlParameter("@fechaInicio ", inicio),
				 new SqlParameter("@fechaFin", fin),
				 new SqlParameter("@tipous", "P"),
				 new SqlParameter("tipodoc", "FACT"),
			};
			//string resp = await connection.QueryParameterSP("YINSA_PRUEBA.dbo.FacturasPorUsuario", parametros);
			string resp = await connection.QueryParameterSP("YINSA_PRUEBA.dbo.DocumentosUsuario", parametros);
			var lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Documento>>(resp);

			return lst;
		}
		public async Task<List<Documento>> NotasCreditoCliente(string id, string estatus, DateTime inicio, DateTime fin)
		{
			List<SqlParameter> parametros = new()
			{
				 new SqlParameter("@idUsuario", id),
				 new SqlParameter("@estat", estatus),
				 new SqlParameter("@fechaInicio ", inicio),
				 new SqlParameter("@fechaFin", fin),
				 new SqlParameter("@tipous", "C"),
				 new SqlParameter("tipodoc", "NTCR"),
			};
			//string resp = await connection.QueryParameterSP("YINSA_PRUEBA.dbo.FacturasPorUsuario", parametros);
			string resp = await connection.QueryParameterSP("YINSA_PRUEBA.dbo.DocumentosUsuario", parametros);
			var lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Documento>>(resp);

			return lst;
		}
		public async Task<List<Documento>> NotasCreditoProveedor(string id, string estatus, DateTime inicio, DateTime fin)
		{
			List<SqlParameter> parametros = new()
			{
				 new SqlParameter("@idUsuario", id),
				 new SqlParameter("@estat", estatus),
				 new SqlParameter("@fechaInicio ", inicio),
				 new SqlParameter("@fechaFin", fin),
				 new SqlParameter("@tipous", "P"),
				 new SqlParameter("tipodoc", "NTCR"),
			};
			//string resp = await connection.QueryParameterSP("YINSA_PRUEBA.dbo.FacturasPorUsuario", parametros);
			string resp = await connection.QueryParameterSP("YINSA_PRUEBA.dbo.DocumentosUsuario", parametros);
			var lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Documento>>(resp);

			return lst;
		}
		public async Task<List<Documento>> PedidosCliente(string id,string estat,DateTime inicio, DateTime fin)
		{
			List<SqlParameter> parametros = new()
			{
				 new SqlParameter("@idSocio", id),
				 new SqlParameter("@estat", estat),
				 new SqlParameter("@inicio ", inicio),
				 new SqlParameter("@fin", fin)
			};
			string resp = await connection.QueryParameterSP("YINSA_PRUEBA.dbo.PedidosCliente", parametros);

			var lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Documento>>(resp);

			return lst;
		}
		public async Task<List<Documento>> ComprasProveedor(string id, string estat, DateTime inicio, DateTime fin)
		{
			List<SqlParameter> parametros = new()
			{
				 new SqlParameter("@id", id),
				 new SqlParameter("@estat", estat),
				 new SqlParameter("@inicio ", inicio),
				 new SqlParameter("@fin", fin)
			};
			string resp = await connection.QueryParameterSP("YINSA_PRUEBA.dbo.ComprasProveedor", parametros);

			var lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Documento>>(resp);

			return lst;
		}

		public async Task<List<Documento>> EdoCuentaCliente(string id,DateTime inicio, DateTime fin)
		{
			List<SqlParameter> parametros = new()
			{
				 new SqlParameter("@id", id),
				 new SqlParameter("inicio ", inicio),
				 new SqlParameter("@fin", fin),
				 new SqlParameter("socio", 'C'),
				 new SqlParameter("reporte","HIS")
			};
			string resp = await connection.QueryParameterSP("YINSA_PRUEBA.dbo.EstadoDeCuenta", parametros);

			var lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Documento>>(resp);

			return lst;
		}
		public async Task<List<Documento>> EdoCuentaProveedor(string id, DateTime inicio, DateTime fin)
		{
			List<SqlParameter> parametros = new()
			{
				 new SqlParameter("@id", id),
				 new SqlParameter("inicio ", inicio),
				 new SqlParameter("@fin", fin),
				 new SqlParameter("socio", 'S'),
				 new SqlParameter("reporte","HIS")
			};
			string resp = await connection.QueryParameterSP("YINSA_PRUEBA.dbo.EstadoDeCuenta", parametros);

			var lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Documento>>(resp);

			return lst;
		}
		public async Task<List<Documento>> CuentasaPagarCliente(string id, DateTime inicio, DateTime fin)
		{
			List<SqlParameter> parametros = new()
			{
				 new SqlParameter("@id", id),
				 new SqlParameter("inicio ", inicio),
				 new SqlParameter("@fin", fin),
				 new SqlParameter("socio", 'C'),
				 new SqlParameter("reporte","EDO")
			};
			string resp = await connection.QueryParameterSP("YINSA_PRUEBA.dbo.EstadoDeCuenta", parametros);

			var lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Documento>>(resp);

			return lst;
		}
		public async Task<List<Documento>> CuentasaCobrarProveedor(string id, DateTime inicio, DateTime fin)
		{
			List<SqlParameter> parametros = new()
			{
				 new SqlParameter("@id", id),
				 new SqlParameter("inicio ", inicio),
				 new SqlParameter("@fin", fin),
				 new SqlParameter("socio", 'S'),
				 new SqlParameter("reporte","EDO")

			};
			string resp = await connection.QueryParameterSP("YINSA_PRUEBA.dbo.EstadoDeCuenta", parametros);

			var lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Documento>>(resp);

			return lst;
		}
	}
}
