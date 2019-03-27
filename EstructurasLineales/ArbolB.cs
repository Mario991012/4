using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Collections;
using Newtonsoft.Json;

namespace EstructurasLineales
{

    public class ArbolB<T>
    {

        public NodoB<T> Raiz { get; set; }

        public ArbolB()
        {
            Raiz = new NodoB<T>();
        }


        public void Escritura(NodoB<T> Nodo, string ubicacion)
        {
            
            string hijos = "";
            string meds = "";
            for (int i = 0; i < Nodo.max; i++)
            {
                if (i < Nodo.cantidadDentro)
                {
                    if (Nodo.Hijos.Count == 0)
                    {
                        hijos = "-#";
                    }
                    else
                    {
                        hijos += Nodo.Hijos[i];
                    }
                    meds = Nodo.meds[i] + "#";
                }
                else
                {
                    hijos += "-#";
                    meds += "-#";
                }
            }
            using (StreamWriter fs = File.AppendText(ubicacion))
            {
                fs.WriteLine($"#{Nodo.id.ToString()}#{Nodo.padre.ToString()}#{hijos}{meds}|");
            }




            
        }





        //string[] lineas = File.ReadAllLines(ubicacion);
        //foreach (var linea in lineas)
        //{
        //    if (linea[1] == Nodo.id)
        //    {
        //        using (StreamWriter nodos = new StreamWriter(ubicacion))
        //        {
        //            nodos.WriteLine("|{0}|{1}|{2}{3}", Nodo.id.ToString(), Nodo.padre.ToString(), hijos, meds);
        //        }
        //    }
        //}

        //using (var fs = new FileStream(ubicacion, FileMode.OpenOrCreate))
        //{
        //    foreach (var item in Nodo)
        //    {
        //        fs.Write(ByteGenerator.ConvertToBytes(item.ToFixedLenghtString()), 0, NodoB<T>.FixedSize);
        //    }
        //}



        //string NodoNuevo = JsonConvert.SerializeObject(Nodo);
        //string hijos = "";
        //string meds = "";
        //for(int i = 0; i < Nodo.max; i++)
        //{
        //    if(i < Nodo.cantidadDentro)
        //    {
        //        if(Nodo.Hijos.Count == 0)
        //        {
        //            hijos = "-|";
        //        }
        //        else
        //        {
        //            hijos += Nodo.Hijos[i];
        //        }
        //        meds = Nodo.meds[i] + "|";
        //    }
        //    else
        //    {
        //        hijos += "-|";
        //        meds += "-|";
        //    }
        //}

        //using (StreamWriter nodos = File.AppendText(ubicacion))
        //{
        //    nodos.WriteLine("|{0}|{1}|{2}{3}", Nodo.id.ToString(), Nodo.padre.ToString(), hijos, meds);
        //    nodos.WriteLine(NodoNuevo);
        //}


