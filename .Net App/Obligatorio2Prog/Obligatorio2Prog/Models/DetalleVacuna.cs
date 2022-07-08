using Dominio.SeguimientoVacunas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Obligatorio2Prog.Models
{
    public class DetalleVacuna
    {
		public int idVac { get; set; }
		public List<string> laboratorios { get; set; } = new List<string>();

		public int cantDosis { get; set; }

		public int plazoDosis { get; set; }

		public string nombre { get; set; }

		public int faseClinica { get; set; }

		public bool aprovEmerg { get; set; }

		public string efectosAdvers { get; set; }

		public decimal precio { get; set; }

		public bool mecanismoCOVAX { get; set; }

		public string tipo { get; set; }

		public int tempMin { get; set; }

		public int tempMax { get; set; }

		public int edadMin { get; set; }

		public int edadMax { get; set; }

		public string status { get; set; }

		public string ciUsuario { get; set; }

		public int prevHosp { get; set; }

		public int prevCTI { get; set; }

		public int prevCovid { get; set; }

		public DateTime fechActua { get; set; }
		public int produAnual { get; set; }

		
	}
}