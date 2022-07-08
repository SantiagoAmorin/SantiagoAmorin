using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Dominio.SeguimientoVacunas
{
	[Table("Laboratorios")]
	public class Laboratorio
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int Id { get; set; }
		[Required]
		public bool experiencia { get; set; }

		[Index(IsUnique = true)]
		[StringLength(450)]
		public string nombre { get; set; }
		[Required]
		public string pais { get; set; }
		[JsonIgnore]
		public virtual ICollection<Vacuna> vacuna { get; set; }

		public bool validar()
        {
			return this.Id != 0
				&& this.experiencia != null
				&& this.nombre != ""
				&& this.pais != "";
        }

	}

}

