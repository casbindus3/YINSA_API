namespace WebApiYINSA.Models
{
	public class AuthResponse
	{
		public string Token { get; set; }
		public DateTime Expiracion { get; set; }
		public bool Status { get; set; }
		public string StatusMessage { get; set; }
		public Usuario Usuario { get; set; }
	}
}
