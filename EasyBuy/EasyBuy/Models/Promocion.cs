using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyBuy.Models
{
    public class Promocion
    {
        public int id_promocion { get; set; }
        public int id_producto { get; set; }
        public int nuevo_precio { get; set; }
        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_final { get; set; }
    }
}
