using DataAccess.Repositorio;
using Dominio.SeguimientoVacunas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Obligatorio2Prog.Controllers
{
    public class UsuarioController : Controller
    {
        public ActionResult Update(string ci)
        {
            return View();
        }
        // POST: Usuario/Edit/5
        [HttpPost]
        public ActionResult Update(string ci, FormCollection collection, string Contrasena, string segundaContra)
        {
            RepositorioUsuario repoUsuario = new RepositorioUsuario();
            try
            {
                if (Session["logueado"] != null)
                {

                    Usuario usr = repoUsuario.FindbyCI(ci);
                    if (usr.validarContra(Contrasena) && Contrasena.Equals(segundaContra))
                    {
                        usr.contrasena = Contrasena;
                        if (repoUsuario.updateContra(usr) != null)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return View("Update");
                        }

                    }
                    else
                    {
                        ViewBag.errorCreandoContrasena= "Las contraseñas deben ser iguales o no cumple con los requerimientos" ;
                        return View();
                    }

                    //falta traer el valor de contra y actualizarlo
                    return View("Index");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}

