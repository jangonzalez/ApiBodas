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
    [RoutePrefix("Hoteles")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class C_HotelesController : ApiController
    {
        BodasEntities bd = new BodasEntities();
        private static readonly Logger logInfo = LogManager.GetLogger("info");
        private static readonly Logger logError = LogManager.GetLogger("error");

        [HttpGet]
        [Route("getHoteles")]
        public IHttpActionResult GetHoteles()
        {
            try
            {
                var listahoteles = bd.C_Hoteles.ToList();

                if (!listahoteles.Any())
                {
                    return Ok(new { ok = false, mensaje = "No se encontró hoteles registrados." });
                }

                return Ok(new { ok = true, hoteles = listahoteles, total = listahoteles.Count() });
            }
            catch (Exception ex)
            {
                logError.Error(ex, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                return Ok(new
                {
                    ok = false,
                    mensaje = $"Se produjo un error al obtener los hoteles",
                    errors = new { mensaje = ex.Message }
                });

            }

        }

        [HttpGet]
        [Route("getHotelById/{id}")]
        public IHttpActionResult GetHotelById(int id)
        {
            var mensaje = "";

            try
            {
                var lista = bd.C_Hoteles.Find(id);

                if (lista == null)
                {
                    mensaje = "No se encontró hotel";
                    return Ok(new { ok = false, mensaje });
                }

                return Ok(new { ok = true, hoteles = lista });

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
        [Route("saveHotel")]
        public IHttpActionResult SaveHotel(C_Hoteles item)
        {
            var mensaje = "";
            try
            {

                if (item.name.Trim().Length <= 0)
                {
                    return Ok(new { ok = false, mensaje = "Debe ingresar un nombre válido" });
                }

                if (item.codigo.Trim().Length <= 0)
                {
                    return Ok(new { ok = false, mensaje = "Debe ingresar un codigo " });
                }

                var r = bd.C_Hoteles.Add(item);
                bd.SaveChanges();

                mensaje = $"Se agregó el hotel, correctamente";

                logInfo.Info(mensaje);

                return Ok(new { ok = true, mensaje, hotel = r });
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
        [Route("updateHotel/{id:int}")]
        public IHttpActionResult actualizarHotel(int id, C_Hoteles item)
        {
            var mensaje = "";
            try
            {

                var itemdb = bd.C_Hoteles.Find(id);

                if (itemdb == null)
                {
                    mensaje = "No se encontró hotel";
                    return Ok(new { ok = false, mensaje });
                }
                itemdb.name = item.name;
                itemdb.codigo = item.codigo;
                itemdb.empresa = item.empresa;

                bd.SaveChanges();

                return Ok(new { ok = true, mensaje = "Se actualizo el Hotel, correctamente", hotel = itemdb });
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
        [Route("deleteHotel/{id}")]
        public IHttpActionResult EliminarHotel(int id)
        {
            var mensaje = "";
            try
            {
                var itemdb = bd.C_Hoteles.Find(id);

                if (itemdb != null)
                {
                    bd.C_Hoteles.Remove(itemdb);
                    bd.SaveChanges();

                    return Ok(new { ok = true, mensaje = "Se eliminó el hotel correctamente" });
                }
                else
                {
                    return Ok(new { ok = false, mensaje = "No se encontró el hotel a eliminar" });
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
