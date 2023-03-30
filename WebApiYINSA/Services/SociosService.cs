using System.Data.SqlClient;
using WebApiYINSA.Database;
using WebApiYINSA.Models;

namespace WebApiYINSA.Services
{
	public interface ISociosService
	{

		Task<List<Socio>> GetClientes();
		Task<List<Socio>> GetProveedores();
		Task<List<Socio>> GetSocio(string id);
	}
	public class SociosService: ISociosService
	{
		private readonly IDBConnection connection;

		public SociosService(IDBConnection connection) {
			this.connection = connection;
		}

		public async Task<List<Socio>> GetClientes()
		{

			string resp = await connection.ConsultarSP("YINSA_PRUEBA.dbo.ObtenerClientes");

			var lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Socio>>(resp);

			return lst;
		}
		public async Task<List<Socio>> GetProveedores()
		{
			string resp = await connection.ConsultarSP("YINSA_PRUEBA.dbo.ObtenerProveedores");

			var lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Socio>>(resp);

			return lst;

		}
		public async Task<List<Socio>> GetSocio(string id)
		{
			List<SqlParameter> parametros = new()
			{
				 new SqlParameter("@id", id)
			};
			string resp = await connection.QueryParameterSP("YINSA_PRUEBA.dbo.ObtenerSocioId", parametros);

			var socio = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Socio>>(resp);
			return socio;
		}

		
	}
}
