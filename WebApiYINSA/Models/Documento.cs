namespace WebApiYINSA.Models
{
	public class Documento
	{
		public string TipoKey { get; set; }
		public string Tipo { get; set; }
		public int DocEntry { get; set; }
		public int NoDocumento { get; set; }
		public int? NoSerie { get; set; }
		public string TipoDocumento { get; set; }
		public string Cancelado { get; set; }
		public DateTime Fecha { get; set; }
		public DateTime FechaVencimiento { get; set; }
		public string CodigoS { get; set; }
		public string NombreS { get; set; }
		public string RFC { get; set; }
		public string Referencia { get; set; }
		public string Moneda { get; set; }
		public string UUID { get; set; }
		public int NoLinea { get; set; }
		public string EstatusLinea { get; set; }
		public string ProductoKey { get; set; }
		public string Producto { get; set; }
		public DateTime FechaEntrega { get; set; }
		public float Cantidad { get; set; }
		public float Precio { get; set; }
		public string UnidadMedida { get; set; }
		public float Importe { get; set; }
		public float Descuento { get; set; }
		public string AsesorKey { get; set; }
		public float TasaImpuesto { get; set; }
		public float ImporteImpto { get; set; }
		public string DireccionEntrega { get; set; }
		public float Total { get; set; }
		public float Cargo { get; set; }
		public float Abono { get; set; }
		public float Saldo { get; set; }
		public string Antiguedad { get; set; }
		public string EstatusCte { get; set; }
	}

}
