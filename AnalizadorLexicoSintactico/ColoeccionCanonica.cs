using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalizadorLexicoSintactico
{
    public class ColoeccionCanonica:List<Conjunto>
    {
        public List<Produccion> gramatica = new List<Produccion>();
        public List<String> terminales = new List<string>();
        public List<String> Noterminales = new List<string>();
        public List<String> simbolos = new List<string>();
        public ColoeccionCanonica(List<Produccion> G)
        {
            bool bandera = false;
            generaSimbolos(G);

            foreach (Produccion pro in G)
            {
                gramatica.Add(pro);
            }
            Conjunto I0 = new Conjunto(0);
            I0.elementos.Add(gramatica[0].GetEncabezado() + "->." + gramatica[0].cuerpo[0]);
            this.Add(cerradura(I0));
            int marcados = 0;
            int totales = 1;
            int actual = 0;

            Conjunto prueba = new Conjunto(0);
            Conjunto prueba2 = new Conjunto(0);
            prueba.elementos.Add("P->a.S");
            prueba.elementos.Add("P->a.D");
            

            while (totales>marcados)
            {
                
                    if (!this[marcados].marcado)
                    {
                        
                        
                        foreach (String term in simbolos)
                        {
                        
                            if (terDespues(this[marcados], term))
                            {
                            
                            Conjunto nuevo = ir_A(this[marcados], term);

                                if (nuevo != null)
                                {

                                    foreach (Conjunto conj in this)
                                    {
                                        if (comparaConjuntos(conj, nuevo))
                                        {
                                            Transicion nueva = new Transicion(term);
                                            nueva.origen = this[marcados];
                                            nueva.destino = conj;
                                            this[marcados].transiciones.Add(nueva);
                                            bandera = true;
                                        
                                            break;
                                        }
                                    }
                                    if (!bandera)
                                    {
                                        this.Add(nuevo);
                                    
                                    Transicion nueva = new Transicion(term);
                                        nueva.origen = this[marcados];
                                        nueva.destino = nuevo;
                                        this[marcados].transiciones.Add(nueva);
                                    totales++;
                                        
                                    }
                                    bandera = false;
                                }
                            }
                        }
                    
                    }
                    this[marcados].marcado = true;
                    marcados++;
                    
            }
                
            
            
        }

        private bool terDespues(Conjunto elementos, String x)
        {
            bool band = false;
            foreach(String ele in elementos.elementos)
            {
                if (ele.Contains("." + x))
                    return true;
            }
            return false;
        }
        void imprime(Conjunto I)
        {
            MessageBox.Show("Estado" + I.nombre.ToString());
            foreach(String el in I.elementos)
            {
                MessageBox.Show(el);
            }
        }
        private void generaSimbolos(List<Produccion> G)
        {
            String[] aux;
            foreach (Produccion pro in G)
            {
                if (!Noterminales.Contains(pro.GetEncabezado()))
                {
                    Noterminales.Add(pro.GetEncabezado());
                    
                }

            }
            foreach (Produccion prod in G)
            {
                foreach (String cuerpo in prod.cuerpo)
                {
                    aux = cuerpo.Split(' ');
                    foreach (String simb in aux)
                    {
                        if (!Noterminales.Contains(simb) && !terminales.Contains(simb))
                        {
                            terminales.Add(simb);
                        }
                    }
                }
            }
            foreach(String simb in terminales)
            {
                simbolos.Add(simb);
            }
            for(int i = 1; i<Noterminales.Count; i++)
            {
                simbolos.Add(Noterminales[i]);
            }


        }
        private bool comparaConjuntos(Conjunto conj1, Conjunto conj2)
        {
            bool resultado = true;
            foreach (String elemento in conj1.elementos)
            {
                resultado = false;
                foreach (String element in conj2.elementos)
                {
                    if (element == elemento)
                    {
                        resultado = true;
                        break;
                    }
                }
                if (!resultado)
                    break;
            }
            return resultado;
        }
        private Conjunto cerradura(Conjunto I)
        {
            List<String> Gaumentada = new List<string>();
            // Gaumentada = gAumentada(Gaumentada);
            Gaumentada.Add("P'→.P"); //SE PONE DIRECTO YA QUE HUBO PROBLEMAS AL HACERLO DE FORMA ITERATIVA
            Gaumentada.Add("P→.S");
            Gaumentada.Add("S→.S;T");

            Gaumentada.Add("S→.T");
            Gaumentada.Add("T→.I");
            Gaumentada.Add("T→.R");
            Gaumentada.Add("T→.A");
            Gaumentada.Add("T→.D");
            Gaumentada.Add("T→.W");
            Gaumentada.Add("I→.iXtSe");
            Gaumentada.Add("I→.iXtSlSe");
            Gaumentada.Add("R→.rSuX");
            Gaumentada.Add("A→.d,X");
            Gaumentada.Add("D→.ad");
            Gaumentada.Add("W→.wX");
            Gaumentada.Add("X→.MOM");
            Gaumentada.Add("X→.M");
            Gaumentada.Add("O→.<");
            Gaumentada.Add("O→.>");
            Gaumentada.Add("O→.=");
            Gaumentada.Add("M→.MUE");
            Gaumentada.Add("M→.E");
            Gaumentada.Add("U→.+");
            Gaumentada.Add("U→.-");

            Gaumentada.Add("E→.ELF");
            Gaumentada.Add("E→.F");
            Gaumentada.Add("L→.*");
            Gaumentada.Add("L→./");
            Gaumentada.Add("F→.(X)");

            Gaumentada.Add("F→.n");
            Gaumentada.Add("F→.d");

            String afterP;
            bool mas;
            
            while (true)
            {
                mas = false;
                for (int i = 0; i < I.elementos.Count; i++)
                {
                    String pr = I.elementos[i];
                    afterP = pr.Substring(pr.IndexOf(".") + 1);//, pr.IndexOf(",") - pr.IndexOf(".") - 1);
                    if (afterP != "")
                    {
                        foreach (String B in Gaumentada)
                        {
                            if (char.IsUpper(afterP[0]) && B.Substring(0, 2) == afterP[0] + "→")
                            {
                                if (!I.elementos.Contains(B))
                                {
                                    I.elementos.Add(B);
                                    mas = true;
                                }
                            }
                        }
                    }
                }
                if (!mas)
                {
                    return I;
                }
            }
        }
        private Conjunto ir_A(Conjunto I, String x)
        {
            Char aux = ' ';
            aux = Convert.ToChar(x);
            List<String> J = new List<String>();
            foreach (String str in I.elementos)
            {
                var c = str.IndexOf("."); //posicion del punto
                if ((str.IndexOf(".") + 1) < str.Length) // si la posición del punto + 1 < a la longitud de la cadena
                {
                    String afterP = str.Substring(str.IndexOf(".") + 1, 1);
                    String afterC = str.Substring(str.IndexOf(".") + 2);
                    if (afterP != "" && afterP[0] == aux)
                    {
                        J.Add(str.Substring(0, str.IndexOf(".")) + afterP + "." + afterC);
                    }
                }
            }

            //return Cerradura(J);
            Conjunto estado = new Conjunto(this.Count);
            estado.elementos = J;
            return cerradura(estado); //  return Cerradura(J);
        }
        private String insertaPuntoInicio(String cad)
        {
            cad = cad.Insert(0, ".");
            return cad;
        }
        private List<String> gAumentada(List<String> lis)
        {
            String dobleaux = "";
            foreach (Produccion lp in gramatica)
            {

                foreach (String s in lp.cuerpo)
                {

                    dobleaux = s;
                    dobleaux = insertaPuntoInicio(dobleaux);
                    //  MessageBox.Show(dobleaux);
                    lis.Add(lp.encabezado + "→" + dobleaux);
                }

            }
            return lis;
        }
        private List<String> devueleProduciones(String refe)
        {
            List<String> listaDeProducciones = new List<string>();
            String dobleaux = "";
            int contador = 0;
            foreach (Produccion lp in gramatica)
            {
                if (lp.encabezado.Contains(refe) && !lp.encabezado.Contains("'"))
                {
                    foreach (String s in lp.cuerpo)
                    {

                        dobleaux = s;
                        dobleaux = insertaPuntoInicio(dobleaux);
                        listaDeProducciones.Add(lp.encabezado + "→" + dobleaux);
                        //  MessageBox.Show(dobleaux);
                        // J.elementos.Add(lp.encabezado + "->" + dobleaux);
                        contador++;
                    }
                }

            }


            return listaDeProducciones;
        }
    }
}
