using System.Data;
using System.Data.SqlClient;

namespace WebApiYINSA.Database
{
	public interface IDBConnection
	{
		void Actualizar(string query);
		void Conectar();
		Task<SqlDataReader> Consultar(string query);
		Task<string> ConsultarSP(string query);
		Task<string> QueryParameterSP(string query, List<SqlParameter> parametros);
		
	}
	public class DBConnection : IDBConnection
	{
		private readonly string connectionString;
		public readonly SqlConnection _connection;
        public DBConnection(IConfiguration configuration)
        {
			connectionString = configuration.GetConnectionString("DefaultConnection");
			_connection = new SqlConnection();
		}
		public void Conectar()
		{
			_connection.ConnectionString = connectionString;
			_connection.Open();
		}
		public async Task<SqlDataReader> Consultar(string query)
		{
			Conectar();
			var cmd = new SqlCommand(query, _connection);
			var reader = await cmd.ExecuteReaderAsync();
			_connection.Close();
			return reader;
		}
		public async Task<string> ConsultarSP(string query)
		{
			Conectar();
			var cmd = new SqlCommand(query, _connection)
			{
				CommandType = CommandType.StoredProcedure,
				CommandTimeout= 0
			};
			var reader = await cmd.ExecuteReaderAsync();
			string resp = "";
			while (reader.Read())
			{
				resp += reader[0] + "";
			}
			_connection.Close();

			return resp;
		}
		public async Task<string> QueryParameterSP(string query, List<SqlParameter> parametros)
		{
			Conectar();
			var cmd = new SqlCommand(query, _connection)
			{
				CommandType = CommandType.StoredProcedure,
				CommandTimeout = 0
			};
			cmd.Parameters.AddRange(parametros.ToArray());

			var reader = await cmd.ExecuteReaderAsync();
			string resp = "";
			while (reader.Read())
			{
				resp += reader[0] + "";
			}
			_connection.Close();

			return resp;
		}
		public void Actualizar(string query)
		{
			Conectar();
			var cmd = new SqlCommand(query, _connection);
			cmd.ExecuteNonQueryAsync();
			_connection.Close();
		}
	}
}
