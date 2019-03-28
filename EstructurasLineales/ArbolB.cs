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

            //IMPRESION
            using (StreamWriter sw = File.AppendText(ubicacion))
            {
                sw.WriteLine((Nodo.ToFixedLenghtString(Nodo)));
            }
            
            //INORDEN RETORNANDO EL ID DEL SIGUIENTE NODO
            //CARGAR EL NODO SIGUIENTE CON EL METODO "Lectura" MANDANDO DE PARAMETROS EL NODO ACTUAL, EL ID OBTENIDO EN EL
            //PASO ANTERIOR Y LA UBICACION DEL TEXTO
            //LLAMAR CON RECURSIVIDAD A "Escritura" MANDANDO DE PARAMETROS EL NODO CARGADO Y LA UBICACION DEL TXT

        }

        //string NodoNuevo = JsonConvert.SerializeObject(Nodo);      

        public NodoB<T> Lectura(NodoB<T> Nodoactual, int ID, string ubicaciontxt)
        {
            string[] lineas = File.ReadAllLines(ubicaciontxt);

            NodoB<T> nodoNuevo = new NodoB<T>();
            foreach (var linea in lineas)
            {

                if (ID == int.Parse(linea.Split('|')[0]))
                {
                    nodoNuevo.id = ID;
                    nodoNuevo.padre = int.Parse(linea.Split('|')[1].Trim());
                    nodoNuevo.max = int.Parse(linea.Split('|')[2].Trim());
                    nodoNuevo.min = int.Parse(linea.Split('|')[3].Trim());
                    nodoNuevo.cantidadDentro = int.Parse(linea.Split('|')[4].Trim());

                    for (int i = 5; i <= 8; i++)
                    {
                        if (linea.Split('|')[i] != "-----")
                        {
                            nodoNuevo.Hijos.Add(int.Parse(linea.Split('|')[i].Trim()));
                        }
                    }

                    for (int i = 9; i < 11; i++)
                    {
                        if (linea.Split('|')[i].Trim() != "")
                        {
                            nodoNuevo.meds.Add(linea.Split('|')[i].Trim());
                        }
                    }
                    break;
                }
            }
            return nodoNuevo;
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
        public static NodoB<T> NuevaRaiz = new NodoB<T>();
        public static NodoB<T> NuevoNodoPadre = new NodoB<T>();
        public static NodoB<T> NuevoNodoDer = new NodoB<T>();
        public static NodoB<T> NuevoNodoIzq = new NodoB<T>();

        public NodoB<T> Separar(ref NodoB<T> Nodo, string ubicaciontxt, string nombre)
        {
            
            if(Nodo.padre == 0)
            {
                siguientePosicion++;
               
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
                NuevaRaiz = NuevoNodoPadre;
            }
            else
            {
                //Separar y subir el dato a la raiz del nodo que se separo :(
            }
            NuevoNodoIzq = NuevaRaiz;
            return NuevaRaiz;
        }

        public int Comparador(NodoB<T> Nodo, string nombre)
        {
            int NodoEncontrado = Nodo.cantidadDentro;
            if (Nodo.meds.Count <= 1)
            {
                if (nombre.CompareTo(Nodo.meds[0]) < 0)
                {
                    NodoEncontrado = 0;
                }
                else
                {
                    NodoEncontrado = 1;
                }
            }
            else
            {
                for (int i = 0; i < Nodo.cantidadDentro - 1; i++)
                {
                    if (nombre.CompareTo(Nodo.meds[i]) < 0)
                    {
                        NodoEncontrado = i;
                        break;
                    }
                    else if (nombre.CompareTo(Nodo.meds[i]) > 0 && nombre.CompareTo(Nodo.meds[i + 1]) < 0)
                    {
                        NodoEncontrado = i + 1;
                        break;
                    }
                }
            }


            return NodoEncontrado;
        }


        public void Agregar(string ubicaciontxt, string nombre, int id, ref NodoB<T> Nodo)
        {
            //Si existe espacio en el nodo y no hay hijos (es decir que es una hoja) entonces se agrega a la hoja
            if (ExisteEspacio(Nodo) == true && HayNodosHijo(Nodo) == false)
            {
                Nodo.meds.Add(nombre);
                Nodo.meds.Sort();
                Nodo.cantidadDentro++;
            }//Si existe espacio pero es una raiz se pasa al nodo correspondiente a agregar el valorts
            else if (ExisteEspacio(Nodo) == true && HayNodosHijo(Nodo) == true)
            {
                //Retorna la posicion de la lista del padre, del hijo correspondiente
                int IDNodo = Nodo.Hijos[Comparador(Nodo, nombre)];

                //SE DEBE OBTENER EL ID DEL HIJO CORRESPONDIENTE YA QUE SOLO ENVÍA LA POSICION EN LA LISTA
                //DEL PADRE 

                //Se carga el archivo
                Nodo =  Lectura(Nodo, IDNodo, ubicaciontxt);
                
                Agregar(ubicaciontxt, nombre, id, ref Nodo);
            }



            if (ExisteEspacio(Nodo) == false)
            {
                Nodo = Separar(ref Nodo, ubicaciontxt, nombre);
            }
        }

        //Para eliminacion
        public bool ExisteUnderFlow(NodoB<T> Nodo)
        {
            if (Nodo.min < Nodo.cantidadDentro)
            { return true; }
            else
            { return false; }
        }
        
    }
}
