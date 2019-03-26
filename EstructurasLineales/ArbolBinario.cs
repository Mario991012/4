using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructurasLineales
{
    public class ArbolBinario<T> : IEnumerable<T> where T : IComparable
    {
        public Nodo<T> RaizAux { get; set; }//Para formar el arbol
        public ArbolBinario() { RaizAux = null; }//Metodo constructor

        public void AgregarNodoR(T value)//El que quiero insertar, su papa, su valor
        {
            var Nodo = new Nodo<T>(value);
            if (RaizAux ==  null)//Raiz pura del arbol
            {
                RaizAux = Nodo;
                RaizAux.posicion = Nodo.posicion;
                RaizAux.valor = value;
                RaizAux.Creador = null ;
            }
            else
            {
                AgregarHojas(RaizAux, Nodo, value);
            }
        }

        public void AgregarHojas(Nodo<T> Raiz, Nodo<T> hijo, T hojaV)//La raiz
        {
            if (MyDelegate(Raiz, hijo) < 0)//Verifico si va hacia la derecha
            {
                if (Raiz.Derecha == null) //Si el lado derecho esta vacio
                {
                    Raiz.Derecha = hijo;
                    Raiz.Derecha.valor = hojaV;
                    Raiz.Derecha.Creador = Raiz;
                    Raiz.Derecha.posicion = hijo.posicion; //POSICION REPRESENTA EL ID
                    
                }
                else //De lo contrario, lo vuelve a llamar hasta encontrarle un espacio
                {
                    AgregarHojas(Raiz.Derecha, hijo, hojaV);
                }
            }
            else if (MyDelegate(Raiz, hijo) > 0)//Verifico si va hacia la izquierda
            {
                if (Raiz.Izquierda == null) //Si el lado izquierdo esta vacio
                {
                    Raiz.Izquierda = hijo;
                    Raiz.Izquierda.valor = hojaV;
                    Raiz.Izquierda.Creador = Raiz;
                    Raiz.Izquierda.posicion = hijo.posicion; //POSICION REPRESENTA EL ID
                }
                else//De lo contrario, lo vuelve a llamar hasta encontrarle un espacio
                {
                    AgregarHojas(Raiz.Izquierda, hijo, hojaV);
                }
            }
            //else if ((int)MyDelegate.DynamicInvoke(Raiz, hijo) == 0)//Verifico que sean iguales
            //{
            //    throw new DuplicateWaitObjectException("DUPLICADOS NO EXISTEN EN ARBOL"); 
            //}
        }      

        public T CrearNodo(T valor) //METODO A LLAMAR CUANDO SE BUSQUE UN FARMACO
        {
            var Nodo = new Nodo<T>(valor);
            return BuscadorNodo(RaizAux, Nodo);
        }

        public T BuscadorNodo(Nodo<T>Raiz, Nodo<T>nodo)
        {
            if (Raiz == null) { return nodo.valor; }// si el arbol esta vacio o no se encontro

            else if (nodo.valor.CompareTo(Raiz.valor) == 0) { return Raiz.valor; }//Si el valor se encuentra en la raiz

            else if (Raiz.valor.CompareTo(nodo.valor) > 0) { return BuscadorNodo(Raiz.Izquierda, nodo); }//Si es menor el dato, se vuelve a llamar
            else { return BuscadorNodo(Raiz.Derecha, nodo); }//Si es mayor el dato, se vuelve a llamar

            //ESTE METODO RETORNA 0 O  n... ENTONCES, CUANDO LO LLAMEMOS EN EL CONTROLADOR, DEBEMOS DE PONER ANTES UN CONDICIONAL PARA QUE FUNCIONE
        }

        public void BuscarNodoAEliminar(Nodo<T>arbol, Nodo<T>eliminado)//Envio la raiz y el nodo a eliminar
        {
            if (arbol == null) { return; }//Si el arbol esta vacio

            else if ((int)MyDelegate.DynamicInvoke(arbol.valor, eliminado.valor) > 0) //Si el valor es mayor
            { BuscarNodoAEliminar(arbol.Izquierda, eliminado); } //Recursividad hacia la derecha

            else if ((int)MyDelegate.DynamicInvoke(arbol.valor, eliminado.valor) < 0) //Si el valor es menor
            { BuscarNodoAEliminar(arbol.Derecha, eliminado); }//Recursividad hacia la izquierda

            else if ((int)MyDelegate.DynamicInvoke(arbol.valor, eliminado.valor) == 0) //Cuando lo encuentre
            { Borrar(arbol); }//Llama al metodo para verificar cuantos hijos tiene
        }

        public void Borrar(Nodo<T> borrado)//Revisa cuantos hijos tiene (NODO A ELIMINAR)
        {
            if ((borrado.Derecha !=null) &&(borrado.Izquierda != null)) //Si tiene ambos hijos
            {
                Nodo<T> Nodo = Minimo(borrado.Derecha);//Instanceo el nodo, que se define como el menor de la derecha
                borrado.posicion = Nodo.posicion;
                borrado.valor = Nodo.valor; //Reemplazo el valor del nodo a borrar con el del izquierdo mas pequenio
                Borrar(Nodo);
            }
            else if (borrado.Izquierda!= null) //Cuando solo tenga un hijo izquierdo
            {
                Reemplazar(borrado, borrado.Izquierda);
                Destruir(borrado);
            }
            else if (borrado.Derecha != null) //Cuando solo tenga un hijo derecho
            {
                Reemplazar(borrado, borrado.Derecha);
                Destruir(borrado);
            }
            else  //Cuando sea una hoja el nodo a eliminar
            {
                Reemplazar(borrado, null);
                Destruir(borrado);
            } 

        }
        public Nodo<T> Minimo(Nodo<T> tmp)//Funcion para determinar el nodo mas izquierdo del hijo derecho del nodo a eliminar
        {
            if (tmp == null) { return null; }//Si el arbol esta vacio
            else if (tmp.Izquierda != null) { return Minimo(tmp.Izquierda); }//va a recorrer izquierda hasta encontrar el menor de la derecha
            else { return tmp; }
                    
        }
        //Le mandamos el nodo a eliminar y su hijo NUEVO= HIJO DERECHO O IZQUIERDO
        public void Reemplazar(Nodo<T> arbol, Nodo<T> nuevo) //Cuando tenga un solo hijo (ya sea izquierda o derecha))
        {
            if (arbol.Creador != null)//El padre apunta a nuevo padre
            {
                if ((int)MyDelegate.DynamicInvoke(arbol.valor, arbol.Creador.Izquierda.valor) == 0) //Si es el hijo izquierdo
                {
                    arbol.Creador.Izquierda = nuevo; //El padre convierte a su hijo izquierdo el hijo del eliminado
                }
                else if((int)MyDelegate.DynamicInvoke(arbol.valor, arbol.Creador.Derecha.valor) == 0) //Si es el hijo derecho
                {
                    arbol.Creador.Derecha = nuevo; //El padre convierte a su hijo izquierdo el hijo del eliminado
                }
            }
            if (nuevo != null) //Le asignamos un padre
            { nuevo.Creador = arbol.Creador; }
        }

        public void Destruir(Nodo<T> destruido)
        {
            destruido.Derecha = null;
            destruido.Izquierda = null;
        }
        public IEnumerator<T> GetEnumerator()
        {
            var NodoAux = RaizAux;
            while (NodoAux != null)
            {
                yield return NodoAux.valor;
                NodoAux = NodoAux.Derecha;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        //Metodos de comparacion entre nodos que actuaran como delegados
        public static Comparison<Nodo<T>> CompareByName = delegate (Nodo<T> n1, Nodo<T> n2)
        {
            return n1.valor.CompareTo(n2.valor);
        };

        //Creacion del delegado
        public delegate int Buscador(Nodo<T> n1, Nodo<T> n2);

        //Instancia del tipo delegado y referencia al metodo
        Buscador MyDelegate = new Buscador(CompareByName);
    }
}
