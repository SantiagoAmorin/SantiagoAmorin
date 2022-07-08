
using DataAccess.Repositorio;
using Dominio.SeguimientoVacunas;
using Obligatorio2Prog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Obligatorio2Prog.Controllers
{
    public class InstitucionController : Controller
    {
        // GET: Institucion/Create
        public ActionResult Create()
        {
            if (Session["logueado"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        // POST: Institucion/Create
        [HttpPost]
        public ActionResult Create(DetalleInstitucion Insti)
        {
            if (Session["logueado"] != null)
            {
                try
                    {
                        RepositorioInstitucion repoIntsti = new RepositorioInstitucion();
                        char idpepe = Insti.idInst[0];
                        if (Insti.idInst.Length <= 6 && !Insti.idInst[0].ToString().Equals(0.ToString()))
                        {
                            Institucion instiFin = Insti.mapearInstitucion();
                            if (repoIntsti.Add(instiFin))
                            {
                                Session["msgAgregadoInstitucion"] = "Institucion agregada exitosamente";
                            }
                            else
                            {
                                Session["msgAgregadoInstitucion"] = "La institucion no se pudo agregar";
                            }

                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ViewBag.msgError = "El identificador de institucion debe tener un largo menor a 6 y no puede empezar en 0";
                            return View();
                        }

                }
                    catch
                    {
                        return View();
                    }
            }
            else
            {
                return RedirectToAction("Index",  "Home");
            }
        }
        
       
    }
}
