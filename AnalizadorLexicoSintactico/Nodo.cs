using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorLexicoSintactico
{
    public class Nodo
    {
        public List<Arista> hijas = new List<Arista>();
        public List<Nodo> superhijas = new List<Nodo>();
        public Arista ancestro = new Arista();
        public Arista superpapa = new Arista();
        public String nombre;
        public Nodo(String nom)
        {
            nombre = nom;
        }
    }
}
