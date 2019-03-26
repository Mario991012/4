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

        public List<NodoB<T>> hijos { get; set; }

        public int cantidadDentro { get; set; }
        
        public NodoB<T> Padre { get; set; }

        public int posicion { get; set; }
        public int max { get; set; }
        public int min { get; set; }
        public T nombrebuscado { get; set; }
        

        public NodoB()
        {
            cantidadDentro = 0;
            Meds = new List<Info>();
            hijos = new List<NodoB<T>>();
            Padre = null;
            posicion = 1;
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
