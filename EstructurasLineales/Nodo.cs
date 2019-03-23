using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructurasLineales
{
    public class Nodo<T>
    {
        public Nodo<T> RaizAux { get; set; }//Para formar el arbol
        //Para el arbol binario
        public Nodo<T> Derecha { get; set; }
        public Nodo<T> Izquierda { get; set; }
        public Nodo<T> Creador { get; set; } //Sirve para el metodo de eliminar

        //Para la lista doblemente enlazada
        public Nodo<T> Siguiente { get; set; }
        public Nodo<T> Anterior { get; set; }

        public int posicion { get; set; } //Id del farmaco
        public T valor { get; set; }//Nombre del farmaco

        public  Nodo (T value) //Metodo constructor
        {
            this.Derecha = null;
            this.Izquierda = null;
            this.Creador = null;

            this.Siguiente = null;
            this.Anterior = null;

            posicion = 0; //Numero de linea
            valor = value;
        }

    }
}
