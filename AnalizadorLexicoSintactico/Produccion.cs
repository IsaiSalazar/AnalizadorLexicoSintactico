using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorLexicoSintactico
{
    public class Produccion
    {
        public String encabezado;
        public List<String> cuerpo = new List<string>();
        public List<String> primeros = new List<String>();
        public List<String> siguientes = new List<String>();
        public int num;
        public Produccion(String Encabezado, String Cuerpo, String Primero, String Siguiente)
        {
            encabezado = Encabezado;
            cuerpo.Add(Cuerpo);
            primeros.Add(Primero);
            siguientes.Add(Siguiente);

        }
        public Produccion(String Encabezado)
        {
            encabezado = Encabezado;
        }

        public string GetEncabezado()
        {
            return encabezado;
        }

        public void SetEncabezado(string value)
        {
            encabezado = value;
        }

        public void addCuerpo(String Cuerpo)
        {
            cuerpo.Add(Cuerpo);
        }
        public void addPrimero(String Primero)
        {
            primeros.Add(Primero);
        }
        public void addSiguiente(String Siguiente)
        {
            String[] fragmento = Siguiente.Split(' '); 
            foreach(String fr in fragmento)
            {
                siguientes.Add(fr);
            }
           
        }
    }
}
