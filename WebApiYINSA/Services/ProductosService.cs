using System.Data.SqlClient;
using System.Net.NetworkInformation;
using WebApiYINSA.Database;
using WebApiYINSA.Models;

namespace WebApiYINSA.Services
{
	public interface IProductosService
	{
		Task<List<ProductoModel>> ObtenerProductos();
		Task<List<ProductoModel>> ProductByCat(int idcat);
	}
	public class ProductosService : IProductosService
	{
		private readonly IDBConnection connection;

		public ProductosService(IConfiguration configuration, IDBConnection conexion)
		{
			connection = conexion;
		}
		public async Task<List<ProductoModel>> ObtenerProductos()
		{
			string resp= await connection.ConsultarSP("YINSA_PRUEBA.dbo.ObtenerProductos");
			
			var lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProductoModel>>(resp);

			return lst;
		}

		//ObtenerProductosPorId
		public async Task<List<ProductoModel>> ProductByCat(int idcat)
		{
			List<SqlParameter> parametros = new()
			{
				 new SqlParameter("@idcategoria",idcat),
			};
			//string resp = await connection.QueryParameterSP("YINSA_PRUEBA.dbo.FacturasPorUsuario", parametros);
			string resp = await connection.QueryParameterSP("YINSA_PRUEBA.dbo.ProductosPorCategoria",parametros);
			var lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProductoModel>>(resp);

			return lst;
		}
	}
}

/*
 public async Task<IEnumerable<ProductoModel>> ObtenerProductos()
		{
			using var connection = new SqlConnection(connectionString);
		    var prod=await connection.QueryAsync<ProductoModel>(
				"YINSA_PRUEBA.dbo.ObtenerProductos"
				,commandType:System.Data.CommandType.StoredProcedure);
			return prod;
		}
 */

/*	public async Task<List<ProductoModel>> JSONProductos()
   {
	   var connection = new SqlConnection(connectionString);
	   connection.Open();
	   SqlCommand cmd = new SqlCommand("YINSA_PRUEBA.dbo.ObtenerProductos", connection)
	   {
		   CommandType = CommandType.StoredProcedure
	   };
	   SqlDataReader reader = await cmd.ExecuteReaderAsync();
	   string query = "";
	   while (reader.Read())
	   {
		   query += reader[0] + "";
	   }
	   reader.Close();
	   connection.Close();

	   var lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProductoModel>>(query);

	   return lst;
   }*/