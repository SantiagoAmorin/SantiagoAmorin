using Dominio.SeguimientoVacunas;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{
    public class Obligatorio2Context : DbContext
    {
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Institucion> Instituciones { get; set; }
        public DbSet<Laboratorio> Laboratorios { get; set; }
        public DbSet<Tipo> Tipos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Vacuna> Vacunas { get; set; }

        public Obligatorio2Context() :
        base("conObligatorio2")
        {

        }
    }
    
}
