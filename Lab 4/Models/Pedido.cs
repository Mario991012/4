using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace Lab_4.Models
{
    public class Pedido : IComparable
    {
        [DisplayName("Nombre del Cliente")]
        public string NombreCliente { get; set; }
        [DisplayName("Direccion")]
        public string Direccion { get; set; }
        [DisplayName("NIT")]
        public string nit { get; set; }
        [DisplayName("Total a Cancelar")]
        public double total { get; set; }

        public string MedComprados { get; set; }

        public int CompareTo(object obj)
        {
            var comparable = (Pedido)obj;
            return NombreCliente.CompareTo(comparable.NombreCliente);
        }
    }
}