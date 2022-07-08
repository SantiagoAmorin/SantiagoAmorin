using DataAccess.Context;
using Dominio.InterfacesRepositorios;
using Dominio.SeguimientoVacunas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositorio
{
    public class RepositorioUsuario : IRepositorioUsuario
    {
        public bool Add(Usuario unUsuario)
        {
            try
            {
                if (unUsuario.validar())
                {
                    using (Obligatorio2Context db = new Obligatorio2Context())
                    {
                        db.Usuarios.Add(unUsuario);
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

        public IEnumerable<Usuario> FindAll()
        {
            try
            {
                using (Obligatorio2Context db = new Obligatorio2Context())
                {
                    return db.Usuarios.ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Usuario FindbyCI(string ci)
        {
            try
            {
                using (Obligatorio2Context db = new Obligatorio2Context())
                {
                    var q = db.Usuarios.Where(u => u.cedulaId == ci).SingleOrDefault();
                        return q;
                    
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Usuario updateContra(Usuario usr)
        {
            try
            {
                using (Obligatorio2Context db = new Obligatorio2Context())
                {
                    var q = (from u in db.Usuarios
                             where u.cedulaId == usr.cedulaId
                             select u).SingleOrDefault();
                    q.contrasena = usr.Encriptar();
                    db.SaveChanges();
                    return usr;

                }
            }
            catch(Exception e)
            {
                return null;
            }

        }
    }

}

