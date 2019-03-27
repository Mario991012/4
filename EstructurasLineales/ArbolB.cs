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

        public static string stringMed = "";
        public static int CantidadDeNodosActuales = 1;

        public void Escritura(NodoB<T> Nodo, string ubicacion)
        {
            //using (var fs = new FileStream(ubicacion, FileMode.OpenOrCreate))
            //{
            //        fs.Write(ByteGenerator.ConvertToBytes(Nodo.ToFixedLenghtString(Nodo)), 0, NodoB<T>.FixedSize);
            //}


            //NodoB<T> NodoAux1 = Nodo;
            //int contador = 0;
            //string[] lineas = File.ReadAllLines(ubicacion);

            //using (StreamReader siguiente = new StreamReader(ubicacion))
            //{
            //    foreach(var linea in lineas)
            //    {
            //        int idSiguiente = int.Parse(linea.Split('|')[0].Trim());
            //        if (idSiguiente == contador + 1)
            //        {
            //            NodoAux1.id = idSiguiente;
            //            NodoAux1.padre = int.Parse(linea.Split('|')[1].Trim());
            //            if(linea.Split('|')[2] != "-----")
            //            {
            //                for(int i = 0; i < 4; i++)
            //                {
            //                    int hijo = int.Parse(linea.Split('|')[i]);
            //                    NodoAux1.Hijos.Add(hijo);
                                
            //                }
                            
            //            }

            //            string med = linea.Split('|')[6].Trim();
                        

            //        }
            //    }
            //}

            using (StreamWriter sw = new StreamWriter(ubicacion))
            {
                

                //while (contador < siguientePosicion)
                //{
                    sw.WriteLine((Nodo.ToFixedLenghtString(Nodo)));
                //    contador++;
                //}
            }
             
        }


        //string NodoNuevo = JsonConvert.SerializeObject(Nodo);
        
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


        public static int siguientePosicion = 1;

        public void Separar(NodoB<T> Nodo, string ubicaciontxt)
        {
            siguientePosicion++;
            var NuevoNodoDer = new NodoB<T>();
            NuevoNodoDer.AsignandoGrado(Nodo.max + 1);
            NuevoNodoDer.max = Nodo.max;
            NuevoNodoDer.min = Nodo.min;
            
            NuevoNodoDer.id = siguientePosicion;
           
            
            for (int i = (Nodo.max / 2) + 1; i <= Nodo.max; i++)
            {
                NuevoNodoDer.meds.Add(Nodo.meds[i]);
            }
            NuevoNodoDer.cantidadDentro = NuevoNodoDer.meds.Count();


            siguientePosicion++;
            var NuevoNodoPadre = new NodoB<T>();
            NuevoNodoPadre.AsignandoGrado(Nodo.max + 1);
            NuevoNodoPadre.id = siguientePosicion;
            NuevoNodoPadre.meds.Add(Nodo.meds[Nodo.max / 2]);
            NuevoNodoPadre.cantidadDentro = NuevoNodoPadre.meds.Count();
            NuevoNodoPadre.Hijos.Add(Nodo.id);
            NuevoNodoPadre.Hijos.Add(NuevoNodoDer.id);

            NuevoNodoDer.padre = NuevoNodoPadre.id;
            Nodo.padre = NuevoNodoPadre.id;

            for (int i = Nodo.max; i >= Nodo.max / 2; i--)
            {
                Nodo.meds.Remove(Nodo.meds[i]);
            }
            Nodo.cantidadDentro = Nodo.meds.Count();

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
                Nodo.meds.Sort();
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
