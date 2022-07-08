using DataAccess.Repositorio;
using Dominio.SeguimientoVacunas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Obligatorio2Prog.Models
{
    public class DetalleCompra
    {

		public DateTime fecha { get; set; }

		public int cantidadVac { get; set; }

		public decimal precioVac { get; set; }

		public decimal montoTotal { get; set; }

		public DetalleVacuna vacuna { get; set; }

		public DetalleInstitucion institucion { get; set; }		
        
        public SelectList todosInstituciones { get; set; }

        public bool validar()
        {
            RepositorioCompra repoC = new RepositorioCompra();
            List<Compra> compras = repoC.FindAll().ToList();
            decimal montoGastado = 0;
            int comprasDisp = this.institucion.cantCompras;
            string mesActual = DateTime.Now.Month.ToString();
            foreach (Compra c in compras)
            {
                if (c.fecha.Month.ToString() == mesActual && c.idInstitucion.ToString().Equals(this.institucion.idInst))
                {
                    montoGastado += c.montoTotal;
                    comprasDisp--;
                }
            }
            if ((this.institucion.montoMax * this.institucion.cantAfili) - montoGastado >= this.montoTotal && comprasDisp > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public decimal calcularMonto()
        {
            return this.precioVac * this.cantidadVac;
        }

    }
}