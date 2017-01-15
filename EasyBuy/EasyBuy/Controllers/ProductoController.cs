using EasyBuy.Models;
using EasyBuyCR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            int idProducto = 0;
            bool estado = false;
            try
            {
                String id = (String)Session["correo_tienda"];
                producto.id_empresa = id;
                idProducto = con.GuardarProducto(producto);
                mensaje = "El plan se ha ingresado correctamente \n";
                estado = true;
            }
            catch (Exception exc)
            {
                mensaje = "Error al crear producto" + exc.Message;
            }

            return new JsonResult { Data = new { estado = estado, mensaje = mensaje, idProducto = idProducto } };
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
        public JsonResult AgregarDetalle(detalle_producto capa)
        {
            bool estado = false;
            string mensaje = "";
            try
            {
                //lista.agregar(capa);
              //  con.guardarCapacitacion(capa);
                mensaje = "Detalle ingresada correctamente \n";
                estado = true;
            }
            catch (Exception exc)
            {
                mensaje = "Error al crear la detalle" + exc.Message;
            }

            return new JsonResult { Data = new { estado = estado, mensaje = mensaje } };
        }

    }
}