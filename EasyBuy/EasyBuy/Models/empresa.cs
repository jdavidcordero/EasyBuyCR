using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyBuy.Models
{
    public class empresa
    {
       public int id_empresa { get; set; }
       public String  nombre_empresa { get; set; }
       public String numero_telefono { get; set; }
       public String direccion { get; set; }
       public String correo_tienda { get; set; }
       public List<Producto> lista_producto {get; set;}
    }
}