using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalizadorLexicoSintactico
{
    class AnalizadorLexico
    {
        private String lexema;
        AutomataAFD automata;
        ExpresionRegular expresion;
        
        public AnalizadorLexico(ExpresionRegular ER)
        {
            expresion = ER;
        }

        public bool verificarLexema(String lexema)
        {
            AutomataAFN afn = new AutomataAFN(expresion.posfija);
            afn.alfabeto = expresion.obtieneAplfabheto(expresion.posfija);
            automata = new AutomataAFD(afn);
            if (recorreAutomata(automata.inicio, lexema)==1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private int recorreAutomata(Estado actual, String lexema)
        {
            
            int res = 0;
            if (lexema.Length == 1)
            {
                foreach (Transicion tran in actual.transiciones)
                {
                    if (tran.etiqueta.ToString() == lexema)
                    {
                        
                        foreach (Estado est in automata.EstadosAceptacion)
                        {
                            
                            if (tran.destino == est)
                            {
                                return 1;
                            }
                        }
                    }
                }

                
            }
            else
            {
                foreach (Transicion tran in actual.transiciones)
                {
                    if (tran.etiqueta.ToString() == lexema.Substring(0, 1))
                    {
                        res = recorreAutomata(tran.destino, lexema.Substring(1, lexema.Length - 1));
                    }
                }
                
            }
            return res;

        }
    }
}
