using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Bodas.Model.BD;
using NLog;

namespace ApiBodas.Controllers
{
    [RoutePrefix("Agencia")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class C_AgenciaController : ApiController
    {
        BodasEntities bd = new BodasEntities();
        private static readonly Logger logInfo = LogManager.GetLogger("info");
        private static readonly Logger logError = LogManager.GetLogger("error");

        [HttpGet]
        [Route("getAgencias")]
        public IHttpActionResult GetAgencias()
        {
            try
            {
                var listaAgencias = bd.C_Agencia.ToList();
                if (!listaAgencias.Any())
                {
                    return Ok(new { ok = false, mensaje = "No se encontraron Agencias." });
                }

                return Ok(new { ok = true, agencias = listaAgencias, total = listaAgencias.Count });
            }
            catch (Exception ex)
            {
                logError.Error(ex, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                return Ok(new
                {
                    ok = false,
                    mensaje = $"Se produjo un error al obtener las Agencias",
                    errors = new { mensaje = ex.Message }
                });
            }
        }

        [HttpGet]
        [Route("getAgenciaById/{id}")]
        public IHttpActionResult GetAgenciaById(int id)
        {

            var mensaje = "";

            try
            {
                var lista = bd.C_Agencia.Find(id);

                if (lista == null)
                {
                    mensaje = "No se encontró la Agencia";
                    return Ok(new { ok = false, mensaje });
                }

                return Ok(new { ok = true, agencia = lista });

            }
            catch (Exception ex)
            {
                logError.Error(ex, ex.InnerException != null ? ex.InnerException.Message : ex.Message);

                return Ok(new
                {
                    ok = false,
                    mensaje,
                    errors = new { mensaje = ex.Message }
                });
            }

        }

        [HttpPost]
        [Route("saveAgencia")]
        public IHttpActionResult SaveAgencia(C_Agencia item)
        {
            var mensaje = "";
            try
            {

                if (item.name.Trim().Length <= 0)
                {
                    return Ok(new { ok = false, mensaje = "Debe ingresar un nombre válido" });
                }

                if (item.correo.Trim().Length <= 0)
                {
                    return Ok(new { ok = false, mensaje = "Debe ingresar un correo válido " });
                }

                if (item.telefono.Trim().Length <= 0)
                {
                    return Ok(new { ok = false, mensaje = "Debe ingresar un número telefonico " });
                }

                var r = bd.C_Agencia.Add(item);
                bd.SaveChanges();

                mensaje = $"Se agregó la Agencia correctamente";

                logInfo.Info(mensaje);

                return Ok(new { ok = true, mensaje, agencia = r });
            }
            catch (Exception ex)
            {
                logError.Error(ex, ex.InnerException != null ? ex.InnerException.Message : ex.Message);

                return Ok(new
                {
                    ok = false,
                    mensaje,
                    errors = new { mensaje = ex.Message }
                });
            }
        }

        [HttpPost]
        [Route("updateAgencia")]
        public IHttpActionResult actualizarAgencia(C_Agencia item)
        {
            var mensaje = "";
            try
            {
                var itemdb = bd.C_Agencia.Find(item.id);

                if (itemdb == null)
                {
                    mensaje = "No se encontró Agencia";
                    return Ok(new { ok = false, mensaje });
                }
                itemdb.name = item.name;
                itemdb.correo = item.correo;
                itemdb.direccion = item.direccion;
                itemdb.telefono = item.telefono;

                bd.SaveChanges();

                return Ok(new { ok = true, mensaje = "Se actualizo la Agencia correctamente", agencia = itemdb });
            }
            catch (Exception ex)
            {
                logError.Error(ex, ex.InnerException != null ? ex.InnerException.Message : ex.Message);

                return Ok(new
                {
                    ok = false,
                    mensaje,
                    errors = new
                    {
                        mensaje = ex.Message
                    }
                });
            }

        }

        [HttpDelete]
        [Route("deleteAgencia/{id:int}")]
        public IHttpActionResult DeleteAgencia(int id)
        {
            var mensaje = "";
            try
            {
                var itemdb = bd.C_Agencia.Find(id);

                if (itemdb != null)
                {
                    bd.C_Agencia.Remove(itemdb);
                    bd.SaveChanges();

                    return Ok(new { ok = true, mensaje = "Se eliminó la Agencia correctamente" });
                }
                else
                {
                    return Ok(new { ok = false, mensaje = "No se encontró la Agencia a eliminar" });
                }

            }
            catch (Exception ex)
            {

                logError.Error(ex, ex.InnerException != null ? ex.InnerException.Message : ex.Message);

                return Ok(new
                {
                    ok = false,
                    mensaje,
                    errors = new { mensaje = ex.Message }
                });
            }
        }
    }
}
