using System;

namespace WebApiYINSA.Models
{
	public class ProductoModel
	{
		public string ProductoKey { get; set; }
		public string Producto { get; set; }
		public int? GrupoKey { get; set; }
		public string UnidadMedida { get; set; }
		public string Grupo { get; set; }
		public int? LineaKey { get; set; }
		public string Linea { get; set; }
		public int? SubLineaKey { get; set; }
		public string SubLinea { get; set; }
		public int? ClaseKey { get; set; }
		public string Clase { get; set; }
		public string PlantaKey { get; set; }
		public string Planta {get;set;} 
	}

}
