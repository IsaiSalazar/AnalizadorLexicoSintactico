using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorLexicoSintactico
{
    public class Automata : List<Estado>
    {
        public Estado inicio;
        public Estado aceptacion;
        public String posfija;
        protected char epsilon = 'ε';

        public String alfabeto;
        public void enumerate(Estado actual, int cont)
        {
            for(int i = 0; i<this.Count; i++)
            {
                this[i].nombre = i;
            }
        }
        public void limpiaAutomata()
        {
            foreach(Estado est in this)
            {
                est.visitado = false;
            }
        }


    }

    

}
