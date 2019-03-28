using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructurasLineales
{
    public class NodoB<T>
    {

        public bool EsRaiz { get; set; }

        public List<Info> Meds { get; set; }

        public List<NodoB<T>> Hijos { get; set; }

        public int cantidadDentro { get; set; }
        
        public NodoB<T> Padre { get; set; }

        public int id { get; set; }
        public int max { get; set; }
        public int min { get; set; }
        public T nombrebuscado { get; set; }

        public string InformacionNodo()
        {
            string meds = "";


            foreach (Info items in Meds)
            {
                meds += items.Nombre + "|";
            }
            return $"{id.ToString("00000;-0000")}|{max.ToString("00000;-0000")}|{min.ToString("00000;-0000")}|{cantidadDentro.ToString("00000;-0000")}|" + Meds;
        }
 

        public NodoB()
        {
            cantidadDentro = 0;
            Meds = new List<Info>();
            Hijos = new List<NodoB<T>>();
            Padre = null;
            id = 1;
            EsRaiz = false;
            min = 0;
            max = 0;

        }

        public void AsignandoGrado(int grado)
        {
            max = (int)Math.Ceiling((double)grado - 1);
            min = (int)Math.Ceiling((double)(grado - 1) / 2);
        }

        public int AsignandoPosicion(int posicion)
        {
            return posicion + 1;
        }

        public int CompareTo(object obj)
        {
            var comparable = (NodoB<T>)obj;
            return Meds.First().Nombre.CompareTo(comparable.Meds.First().Nombre);
        }

    }
}
