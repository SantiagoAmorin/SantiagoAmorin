using Dominio.SeguimientoVacunas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Dominio.SeguimientoVacunas
{
	[Table("Vacunas")]
	public class Vacuna
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int Id { get; set; }

		public virtual ICollection<Laboratorio> laboratorio { get; set; }
		[Required]
		public int cantDosis { get; set; }
		[Required]
		public int plazoDosis { get; set; }
		[Required]
		public string nombre { get; set; }
		[Required]
		public int faseClinica { get; set; }
		[Required]
		public bool aprovEmerg { get; set; }
		[Required]
		public string efectosAdvers { get; set; }
		[Required]
		public decimal precio { get; set; }
		[Required]
		public bool mecanismoCOVAX { get; set; }

		[ForeignKey("miTipo")]
		public string idTipo { get; set; }
        public virtual Tipo miTipo { get; set; }
		[Required]
		public int tempMin { get; set; }
		[Required]
		public int tempMax { get; set; }
		[Required]
		public string status { get; set; }
		[ForeignKey("Usuario")]
		public string idUsuario { get; set; }
		public virtual Usuario Usuario { get; set; }
		[Required]
		public DateTime fechActua { get; set; }
		[Required]
		public int prevHosp { get; set; }
		[Required]
		public int prevCTI { get; set; }
		[Required]
		public int prevCovid { get; set; }
		[Required]
		public int produAnual { get; set; }
		[Required]
		public int edadMin { get; set; }
		[Required]
		public int edadMax { get; set; }
		public bool validar()
		{
			if (this.prevCovid >=0 && this.prevCovid <=100 
				&& this.prevHosp >= 0 && this.prevHosp<=100 
				&& this.prevCTI >=0 && this.prevCTI<=100 
				&& this.tempMin>=-100 && this.tempMax <=50 && this.tempMax>=this.tempMin 
				&& this.faseClinica<=4 && this.faseClinica >= 1)
            {
				return true;
			}
            else
            {
				return false;
            }
			
		}

		public bool verificarCaract(int caract)
		{
			return false;
		}

		public bool verificarTemp(int tempMin, int tempMax)
		{
			return false;
		}

		public bool actualizarFecha()
		{
			return false;
		}

		public bool verificarFaseClinica(int faseClinica)
		{
			return false;
		}

		public bool agregarLaboratorio(Laboratorio laboratorio)
		{
            if (laboratorio != null)
            {
				this.laboratorio.Add(laboratorio);
				return true;
            }
            else
            {
				return false;
            }
			 
			
		}
		

	}

}

