using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorLexicoSintactico
{
    public class Estado
    {
       private List<Transicion> TRANSICIONES;
        public List<Transicion> transiciones { set { TRANSICIONES = value; } get { return TRANSICIONES; } }
        private int NOMBRE;
        private char NOMBRECHAR;
        private bool VISITADO = false;
        private int COLORSITO = 0;
        private int ANTERIOR = -1;
        private int PESO = 1000;
        
        private bool ETIQUETADO = false;

        public int nombre { set { NOMBRE = value; } get { return NOMBRE; } }
        public char nombrechar { set { NOMBRECHAR = value; } get { return NOMBRECHAR; } }
        public bool etiquetado { set { ETIQUETADO = value; } get { return ETIQUETADO; } }
        public int colorsito { set { COLORSITO = value; } get { return COLORSITO; } }
   
        public bool visitado { set { VISITADO = value; } get { return VISITADO; } }
     

        public int anterior { set { ANTERIOR = value; } get { return ANTERIOR; } }

         public int peso { set { PESO = value; } get { return PESO; } }


        public Estado(int n)
        {
            TRANSICIONES = new List<Transicion>();
            NOMBRE = n;
            
        }
        public Estado(Char  c)
        {
            TRANSICIONES = new List<Transicion>();
            NOMBRECHAR = c;
        }
    }
}

