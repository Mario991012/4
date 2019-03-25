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

        public List<T> meds { get; set; }
        public List<NodoB<T>> hijos { get; set; }

        public int cantidadDentro { get; set; }
        
        public NodoB<T> Padre { get; set; }

        public int posicion { get; set; }
        public int max { get; set; }
        public int min { get; set; }

        

        public NodoB(int grado)
        {
            cantidadDentro = 0;
            meds = new List<T>();
            hijos = new List<NodoB<T>>();
            Padre = null;
            posicion = 1;
            EsRaiz = false;
            max = (int)Math.Ceiling((double)grado - 1);
            min = (int)Math.Ceiling((double)(grado-1)/2);
        }

    }
}
