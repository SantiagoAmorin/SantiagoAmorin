using DataAccess.Repositorio;
using Dominio.SeguimientoVacunas;
using Obligatorio2Prog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace Obligatorio2Prog.Controllers
{
    public class CompraController : Controller
    {
        private HttpClient clienteApi = new HttpClient();
        private HttpResponseMessage respuesta = new HttpResponseMessage();

        //El puerto puede variar, sustituirlo por el número de puerto
        //en que se esté ejecutando la webapi.

        private Uri UriBase = new Uri(@"http://localhost:56629/api/WebApi");

        // GET: Producto       
        private void ConfigurarCliente()
        {
            //Configurar la ruta para acceder a la raíz de la api:
            clienteApi.BaseAddress = UriBase;

            //Configurar el tipo de datos para trabajar con JSON 
            clienteApi.DefaultRequestHeaders.Accept.Clear();
            clienteApi.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }
        // GET: Compra
        public ActionResult ComprasInstitucion(string idInsti)
        {
            if (Session["logueado"] != null)
            {
                IEnumerable<DetalleCompra> compras = new List<DetalleCompra>();
                try
                {
                    ConfigurarCliente();

                    var ruta = clienteApi.BaseAddress + "/" + "compras?idInsti=" + idInsti;
                    respuesta = clienteApi.GetAsync(ruta).Result;
                    if (respuesta.IsSuccessStatusCode)
                    {
                        var contenido = respuesta.Content.ReadAsAsync<IEnumerable<Dominio.SeguimientoVacunas.Compra>>().Result;
                        List<DetalleCompra> detComp = new List<DetalleCompra>();
                        foreach (Compra comp in contenido)
                        {
                            detComp.Add(mapearCompra(comp));
                        }
                        if (contenido != null)
                        {
                            ViewBag.saldoDisp = detComp[0].institucion.saldoDisponible();
                            ViewBag.comprasDisp = detComp[0].institucion.comprasDisponibles();
                            ViewBag.montoAutorizado = detComp[0].institucion.cantAfili * detComp[0].institucion.montoMax;

                            return View(detComp);
                        }

                    }
                    ModelState.AddModelError("Error Api", "No se obtuvo respuesta   " + respuesta.ReasonPhrase);
                    return View();
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Se produjo una excepción: ", e.Message);
                    return View();
                }                

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public DetalleCompra mapearCompra(Compra detComp)
        {
            RepositorioInstitucion repoInsti = new RepositorioInstitucion();
            RepositorioVacuna repoVac = new RepositorioVacuna();
            if (detComp == null)
            {
                return null;
            }
            DetalleCompra unaComp = new DetalleCompra
            {
                institucion = MapearInstitucion(repoInsti.FindByName(detComp.insti.nombre)),
                fecha = detComp.fecha,
                cantidadVac = detComp.cantidadVac,
                precioVac = detComp.precioVac,
                montoTotal = detComp.montoTotal,
                vacuna = MapearUnaVacuna(repoVac.FindById(detComp.vac.Id))
            };

            return unaComp;
        }

        public DetalleInstitucion MapearInstitucion(Institucion insti)
        {
            if (insti == null)
            {
                return null;
            }
            DetalleInstitucion unaInsti = new DetalleInstitucion
            {
                idInst = insti.Id.ToString(),
                nombre = insti.nombre,
                telefono = insti.telefono,
                contacto = insti.contacto,
                cantCompras = insti.cantCompras,
                cantAfili = insti.cantAfili,
                montoMax = insti.montoMax
            };
            return unaInsti;
        }

        public DetalleVacuna MapearUnaVacuna(Vacuna v)
        {
            if (v == null)
                return null;

            DetalleVacuna unaVacuna = new DetalleVacuna
            {
                idVac = v.Id,
                cantDosis = v.cantDosis,
                nombre = v.nombre,
                faseClinica = v.faseClinica,
                aprovEmerg = v.aprovEmerg,
                efectosAdvers = v.efectosAdvers,
                precio = v.precio,
                mecanismoCOVAX = v.mecanismoCOVAX,
                tempMin = v.tempMin,
                tempMax = v.tempMax,
                edadMin = v.edadMin,
                edadMax = v.edadMax,
                prevHosp = v.prevHosp,
                prevCTI = v.prevCTI,
                prevCovid = v.prevCovid,
                fechActua = v.fechActua,
                produAnual = v.produAnual,
                tipo = v.miTipo.codigoId,
                ciUsuario = v.Usuario.cedulaId,
                status = v.status
            };
            foreach (Laboratorio lab in v.laboratorio)
            {
                unaVacuna.laboratorios.Add(lab.nombre);
            }
            return unaVacuna;
        }

        // GET: Compra/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

       
    }
}
