using Dominio.SeguimientoVacunas;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Dominio.SeguimientoVacunas
{
	[Table("Compras")]
	public class Compra
	{		
		public int Id { get; set; }

		[ForeignKey("insti")]
		public int idInstitucion { get; set; }
		[Required]
		public Institucion insti { get; set; }

		public DateTime fecha { get; set; }

		[ForeignKey("vac")]
		public int idVacuna { get; set; }
		[Required]
		public Vacuna vac { get; set; }

		public int cantidadVac { get; set; }

		public decimal precioVac { get; set; }

		public decimal montoTotal { get; set; }


        public bool validar()
        {
                  
            if ((this.insti.montoMax * this.insti.cantAfili) >= this.montoTotal)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

		public bool agregarInstitucion(Institucion inst)
		{
			return false;
		}

	}

	

}