        public void Lectura(string ubicacion)
        {
            

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

        //public int Comparador(NodoB<T> Nodo, string nombre)
        //{
        //    int NodoEncontrado = Nodo.cantidadDentro;
        //    if(Nodo.Meds.Count <= 1)
        //    {
        //        if (nombre.CompareTo(Nodo.Meds[0].Nombre) < 0)
        //        {
        //            NodoEncontrado = 0;
        //        }else
        //        {
        //            NodoEncontrado = 1;
        //        }
        //    }
        //    else
        //    {
        //        for (int i = 0; i < Nodo.cantidadDentro -1; i++)
        //        {
        //            if (nombre.CompareTo(Nodo.Meds[i].Nombre) < 0)
        //            {
        //                NodoEncontrado = i;
        //                break;
        //            }
        //            else if (nombre.CompareTo(Nodo.Meds[i].Nombre) > 0 && nombre.CompareTo(Nodo.Meds[i + 1].Nombre) < 0)
        //            {
        //                NodoEncontrado = i + 1;
        //                break;
        //            }
        //        }
        //    }
            

        //    return NodoEncontrado;
        //}

        public void Separar(NodoB<T> Nodo, string ubicaciontxt)
        {
            var NuevoNodoDer = new NodoB<T>();
            NuevoNodoDer.AsignandoGrado(Nodo.max + 1);
            NuevoNodoDer.id = Nodo.id + 1;

            for (int i = (Nodo.max / 2) + 1; i <= Nodo.max; i++)
            {
                NuevoNodoDer.meds.Add(Nodo.meds[i]);
                NuevoNodoDer.cantidadDentro++;
            }

            var NuevoNodoPadre = new NodoB<T>();
            NuevoNodoPadre.AsignandoGrado(Nodo.max + 1);
            NuevoNodoPadre.id = NuevoNodoDer.id + 1;
            NuevoNodoPadre.meds.Add(Nodo.meds[Nodo.max / 2]);



            Escritura(Nodo, ubicaciontxt);
            Escritura(NuevoNodoDer, ubicaciontxt);
            Escritura(NuevoNodoPadre, ubicaciontxt);


            //if (Nodo.padre != 0)
            //{
            //    NuevoNodoPadre.Meds.Add(Nodo.Meds[(Nodo.max / 2)]);
            //    Nodo = NuevoNodoPadre;
            //    NuevoNodoIzq.Padre = Nodo;
            //    NuevoNodoDer.Padre = Nodo;
            //    Nodo.hijos.Add(NuevoNodoIzq);
            //    Nodo.hijos.Add(NuevoNodoDer);
            //    NuevoNodoIzq.posicion = NuevoNodoIzq.AsignandoPosicion(Nodo.posicion);
            //    NuevoNodoDer.posicion = NuevoNodoDer.AsignandoPosicion(NuevoNodoIzq.posicion);
            //    Nodo.EsRaiz = true;
            //    Nodo.cantidadDentro = NuevoNodoPadre.Meds.Count();
            //    Nodo.hijos.Sort((x, y) => x.CompareTo(y));
            //}
            //else if (Nodo.Padre != null)
            //{
            //    Nodo.Padre.hijos.Add(NuevoNodoIzq);
            //    Nodo.Padre.hijos.Add(NuevoNodoDer);
            //    Nodo.Padre.hijos.Remove(Nodo);

            //    Nodo.Padre.Meds.Add(Nodo.Meds[(Nodo.max / 2)]);

            //    Nodo.Padre.Meds.Sort((x, y) => x.CompareTo(y));

            //    Nodo.Padre.cantidadDentro = Nodo.Padre.Meds.Count();

            //    NuevoNodoIzq.posicion = NuevoNodoIzq.AsignandoPosicion(Nodo.posicion);
            //    NuevoNodoDer.posicion = NuevoNodoDer.AsignandoPosicion(NuevoNodoIzq.posicion);

            //    Nodo = Nodo.Padre;
            //    Nodo.EsRaiz = true;

            //    NuevoNodoIzq.Padre = NuevoNodoDer.Padre = Nodo;
            //    Nodo.hijos.Sort((x, y) => x.CompareTo(y));
            //}


            //if (Nodo.cantidadDentro > Nodo.max)
            //{
            //    var nodo = Nodo;
            //    Separar(nodo);
            //    Nodo = nodo;
            //}
        }

        public void Agregar(string ubicaciontxt, string nombre, int id, ref NodoB<T> Nodo)
        {

            if (ExisteEspacio(Nodo) == true && HayNodosHijo(Nodo) == false)
            {
                Nodo.meds.Add(nombre);
                Nodo.cantidadDentro++;
                Escritura(Nodo, ubicaciontxt);
            }
            //else if (ExisteEspacio(Nodo) == true && HayNodosHijo(Nodo) == true)
            //{
            //    var nodo = new NodoB<T>();
            //    nodo = Nodo.hijos[Comparador(Nodo, nombre)];
            //    nodo.posicion = nodo.AsignandoPosicion(Nodo.posicion);
            //    Agregar(ubicaciontxt, nombre, id, ref nodo);
            //}

            

            if (ExisteEspacio(Nodo) == false)
            {
                
                Separar(Nodo, ubicaciontxt);
            }
        }


        //agrega el med al nodo actual
        //public void AgregarYOrdenarANodo(string nombre, int id, NodoB<T> Nodo)
        //{
        //    Info info = new Info();
        //    info.Nombre = nombre;
        //    info.id = id;

        //    Nodo.Meds.Add(info);

        //    Nodo.Meds.Sort((x,y) => x.CompareTo(y));

        //    Nodo.cantidadDentro++;
        //}


        public bool ExisteUnderFlow(NodoB<T> Nodo)
        {
            if (Nodo.min < Nodo.cantidadDentro)
            { return true; }
            else
            { return false; }
        }

    }
}
