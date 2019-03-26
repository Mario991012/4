using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructurasLineales
{
    public class Info
    {
        public string Nombre { get; set; }
        public int id { get; set; }

        public Info()
        {
            Nombre = "";    
            id = 0;
        }

        public int CompareTo(object obj)
        {
            var comparable = (Info)obj;
            return Nombre.CompareTo(comparable.Nombre);
        }
    }
}
