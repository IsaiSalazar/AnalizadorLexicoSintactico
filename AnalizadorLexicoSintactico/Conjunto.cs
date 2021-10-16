using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorLexicoSintactico
{
    public class Conjunto:Estado
    {
        public List<String> elementos;
        public bool marcado = false;
        public Conjunto(int num) : base(num)
        {
            elementos = new List<string>();
        }
    }
}
