﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyBuy.Models
{
    public class Filtrar
    {
        public List<String> color { get; set; }
        public List<String> precio { get; set; }
        public List<String> lugar { get; set; }
        public List<String> tienda { get; set; }
    }
}