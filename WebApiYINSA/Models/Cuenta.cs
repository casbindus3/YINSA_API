namespace WebApiYINSA.Models
{
	public class Cuenta
	{
		public string CodigoS { get; set; }
		public string NombreS { get; set; }
		public string Tipo { get; set; }
	}
	public class CuentaAsignacion
	{	public int UserId { get; set; }
		public List<Cuenta> Cuentas { get; set; }

	}
}

