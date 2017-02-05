using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EasyBuy.Models
{
    public class empresa
    {
        public int id_empresa { get; set; }
        public String nombre_empresa { get; set; }
        public String numero_telefono { get; set; }
        public String direccion { get; set; }
        [Required(ErrorMessage = "Por favor ingrese su correo", AllowEmptyStrings = false)]
        public String correo_tienda { get; set; }
        [Required(ErrorMessage = "Por favor ingrese su contraseña", AllowEmptyStrings = false)]
        public String password { get; set; }
        public String provincia { get; set; }
        public List<Producto> lista_producto { get; set; }
    }
}