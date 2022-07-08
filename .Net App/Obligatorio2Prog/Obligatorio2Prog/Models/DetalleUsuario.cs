using Dominio.SeguimientoVacunas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Obligatorio2Prog.Models
{
    public class DetalleUsuario
    {
        public string Cedula { get; set; }

        public string Contrasena { get; set; }


        public Usuario mapearUsuario()
        {
            if (this == null)
            {
                return null;
            }
            else
            {
                return new Usuario
                {
                    cedulaId = this.Cedula,
                    contrasena = this.Contrasena
                };
            }

        }
    }
}