using System.Data.SqlClient;
using WebApiYINSA.Database;
using WebApiYINSA.Models;

namespace WebApiYINSA.Services
{
	public interface ISociosService
	{

		Task<List<Socio>> GetClientes();
		Task<List<Socio>> GetProveedores();
		Task<Socio> GetSocio(string id);
	}
	public class SociosService: ISociosService
	{
		private readonly IDBConnection connection;

		public SociosService(IDBConnection connection) {
			this.connection = connection;
		}

		public async Task<List<Socio>> GetClientes()
		{
			List<SqlParameter> parametros = new()
			{
				 new SqlParameter("@tipo", "C"),

			};
			string resp = await connection.QueryParameterSP("YINSA_PRUEBA.dbo.ObtenerSociosTipo", parametros);

			var lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Socio>>(resp);

			return lst;
		}
		public async Task<List<Socio>> GetProveedores()
		{
			List<SqlParameter> parametros = new()
			{
				 new SqlParameter("@tipo", "S"),

			};
			string resp = await connection.QueryParameterSP("YINSA_PRUEBA.dbo.ObtenerSociosTipo", parametros);

			var lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Socio>>(resp);

			return lst;

		}
		public async Task<Socio> GetSocio(string id)
		{
			List<SqlParameter> parametros = new()
			{
				 new SqlParameter("@id", id)
			};
			string resp = await connection.QueryParameterSP("YINSA_PRUEBA.dbo.ObtenerSocioId", parametros);

			var socio = Newtonsoft.Json.JsonConvert.DeserializeObject<Socio>(resp);
			return socio;
		}

		
	}
}
