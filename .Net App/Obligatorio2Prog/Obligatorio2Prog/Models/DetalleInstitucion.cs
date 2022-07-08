using DataAccess.Repositorio;
using Dominio.SeguimientoVacunas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Obligatorio2Prog.Models
{
    public class DetalleInstitucion
    {
		public string idInst { get; set; }

		public string nombre { get; set; }

		public string telefono { get; set; }

		public string contacto { get; set; }
		
		public int cantCompras { get; set; }

		public int cantAfili { get; set; }

		public int montoMax { get; set; }

        public Institucion mapearInstitucion()
        {
            if (this == null)
            {
                return null;
            }
            else
            {
                return new Institucion()
                {
                    Id = Int32.Parse(this.idInst),
                    nombre = this.nombre,
                    telefono = this.telefono,
                    contacto = this.contacto,
                    cantAfili = this.cantAfili,
                    cantCompras = this.cantCompras,
                    montoMax = this.montoMax
                };
            }
        }
        public decimal saldoDisponible()
        {
            RepositorioCompra repoC = new RepositorioCompra();
            List<Compra> compras = repoC.FindByInst(Int32.Parse(this.idInst)).ToList();
            decimal montoGastado = 0;
            string mesActual = DateTime.Now.Month.ToString();
            foreach (Compra c in compras)
            {
                if (c.fecha.Month.ToString() == mesActual)
                {
                    montoGastado += c.montoTotal;
                }
            }
            return (this.montoMax * this.cantAfili) - montoGastado;
        }
        public int comprasDisponibles()
        {
            RepositorioCompra repoC = new RepositorioCompra();
            List<Compra> compras = repoC.FindByInst(Int32.Parse(this.idInst)).ToList();
            int comprasDisp = cantCompras;
            string mesActual = DateTime.Now.Month.ToString();
            foreach (Compra c in compras)
            {
                if (c.fecha.Month.ToString() == mesActual)
                {
                    comprasDisp--;
                }
            }
            return comprasDisp;
        }
    }
}