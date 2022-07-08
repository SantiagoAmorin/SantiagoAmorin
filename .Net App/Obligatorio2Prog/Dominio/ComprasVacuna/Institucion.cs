using Dominio.InterfacesRepositorios;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;


namespace Dominio.SeguimientoVacunas
{
	[Table("Instituciones")]
	public class Institucion
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int Id { get; set; }

		public string nombre { get; set; }

		public string telefono { get; set; }

		public string contacto { get; set; }

		public int cantCompras { get; set; }

		public int cantAfili { get; set; }

		public int montoMax { get; set; }

		public bool validar()
		{
			return this.Id.ToString().Length <= 6
				&& !this.Id.ToString()[0].Equals(0.ToString());
		}

	}

}

