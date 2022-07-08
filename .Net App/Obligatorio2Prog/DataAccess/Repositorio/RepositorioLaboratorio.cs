using DataAccess.Context;
using Dominio.InterfacesRepositorios;
using Dominio.SeguimientoVacunas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositorio
{
    public class RepositorioLaboratorio : IRepositorioLaboratorio
    {

        public bool Add(Laboratorio unLaboratorio)
        {
            try
            {
                if (unLaboratorio.validar())
                {
                    using (Obligatorio2Context db = new Obligatorio2Context())
                    {
                        var existe = db.Laboratorios.Count(l => l.nombre == unLaboratorio.nombre);
                        if (existe == 0)
                        {
                            db.Laboratorios.Add(unLaboratorio);
                            db.SaveChanges();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                        
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
        public IEnumerable<Laboratorio> FindAll()
        {
            try
            {
                using (Obligatorio2Context db = new Obligatorio2Context())
                {
                    return db.Laboratorios.ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Laboratorio FindByName(string laboratorio)
        {
            try
            {
                using (Obligatorio2Context db = new Obligatorio2Context())
                {

                    var q = db.Laboratorios.Where(l => l.nombre == laboratorio).SingleOrDefault();

                    return q;
                    
                    
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public IEnumerable<Laboratorio> FindByNames(List<string> laboratorios)
        {
            throw new System.NotImplementedException();
        }
    }

}

