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

        public ActionResult Hombre() {
            if (Session["Correo"] != null)
            {
                List<Producto> listaProductos = con.ObtenerAbrigosHombre();
                return View();
            }
            else
                return RedirectToAction("Login", "Account");
        }

        public ActionResult Mujer()
        {
            if (Session["Correo"] != null)
                return View();
            else
                return RedirectToAction("Login", "Account");
        }
    }
}