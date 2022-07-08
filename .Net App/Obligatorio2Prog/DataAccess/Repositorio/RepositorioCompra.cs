using DataAccess.Context;
using Dominio.InterfacesRepositorios;
using Dominio.SeguimientoVacunas;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repositorio
{
    public class RepositorioCompra : IRepositorioCompra
    {
        public bool Add(Compra unaCompra)
        {
            try
            {
                if (unaCompra.validar())
                {
                    using (Obligatorio2Context db = new Obligatorio2Context())
                    {
                        db.Entry(unaCompra.insti).State = System.Data.Entity.EntityState.Unchanged;
                        db.Entry(unaCompra.vac).State = System.Data.Entity.EntityState.Unchanged;
                        db.Compras.Add(unaCompra);
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

        public IEnumerable<Compra> FindAll()
        {
            try
            {
                using (Obligatorio2Context db = new Obligatorio2Context())
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    db.Configuration.ProxyCreationEnabled = false;
                    var q = db.Compras.Select(c => c)
                        .Include(com => com.insti)
                        .Include(com => com.vac);                        
                    return q.ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public IEnumerable<Compra> FindByInst(int regInst)
        {
            try
            {
                
                using (Obligatorio2Context db = new Obligatorio2Context())
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    db.Configuration.ProxyCreationEnabled = false;
                    var q = db.Compras.Where(c => c.insti.Id == regInst)
                        .Select(c => c)
                        .Include(com => com.insti)
                        .Include(com => com.vac)
                        .ToList();
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

