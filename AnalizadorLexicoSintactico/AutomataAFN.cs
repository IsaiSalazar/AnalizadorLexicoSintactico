using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalizadorLexicoSintactico
{
   public class AutomataAFN : Automata
    {


        private AutomataAFN()
        {

        }
        
        public AutomataAFN(String posfija)
        {
            List<int[]> pila = new List<int[]>();
            
            foreach (char car in posfija)
            {
                int[] extremos = new int[2];
                if ((car != '?') && (car != '*') && (car != '+') && (car != '&') && (car != '|'))
                {
                    Estado est1 = new Estado(this.Count);
                    extremos[0] = this.Count;
                    this.Add(est1);
                    Estado est2 = new Estado(this.Count);
                    extremos[1] = this.Count;
                    this.Add(est2);
                    Transicion simple = new Transicion(car);
                    simple.origen = this[extremos[0]];
                    simple.destino = this[extremos[1]];
                    this[extremos[0]].transiciones.Add(simple);
                    pila.Add(extremos);
                }
                else if (car == '|')
                {
                    
                    Estado est1 = new Estado(this.Count);
                    extremos[0] = this.Count;
                    this.Add(est1);
                    Estado est2 = new Estado(this.Count);
                    extremos[1] = this.Count;
                    this.Add(est2);

                    Transicion bifurcacion1 = new Transicion(epsilon);
                    bifurcacion1.origen = this[extremos[0]];
                    bifurcacion1.destino = this[pila[pila.Count-2][0]];
                 
                    

                    Transicion bifurcacion2 = new Transicion(epsilon);
                    bifurcacion2.origen = this[extremos[0]];
                    bifurcacion2.destino = this[pila[pila.Count - 1][0]];

                    this[extremos[0]].transiciones.Add(bifurcacion1);
                    this[extremos[0]].transiciones.Add(bifurcacion2);

                    Transicion bifurcacion3 = new Transicion(epsilon);
                    bifurcacion3.destino = this[extremos[1]];
                    bifurcacion3.origen = this[pila[pila.Count - 2][1]];

                    Transicion bifurcacion4 = new Transicion(epsilon);
                    bifurcacion4.destino = this[extremos[1]];
                    bifurcacion4.origen = this[pila[pila.Count - 1][1]];

                    this[pila[pila.Count - 2][1]].transiciones.Add(bifurcacion3);
                    this[pila[pila.Count - 1][1]].transiciones.Add(bifurcacion4);

                    pila.RemoveAt(pila.Count - 1);
                    pila.RemoveAt(pila.Count - 1);
                    pila.Add(extremos);
                }

                else if(car == '&')
                {
                    foreach(Transicion tran in this[pila[pila.Count-1][0]].transiciones)
                    {
                        tran.origen = this[pila[pila.Count - 2][1]];
                        this[pila[pila.Count - 2][1]].transiciones.Add(tran);
                    }
                    
                    foreach (int[] arr in pila)
                    {
                        if (arr[0] > pila[pila.Count - 1][0])
                            arr[0]--;
                        if (arr[1] > pila[pila.Count - 1][0])
                            arr[1]--;
                    }
                    extremos[0] = pila[pila.Count - 2][0];
                    extremos[1] = pila[pila.Count - 1][1];
                    this.RemoveAt(pila[pila.Count - 1][0]);

                    pila.RemoveAt(pila.Count - 1);
                    pila.RemoveAt(pila.Count - 1);
                    pila.Add(extremos);
                }

                else if(car == '?')
                {
                    Estado est1 = new Estado(this.Count);
                    extremos[0] = this.Count;
                    this.Add(est1);
                    Estado est2 = new Estado(this.Count);
                    extremos[1] = this.Count;
                    this.Add(est2);
                    //Nuevo Estado de inicio union transicion epsilon antiguo estado de inicio
                    Transicion bifurcacion1 = new Transicion(epsilon);
                    bifurcacion1.origen = this[extremos[0]];
                    bifurcacion1.destino = this[pila[pila.Count - 1][0]];
                    this[extremos[0]].transiciones.Add(bifurcacion1);

                    //Nuevo estado de aceptacion union transicion epsilon nuevo estado de aceptacion
                    Transicion bifurcacion2 = new Transicion(epsilon);
                    bifurcacion2.origen = this[pila[pila.Count - 1][1]];
                    bifurcacion2.destino = this[extremos[1]];
                    this[pila[pila.Count - 1][1]].transiciones.Add(bifurcacion2);

                    //Union estado de aceptacion con estado de inicio
                    Transicion bifurcacion3 = new Transicion(epsilon);
                    bifurcacion3.origen = this[extremos[0]];
                    bifurcacion3.destino = this[extremos[1]];
                    this[extremos[0]].transiciones.Add(bifurcacion3);

                    //Actualizacion de la pila
                    pila.RemoveAt(pila.Count - 1);
                    pila.Add(extremos);

                }

                else if(car == '+')
                {
                    Estado est1 = new Estado(this.Count);
                    extremos[0] = this.Count;
                    this.Add(est1);
                    Estado est2 = new Estado(this.Count);
                    extremos[1] = this.Count;
                    this.Add(est2);
                    //Nuevo Estado de inicio union transicion epsilon antiguo estado de inicio
                    Transicion bifurcacion1 = new Transicion(epsilon);
                    bifurcacion1.origen = this[extremos[0]];
                    bifurcacion1.destino = this[pila[pila.Count - 1][0]];
                    this[extremos[0]].transiciones.Add(bifurcacion1);

                    //Nuevo estado de aceptacion union transicion epsilon nuevo estado de aceptacion
                    Transicion bifurcacion2 = new Transicion(epsilon);
                    bifurcacion2.origen = this[pila[pila.Count - 1][1]];
                    bifurcacion2.destino = this[extremos[1]];
                    this[pila[pila.Count - 1][1]].transiciones.Add(bifurcacion2);

                    //Antiguo estado de aceptacion con antiguo estado de inicio

                    Transicion bifurcacion3 = new Transicion(epsilon);
                    bifurcacion3.origen = this[pila[pila.Count - 1][1]];
                    bifurcacion3.destino = this[pila[pila.Count - 1][0]];
                    this[pila[pila.Count - 1][1]].transiciones.Add(bifurcacion3);

                    //Actualizacion de la pila
                    pila.RemoveAt(pila.Count - 1);
                    pila.Add(extremos);

                }
                else if(car == '*')
                {
                    Estado est1 = new Estado(this.Count);
                    extremos[0] = this.Count;
                    this.Add(est1);
                    Estado est2 = new Estado(this.Count);
                    extremos[1] = this.Count;
                    this.Add(est2);
                    //Nuevo Estado de inicio union transicion epsilon antiguo estado de inicio
                    Transicion bifurcacion1 = new Transicion(epsilon);
                    bifurcacion1.origen = this[extremos[0]];
                    bifurcacion1.destino = this[pila[pila.Count - 1][0]];
                    this[extremos[0]].transiciones.Add(bifurcacion1);

                    //Nuevo estado de aceptacion union transicion epsilon nuevo estado de aceptacion
                    Transicion bifurcacion2 = new Transicion(epsilon);
                    bifurcacion2.origen = this[pila[pila.Count - 1][1]];
                    bifurcacion2.destino = this[extremos[1]];
                    this[pila[pila.Count - 1][1]].transiciones.Add(bifurcacion2);

                    //Union estado de aceptacion con estado de inicio
                    Transicion bifurcacion3 = new Transicion(epsilon);
                    bifurcacion3.origen = this[extremos[0]];
                    bifurcacion3.destino = this[extremos[1]];
                    this[extremos[0]].transiciones.Add(bifurcacion3);

                    //Antiguo estado de aceptacion con antiguo estado de inicio

                    Transicion bifurcacion4 = new Transicion(epsilon);
                    bifurcacion4.origen = this[pila[pila.Count - 1][1]];
                    bifurcacion4.destino = this[pila[pila.Count - 1][0]];
                    this[pila[pila.Count - 1][1]].transiciones.Add(bifurcacion4);

                    //Actualizacion de la pila
                    pila.RemoveAt(pila.Count - 1);
                    pila.Add(extremos);
                }
            }
            this.inicio = this[pila[pila.Count - 1][0]];
            this.aceptacion = this[pila[pila.Count - 1][1]];
            enumerate(this.inicio, 0);
            limpiaAutomata();
        }
        
    }
}

