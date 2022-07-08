using DataAccess.Context;
using Dominio.InterfacesRepositorios;
using Dominio.SeguimientoVacunas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositorio
{
    public class RepositorioInstitucion : IRepositorioInstitucion
    {
        public bool Add(Institucion unaInstitucion)
        {
            try
            {
                if (unaInstitucion.validar())
                {
                    using (Obligatorio2Context db = new Obligatorio2Context())
                    {
                        var existe = db.Instituciones.Count(i => i.Id == unaInstitucion.Id);
                        if (existe == 0)
                        {
                            db.Instituciones.Add(unaInstitucion);
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

        public IEnumerable<Institucion> FindAll()
        {
            try
            {
                using (Obligatorio2Context db = new Obligatorio2Context())
                {
                    var q = db.Instituciones.ToList();
                    return q;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Institucion FindById(int id)
        {
            try
            {
                using (Obligatorio2Context db = new Obligatorio2Context())
                {
                    var q = db.Instituciones.Where(i=>i.Id==id).SingleOrDefault();
                    return q;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Institucion FindByName(string name)
        {
            try
            {
                using (Obligatorio2Context db = new Obligatorio2Context())
                {
                    var q = db.Instituciones.Where(i => i.nombre == name).SingleOrDefault();
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

