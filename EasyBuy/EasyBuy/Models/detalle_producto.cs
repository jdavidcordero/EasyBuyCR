using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyBuy.Models
{
    public class detalle_producto
    {
        int id_detalle { get; set; }
        int id_producto { get; set;}
        int cantidad { get; set; }
        String color { get; set; }
        String talla { get; set; }
        String precio { get; set; }
        String imagen { get; set; }
     }
}