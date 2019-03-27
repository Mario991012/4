using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructurasLineales
{
    public class NodoB<T> : IEnumerable, IComparable
    {

        public int id { get; set; }
        public int padre { get; set; }
        public List<int> Hijos { get; set; }
        public List<string> meds { get; set; }

        public int max { get; set; }
        public int min { get; set; }
        public int cantidadDentro { get; set; }

        public static int buffer = 0;

        public string ToFixedLenghtString(NodoB<T> NodoAConvertir)
        {

            string hijos = "", Meds = "";
            for(int i = 0; i < NodoAConvertir.max; i++)
            {
                if(i < NodoAConvertir.cantidadDentro  && NodoAConvertir.Hijos.Count > 0)
                {
                    buffer += 4;
                    hijos += $"{string.Format("{0,5}", NodoAConvertir.Hijos[i])}|";
                }
                else
                {
                    hijos += "-----|";
                }
            }

            foreach (var items in NodoAConvertir.meds)
            {
                buffer += 49;
                Meds += $"{string.Format("{0,100}", items)}|";
            }
            buffer += 20;
            return $"{id.ToString("00000;-0000")}|{padre.ToString("00000;-0000")}|" + hijos + Meds;
        }

        public int FixedSizeText
        {
            get { return FixedSize; }
        }

        public static int FixedSize { get { return buffer; } }

        public NodoB()
        {
            id = 1;
            padre = 0;
            Hijos = new List<int>();
            meds = new List<string>();


            cantidadDentro = 0;
            min = 0;
            max = 0;

        }

        public void AsignandoGrado(int grado)
        {
            max = (int)Math.Ceiling((double)grado - 1);
            min = (int)Math.Ceiling((double)(grado - 1) / 2);
        }

        public int AsignandoID(int id)
        {
            return id + 1;
        }


        public int CompareTo(object obj)
        {
            var comparable = (NodoB<T>)obj;
            return meds.First().CompareTo(comparable.meds.First());
        }

        public IEnumerator GetEnumerator()
        {
            bool[] recorridos = new bool[4] { false, false, false, false };

            if (recorridos[0] == false)
            {
                recorridos[0] = true;
                yield return id;
            }
            else if (recorridos[1] == false)
            {
                recorridos[1] = true;
                yield return padre;
            }
            else if (recorridos[2] == false)
            {
                recorridos[2] = true;
                yield return Hijos;
            }
            else if (recorridos[3] == false)
            {
                recorridos[3] = true;
                yield return meds;
            }

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
