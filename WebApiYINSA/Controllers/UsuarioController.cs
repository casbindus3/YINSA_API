using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApiYINSA.Models;
using WebApiYINSA.Services;

namespace WebApiYINSA.Controllers
{
	//[Authorize]
	[ApiController]
	[Route("/api/usuario")]
	public class UsuarioController : ControllerBase
	{
		private readonly IConfiguration _configuration;
		private readonly IUsuarioService usuarioService;
		//private readonly HashService hashService;

		public UsuarioController(IConfiguration configuration, IUsuarioService usuarioService)
		{
			_configuration = configuration;
			this.usuarioService = usuarioService;
	
		}
		
		[HttpGet]
		[Route("nombre")]
		public async Task<ActionResult> UserByName(string username)
		{
			var res = await usuarioService.UserByName(username);

			return Ok(res);
		}
		[HttpGet]
		[Route("id")]
		public async Task<ActionResult> UserById(int id)
		{
			var res = await usuarioService.UserById(id);

			return Ok(res);
		}
		[HttpGet]
		[Route("usuarios")]
		public async Task<ActionResult> Usuarios()
		{
			var res = await usuarioService.Usuarios();

			return Ok(res);
		}

		[HttpPost]
		[Route("registro")]
		[AllowAnonymous]
		//[Authorize(Policy = "Admin")] //La api no encripta la contraseña, la recibe encriptada
		public async Task<ActionResult<AuthResponse>> Registrar(CredencialesUsuario modelo)
		{
			//var passwordHash = hashService.Hash(modelo.Password);
			//string salt = Convert.ToBase64String(passwordHash.Sal);

			//var registro = await usuarioService.Registro(new CredencialesUsuario
			//{	UserName = modelo.UserName,
			//	Email = modelo.Email,
			//	RolId = modelo.RolId,
			//	Password = passwordHash.Hash
			//}, salt);

			//validar el modelo de alguna forma
			var registro = await usuarioService.Registro(modelo);


			if (registro.Status != false)
			{
				return Ok(new AuthResponse()
				{
					Status = true,
					StatusMessage = "Usuario registrado con éxito.",
					Usuario = registro.Usuario
				});
				//no se necesita regresar token, pues no se mantendrá loggeado
				//con el usuario registrado
				//return GenerarToken(new CredencialesUsuario
				//{
				//	UserName = registro.Usuario.UserName,
				//	RolId = registro.Usuario.RolId,
				//});
			}
			else
			{
				return BadRequest(new AuthResponse { Status = registro.Status, StatusMessage = registro.StatusMessage });
			}

		}
		
		[HttpPost("login")]
		[AllowAnonymous]
		public async Task<ActionResult<AuthResponse>> Login(LoginUser credenciales)
		{
			//var resultado = await signInManager.PasswordSignInAsync(credencialesUsuario.Email,
			//	credencialesUsuario.Password, isPersistent: false, lockoutOnFailure: false);
			//var usuario = new Usuario() { Email = credencialesUsuario.Email, Password = credencialesUsuario.Password };
			//var passwordHash = userManager.PasswordHasher.HashPassword(usuario, password: credencialesUsuario.Password);

			//var resultado = await usuarioService.Validacion(new LoginUser { Email = credencialesUsuario.Email, Password = passwordHash });
			//if (resultado.Status)

			//var usuario = await usuarioService.UserByName(credenciales.UserName);
			//byte[] saltbytes = Convert.FromBase64String(usuario.Salt);


			//QUITAR EL HASH DE AQUI, SOLO SE ENCRIPTARA EN EL SITIO
			//var passwordHash = HashService.ConvertirSha256(credenciales.Password);

			//string salt= Encoding.UTF8.GetString(passwordHash.Sal);
			//var resultado = await usuarioService.Validacion(new LoginUser 
			//{ UserName = credenciales.UserName, Password = credenciales.Password });

			var resultado = await usuarioService.Validacion(credenciales);

			if (resultado.Status)
			{
				//return GenerarToken(new CredencialesUsuario
				//{
				//	UserName = resultado.Usuario.UserName,
				//	RolId= resultado.Usuario.RolId
				//});
				return GenerarToken(resultado.Usuario);
			}
			else
			{
				return BadRequest(new AuthResponse
				{
					Status = false,
					StatusMessage = "Credenciales incorrectas",
				});
			}
		}
		
		[HttpPost]
		[Route("actualizar")]
		public async Task<ActionResult> ActualizarUsuario(Usuario user)
		{
			if (user == null)
			{
				return BadRequest(new { status = 400, message = "La información enviada no se ha insertado." });
			}
			var resp = await usuarioService.ActualizarUsuario(user);

			if (resp == 0)
			{
				return BadRequest(new { status = 400, message = "La información enviada no se ha insertado." });
			}

			return Ok(new { status=200, message= "Actualización exitosa" });

		}

		[HttpGet]
		[Route("cuentas/buscar")]
		public async Task<ActionResult> BuscarCuentas(string cuenta)
		{
			var resultado = await usuarioService.BuscarCuentas(cuenta);

			if (resultado != null)
			{
				return Ok(resultado);
			}
			return BadRequest();
		}
		
