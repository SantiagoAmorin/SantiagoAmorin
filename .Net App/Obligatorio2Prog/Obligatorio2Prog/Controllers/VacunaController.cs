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
    public class VacunaController : Controller
    {
        #region variables para el cliente MVC usadas en las acciones

        private HttpClient clienteApi = new HttpClient();
        private HttpResponseMessage respuesta = new HttpResponseMessage();

        //El puerto puede variar, sustituirlo por el número de puerto
        //en que se esté ejecutando la webapi.

        private Uri UriBase = new Uri(@"http://localhost:56629/api/WebApi");
        #endregion
        // GET: Producto
        #region Configuración del cliente
        private void ConfigurarCliente()
        {
            //Configurar la ruta para acceder a la raíz de la api:
            clienteApi.BaseAddress = UriBase;

            //Configurar el tipo de datos para trabajar con JSON 
            clienteApi.DefaultRequestHeaders.Accept.Clear();
            clienteApi.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }
        #endregion
        // GET: Vacuna
        public ActionResult ListadoVacunas()
        {
            if (Session["logueado"] != null)
            {
                IEnumerable<DetalleVacuna> vacunas = new List<DetalleVacuna>();
                try
                {
                    ConfigurarCliente();

                    respuesta = clienteApi.GetAsync(clienteApi.BaseAddress).Result;


                    if (respuesta.IsSuccessStatusCode)
                    {
                        var contenido = respuesta.Content.ReadAsAsync<IEnumerable<Dominio.SeguimientoVacunas.Vacuna>>().Result;
                        vacunas = MapearVacuna(contenido);
                        if (vacunas != null)
                            return View(vacunas);
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


        // POST: Vacuna/Create
        [HttpPost]
        public ActionResult ListadoVacunas(int? faseClinica, int? precioMin, int? precioMax, string tipoVac, string nomLab, string vacPais, string filtradoElej)
        {
            if (Session["logueado"] != null) 
            { 
            IEnumerable<DetalleVacuna> vacunas = new List<DetalleVacuna>();
                try
                {
                    if (filtradoElej == null 
                        || faseClinica == null && precioMin == null && precioMax == null && tipoVac == "" && nomLab == "" && vacPais == ""  
                        || precioMin>=precioMax 
                        || precioMin != null && precioMax == null 
                        || precioMax != null && precioMin == null)
                {
                    return RedirectToAction("ListadoVacunas");
                }
                ConfigurarCliente();
                var ruta = clienteApi.BaseAddress+"/" +"filtrados?fase=" + faseClinica + "&preciomin=" + precioMin + "&preciomax=" + precioMax + "&tipovac=" + tipoVac + "&nomlab=" + nomLab + "&vacpais=" + vacPais + "&filtro=" + filtradoElej;
                var respuesta = clienteApi.GetAsync(ruta).Result;
                if (respuesta.IsSuccessStatusCode)
                {
                    var contenido=respuesta.Content.ReadAsAsync<IEnumerable<Dominio.SeguimientoVacunas.Vacuna>>().Result;
                    vacunas = MapearVacuna(contenido);
                    if (vacunas != null)
                        return View(vacunas);
                }
                ModelState.AddModelError("Error Api", "No se obtuvo respuesta   " + respuesta.ReasonPhrase);
                return View();
            }
            catch(Exception e)
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

        public ActionResult CompraVacuna(int id)
        {
            if (Session["logueado"] != null)
            {
                TempData["idVac"] = id;
                RepositorioVacuna repoVac = new RepositorioVacuna();
                Vacuna vac = repoVac.FindById(id);
                DetalleVacuna vac1 = MapearUnaVacuna(vac);
                DetalleCompra compra = new DetalleCompra
                {
                    fecha = DateTime.Now,
                    precioVac = vac1.precio,
                    vacuna = vac1
                };
                IncluirInstiModel(compra);
                return View(compra);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult CompraVacuna(string institucion, int cantidadVac, FormCollection coll)
        {
            if (Session["logueado"] != null && cantidadVac>0)
            {
                string idVacAux = TempData["idVac"].ToString();
                RepositorioCompra repoComp = new RepositorioCompra();
                RepositorioVacuna repoVac = new RepositorioVacuna();
                RepositorioInstitucion repoInsti = new RepositorioInstitucion();
                Vacuna vac = repoVac.FindById(Int32.Parse(idVacAux));
                DetalleVacuna vac1 = MapearUnaVacuna(vac);
                DetalleCompra compra = new DetalleCompra
                {
                    cantidadVac = cantidadVac,
                    fecha = DateTime.Now,
                    precioVac = vac1.precio,
                    institucion = MapearInstitucion(repoInsti.FindByName(institucion)),
                    vacuna = vac1
                };
                compra.montoTotal = compra.calcularMonto();
                if (compra.validar())
                {
                    Compra comp = mapearCompra(compra);
                    ConfigurarCliente();
                    var ruta = clienteApi.BaseAddress;
                    var accesoApi = clienteApi.PostAsJsonAsync(ruta, comp);

                    accesoApi.Wait();

                    respuesta = accesoApi.Result;

                    if (respuesta.IsSuccessStatusCode)
                    {
                        TempData["ResultadoOperacion"] = "Compra agregada con exito";
                        return RedirectToAction("ComprasInstitucion", "Compra", new { idInsti = compra.institucion.idInst.ToString() });
                    }

                    ViewBag.msgError = "No fue posible ingresar la compra. Error: " + respuesta.StatusCode;
                    return View();
                }
                else
                {
                    Session["msgErrorCompraVacuna"] = "Verifique los datos ingreados o la institucion no puede realizar la compra";
                    return RedirectToAction("ListadoVacunas", "Vacuna");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
                        
        }

        

        private void IncluirInstiModel(DetalleCompra compra)
        {
            RepositorioInstitucion repoInst = new RepositorioInstitucion();
            compra.todosInstituciones = new SelectList(repoInst.FindAll(), "nombre", "nombre");

        }

        public Compra mapearCompra (DetalleCompra detComp)
        {
            RepositorioInstitucion repoInsti = new RepositorioInstitucion();
            RepositorioVacuna repoVac = new RepositorioVacuna();
            if (detComp == null)
            {
                return null;
            }
            Compra unaComp = new Compra
            {
                insti = repoInsti.FindByName(detComp.institucion.nombre),
                fecha = detComp.fecha,
                cantidadVac = detComp.cantidadVac,
                precioVac = detComp.precioVac,
                montoTotal = detComp.montoTotal,
                vac = repoVac.FindById( detComp.vacuna.idVac)
            };

            return unaComp;
        }

        public DetalleInstitucion MapearInstitucion (Institucion insti)
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
    

        public IEnumerable<DetalleVacuna> MapearVacuna(IEnumerable<Vacuna> vac)
        {

            if (vac == null)
                return null;

            List<DetalleVacuna> vacs = new List<DetalleVacuna>();
            foreach (Vacuna v in vac)
            {
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
                    status=v.status
                };
                foreach (Laboratorio lab in v.laboratorio)
                {
                    unaVacuna.laboratorios.Add(lab.nombre);
                }
                vacs.Add(unaVacuna);
            }
            return vacs;
        }
    }
}
