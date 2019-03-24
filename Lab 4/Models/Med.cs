using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace Lab_4.Models
{
    public class Med : IComparable
    {
        [DisplayName("ID")]
        public int id { get; set; }
        [DisplayName("Nombre de Medicamento")]
        public string Nombre { get; set; }
        [DisplayName("Descripcion")]
        public string Descripcion { get; set; }
        [DisplayName("Casa Medica")]
        public string Casa { get; set; }
        [DisplayName("Precio")]
        public double Precio { get; set; }
        [DisplayName("Existencias")]
        public int Existencia { get; set; }

        

        

        public int CompareTo(object obj)
        {
            var comparable = (Med)obj;
            return Nombre.CompareTo(comparable.Nombre);
        }
    }
}