using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalizadorLexicoSintactico
{
    public class ExpresionRegular
    {
        public String expresion = "";
        public String posfija = "";
        public String alfabeto;
        public String EvaluaEspacios(String cadena)
        {
            String resultado = "";
            String[] fragmentos = cadena.Split(' ');
            foreach(String c in fragmentos)
            {
                resultado += c;
            }
            return resultado;
        }
        public String convierteteEnPosfija(String cadena)
        {
            
            List<char> pila = new List<char>();
            
            foreach(char c in cadena)
            {
                if(c=='(')
                {
                    pila.Add(c);
                }
                else if (c == ')')
                {
                    char aux = pila[pila.Count - 1];
                    if (aux == '(')
                        pila.RemoveAt(pila.Count - 1);
                    else
                    {
                        while (aux != '(')
                        {
                            
                            posfija += aux;
                            pila.RemoveAt(pila.Count - 1);
                            aux = pila[pila.Count - 1];
                        }
                        pila.RemoveAt(pila.Count - 1);
                    }
                }
                else if(c == '|' || c == '&' || c == '+' || c =='?' || c=='*')
                {
                    if(pila.Count==0)
                    {
                        pila.Add(c);
                    }
                    else
                    {
                        if (evaluPrioridad(c) > evaluPrioridad(pila[pila.Count - 1]))
                        {
                            pila.Add(c);
                        }
                        else
                        {
                            while (evaluPrioridad(c) <= evaluPrioridad(pila[pila.Count - 1]))
                            {
                                posfija += pila[pila.Count - 1];
                                pila.RemoveAt(pila.Count - 1);
                                if (pila.Count == 0)
                                    break;
                            }
                            pila.Add(c);
                        }
                    }
                }
                else
                {
                    posfija += c;
                }
            }
            if(pila.Count>0)
            {
                while(pila.Count>0)
                {
                    posfija += pila[pila.Count - 1];
                    pila.RemoveAt(pila.Count - 1);
                }
            }
            return posfija;
        }
        private int evaluPrioridad(char caracter)
        {
            int prioridad = -1;
            switch(caracter)
            {
                case '(':
                    prioridad = 0;
                    break;
                case '|':
                    prioridad = 1;
                    break;
                case '&':
                    prioridad = 2;
                    break;
                case '+':
                    prioridad = 3;
                    break;
                case '*':
                    prioridad = 3;
                    break;
                case '?':
                    prioridad = 3;
                    break;


            }
            return prioridad;
        }

        public String convierteCadena(String cadena)
        {
            String convertida = "";
            String cadAux1="";
            String cadAux2 = "";
            bool bandera = false;
            foreach(char c in cadena)
            {
                if(bandera && c!=']')
                {
                    cadAux1 += c;
                }
                if(c=='[')
                {
                    bandera = true;
                }
                if(c==']')
                {
                    bandera = false;
                    cadAux2 += corchete(cadAux1);
                    cadAux1 = "";
                }
                if((!bandera)&&(c!=']')&&(c!='['))
                {
                    cadAux2 += c;
                }
            }

            return cadAux2;
        }
        public String corchete(String expresion)
        {
            String nExpresion="(";
            int numero = 0;
            int numero2 = 0;
            bool bandera = true;
            String[] copia;
            foreach(char c in expresion)
            {
                if(c=='-')
                {
                    bandera = false;
                    copia = expresion.Split('-');
                    if(int.TryParse(copia[0],out numero))
                    {
                        numero = int.Parse(copia[0]);
                        numero2 = int.Parse(copia[1]);
                        for(int i =numero; i<=numero2; i++)
                        {
                            nExpresion += i.ToString();
                            nExpresion += '|';
                        }
                        nExpresion = nExpresion.Trim('|');

                    }
                    else
                    {
                        char caracter1 = copia[0][0];
                        char caracter2 = copia[1][0];
                        for(char car = caracter1; car<=caracter2; car++)
                        {
                            nExpresion += car.ToString();
                            nExpresion += '|';
                        }

                        nExpresion = nExpresion.Trim('|');
                    }
                }
            }
            if(bandera)
            {
                foreach(char letra in expresion)
                {
                    nExpresion += letra;
                    nExpresion += '|';
                }
                nExpresion = nExpresion.Trim('|');
            }
            nExpresion += ')';
            return nExpresion;
        }

        public String ConvierteAper(String cadAper)
        {
            char[] arr = new char[1000];
            char[] arr1 = new char[1000];
            char[] arr3 = new char[1000];
            char[] arr4 = new char[1000];
            int i = 0;
            foreach (char a in cadAper) //util
            {
                arr1[i] = a;
                i++;
            }
            int z = 0; //secion de &
            for (int j = 0; j < cadAper.Length * 2; j++)
            {

                if (j % 2 == 0)
                {
                    arr[j] = arr1[z];
                    z++;
                }
                else
                {
                    arr[j] = '&';
                }
            }
            string nuevo = new string(arr);//fin seccion de &                          //  MessageBox.Show(nuevo);
            nuevo = nuevo.Substring(0, (cadAper.Length * 2) - 1);
            int f = 0;
            foreach (char b in nuevo)
            {
                arr3[f] = b;
                f++;
            }
            int contadorsalvacion = 0;
            for (int d = 0; d < arr3.Length; d++)
            {
                if (arr3[d] == '&')
                {
                    if (arr3[d - 1] == '(' | arr3[d + 1] == ')' | arr3[d - 1] == '|' | arr3[d + 1] == '|' | arr3[d + 1] == '?' | arr3[d + 1] == '*' | arr3[d + 1] == '+' | arr3[d - 1] == '-' | arr3[d + 1] == '-' | arr3[d - 1] == '[' | arr3[d + 1] == ']')
                    {
                        arr3[d] = 'X';
                        contadorsalvacion++;
                    }
                }
                else
                {

                }
            }
            string nuevo2 = new string(arr3);
            nuevo2 = nuevo2.Replace("X", "");
            nuevo2 = nuevo2.Replace("[", "(");
            nuevo2 = nuevo2.Replace("]", ")");
            nuevo2 = nuevo2.Substring(0, (f - contadorsalvacion));
            return nuevo2;
        }
        public string obtieneAplfabheto(string alphabeto)
        {
            string cadApoyo;
            alphabeto = alphabeto.Replace("&", "");
            alphabeto = alphabeto.Replace("|", "");
            alphabeto = alphabeto.Replace("*", "");
            alphabeto = alphabeto.Replace("?", "");
            alphabeto = alphabeto.Replace("+", "");
            char[] nuevacad = new char[alphabeto.Length];
            int num = 0;
            foreach (char a in alphabeto)
            {
                nuevacad[num] = a;
                num++;
            }
            char caracter;

            char[] lastcadena = new char[100];
            int iterador = 0;
            bool car;
            for (int i = 0; i < nuevacad.Length; i++)
            {
                caracter = nuevacad[i];

                for (int j = 0; j < nuevacad.Length; j++)
                {
                    car = lastcadena.Contains(caracter);
                    if (car == false)
                    {

                        lastcadena[iterador] = caracter;
                        iterador++;
                    }
                }


            }
            StringBuilder builder = new StringBuilder();
            foreach (char value in lastcadena)
            {
                builder.Append(value);
            }

            cadApoyo = builder.ToString();
            cadApoyo = cadApoyo.Substring(0, iterador);
            cadApoyo = cadApoyo.Insert(cadApoyo.Length, "ε");
            return cadApoyo;

        }
    }
}
