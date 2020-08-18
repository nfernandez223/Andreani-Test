using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGeo.Model
{
    public class Location
    {
        [Key]
        public int id { get; set; }
        public string calle { get; set; }
        public string numero { get; set; }
        public string ciudad { get; set; }
        public string codigo_postal { get; set; }
        public string provincia { get; set; }
        public string pais { get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }
        public bool estado { get; set; }
    }
}
