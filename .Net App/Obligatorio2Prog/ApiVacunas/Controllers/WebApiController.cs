using DataAccess.Repositorio;
using Dominio.SeguimientoVacunas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace ApiVacunas.Controllers
{
    
    public class WebApiController : ApiController
    {
        private RepositorioVacuna repoVac = new RepositorioVacuna();
        private RepositorioCompra repoCom  =  new RepositorioCompra();
        // GET: api/WebApi
        
        public IHttpActionResult Get()
        {
            try
            {
                var vacunas = repoVac.FindAll();
                return Ok(vacunas);
            }
            catch(Exception e)
            {
                return null;
            }
        }

        // GET: api/WebApi/5
        [Route("api/WebApi/filtrados")]
        [HttpGet]
        public IHttpActionResult FiltrarVacunas(int? fase, int? preciomin, int? preciomax,string tipovac,string nomlab,string vacpais,string filtro)
        {
            try
            {
                if (filtro.Equals("fltComb"))
                {
                    var vacunas = repoVac.filtrarVacunas(fase, preciomin, preciomax, tipovac, nomlab, vacpais);
                    return Ok(vacunas);
                }else
                {
                    var vacunas = repoVac.filtrarVacunas2(fase, preciomin, preciomax, tipovac, nomlab, vacpais);
                    return Ok(vacunas);
                }                                                
            }
            catch(Exception e)
            {
                return InternalServerError(new Exception("No es posible filtrar los productos",
                    e.InnerException));

            }
            
        }

        // POST: api/WebApi
        [HttpPost]
        public IHttpActionResult Post([FromBody]Compra comp)
        {
            RepositorioCompra repoC = new RepositorioCompra();
            repoC.Add(comp);
            return Ok(repoC.FindByInst(comp.insti.Id));
        }

        [Route("api/WebApi/compras")]
        [HttpGet]
        public IHttpActionResult comprasInsti(string idInsti)
        {
            try
            {
                int idAux  =  Int32.Parse(idInsti);
                var compras  =  repoCom.FindByInst(idAux);
                return Ok(compras);
            }
            catch(Exception e)
            {
                return InternalServerError(new Exception("No es posible filtrar las compras",
                    e.InnerException));
            }
        }

        // PUT: api/WebApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/WebApi/5
        public void Delete(int id)
        {
        }
    }
}
