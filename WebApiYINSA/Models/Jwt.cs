using System.Security.Claims;
using WebApiYINSA.Services;

namespace WebApiYINSA.Models
{
	public class Jwt
	{
		public string Key { get; set; }
		public string Issuer{ get; set; }
		public string Audience { get; set; }
		public string Subject { get; set; }	

	}
}
