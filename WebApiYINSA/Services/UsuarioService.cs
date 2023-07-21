using System.Data.SqlClient;
using System.Numerics;
using System.Security.Claims;
using WebApiYINSA.Database;
using WebApiYINSA.Models;

namespace WebApiYINSA.Services
{
	public interface IUsuarioService
	{
		Task<int> ActualizarUsuario(Usuario user);
		Task<int> AsignarCuentas(CuentaAsignacion cuentas);
		Task<List<Cuenta>> BuscarCuentas(string clave);
		Task<DBUserResponse> Registro(CredencialesUsuario usuario);
		Task<Usuario> UserById(int id);
		Task<Usuario> UserByName(string username);
		Task<List<Usuario>> Usuarios();
		Task<DBUserResponse> Validacion(LoginUser usuario);
		Task<AuthResponse> ValidarToken(ClaimsIdentity identity);
	}
	public class UsuarioService:IUsuarioService
	{
		private readonly IDBConnection connection;

		public UsuarioService(IDBConnection connection)
		{
			this.connection = connection;
		}

		public async Task<DBUserResponse> Registro(CredencialesUsuario usuario)
		{
			List<SqlParameter> parametros = new()
			{    new SqlParameter("@username", usuario.UserName),
				 new SqlParameter("@email", usuario.Email),
				 new SqlParameter("@password", usuario.Password),
				 new SqlParameter("@rolId", usuario.RolId)
				// new SqlParameter("@salt", salt)
			};
			string resp = await connection.QueryParameterSP("dbo.SP_UsuarioRegistro", parametros);

			var user = Newtonsoft.Json.JsonConvert.DeserializeObject<DBUserResponse>(resp);

			return user;
		}
		public async Task<List<Cuenta>> BuscarCuentas(string clave)
		{
			List<SqlParameter> parametros = new()
			{    new SqlParameter("@string", clave)

			};
			string resp = await connection.QueryParameterSP("dbo.BuscarCuentas", parametros);

			var cuentas = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cuenta>>(resp);

			return cuentas;
		}
		public async Task<int> AsignarCuentas(CuentaAsignacion cuentas)
		{
			var json = Newtonsoft.Json.JsonConvert.SerializeObject(cuentas);
				List<SqlParameter> parametros = new()
					{    new SqlParameter("@json", json)

					};
			var resp = await connection.Modificar("dbo.SP_AsignarCuentas", parametros);

			return resp;	//no funciona 
			//string query = "INSERT INTO YINSA_PRUEBA.dbo.CuentasUsuarios (UserId, CardCode) VALUES ";
			//string values = string.Join(",", cuentas.Cuentas.Select(cuenta =>
			//	$"({cuentas.UserId}, '{cuenta.CodigoS}')"));
			//query += values;

			//return await connection.Modificacion(query);
		}

		public async Task<DBUserResponse> Validacion(LoginUser usuario)
		{
			List<SqlParameter> parametros = new()
			{
				 new SqlParameter("@username", usuario.UserName),
				 new SqlParameter("@password", usuario.Password)
			};
			string resp = await connection.QueryParameterSP("dbo.SP_UsuarioValidacion", parametros);

			var user = Newtonsoft.Json.JsonConvert.DeserializeObject<DBUserResponse>(resp);

			return user;
		}
		public async Task<Usuario> UserById(int id)
		{
			List<SqlParameter> parametros = new()
			{
				 new SqlParameter("@id", id),
			};
			string resp = await connection.QueryParameterSP("dbo.SP_UsuarioPorId", parametros);

			var user = Newtonsoft.Json.JsonConvert.DeserializeObject<Usuario>(resp);

			return user;
		}
		
		public async Task<Usuario> UserByName(string username)
		{
			List<SqlParameter> parametros = new()
			{
				 new SqlParameter("@username", username)
			};
			string resp = await connection.QueryParameterSP("dbo.SP_Usuario", parametros);

			var user = Newtonsoft.Json.JsonConvert.DeserializeObject<Usuario>(resp);

			if (user == null)
			{
				return new Usuario();
			}

			return user;
		}
		public async Task<AuthResponse> ValidarToken(ClaimsIdentity identity)
		{
			try
			{

				if (!identity.Claims.Any())
				{
					return new AuthResponse
					{
						Status = false,
						StatusMessage = "Verificar si se está enviando un token válido",
					};
				}

				var id = Convert.ToInt32(identity.Claims.FirstOrDefault(x => x.Type == "id").Value);

				var usuario = await UserById(id);

				return new AuthResponse
				{
					Status = true,
					StatusMessage = "Token válido",
					Usuario = usuario

				};

			}
			catch (Exception ex)
			{
				return new AuthResponse
				{
					Status = false,
					StatusMessage = "Catch: " + ex.Message,
				};
			}
		}
		public async Task<List<Usuario>> Usuarios()
		{
			
			string resp = await connection.ConsultarSP("dbo.SP_Usuarios"); 


			if (string.IsNullOrEmpty(resp)) { return new List<Usuario>(); }

			var users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Usuario>>(resp);



			return users;
		}

		public async Task<int> ActualizarUsuario(Usuario user)
		{
			var json = Newtonsoft.Json.JsonConvert.SerializeObject(user);
			List<SqlParameter> parametros = new()
					{    new SqlParameter("@json", json)

					};
			var resp = await connection.Modificar("dbo.SP_ActualizarUsuario", parametros);

			return resp;   
		}
	}
}
