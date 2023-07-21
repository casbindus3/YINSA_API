using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;
using WebApiYINSA.Models;

namespace WebApiYINSA.Services
{
	public class HashService
	{
		public ResultadoHash Hash(string textoPlano)
		{
			var sal = new byte[16];
			using (var random = RandomNumberGenerator.Create())
			{
				random.GetBytes(sal);
			}

			return Hash(textoPlano, sal);
		}

		public ResultadoHash Hash(string textoPlano, byte[] sal)
		{
			var llaveDerivada = KeyDerivation.Pbkdf2(password: textoPlano,
				salt: sal, prf: KeyDerivationPrf.HMACSHA1,
				iterationCount: 10000,
				numBytesRequested: 32);

			var hash = Convert.ToBase64String(llaveDerivada);

			return new ResultadoHash()
			{
				Hash = hash,
				Sal = sal
			};
		}

		//SHA256
		public static string ConvertirSha256(string texto)
		{

			StringBuilder Sb = new();
			using (SHA256 hash = SHA256.Create())
			{
				Encoding enc = Encoding.UTF8;
				byte[] result = hash.ComputeHash(enc.GetBytes(texto));

				foreach (byte b in result)
					Sb.Append(b.ToString("x2"));
			}

			return Sb.ToString();
		}
		public static string GeneraSHA256(string str)
		{
			SHA256 sha2 = SHA256.Create();
			ASCIIEncoding encoding = new ASCIIEncoding();
			byte[] stream = null;
			StringBuilder sb = new StringBuilder();
			stream = sha2.ComputeHash(encoding.GetBytes(str));
			for (int i = 0; i < stream.Length; i++)
				sb.AppendFormat("{0:x2}", stream[i]);

			return sb.ToString();
		}
		public static string string2sha256(string s)
		{
			if (string.IsNullOrEmpty(s)) return "#";
			using ( var h = SHA256.Create())
			{
				var hb = h.ComputeHash(Encoding.UTF8.GetBytes(s));
				return string.Concat(Array.ConvertAll(hb, b => b.ToString("X2")));
			}
		}
	}
}