		[HttpPost]
		[Route("cuentas/asignar")]
		public async Task<ActionResult> AsignarCuentas(CuentaAsignacion cuentas)
		{
			if (cuentas == null || !ModelState.IsValid)
			{
				return BadRequest("La información enviada es incorrecta o incompleta.");
			}

			var resp = await usuarioService.AsignarCuentas(cuentas);
			
			if (resp == 0)
			{
				return BadRequest("La información enviada no se ha insertado.");
			}

			return Ok("Asignación exitosa");
		}

		[HttpPost]
		[Route("eliminar")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy ="AdminMaster")]
		public async Task<ActionResult> EliminarUsuario(Socio socio)
		{

			var identity = HttpContext.User.Identity as ClaimsIdentity;
			
			var resToken = await usuarioService.ValidarToken(identity);

			if (!resToken.Status) return BadRequest(resToken);

			Usuario usuario = resToken.Usuario;

			if(usuario.Rol != "sa" || usuario.Rol != "admin")
			{
				return BadRequest(new AuthResponse
				{
					Status = false,
					StatusMessage = "No cuenta con los permisos",
				});
			}

			//codigo para eliminar
			return Ok(new
			{
				success = true,
				message = "elimando con éxito",
				result = socio
			});

		}
	
		private AuthResponse GenerarToken(Usuario usuario)
		{  //(CredencialesUsuario usuario)
			var jwt = _configuration.GetSection("Jwt").Get<Jwt>();

			var claims = new List<Claim>()
			{   new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
			    new Claim("id", usuario.UserId.ToString()),//pendiente, quizás lo quitamos
				new Claim("rol", usuario.RolId.ToString()),
				new Claim("user", usuario.UserName) 
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
			var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var expiracion = DateTime.Now.AddDays(1);
			var token = new JwtSecurityToken(
				jwt.Issuer,
				jwt.Audience,
				claims,
				expires: expiracion,//si no lo quiero no lo pongo
				signingCredentials: signIn
				);
			return new AuthResponse()
			{
				Token = new JwtSecurityTokenHandler().WriteToken(token),
				Expiracion = expiracion,
				Status = true,
				StatusMessage = "",
				Usuario = usuario
			};
		}
	}
}
//private AuthResponse GenerarToken(Usuario usuario)
//{
//	var claims = new List<Claim>()
//	{
//		new Claim("email", usuario.Email)
//		//new Claim("")
//	};

//	var key= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
//	var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
//	var expiracion = DateTime.Now.AddDays(1);
//	var token = new JwtSecurityToken(
//		issuer:null,
//		audience: null,
//		claims,
//		expires:expiracion ,//si no lo quiero no lo pongo
//		signingCredentials: signIn
//		);

//	return new AuthResponse()
//	{
//		Token = new JwtSecurityTokenHandler().WriteToken(token),
//		Expiracion = expiracion,
//		Status = true,
//		StatusMessage = ""
//	};
//}


//[HttpPost]
//[Route("login")]
//public async Task<AuthResponse> Login([FromBody] LoginUser credenciales)
//{
//	var resp = await usuarioService.Validacion(credenciales);

//	if(resp.Usuario == null) {
//		return new AuthResponse
//		{
//			Status = false,
//			StatusMessage = "Credenciales incorrectas",
//		};
//	}
//	else
//	{
//		return GenerarToken(new CredencialesUsuario
//		{
//			Email = resp.Usuario.Email,
//			RolId = resp.Usuario.RolId,
//		});
//	}

//}
//[HttpPost]
//[Route("registro")]
//[AllowAnonymous]
//public async Task<ActionResult<AuthResponse>> Registrar(CredencialesUsuario modelo)
//{

//	var usuario = new Usuario() { Email = modelo.Email, RolId = modelo.RolId };

//	var resultado = await userManager.CreateAsync(usuario, password: modelo.Password);

//	//var usuario = new Usuario() { Email = modelo.Email, RolId = modelo.RolId, Password = modelo.Password };
//	//var passwordHash = userManager.PasswordHasher.HashPassword(usuario, password: modelo.Password);

//	//var resultado = await usuarioService.Registro(new CredencialesUsuario
//	//{
//	//	Email = modelo.Email,
//	//	RolId = modelo.RolId,
//	//	Password = passwordHash
//	//});

//	if (resultado.Succeeded)
//	{				
//		return GenerarToken(modelo);
//	}
//	else
//	{	
//		return BadRequest(new AuthResponse { Status =false, StatusMessage = "No se ha podido realizar el registro." });
//	}
//	//var registro = await usuarioService.Registro(usuario);

//	//if (registro.Status != false)
//	//{

//	//	return GenerarToken(registro.Usuario);
//	//}
//	//else
//	//{
//	//	return BadRequest(new AuthResponse { Status = registro.Status, StatusMessage = registro.StatusMessage });
//	//}

//}