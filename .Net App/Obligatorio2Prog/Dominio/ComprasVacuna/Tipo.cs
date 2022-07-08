using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Dominio.SeguimientoVacunas
{
	[Table("Tipos")]
	public class Tipo
	{
		[Key]
		public string codigoId { get; set; }
		[Required]
		public string descripcion { get; set; }
		[Required]
		public string nombre { get; set; }

		public bool validar()
		{
			return this.codigoId!=null
				&& this.descripcion!=null
				&& this.nombre!=null;
		}

	}

}

