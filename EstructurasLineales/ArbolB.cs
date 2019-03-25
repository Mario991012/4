using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructurasLineales
{
    public class ArbolB<T>: IEnumerable<T> where T: IComparable
    {
        public NodoB<T> Raiz { get; set; }

        public ArbolB(int grado) => Raiz = new NodoB<T>(grado);


        public bool NodoVacio(NodoB<T> Nodo)
        {
            bool Vacio = true;
            foreach(var item in Nodo.meds)
            {
                if(item != null)
                {
                    Vacio = false;
                }
            }
            return Vacio;
        }

        public bool HayNodosHijo(NodoB<T> Nodo, List<T> Lista)
        {
            
            if(Nodo.hijos.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }

        public bool ExisteEspacio(NodoB<T> Nodo)
        {
            if(Nodo.cantidadDentro <= Nodo.max)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }


        public int Comparador(NodoB<T> Nodo, T valor)
        {
            int NodoEncontrado = 0;
            if(Nodo.meds.Count <= 1)
            {
                if (valor.CompareTo(Nodo.meds[0]) < 0)
                {
                    NodoEncontrado = 0;
                }else
                {
                    NodoEncontrado = 1;
                }
            }
            else
            {
                for (int i = 0; i < Nodo.cantidadDentro; i++)
                {
                    if (valor.CompareTo(Nodo.meds[i]) < 0)
                    {
                        NodoEncontrado = i;
                        break;
                    }
                    else if (valor.CompareTo(Nodo.meds[i]) > 0 && valor.CompareTo(Nodo.meds[i + 1]) < 0)
                    {
                        NodoEncontrado = i + 1;
                        break;
                    }
                }
            }
            

            return NodoEncontrado;
        }

        public void Agregar(T valor, ref NodoB<T> Nodo, int grado)
        {
            //var nodo = new NodoB<T>(grado, valor);

            if (NodoVacio(Nodo) == true)
            {
                Nodo.meds.Add(valor);
                Nodo.cantidadDentro++;
            }
            else if(ExisteEspacio(Nodo) == true && HayNodosHijo(Nodo,Nodo.meds) == false)
            {
                Nodo.meds.Add(valor);
                Nodo.cantidadDentro++;
                Nodo.meds.Sort();
            }
            else if (ExisteEspacio(Nodo) == true && HayNodosHijo(Nodo,Nodo.meds) == true)
            {
                Nodo.EsRaiz = true;
                var nodo = Nodo.hijos[Comparador(Nodo, valor)];
                Agregar(valor, ref nodo, grado);
            }

            if (ExisteEspacio(Nodo) == false)
            {
                Separar(ref Nodo, valor);
            }


        }

        public void Separar(ref NodoB<T> Nodo, T valor)
        {
            var NuevoNodoPadre = new NodoB<T>(Nodo.max + 1);
            var NuevoNodoIzq = new NodoB<T>(Nodo.max + 1);
            var NuevoNodoDer = new NodoB<T>(Nodo.max + 1);

            for(int i = 0; i < (Nodo.max/2); i++)
            {
                NuevoNodoIzq.meds.Add(Nodo.meds[i]);
                NuevoNodoIzq.cantidadDentro++;
            }
            for (int i = (Nodo.max / 2) + 1; i <= Nodo.max; i++)
            {
                NuevoNodoDer.meds.Add(Nodo.meds[i]);
                NuevoNodoDer.cantidadDentro++;
            }

            NuevoNodoPadre.meds.Add(Nodo.meds[(Nodo.max / 2)]);
            NuevoNodoIzq.Padre = NuevoNodoPadre;
            NuevoNodoDer.Padre = NuevoNodoPadre;
            NuevoNodoPadre.Padre = Nodo.Padre;
            NuevoNodoPadre.hijos.Add(NuevoNodoIzq);
            NuevoNodoPadre.hijos.Add(NuevoNodoDer);
            Nodo = NuevoNodoPadre;
            Nodo.EsRaiz = true;
            Nodo.cantidadDentro = NuevoNodoPadre.meds.Count();
            
        }

        public void CreandoIzquierdo(NodoB<T> Nodo)
        {

        }

        public void CreandoDerecho(NodoB<T> Nodo)
        {

        }

        //agrega el med al nodo actual
        public void AgregarANodo(T valor, NodoB<T> Nodo)
        {
            Nodo.meds.Add(valor);
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
