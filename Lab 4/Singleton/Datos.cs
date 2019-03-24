using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lab_4.Models;
using System.IO;
using System.Windows;

namespace Lab_4.Singleton
{
    public class Datos
    {
        private static Datos instancia = null;
        public static Datos Instancia
        {
            get
            {
                if(instancia == null)
                {
                    instancia = new Datos();
                }
                return instancia;
            }
        }

        public List<Med> ListaMed = new List<Med>();

        public List<Pedido> ListaPedidos = new List<Pedido>();

        public EstructurasLineales.ArbolBinario<Med> ArbolMed = new EstructurasLineales.ArbolBinario<Med>();

        public List<Med> MedEliminados = new List<Med>();

        public List<Med> MedAVender = new List<Med>();

        public List<Med> MedBuscados = new List<Med>();

        public double TotalPedido { get; set; }


        public int Buscador(Med a, Med b)
        {
            return a.Nombre.CompareTo(b.Nombre);
        }

        public void LecturaArchivo(string path)
        {
            string[] lineas = File.ReadAllLines(path);
            int contador = 0;
            char[] separadores = { ',' };

            foreach (var linea in lineas)
            {
                Med tmp = new Med();

                if (contador > 0)
                {
                    int i = 0;  //Variables para contador de id

                    //ID
                    while (linea[i] != ',')
                    {
                        i++;
                    }
                    tmp.id = int.Parse(linea.Substring(0, i));
                    i++;

                    //NOMBRE
                    int i2 = i; //Variables para contador de nombre
                    if (linea[i] == '"')
                    {
                        while (linea[i + 1] != '"') { i++; }
                        tmp.Nombre = linea.Substring(i2 + 1, i - i2);
                        i += 3;
                    }
                    else
                    {
                        while (linea[i] != ',')
                        {
                            i++;
                        }
                        tmp.Nombre = linea.Substring(i2, i - i2);
                        i++;
                    }


                    //DESCRIPCION
                    int i3 = i; //Variables para contador de la descripcion
                    if (linea[i] == '"')
                    {
                        while (linea[i + 1] != '"') { i++; }
                        tmp.Descripcion = linea.Substring(i3 + 1, i - i3);
                        i += 3;
                    }
                    else
                    {
                        while (linea[i] != ',')
                        {
                            i++;
                        }
                        tmp.Descripcion = linea.Substring(i3, i - i3);
                        i++;
                    }

                    //CASA PRODUCTORA
                    int i4 = i; //Variables para contador de la casa productora
                    if (linea[i] == '"')
                    {
                        while (linea[i + 1] != '"') { i++; }
                        tmp.Casa = linea.Substring(i4 + 1, i - i4);
                        i += 3;
                    }
                    else
                    {
                        while (linea[i] != ',')
                        {
                            i++;
                        }
                        tmp.Casa = linea.Substring(i4, i - i4);
                        i++;
                    }

                    //PRECIO
                    int i5 = i; //Variables para contador de la casa productora
                    if (linea[i] == '"')
                    {
                        while (linea[i + 1] != '"') { i++; }
                        tmp.Precio = double.Parse(linea.Substring(i5 + 2, i - i5 - 1));
                        i += 2;
                    }
                    else
                    {
                        while (linea[i] != ',')
                        {
                            i++;
                        }
                        tmp.Precio = double.Parse(linea.Substring(i5 + 1, i - i5 - 1));
                        i++;
                    }

                    //EXISTENCIA
                    if (linea[linea.Length - 2] == ',')
                    {
                        tmp.Existencia = int.Parse(linea.Substring(linea.Length - 1));
                    }
                    else
                    {
                        tmp.Existencia = int.Parse(linea.Substring(linea.Length - 2));
                    }

                    ListaMed.Add(tmp);
                    ArbolMed.AgregarNodoR(tmp);
                }
                else { contador++; }


            }

        }

    }
}