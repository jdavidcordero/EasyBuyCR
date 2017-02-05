using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyBuy.Models
{
    public class Producto
    {
        public int id_producto { get; set; }
        public String description { get; set; }
        public String id_empresa { get; set; }
        public empresa empresa { get; set; }
        public String categoria { get; set; }
        public List<detalle_producto> list_detalle_producto { set; get; }
    }
}