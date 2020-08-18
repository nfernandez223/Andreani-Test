﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geolocalizador.Model
{
    public class Message
    {
        public int id { get; set; }
        public string calle { get; set; }
        public string numero { get; set; }
        public string ciudad { get; set; }
        public string codigo_postal { get; set; }
        public string provincia { get; set; }
        public string pais { get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }
    }
}
