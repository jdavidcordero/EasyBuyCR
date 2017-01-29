using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyBuy.Models
{
    public class detalle_producto
    {
        public int id_detalle { get; set; }
        public int id_producto { get; set;}
        public int cantidad { get; set; }
        public int precio { get; set; }
        public String color { get; set; }
        public String talla { get; set; }       
        public String imagen { get; set; }
        public bool promocion { get; set; }
        public String genero { get; set; }
        public int precio_promocion { get; set; }

        public List<String> ObtenerComboGenero()
        {
            List<String> list = new List<string>();
            list.Add("Mujer");
            list.Add("Hombre");
            return list;
        }
    }
}