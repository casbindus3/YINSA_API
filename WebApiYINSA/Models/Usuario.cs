using System.ComponentModel.DataAnnotations;

namespace WebApiYINSA.Models
{
	public class Usuario
	{
		public int UserId { get; set; }
		public string UserName { get; set; }
		//[EmailAddress(ErrorMessage ="Debe ingresar un correo válido")]
		public string Email { get; set; }
		public DateTime Creacion { get; set; }
		public string Estatus { get; set; }
		public string Password { get; set; }
		public int RolId { get; set; }
		public string Rol { get; set; }
		//public string Salt { get; set; }
		public List<Cuenta> Cuentas { get; set; }
	}

	public class CredencialesUsuario
	{
		[Required]
		public string UserName { get; set; }

		[Required]
		[EmailAddress(ErrorMessage = "Debe ingresar un correo válido")]
		public string Email { get; set; }
		[Required]
		//[MaxLength(40, ErrorMessage = "La contraseña no debe exceder 40 caracteres")]
		public string Password { get; set; }
		[Required]
		public int RolId { get; set; }

	}
	public class LoginUser
	{
		[Required]
		//[EmailAddress]
		//public string Email { get; set; }
		public string UserName { get; set; }

		[Required]
		public string Password { get; set; }
	}
	public class DBUserResponse
	{
		public Usuario Usuario { get; set; }
		public bool Status { get; set; }
		public string StatusMessage { get; set; }
	}
	//public class UsuarioResponse
	//{
	//	public Usuario Usuario { get; set; }
	//	public List<Cuenta> Cuentas { get; set; }
	//}
}



