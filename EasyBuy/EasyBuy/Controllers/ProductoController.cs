using EasyBuy.Models;
using EasyBuyCR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EasyBuy.Controllers
{
    public class ProductoController : Controller
    {
        List<Producto> listaProductos = new List<Producto>();
        OracleConection con = new OracleConection();


        public ActionResult EmpresaIndex()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult RegistrarProducto()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        // GET: Producto
        public ActionResult Index()
        {
            return RedirectToAction("RegistrarProducto", "Producto");
        }

  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult RegistrarProducto(Producto producto)
        {
            String mensaje = "";
            int id_producto = 0;
            bool estado = false;
            try
            {
                String id = (String)Session["Correo"];
                producto.id_empresa = id;
                id_producto = con.GuardarProducto(producto);
                mensaje = "El   producto se ha ingresado correctamente \n";
                estado = true;
            }
            catch (Exception exc)
            {
                mensaje = "Error al crear producto" + exc.Message;
            }

            return new JsonResult { Data = new { estado = estado, mensaje = mensaje, id_producto = id_producto } };
        }

   

        public ActionResult AgregarDetalle()
        {
          return PartialView("_AgregarDetalle");
        }

        public ActionResult AgregarDetalleCP()
        {
                return PartialView("_AgregarDetalle");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AgregarDetalle(detalle_producto detalle)
        {
            bool estado = false;
            string mensaje = "";
            try
            {
                con.guardarDetalle(detalle);
                mensaje = "Detalle ingresada correctamente \n";
                estado = true;
            }
            catch (Exception exc)
            {
                mensaje = "Error al crear la detalle" + exc.Message;
            }

            return new JsonResult { Data = new { estado = estado, mensaje = mensaje } };
        }

        public ActionResult ObtenerDetalle(int id_producto)
        {
            return PartialView("_TablaDetalle", con.getDetalles(id_producto));
        }

        public ActionResult ObtenerDetalleCP(int id_producto)
        {
            //ViewBag.direccion = "CP";
            return PartialView("_TablaDetalle", con.getDetalles(id_producto));
        }


        public ActionResult EliminarDetalle(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detalle_producto detalle = con.ObtenerDetalle(id);

            return PartialView(detalle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("EliminarDetalle")]
        public JsonResult Eliminar(int id_detalle)
        {
            bool estado = false;
            string mensaje = "";
            try
            {


                if (id_detalle < 0)
                {
                    mensaje = "EL id no puede ser negativo";
                }

                con.EliminarDetalle(id_detalle);
                estado = true;
                mensaje = "Detalle eliminada...";
            }
            catch (Exception exc)
            {
                mensaje = "Error al eliminar Detalle" + exc;
            }

            return new JsonResult { Data = new { estado = estado, mensaje = mensaje } };
        }


        public ActionResult EliminarProducto(int id)
        {
            String correo_tienda = (String)Session["Correo"];
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = con.ObtenerProducto(id);

            return PartialView(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("EliminarProducto")]
        public JsonResult EliminarProduct(int id_producto)
        {
            bool estado = false;
            string mensaje = "";
            try
            {


                if (id_producto < 0)
                {
                    mensaje = "EL id no puede ser negativo";
                }

                con.EliminarDetallesProducto(id_producto);
                con.EliminarProducto(id_producto);
                estado = true;
                mensaje = "Producto eliminado...";
            }
            catch (Exception exc)
            {
                mensaje = "Error al eliminar Producto" + exc;
            }

            return new JsonResult { Data = new { estado = estado, mensaje = mensaje } };
        }

        public ActionResult ProductosInventario()
        {

                String correo_tienda = (String)Session["Correo"];
                Session["Notificaciones"] = null;
                return View(con.getProducto(correo_tienda));
          
        }

        public ActionResult AgregarPromocion(int id)
        {
            ViewBag.id_detalle = id.ToString();
            return PartialView("_AgregarPromocion");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AgregarPromocion(Promocion promocion)
        {
            bool estado = false;
            string mensaje = "";
            try
            {
                con.guardarPromocion(promocion);
                mensaje = "Promocion ingresada correctamente \n";
                estado = true;
            }
            catch (Exception exc)
            {
                mensaje = "Error al guardar la promocion" + exc.Message;
            }

            return new JsonResult { Data = new { estado = estado, mensaje = mensaje } };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ObtenerDetalle2(int id)
        {
            bool estado = false;
            detalle_producto det = new detalle_producto();
            try
            {
                det = con.ObtenerDetalle(id);
                estado = true;
            }
            catch (Exception exc)
            {

            }

            //String Palimentacion = capa.CostoAlimentacion+"";
            String Det = "";
            var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            Det = javaScriptSerializer.Serialize(det);

            return new JsonResult { Data = new { estado = estado, Det = Det } };
        }

        public ActionResult EditarDetalle(int id)
        {
            

                if (id < 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                detalle_producto det = con.ObtenerDetalle(id);
                ViewBag.detalle_producto = det;
                if (det == null)
                {
                    return HttpNotFound();
                }
                return PartialView(det);
            
        }

       [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EditarDetalle(detalle_producto det)
        {
            bool estado = false;
            string mensaje = "";
            try
            {
                if (det.id_detalle > 0) {
                    con.editarDetalle(det);
                    estado = true;
                    mensaje = "Detalle modificada exitosamente";
                }
                else
                {
                    mensaje = "Error al actualizar Detalle";
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error al actualizar Detalle";
            }
            return new JsonResult { Data = new { estado = estado, mensaje = mensaje } };
        }

            
    }


}