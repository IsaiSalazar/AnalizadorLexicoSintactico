using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorLexicoSintactico
{
    public class Transicion
    {
        private char ETIQUETA;
        private String NOMBRE;
        public String nombre { set { NOMBRE = value; } get { return NOMBRE; } }
        public char etiqueta { set { ETIQUETA = value; } get { return ETIQUETA; } }
        private Estado DESTINO;
        public Estado destino { set { DESTINO = value; } get { return DESTINO; } }
        private Estado ORIGEN;
        public Estado origen { set { ORIGEN = value; } get { return ORIGEN; } }
        //  private Pen colornodo;
        // public Pen colorA { set { colornodo = value; } get { return colornodo; } }

        public bool visitado = false;
        public Transicion(char p) //int
        {
            ETIQUETA = p;
            // colorA = new Pen(new SolidBrush(Color.Black));
        }
        public Transicion(String p) //int
        {
            NOMBRE = p;
            // colorA = new Pen(new SolidBrush(Color.Black));
        }

    }
}
