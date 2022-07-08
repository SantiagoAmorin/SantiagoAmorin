using Dominio.SeguimientoVacunas;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Obligatorio2Prog.Models
{
    public class CompraContext : DbContext
    {
              public DbSet<Compra> Compras { get; set; }
    }
}