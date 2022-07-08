using DataAccess.Context;
using DataAccess.Repositorio;
using Dominio.SeguimientoVacunas;
using ImportadoArchivos;
using Obligatorio2Prog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Obligatorio2Prog.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["logueado"] != null)
            {
                string haylogueado = (string)Session["logueado"];
                ViewBag.Msg = "Bienvenido " + haylogueado;

                ViewBag.MsgAlert = Session["MsgAlert"];
            }
            else
            {
                ViewBag.Msg = "No hay usuario logueado";
            }
            return View();
        }

        public ActionResult LoginUsuario()
        {
            if (Session["logueado"] != null)
            {

                return View("Index");

            }
            else
            {
                return View("LoginUsuario");
            }
        }

        public ActionResult Logout()
        {
            Session["logueado"] = null;
            return View("Index");
        }

       [HttpPost]
        public ActionResult LoginUsuario(DetalleUsuario ln)
        {
            RepositorioUsuario repoUsuario = new RepositorioUsuario();
            Usuario usr = repoUsuario.FindbyCI(ln.Cedula);
            Usuario usr2 = ln.mapearUsuario();
            if (usr.esPrimerInicio())
            {
               if (usr2.contrasena == usr.contrasena)
                {
                    Session["logueado"] = ln.Cedula;
                    return RedirectToAction("Update", "Usuario",new { ci= ln.Cedula }); 
                }else{
                    ViewBag.Msg = "Usuario y/o contraseña incorrecto, intente nuevamente";
                    return View();
                }                                          
            }
            else
            {
                if (usr2.Encriptar() == usr.contrasena)
                {
                    Session["logueado"] = ln.Cedula;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Msg = "Usuario y/o contraseña incorrecto, intente nuevamente";
                    return View();
                }
                
            }
            
            
        }

        public ActionResult About()
        {
            ManejoArchivos importadoArch = new ManejoArchivos();
            if (importadoArch.importarDatos()) {
                return RedirectToAction("ListadoVacunas", "Vacuna");
            }else{
                ViewBag.Message = "Todos los datos se encuentran en la DB o hubo un error al importar los mismos";
            }
            

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Santiago Amorin, Tomas Lanterna, Pablo Gomez";

            return View();
        }

    }
}