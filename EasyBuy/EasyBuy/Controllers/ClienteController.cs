using EasyBuy.Models;
using EasyBuyCR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyBuy.Controllers
{
    public class ClienteController : Controller
    {
        OracleConection con = OracleConection.obtenerInstancia();
        // GET: Cliente
        public ActionResult Index()
        {
            if (Session["Correo"] != null)
                return View();
            else
                return RedirectToAction("Login","Account");
        }

        public ActionResult Hombre(String categoria = "abrigos") {
            if (Session["Correo"] != null)
            {
                List<Producto> listaProductos = con.ObtenerProductosHombre(categoria);
                if (listaProductos != null && listaProductos.Count != 0)
                {
                    ViewBag.categoria = listaProductos.ElementAt(0).categoria;
                    if (listaProductos.ElementAt(0).list_detalle_producto != null && listaProductos.ElementAt(0).list_detalle_producto.Count != 0)
                    {
                        ViewBag.genero = listaProductos.ElementAt(0).list_detalle_producto.ElementAt(0).genero;
                        ViewBag.precios = con.ObtenerPreciosProductosHombre(categoria);
                        ViewBag.colores = con.ObtenerColoresProductosHombre(categoria);
                    }
                }
                return View(listaProductos);
            }
            else
                return RedirectToAction("Login", "Account");
        }

        public ActionResult Mujer(String categoria = "abrigos")
        {
            if (Session["Correo"] != null)
            {
                List<Producto> listaProductos = con.ObtenerProductosMujer(categoria);

                if (listaProductos != null && listaProductos.Count != 0)
                {
                    ViewBag.categoria = listaProductos.ElementAt(0).categoria;
                    if (listaProductos.ElementAt(0).list_detalle_producto != null && listaProductos.ElementAt(0).list_detalle_producto.Count != 0)
                    {
                        ViewBag.genero = listaProductos.ElementAt(0).list_detalle_producto.ElementAt(0).genero;
                        ViewBag.precios = con.ObtenerPreciosProductosMujer(categoria);
                        ViewBag.colores = con.ObtenerColoresProductosMujer(categoria);
                    }
                }
                return View(listaProductos);
            }
            else
                return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult Filtrar(Filtrar model) {

            List<Producto> listaProductos = con.ObtenerProductosHombreFiltros(model);
            ViewBag.categoria = model.categoria;
            ViewBag.genero = model.genero;
            ViewBag.precios = con.ObtenerPreciosProductosHombre(model.categoria);
            ViewBag.colores = con.ObtenerColoresProductosHombre(model.categoria);
            return View("Hombre",listaProductos);
        }

        public ActionResult FiltrarMujer(Filtrar model)
        {

            List<Producto> listaProductos = con.ObtenerProductosHombreFiltros(model);
            ViewBag.categoria = model.categoria;
            ViewBag.genero = model.genero;
            ViewBag.precios = con.ObtenerPreciosProductosMujer(model.categoria);
            ViewBag.colores = con.ObtenerColoresProductosMujer(model.categoria);
            return View("Mujer", listaProductos);
        }
    }
}