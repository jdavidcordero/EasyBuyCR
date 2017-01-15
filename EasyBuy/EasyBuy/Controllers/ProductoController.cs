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
        public ActionResult ProductosInventario()
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
                String id = (String)Session["correo_tienda"];
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


        public ActionResult EliminarDetalle(int id_detalle)
        {
            if (id_detalle < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detalle_producto detalle = con.ObtenerDetalle(id_detalle);

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
                mensaje = "Error al eliminar Detalle";
            }

            return new JsonResult { Data = new { estado = estado, mensaje = mensaje } };
        }
    }
}