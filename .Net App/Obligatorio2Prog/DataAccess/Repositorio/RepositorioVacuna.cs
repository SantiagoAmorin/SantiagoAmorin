using DataAccess.Context;
using Dominio.InterfacesRepositorios;
using Dominio.SeguimientoVacunas;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repositorio
{
    public class RepositorioVacuna : IRepositorioVacuna
    {

        public bool AddVacuna(Vacuna unaVacuna)
        {
            try
            {
                if (unaVacuna.validar())
                {
                    using (Obligatorio2Context db = new Obligatorio2Context())
                    {
                        db.Entry(unaVacuna.Usuario).State= System.Data.Entity.EntityState.Unchanged;
                        db.Entry(unaVacuna.miTipo).State= System.Data.Entity.EntityState.Unchanged;
                        foreach (Laboratorio lab in unaVacuna.laboratorio)
                        {
                            db.Entry(lab).State = System.Data.Entity.EntityState.Unchanged;
                        }
                        db.Vacunas.Add(unaVacuna);
                        db.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }catch(Exception e)
            {
                return false;
            }
            
            
        }

        public IEnumerable<Vacuna> FindAll()
        {
            try {
                using (Obligatorio2Context db = new Obligatorio2Context())
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    db.Configuration.ProxyCreationEnabled = false;
                    var q = db.Vacunas.Select(v => v)
                        .Include(vac => vac.miTipo)
                        .Include(vac => vac.Usuario)
                        .Include(vac => vac.laboratorio);
                        return q.ToList();
                }
            } catch(Exception e){
                return null;
            }
        }

        public IEnumerable<Vacuna> FindByAprob(int faseClinica)
        {
            try
            {
                using (Obligatorio2Context db = new Obligatorio2Context())
                {
                    var q = db.Vacunas.Where(v => v.faseClinica == faseClinica).ToList();
                    return q;
                }
            }
            catch (Exception e)
            {
                return null;
            }
            
        }

        public IEnumerable<Vacuna> FindByLab(string laboratorio)
        {
            throw new System.NotImplementedException();
        }

        public Vacuna FindById(int Id)
        {
            try
            {
                using (Obligatorio2Context db = new Obligatorio2Context())
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    db.Configuration.ProxyCreationEnabled = false;
                    var q = db.Vacunas.Where(v => v.Id == Id).Select(v => v)
                        .Include(vac => vac.miTipo)
                        .Include(vac => vac.Usuario)
                        .Include(vac => vac.laboratorio).SingleOrDefault();
                    return q;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public IEnumerable<Vacuna> filtrarVacunas(int? faseDada,int? precioMin,int? precioMax,string tipo,string laboratorio,string pais)
        {
            try
            {
                using (Obligatorio2Context db=new Obligatorio2Context())
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    db.Configuration.ProxyCreationEnabled = false;
                    var vacunasFiltradas = db.Vacunas.AsQueryable<Vacuna>();
                    if (faseDada != null)
                    {
                        vacunasFiltradas = vacunasFiltradas.Where(v => v.faseClinica == faseDada)
                                                           .Select(v => v)
                                                           .Include(vac => vac.miTipo)
                                                           .Include(vac => vac.Usuario)
                                                           .Include(vac => vac.laboratorio);
                    }
                    if(precioMin < precioMax || precioMin != null && precioMax != null)
                    {
                        vacunasFiltradas = vacunasFiltradas.Where(v => v.precio >= precioMin && v.precio <= precioMax)
                                                           .Select(v => v)
                                                           .Include(vac => vac.miTipo)
                                                           .Include(vac => vac.Usuario)
                                                           .Include(vac => vac.laboratorio);
                    }                   
                    if (tipo != null)
                    {
                        vacunasFiltradas = vacunasFiltradas.Where(v => v.idTipo.Equals(tipo, StringComparison.InvariantCultureIgnoreCase)).Select(v => v)
                                                                                                                                          .Include(vac => vac.miTipo)
                                                                                                                                          .Include(vac => vac.Usuario)
                                                                                                                                          .Include(vac => vac.laboratorio);
                    }
                    if (laboratorio != null)
                    {
                        vacunasFiltradas = vacunasFiltradas.Where(v => v.laboratorio
                                                                  .Any(l=>l.nombre.Equals(laboratorio, StringComparison.InvariantCultureIgnoreCase))).Select(v => v)
                                                                  .Include(vac => vac.miTipo)
                                                                   .Include(vac => vac.Usuario)
                                                                   .Include(vac => vac.laboratorio);
                    }
                    if (pais != null)
                    {
                        vacunasFiltradas = vacunasFiltradas.Where(v => v.status.Contains(pais)).Select(v => v)
                        .Include(vac => vac.miTipo)
                        .Include(vac => vac.Usuario)
                        .Include(vac => vac.laboratorio); ;
                    }
                    return vacunasFiltradas.ToList();
                }
            }
            catch(Exception e)
            {
                return null;
            }
        }
        public IEnumerable<Vacuna> filtrarVacunas2(int? faseDada, int? precioMin, int? precioMax, string tipo, string laboratorio, string pais)
        {
            try
            {
                using (Obligatorio2Context db = new Obligatorio2Context())
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    db.Configuration.ProxyCreationEnabled = false;
                    var vacunasFiltradas = db.Vacunas.AsQueryable<Vacuna>();
                    if (faseDada != null)
                    {
                        vacunasFiltradas = db.Vacunas.Where(v => v.faseClinica == faseDada)
                                                           .Select(v => v)
                                                           .Include(vac => vac.miTipo)
                                                           .Include(vac => vac.Usuario)
                                                           .Include(vac => vac.laboratorio);
                    }
                    if (precioMin != null && precioMax != null)
                    {
                        vacunasFiltradas = db.Vacunas.Where(v => v.precio >= precioMin && v.precio <= precioMax)
                                                           .Select(v => v)
                                                           .Include(vac => vac.miTipo)
                                                           .Include(vac => vac.Usuario)
                                                           .Include(vac => vac.laboratorio);
                    }
                    if (tipo != null)
                    {
                        vacunasFiltradas = db.Vacunas.Where(v => v.idTipo.Equals(tipo, StringComparison.InvariantCultureIgnoreCase)).Select(v => v)
                                                                                                                                          .Include(vac => vac.miTipo)
                                                                                                                                          .Include(vac => vac.Usuario)
                                                                                                                                          .Include(vac => vac.laboratorio);
                    }
                    if (laboratorio != null)
                    {
                        vacunasFiltradas = db.Vacunas.Where(v => v.laboratorio
                                                                  .Any(l => l.nombre.Equals(laboratorio, StringComparison.InvariantCultureIgnoreCase))).Select(v => v)
                                                                  .Include(vac => vac.miTipo)
                                                                   .Include(vac => vac.Usuario)
                                                                   .Include(vac => vac.laboratorio);
                    }
                    if (pais != null)
                    {
                        vacunasFiltradas = db.Vacunas.Where(v => v.status.Contains(pais)).Select(v => v)
                                                                                         .Include(vac => vac.miTipo)
                                                                                         .Include(vac => vac.Usuario)
                                                                                         .Include(vac => vac.laboratorio); ;
                    }
                    return vacunasFiltradas.ToList();
                }
                }
            catch(Exception e)
            {
                return null;
            }
        }
    }

}

