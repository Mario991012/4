using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace EstructurasLineales
{

    public class ArbolB<T>: IEnumerable<T> where T: IComparable
    {

        public NodoB<T> Raiz { get; set; }

        public ArbolB()
        {
            Raiz = new NodoB<T>();
        }


        public bool NodoVacio(NodoB<T> Nodo)
        {
            if(Nodo.Meds.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool HayNodosHijo(NodoB<T> Nodo)
        {
            
            if(Nodo.Hijos.Count == 0)
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

        public int Buscador(string buscado, NodoB<T> Raiz)
        {
            int NodoEncontrado = -1;
            bool encontrado = false;
            foreach(var item in Raiz.Meds)
            {
                if(buscado.CompareTo(item.Nombre) == 0)
                {
                    NodoEncontrado = item.id;
                    encontrado = true;
                    break;
                }
            }

            if(encontrado == false)
            {
                NodoEncontrado = Buscador(buscado, Raiz.Hijos[Comparador(Raiz, buscado)]);
            }
            

            return NodoEncontrado;
        }

        public int Comparador(NodoB<T> Nodo, string nombre)
        {
            int NodoEncontrado = Nodo.cantidadDentro;
            if(Nodo.Meds.Count <= 1)
            {
                if (nombre.CompareTo(Nodo.Meds[0].Nombre) < 0)
                {
                    NodoEncontrado = 0;
                }else
                {
                    NodoEncontrado = 1;
                }
            }
            else
            {
                for (int i = 0; i < Nodo.cantidadDentro -1; i++)
                {
                    if (nombre.CompareTo(Nodo.Meds[i].Nombre) < 0)
                    {
                        NodoEncontrado = i;
                        break;
                    }
                    else if (nombre.CompareTo(Nodo.Meds[i].Nombre) > 0 && nombre.CompareTo(Nodo.Meds[i + 1].Nombre) < 0)
                    {
                        NodoEncontrado = i + 1;
                        break;
                    }
                }
            }
            

            return NodoEncontrado;
        }

        public void Separar(ref NodoB<T> Nodo)
        {
            var NuevoNodoPadre = new NodoB<T>();
            NuevoNodoPadre.AsignandoGrado(Nodo.max + 1);
            var NuevoNodoIzq = new NodoB<T>();
            NuevoNodoIzq.AsignandoGrado(Nodo.max + 1);
            var NuevoNodoDer = new NodoB<T>();
            NuevoNodoDer.AsignandoGrado(Nodo.max + 1);

            for (int i = 0; i < (Nodo.max / 2); i++)
            {
                NuevoNodoIzq.Meds.Add(Nodo.Meds[i]);
                NuevoNodoIzq.cantidadDentro++;
            }
            for (int i = (Nodo.max / 2) + 1; i <= Nodo.max; i++)
            {
                NuevoNodoDer.Meds.Add(Nodo.Meds[i]);
                NuevoNodoDer.cantidadDentro++;
            }

            if(Nodo.Padre == null)
            {
                NuevoNodoPadre.Meds.Add(Nodo.Meds[(Nodo.max / 2)]);
                Nodo = NuevoNodoPadre;
                NuevoNodoIzq.Padre = Nodo;
                NuevoNodoDer.Padre = Nodo;
                Nodo.Hijos.Add(NuevoNodoIzq);
                Nodo.Hijos.Add(NuevoNodoDer);
                NuevoNodoIzq.id = NuevoNodoIzq.AsignandoPosicion(Nodo.id);
                NuevoNodoDer.id = NuevoNodoDer.AsignandoPosicion(NuevoNodoIzq.id);
                Nodo.EsRaiz = true;
                Nodo.cantidadDentro = NuevoNodoPadre.Meds.Count();
                Nodo.Hijos.Sort((x, y) => x.CompareTo(y));

            }
            else if(Nodo.Padre != null)
            {
                Nodo.Padre.Hijos.Add(NuevoNodoIzq);
                Nodo.Padre.Hijos.Add(NuevoNodoDer);
                Nodo.Padre.Hijos.Remove(Nodo);

                Nodo.Padre.Meds.Add(Nodo.Meds[(Nodo.max / 2)]);

                Nodo.Padre.Meds.Sort((x, y) => x.CompareTo(y));
                
                Nodo.Padre.cantidadDentro = Nodo.Padre.Meds.Count();

                NuevoNodoIzq.id = NuevoNodoIzq.AsignandoPosicion(Nodo.id);
                NuevoNodoDer.id = NuevoNodoDer.AsignandoPosicion(NuevoNodoIzq.id);

                Nodo = Nodo.Padre;
                Nodo.EsRaiz = true;

                NuevoNodoIzq.Padre = NuevoNodoDer.Padre = Nodo;
                Nodo.Hijos.Sort((x, y) => x.CompareTo(y));
            }
            

            if (Nodo.cantidadDentro > Nodo.max)
            {
                var nodo = Nodo;
                Separar(ref nodo);
                Nodo = nodo;
            }
        }

        public void Agregar(string nombre, int id, ref NodoB<T> Nodo,string ubicacion)
        {
            if(ExisteEspacio(Nodo) == true && HayNodosHijo(Nodo) == false)
            {
                AgregarYOrdenarANodo(nombre, id, Nodo);
            }
            else if (ExisteEspacio(Nodo) == true && HayNodosHijo(Nodo) == true)
            {
                Nodo.EsRaiz = true;
                var nodo = new NodoB<T>();
                nodo = Nodo.Hijos[Comparador(Nodo, nombre)];
                nodo.id = nodo.AsignandoPosicion(Nodo.id);
                Agregar(nombre, id, ref nodo, ubicacion);
            }

            if (ExisteEspacio(Nodo) == false)
            {
                Separar(ref Nodo);
            }

            using (StreamWriter sw = File.AppendText(ubicacion))
            {
                sw.Write(Nodo.InformacionNodo());
            }
        }


        //agrega el med al nodo actual
        public void AgregarYOrdenarANodo(string nombre, int id, NodoB<T> Nodo)
        {
            Info info = new Info();
            info.Nombre = nombre;
            info.id = id;

            Nodo.Meds.Add(info);

            Nodo.Meds.Sort((x,y) => x.CompareTo(y));

            Nodo.cantidadDentro++;
        }


        public bool ExisteUnderFlow(NodoB<T> Nodo)
        {
            if (Nodo.min < Nodo.cantidadDentro)
            { return true; }
            else
            { return false; }
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
