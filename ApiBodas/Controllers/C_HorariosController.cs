using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Bodas.Model.BD;
using NLog;
using System.Web.Http.Cors;
namespace ApiBodas.Controllers
{
    [RoutePrefix("Horarios")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class C_HorariosController : ApiController
    {
        BodasEntities bd = new BodasEntities();
        private static readonly Logger logInfo = LogManager.GetLogger("info");
        private static readonly Logger logError = LogManager.GetLogger("error");

        [HttpGet]
        [Route("getHorarios")]
        public async Task<IHttpActionResult> GetHorarios()
        {
            try
            {
                var listahorarios = bd.C_Horarios.ToList();

                if (!listahorarios.Any())
                {
                    return Ok(new { ok = false, mensaje = "No se encontró horarios registrados." });
                }

                return Ok(new { ok = true, horarios = listahorarios, total = listahorarios.Count() });
            }
            catch (Exception ex)
            {
                logError.Error(ex, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                return Ok(new
                {
                    ok = false,
                    mensaje = $"Se produjo un error al obtener los horarios",
                    errors = new { mensaje = ex.Message }
                });

            }

        }

        [HttpGet]
        [Route("getHorarioById/{id}")]
        public async Task<IHttpActionResult> GetHorarioById(int id)
        {
            var mensaje = "";

            try
            {
                var lista = bd.C_Horarios.Find(id);

                if (lista == null)
                {
                    mensaje = "No se encontró horario";
                    return Ok(new { ok = false, mensaje });
                }

                return Ok(new { ok = true, horarios = lista });

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
        [Route("saveHorario")]
        public async Task<IHttpActionResult> SaveHorario(C_Horarios item)
        {
            var mensaje = "";
            try
            {

                if (item.hora.Trim().Length <= 0)
                {
                    return Ok(new { ok = false, mensaje = "Debe ingresar un horario" });
                }

                if (item.idHotel != 0)
                {
                    return Ok(new { ok = false, mensaje = "Debe ingresar un hotel " });
                }

                var r = bd.C_Horarios.Add(item);
                bd.SaveChanges();

                mensaje = $"Se agregó el horario, correctamente";

                logInfo.Info(mensaje);

                return Ok(new { ok = true, mensaje, horario = r });
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
        [Route("updateHorario/{id:int}")]
        public async Task<IHttpActionResult> actualizarHorario(int id, C_Horarios item)
        {
            var mensaje = "";
            try
            {

                var itemdb = bd.C_Horarios.Find(id);

                if (itemdb == null)
                {
                    mensaje = "No se encontró hotel";
                    return Ok(new { ok = false, mensaje });
                }
                itemdb.hora = item.hora;
                itemdb.idHotel = item.idHotel;
                

                bd.SaveChanges();


                return Ok(new { ok = true, mensaje = "Se actualizo el horario, correctamente", horario = itemdb });
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
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Eliminar(int id)
        {
            var mensaje = "";
            try
            {
                var itemdb = bd.C_Horarios.Find(id);

                if (itemdb != null)
                {
                    bd.C_Horarios.Remove(itemdb);
                    bd.SaveChanges();

                    return Ok(new { ok = true, mensaje = "Se eliminó el horario correctamente" });
                }
                else
                {
                    return Ok(new { ok = false, mensaje = "No se encontró el horario a eliminar" });
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
