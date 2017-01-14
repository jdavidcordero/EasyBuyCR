using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyBuy.Models
{
    public class Product
    {
        public int id_producto { get; set; }
        public String description { get; set; }
        public int id_empresa { get; set; }
        List<detalle_producto> list_detalle_producto { set; get; }
    }
}