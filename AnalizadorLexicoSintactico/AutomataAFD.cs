using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalizadorLexicoSintactico
{
    public class AutomataAFD:Automata
    {
        public List<Estado> EstadosAceptacion = new List<Estado>();
        private List<Estado> mover(List<Estado> nEstado, char trans)
        {
            List<Estado> lista = new List<Estado>();
            foreach(Estado est in nEstado)
            {
                foreach(Transicion tran in est.transiciones)
                {
                    if(tran.etiqueta == trans)
                    {
                        lista.Add(tran.destino);
                    }
                }
            }
            return lista;
        }

        public List<Estado> cerraduraEpsilon(Estado est)
        {
            List<Estado> lista = new List<Estado>();
            lista.Add(est);
            est.visitado = true;
            foreach(Transicion eps in est.transiciones)
            {
                if(eps.etiqueta == epsilon)
                {
                    if(eps.destino.visitado == false)
                    {
                        List<Estado>listaAux = cerraduraEpsilon(eps.destino);
                        foreach(Estado aux in listaAux)
                        {
                            bool existe = lista.Any(x => x == aux);
                            if(!existe)
                            lista.Add(aux);
                        }
                    }
                }
            }
            est.visitado = false;
            return lista;
        }
        
        
        public AutomataAFD(AutomataAFN afn) // entrada mejor un estado public void crearAFD(Estado est, string entrada)
        {
            int marcados = 0;
            int totales = 0;
            bool igual = false;
            Estado aux;
            List<Estado> listAux = new List<Estado>();

            afn.alfabeto = afn.alfabeto.Remove(afn.alfabeto.Length - 1); //a la cadena se le quita epsilon
    
            List<List<Estado>> Destados = new List<List<Estado>>(); //lista de lista destados

            List<Estado> auxLista = new List<Estado>(); //lista auxiliar
            List<Estado> listfinal = new List<Estado>(); //lista auxiliar
            auxLista = cerraduraEpsilon(afn.inicio); //cerraduraepsilon a estado 0
            IEnumerable<Estado> listaEstados = auxLista.OrderByDescending(y => y.nombre); //para ordenarla y sea mas facil comparar
            foreach (Estado est in listaEstados)
            {
                listfinal.Add(est);
               
            }
            Destados.Add(listfinal);
            Estado nuevo = new Estado('A');
            this.Add(nuevo);
            totales++;
            aux = nuevo;

            while(totales>marcados)//Mientras haya estados en Destados
            {
                aux.visitado = true;
                marcados++;

                foreach(char a in afn.alfabeto)//Para cada caracter del alfabeto
                {
                    igual = false;
                    listAux = new List<Estado>();
                    foreach (Estado est in mover(listfinal, a))
                    {
                       
                        //Ceradura de epsilon de los posibles estados que regrese mover
                        foreach(Estado nodo in cerraduraEpsilon(est))
                        {
                         //Si el estado no existe dentro de la lista agregalo
                            bool existe = listAux.Any(x => x == nodo);
                            if(!existe)
                            listAux.Add(nodo);
                        }

                    }
                    if (listAux.Count > 0)//Si la lista de mover no esta vacia
                    {
                        //Compara si en alguna lista se encuentra la lista que se acaba de crear
                        for (int i = 0; i < Destados.Count; i++)
                        {
                            //Si la encuentra agrega una transicion desde el estado actual al estado existente
                            if (comparaListas(Destados[i], listAux))
                            {
                              //  MessageBox.Show("entre");
                                igual = true;
                                Transicion nuevaT = new Transicion(a);
                                nuevaT.destino = this[i];
                                nuevaT.origen = this[marcados - 1];
                              //  MessageBox.Show("union " + nuevaT.origen.nombrechar.ToString()+"con"+ nuevaT.destino.nombrechar.ToString());
                                this[marcados - 1].transiciones.Add(nuevaT);
                                break;
                            }
                        }
                        //Si no es igual crea un nuevo estado 
                        if (!igual)
                        {
                          //  MessageBox.Show("no es igual");
                            Destados.Add(listAux);
                            Estado nuevoEs = new Estado(Convert.ToChar(totales + 65));
                          //  MessageBox.Show("Nuevo estado "+nuevoEs.nombrechar.ToString());
                            Transicion nuevaT = new Transicion(a);
                            nuevaT.origen = this[marcados - 1];
                            nuevaT.destino = nuevoEs;
                            this[marcados - 1].transiciones.Add(nuevaT);
                            this.Add(nuevoEs);
                            totales++;
                        }
                        
                    }
                    
                }
                if(totales>marcados)
                listfinal = Destados[marcados];
            }
            
            for(int i = 0; i<Destados.Count; i++)
            {
                foreach(Estado est in Destados[i])
                {
                    if(est == afn.aceptacion)
                    {
                        EstadosAceptacion.Add(this[i]);
                    }
                }
            }
            this.inicio = this[0];

        }

        private bool comparaListas(List<Estado> lista1, List<Estado> lista2)
        {
            bool res = true;
            foreach(Estado est in lista1)
            {
                res = lista2.Any(x => x == est);
                if (res == false)
                    return res;
            }
            foreach (Estado est in lista2)
            {
                res = lista1.Any(x => x == est);
                if (res == false)
                    return res;
            }
            return res;
        }
         
    }
}
