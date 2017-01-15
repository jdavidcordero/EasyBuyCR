using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EasyBuyCR.Models
{
    public class Usuario
    {
        public String NOMBRE { get; set; }
        public String APELLIDO1 { get; set; }
        [Required(ErrorMessage = "Por favor ingrese su correo", AllowEmptyStrings = false)]
        public String CORREO { get; set; }

        [Required(ErrorMessage = "Por favor ingrese su contraseña", AllowEmptyStrings = false)]
        public String CONTRASENA { get; set; }
        public Boolean RECORDAR { get; set; }
        public char TIPO { get; set; }
        
    }
}