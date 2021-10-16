using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalizadorLexicoSintactico
{
    public class LR0
    {
        public String[,] tablaAFD;
        public ColoeccionCanonica automata;
        public List<String> original = new List<string>();
        public List<Produccion> gramatica;
        Produccion prod0 = new Produccion("P'"); //encabezado
        Produccion prod1 = new Produccion("P");
        Produccion prod2 = new Produccion("S");
        Produccion prod3 = new Produccion("T");
        Produccion prod4 = new Produccion("I");
        Produccion prod5 = new Produccion("R");
        Produccion prod6 = new Produccion("A");
        Produccion prod7 = new Produccion("D");
        Produccion prod8 = new Produccion("W");
        Produccion prod9 = new Produccion("X");
        Produccion prod10 = new Produccion("O");
        Produccion prod11 = new Produccion("M");
        Produccion prod12 = new Produccion("U");
        Produccion prod13 = new Produccion("E");
        Produccion prod14 = new Produccion("L");
        Produccion prod15 = new Produccion("F");

        public LR0()
        {
            int tam;
            int cont = 1;
            bool prim = false;
            this.prod0.addCuerpo("P");

            this.prod1.addCuerpo("S");

            this.prod2.addCuerpo("S ; T");
            this.prod2.addCuerpo("T");

            this.prod3.addCuerpo("I");
            this.prod3.addCuerpo("R");
            this.prod3.addCuerpo("A");
            this.prod3.addCuerpo("D");
            this.prod3.addCuerpo("W");

            this.prod4.addCuerpo("i X t S e");
            this.prod4.addCuerpo("i X t S l S e");

            this.prod5.addCuerpo("r S u X");

            this.prod6.addCuerpo("d , X");

            this.prod7.addCuerpo("a d");

            this.prod8.addCuerpo("w X");

            this.prod9.addCuerpo("M O M");
            this.prod9.addCuerpo("M");

            this.prod10.addCuerpo("<");
            this.prod10.addCuerpo(">");
            this.prod10.addCuerpo("=");

            this.prod11.addCuerpo("M U E");
            this.prod11.addCuerpo("E");

            this.prod12.addCuerpo("+");
            this.prod12.addCuerpo("-");

            this.prod13.addCuerpo("E L F");
            this.prod13.addCuerpo("F");

            this.prod14.addCuerpo("*");
            this.prod14.addCuerpo("/");

            this.prod15.addCuerpo("( X )");
            this.prod15.addCuerpo("n");
            this.prod15.addCuerpo("d");

            // primeros por ahora pendientes

            this.prod1.addPrimero("i r d a w");
            this.prod2.addPrimero("i r d a w");

            this.prod3.addPrimero("i r d a w");
            this.prod4.addPrimero("i");
            this.prod5.addPrimero("r");
            this.prod6.addPrimero("d");
            this.prod7.addPrimero("a");
            this.prod8.addPrimero("w");

            this.prod9.addPrimero("( n d");

            this.prod10.addPrimero("< > =");

            this.prod11.addPrimero("( n d");

            this.prod12.addPrimero("+ -");
            this.prod13.addPrimero("( n d");
            this.prod14.addPrimero("* /");
            this.prod15.addPrimero("( n d");

            //siguientes
            this.prod1.addSiguiente("$");
            this.prod2.addSiguiente("$ ; e l u");

            this.prod3.addSiguiente("$ ; e l u");
            this.prod4.addSiguiente("$ ; e l u");
            this.prod5.addSiguiente("$ ; e l u");
            this.prod6.addSiguiente("$ ; e l u");
            this.prod7.addSiguiente("$ ; e l u");
            this.prod8.addSiguiente("$ ; e l u");

            this.prod9.addSiguiente("t $ ; ) e l u");

            this.prod10.addSiguiente("( n d");

            this.prod11.addSiguiente("< > = t $ ; e l u ) + -");
            this.prod12.addSiguiente("( n d");
            this.prod13.addSiguiente("< > = t $ ; e l u ) + - * /");
            this.prod14.addSiguiente("( n d");
            this.prod15.addSiguiente("< > = t $ ; e l u ) + - * /");

            this.gramatica = new List<Produccion> {
            this.prod0,
            this.prod1,
            this.prod2,
            this.prod3,
            this.prod4,
            this.prod5,
            this.prod6,
            this.prod7,
            this.prod8,
            this.prod9,
            this.prod10,
            this.prod11,
            this.prod12,
            this.prod13,
            this.prod14,
            this.prod15};
            foreach(Produccion prod in gramatica)
            {
                if (prim)
                {
                    prod.num = cont;
                    foreach (String ele in prod.cuerpo)
                    {
                        cont++;
                    }
                }
                prim = true;
            }
            automata = new ColoeccionCanonica(this.gramatica);
            obtenOriginal(automata);
            tam = automata.simbolos.Count + 1;
            tablaAFD = new String[automata.Count + 1, tam];
            generaTablaAFD(automata);
        }

        private void generaTablaAFD(ColoeccionCanonica auto)
        {
            for (int i = 1; i < tablaAFD.GetLength(1); i++)
            {
                tablaAFD[0, i] = auto.simbolos[i - 1];
            }
            for (int j = 1; j < tablaAFD.GetLength(0); j++)
            {
                tablaAFD[j, 0] = auto[j - 1].nombre.ToString();
            }

            for (int i = 1; i < tablaAFD.GetLength(0); i++)
            {
                for (int j = 1; j < tablaAFD.GetLength(1); j++)
                {
                    foreach (Transicion aux in auto[i - 1].transiciones)
                    {
                        if (aux.nombre == tablaAFD[0, j])
                        {
                            tablaAFD[i, j] = aux.destino.nombre.ToString();
                        }
                    }
                }
            }
        }

        public void obtenOriginal(ColoeccionCanonica auto)
        {
            foreach(String sim in auto.simbolos)
            {
                switch(sim)
                {
                    case "P":
                        original.Add("Programa");
                        break;
                    case "S":
                        original.Add("secuencia-sent");
                        break;
                    case "T":
                        original.Add("sentencia");
                        break;
                    case "I":
                        original.Add("sent-if");
                        break;
                    case "R":
                        original.Add("sent-repeat");
                        break;
                    case "A":
                        original.Add("sent-assign");
                        break;
                    case "D":
                        original.Add("sent-read");
                        break;
                    case "W":
                        original.Add("sent-write");
                        break;
                    case "X":
                        original.Add("exp");
                        break;
                    case "O":
                        original.Add("op-comp");
                        break;
                    case "M":
                        original.Add("exp-simple");
                        break;
                    case "U":
                        original.Add("opsuma");
                        break;
                    case "E":
                        original.Add("term");
                        break;
                    case "L":
                        original.Add("opmult");
                        break;
                    case "F":
                        original.Add("factor");
                        break;
                    case "i":
                        original.Add("if");
                        break;
                    case "t":
                        original.Add("then");
                        break;
                    case "e":
                        original.Add("end");
                        break;
                    case "l":
                        original.Add("else");
                        break;
                    case "r":
                        original.Add("repeat");
                        break;
                    case "u":
                        original.Add("until");
                        break;
                    case "d":
                        original.Add("identificador");
                        break;
                    case "a":
                        original.Add("read");
                        break;
                    case "w":
                        original.Add("write");
                        break;
                    case "n":
                        original.Add("numero");
                        break;
                    case ",":
                        original.Add(":=");
                        break;
                    default:
                        original.Add(sim);
                        break;
                }
            }
        }
    }
}
