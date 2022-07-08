using DataAccess.Context;
using Dominio.InterfacesRepositorios;
using Dominio.SeguimientoVacunas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositorio
{
    public class RepositorioTipo : IRepositorioTipo
    {

        public bool Add(Tipo unTipo)
        {
            try
            {
                if (unTipo.validar())
                {
                    using (Obligatorio2Context db = new Obligatorio2Context())
                    {
                        db.Tipos.Add(unTipo);
                        db.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public IEnumerable<Tipo> FindAll()
        {
            try
            {
                using (Obligatorio2Context db = new Obligatorio2Context())
                {
                    return db.Tipos.ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Tipo FindByCode(string codigo)
        {
            try
            {
                using (Obligatorio2Context db = new Obligatorio2Context())
                {
                    var q = db.Tipos.Where(t => t.codigoId == codigo).SingleOrDefault();
                    return q;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }

}

