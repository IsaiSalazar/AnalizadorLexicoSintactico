using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalizadorLexicoSintactico
{
    public partial class Form1 : Form
    {
        ExpresionRegular nueva = new ExpresionRegular();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nueva.posfija = "";
            nueva.expresion = textBox1.Text;
            nueva.expresion = nueva.EvaluaEspacios(nueva.expresion);
            nueva.expresion = nueva.convierteCadena(textBox1.Text);
            nueva.expresion = nueva.ConvierteAper(nueva.expresion);
            nueva.posfija = nueva.convierteteEnPosfija(nueva.expresion);

            textBox2.Text = nueva.posfija;
  
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            AutomataAFN nuevo = new AutomataAFN(textBox2.Text);
            string alfabeto = textBox2.Text;
            nuevo.alfabeto = obtieneAplfabheto(alfabeto);
            alfabeto = obtieneAplfabheto(alfabeto);
            imprimeAfn(nuevo);
            obtieneTabla(nuevo, alfabeto);
            label9.Text = nuevo.inicio.nombre.ToString();
            label10.Text = nuevo.aceptacion.nombre.ToString();


        }

        void imprimeAfn(AutomataAFN prueba)
        {
            char[,] matiz = new char[prueba.Count, prueba.Count];
            for (int p = 0; p < prueba.Count; p++)
            {
                for (int k = 0; k < prueba.Count; k++)
                {
                    matiz[p, k] = '0';
                }
            }
            for (int i = 0; i < prueba.Count; i++)
            {
                foreach (Transicion tran in prueba[i].transiciones)
                {
                    int aux = prueba.FindIndex(x => x.Equals(tran.destino));
                    matiz[i, aux] = tran.etiqueta;
                }
            }
            String ren = "";
            for (int a = 0; a < prueba.Count; a++)
            {
                for (int b = 0; b < prueba.Count; b++)
                {
                    ren += matiz[a, b];
                }
                listBox1.Items.Add(ren);
                ren = "";
            }
        }

        string obtieneAplfabheto(string alphabeto)
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
        void obtieneTabla(AutomataAFN afn, string alfabetoPosfija)
        {
            char[] vistaAlpabheto = new char[alfabetoPosfija.Length];
            int ite = 0;
            foreach (char al in alfabetoPosfija)
            {
                vistaAlpabheto[ite] = al;
                ite++;
            }
            //columnas
            for (int w = 0; w < vistaAlpabheto.Length; w++)
            {
                dataGridView1.Columns.Add(vistaAlpabheto[w].ToString(), vistaAlpabheto[w].ToString());
            }


            foreach (Estado est in afn)
            {
                dataGridView1.Rows.Add();

            }
            //  MessageBox.Show("el contador es: "+ contadorsito);

            for (int i = 0; i < afn.Count; i++)
            {
                dataGridView1.Rows[i].HeaderCell.Value = afn[i].nombre.ToString();
            }



            for (int i = 0; i < afn.Count; i++)
            {
                for (int j = 0; j < afn[i].transiciones.Count; j++)
                {
                    for (int k = 0; k < vistaAlpabheto.Length; k++)
                    {
                        if (afn[i].transiciones[j].etiqueta == vistaAlpabheto[k])
                        {
                            int aux = afn.FindIndex(x => x.Equals(afn[i].transiciones[j].origen));
                            //   MessageBox.Show(" la etiqueta " + afn[i].transiciones[j].etiqueta + " esta en " + afn[i].nombre + "y su destino es "+ afn[i].transiciones[j].destino.nombre );
                            dataGridView1.Rows[aux].Cells[k].Value += afn[i].transiciones[j].destino.nombre.ToString() + ",";
                        }
                        // dataGridView1.Rows[grafo[i].aristas[j].origen.nombre - 1].Cells[grafo[i].aristas[j].destino.nombre - 1].Value = grafo[i].aristas[j].peso;
                    }

                }


            }

            for (int i = 0; i < afn.Count; i++)
            {
                for (int j = 0; j < vistaAlpabheto.Length; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value == null)
                    {
                        dataGridView1.Rows[i].Cells[j].Value = "ø";
                    }
                }

            }

            //fin de la funcion
        }
        //afd

        void obtieneTablaAFD(AutomataAFD afd)
        {
            char[] vistaAlpabheto = new char[afd.alfabeto.Length];
            int ite = 0;
            foreach (char al in afd.alfabeto)
            {
                vistaAlpabheto[ite] = al;
                ite++;
            }
            //columnas
            for (int w = 0; w < vistaAlpabheto.Length; w++)
            {
                dataGridView2.Columns.Add(vistaAlpabheto[w].ToString(), vistaAlpabheto[w].ToString());
            }

            foreach (Estado est in afd)
            {
                dataGridView2.Rows.Add();

            }
            for (int i = 0; i < afd.Count; i++)
            {
                dataGridView2.Rows[i].HeaderCell.Value = afd[i].nombrechar.ToString();
            }

            /*     for (int i = 0; i < afd.Count; i++)
                 {
                     for (int j = 0; j < afd[i].transiciones.Count; j++)
                     {
                         MessageBox.Show("para el nodo:"+ afd[i].nombrechar + "las transiciones son: " +afd[i].transiciones[j].etiqueta.ToString());
                     }

                 }*/
            for (int i = 0; i < afd.Count; i++)
            {
                for (int j = 0; j < afd[i].transiciones.Count; j++)
                {
                    for (int k = 0; k < afd.alfabeto.Length; k++)
                    {
                        if (afd[i].transiciones[j].etiqueta == afd.alfabeto[k])
                        {
                            int aux = afd.FindIndex(x => x.Equals(afd[i].transiciones[j].origen));
                            //   MessageBox.Show(" la etiqueta " + afn[i].transiciones[j].etiqueta + " esta en " + afn[i].nombre + "y su destino es "+ afn[i].transiciones[j].destino.nombre );
                            dataGridView2.Rows[aux].Cells[k].Value += afd[i].transiciones[j].destino.nombrechar.ToString() + ",";
                        }
                        // dataGridView1.Rows[grafo[i].aristas[j].origen.nombre - 1].Cells[grafo[i].aristas[j].destino.nombre - 1].Value = grafo[i].aristas[j].peso;
                    }

                }



            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();
            AutomataAFN nAfn = new AutomataAFN(textBox2.Text);
            nAfn.alfabeto = textBox2.Text;
            nAfn.alfabeto = obtieneAplfabheto(nAfn.alfabeto);
            // MessageBox.Show(nAfn.alfabeto);
            AutomataAFD afd = new AutomataAFD(nAfn);
            afd.alfabeto = nAfn.alfabeto;
          //  MessageBox.Show(afd.Count.ToString());
            obtieneTablaAFD(afd);
        }

        private void buttonValidarLexema_Click(object sender, EventArgs e)
        {
            nueva.posfija = "";
            nueva.expresion = textBox1.Text;
            nueva.expresion = nueva.EvaluaEspacios(nueva.expresion);
            nueva.expresion = nueva.convierteCadena(textBox1.Text);
            nueva.expresion = nueva.ConvierteAper(nueva.expresion);
            nueva.posfija = nueva.convierteteEnPosfija(nueva.expresion);
            AnalizadorLexico lexico = new AnalizadorLexico(nueva);
            textBox2.Text = nueva.posfija;

            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();
            AutomataAFN nAfn = new AutomataAFN(textBox2.Text);
            nAfn.alfabeto = textBox2.Text;
            nAfn.alfabeto = obtieneAplfabheto(nAfn.alfabeto);
            // MessageBox.Show(nAfn.alfabeto);
            AutomataAFD afd = new AutomataAFD(nAfn);
            afd.alfabeto = nAfn.alfabeto;
            //  MessageBox.Show(afd.Count.ToString());
            obtieneTablaAFD(afd);


            if (lexico.verificarLexema(textBoxLexema.Text))
            {
                verificador.Text = "SI Pertenece al lenguaje de la expresión regular";
            }
            else
            {
                verificador.Text = "NO pertenece al lenguaje de la expresión regular";
            }
        }

        private void buttonTokens_Click(object sender, EventArgs e)
        {
            listBoxErrores.Items.Clear();
            cadenaEvaluacion();

        }

        private void textBoxIdentificador_TextChanged(object sender, EventArgs e)
        {
            var bl = !string.IsNullOrEmpty(textBoxIdentificador.Text) &&
                       !string.IsNullOrEmpty(textBoxNumero.Text);
            buttonTokens.Enabled = bl;
        }

        private void textBoxNumero_TextChanged(object sender, EventArgs e)
        {
            var bl = !string.IsNullOrEmpty(textBoxIdentificador.Text) &&
                       !string.IsNullOrEmpty(textBoxNumero.Text);
            buttonTokens.Enabled = bl;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }
        public String[] cadenaEvaluacion()
        {
            List<String> cadAnaLexico = new List<String>();
            dataGridViewTokens.Rows.Clear();
            dataGridViewTokens.Columns.Clear();
            string cadTiny;
            List<string> laux = new List<string>();
            cadTiny = separOperadores();
            cadTiny = cadTiny.Insert(cadTiny.Length, " ");

            cadTiny = Regex.Replace(cadTiny, @"\s+", " "); //quitar espacio de mas
            //MessageBox.Show(cadTiny);
            //cadTiny = ReducirEspaciado(cadTiny);
            //Delimitador
            char[] delimitador = { ' ', '\n' };
            //areglo de cadenas
            string[] trozos = cadTiny.Split(delimitador);
            //recorrer cadena para validar
            for (int i = 0; i < trozos.Length - 1; i++)
            {
                trozos[i] = trozos[i].Trim();
                cadAnaLexico.Add(trozos[i]);
                //MessageBox.Show("la cadena es: " + trozos[i]);
            }
         //   MessageBox.Show("tamaño de la cad: " + cadAnaLexico.Count);
            //areglo de palabras reservadas
            List<string> lpReservadas = new List<string> { "if", "then", "else", "end", "repeat", "until", "read", "write" };
            List<string> lsEspeciales = new List<string> { "+", "-", "*", "/", "=", "<", ">", "(", ")", ";", ":=" };
            //nueva cadena




            //

            int column2 = 2;
            for (int i = 0; i < trozos.Length; i++)
            {
                if (laux.Contains(trozos[i]))
                { }
                else
                    laux.Add(trozos[i]);

            }
            /*foreach(string s in laux)
            {
                MessageBox.Show(s);
            }*/
            //     MessageBox.Show(laux.Count.ToString());
            string[,] matDat;
            //     MessageBox.Show("" + laux.Count);
            //     MessageBox.Show("" + column2);
            List<String> lerrores = new List<String>(); //lista errores se agrego
            matDat = new string[laux.Count - 1, column2];
            bool banIdenti = false;
            bool banNumero = false;
            for (int i = 0; i < laux.Count - 1; i++)
            {
                for (int j = 0; j < column2; j++)
                {
                    if (lpReservadas.Contains(laux[i])) //resevadas
                    {
                        matDat[i, j] = laux[i];
                    }
                    else if (lsEspeciales.Contains(laux[i])) // especiales
                    {
                        matDat[i, j] = laux[i];
                    }
                    else
                    {

                        /* matDat[i, j] = label12.Text;
                         matDat[i, j + 1] = laux[i];
                         j = 2;*/

                        nueva.posfija = "";
                        nueva.expresion = textBoxIdentificador.Text;
                        nueva.expresion = nueva.EvaluaEspacios(nueva.expresion);
                        nueva.expresion = nueva.convierteCadena(nueva.expresion);
                        nueva.expresion = nueva.ConvierteAper(nueva.expresion);
                        nueva.posfija = nueva.convierteteEnPosfija(nueva.expresion);
                        AnalizadorLexico lexico = new AnalizadorLexico(nueva);
                        textBox2.Text = nueva.posfija;

                        /*   dataGridView2.Rows.Clear();
                           dataGridView2.Columns.Clear();
                           AutomataAFN nAfn = new AutomataAFN(textBox2.Text);
                           nAfn.alfabeto = textBox2.Text;
                           nAfn.alfabeto = obtieneAplfabheto(nAfn.alfabeto);
                           // MessageBox.Show(nAfn.alfabeto);
                           AutomataAFD afd = new AutomataAFD(nAfn);
                           afd.alfabeto = nAfn.alfabeto;
                           //  MessageBox.Show(afd.Count.ToString());
                         //  obtieneTablaAFD(afd);
                         */

                        if (lexico.verificarLexema(laux[i]))
                        {
                            verificador.Text = "SI Pertenece al lenguaje de la expresión regular";
                            banIdenti = true;
                        }
                        else
                        {
                            verificador.Text = "NO pertenece al lenguaje de la expresión regular";
                            banIdenti = false;
                        }

                        if (banIdenti == true)
                        {
                            matDat[i, j] = label12.Text;
                            matDat[i, j + 1] = laux[i];
                            j = 2;
                        }
                        else
                        {
                            nueva.posfija = "";
                            nueva.expresion = textBoxNumero.Text;
                            nueva.expresion = nueva.EvaluaEspacios(nueva.expresion);
                            nueva.expresion = nueva.convierteCadena(textBoxNumero.Text);
                            nueva.expresion = nueva.ConvierteAper(nueva.expresion);
                            nueva.posfija = nueva.convierteteEnPosfija(nueva.expresion);
                            AnalizadorLexico lexico1 = new AnalizadorLexico(nueva);
                            textBox2.Text = nueva.posfija;

                            /*        dataGridView2.Rows.Clear();
                                    dataGridView2.Columns.Clear();
                                    AutomataAFN nAfn1 = new AutomataAFN(textBox2.Text);
                                    nAfn1.alfabeto = textBox2.Text;
                                    nAfn1.alfabeto = obtieneAplfabheto(nAfn1.alfabeto);
                                    // MessageBox.Show(nAfn.alfabeto);
                                    AutomataAFD afd1 = new AutomataAFD(nAfn1);
                                    afd1.alfabeto = nAfn1.alfabeto;*/

                            if (lexico1.verificarLexema(laux[i]))
                            {

                                banNumero = true;
                            }
                            else
                            {

                                banNumero = false;
                            }
                            if (banNumero == true)
                            {
                                matDat[i, j] = label13.Text;
                                matDat[i, j + 1] = laux[i];
                                j = 2;
                            }
                            else
                            {

                                matDat[i, j] = "Error léxico";
                                matDat[i, j + 1] = laux[i];
                                lerrores.Add(laux[i]);
                                j = 2;
                            }

                        }



                    }
                }
            }

            //datagridview
            dataGridViewTokens.Columns.Add("Nombre", "Nombre");
            dataGridViewTokens.Columns.Add("Lexema", "Lexema");
            for (int i = 0; i < laux.Count - 1; i++)
                dataGridViewTokens.Rows.Add();
            List<String> zu = new List<String>();
            for (int i = 0; i < laux.Count - 1; i++)
            {
                for (int j = 0; j < column2; j++)
                {
                    if (matDat[i, j] == "Error léxico")
                    {
                        dataGridViewTokens.Rows[i].Cells[j].Style.BackColor = Color.Red;
                        dataGridViewTokens.Rows[i].Cells[j + 1].Style.BackColor = Color.Red;
                        dataGridViewTokens.Rows[i].Cells[j].Value = matDat[i, j];
                        zu.Add("Error"); //para no avanzar al sintactico button 6
                    }
                    else
                    {
                        dataGridViewTokens.Rows[i].Cells[j].Value = matDat[i, j];
                    }


                }
            }

            int contadorLineas = 0;
            contadorLineas = textBoxTINY.Lines.Count();
            String[] arregloLineas = textBoxTINY.Lines;
            listBoxErrores.Items.Add(" Se encontró uno o mas errores en el programa ");
            listBoxErrores.Visible = false;

            listSuccel.Visible = true;
            for (int i = 0; i < contadorLineas; i++)
            {
                foreach (String stt in lerrores)
                {
                    if (arregloLineas[i].Contains(stt))
                    {
                        listBoxErrores.Visible = true;
                        listSuccel.Visible = false;
                       // MessageBox.Show("el error esta en la linea: " + (i + 1));
                        listBoxErrores.Items.Add(" línea: " + (i + 1) + ", " + stt + " no se reconoce ");
                    }
                }

            }
            if (listBoxErrores.Visible == false)
            {
                listSuccel.Items.Add("éxitoso");
            }

            foreach (String xtr in cadAnaLexico)
            {
                for (int i = 0; i < laux.Count - 1; i++)
                {
                    for (int j = 1; j < column2; j++)
                    {
                        if (xtr == matDat[i, j])
                        {
                            zu.Add(matDat[i, j - 1]);
                        }

                    }
                }
            }
            zu.Add("$");
            String[] cadenaFinal;
            cadenaFinal = new String[zu.Count];
            int cnt = 0;
            foreach (String sxcs in zu)
            {
                cadenaFinal[cnt] = sxcs;
                cnt++;
            }
            

            return cadenaFinal;
        }
        public String separOperadores()
        {
            List<string> lsEspeciales = new List<string> { "+", "-", "*", "/", "=", "<", ">", "(", ")", ";", ":=" };
            string cadTiny = "";
            String aux;
            List<string> laux = new List<string>();
            aux = textBoxTINY.Text;
            aux = aux.Insert(aux.Length, " ");
            bool ban = true;
            int pos = 1;
            foreach (char c in aux)
            {
                if (c.ToString() == ":")
                {
                    if (aux.Substring(pos, 2) == ":=")
                    {
                        cadTiny = cadTiny + " " + c.ToString();

                        ban = false;
                    }
                }
                else
                {
                    if (c.ToString() == "=")
                    {
                        if (aux.Substring(pos - 1, 2) == ":=")
                        {
                            cadTiny = cadTiny + " " + c.ToString();
                            ban = false;
                        }
                    }
                    else
                    {
                        foreach (String cad in lsEspeciales)
                        {

                            if (c.ToString() == cad)
                            {

                                cadTiny = cadTiny + " " + c.ToString() + " ";
                                ban = false;
                                break;
                            }

                        }
                    }
                }
                if (ban)
                {
                    cadTiny += c.ToString();
                }
                ban = true;
                pos++;
            }
            return cadTiny;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            LR0 lr0 = new LR0();

            obtieneTablaAFDCanonica(lr0.automata, lr0.original);
            foreach(Conjunto  act in lr0.automata)
            {
                listBox2.Items.Add("Estado "+act.nombre.ToString());
                foreach(String ele in act.elementos)
                {
                    String convert = "";
                    foreach(char c in ele)
                    {
                        convert += cambiaOriginal(c.ToString())+" ";
                    }
                    listBox2.Items.Add(convert);
                }
            }
            generaTablaAnalisis(lr0.automata, lr0.original);
        }
        
        void obtieneTablaAFDCanonica(ColoeccionCanonica afd, List<String> or)
        {
            String[] vistaAlpabheto = new String[afd.simbolos.Count];
            int ite = 0;
            foreach (String al in afd.simbolos)
            {
                vistaAlpabheto[ite] = al;
                ite++;
            }
            //columnas
            for (int w = 0; w < vistaAlpabheto.Length; w++)
            {
                dataGridView3.Columns.Add(vistaAlpabheto[w].ToString(), vistaAlpabheto[w].ToString());
            }

            foreach (Estado est in afd)
            {
                dataGridView3.Rows.Add();

            }
            for (int i = 0; i < afd.Count; i++)
            {
                dataGridView3.Rows[i].HeaderCell.Value = afd[i].nombre.ToString();
            }

            /*     for (int i = 0; i < afd.Count; i++)
                 {
                     for (int j = 0; j < afd[i].transiciones.Count; j++)
                     {
                         MessageBox.Show("para el nodo:"+ afd[i].nombrechar + "las transiciones son: " +afd[i].transiciones[j].etiqueta.ToString());
                     }

                 }*/
            for (int i = 0; i < afd.Count; i++)
            {
                for (int j = 0; j < afd[i].transiciones.Count; j++)
                {
                    for (int k = 0; k < afd.simbolos.Count; k++)
                    {
                        if (afd[i].transiciones[j].nombre == afd.simbolos[k])
                        {
                            int aux = afd.FindIndex(x => x.Equals(afd[i].transiciones[j].origen));
                            //   MessageBox.Show(" la etiqueta " + afn[i].transiciones[j].etiqueta + " esta en " + afn[i].nombre + "y su destino es "+ afn[i].transiciones[j].destino.nombre );
                            dataGridView3.Rows[aux].Cells[k].Value += afd[i].transiciones[j].destino.nombre.ToString() + ",";
                        }
                        // dataGridView1.Rows[grafo[i].aristas[j].origen.nombre - 1].Cells[grafo[i].aristas[j].destino.nombre - 1].Value = grafo[i].aristas[j].peso;
                    }

                }

            }

            for(int i = 0; i<or.Count; i++)
            {
                dataGridView3.Columns[i].HeaderText = or[i];
            }
        }

        private void generaTablaAnalisis(ColoeccionCanonica automata, List<String> or)
        {
            int cont = 0;
            for (int w = 0; w < automata.terminales.Count; w++)
            {
                dataGridView4.Columns.Add(or[w].ToString(), or[w].ToString());
                cont++;
            }

            dataGridView4.Columns.Add("$", "$");


            for (int w = cont; w < or.Count; w++)
            {
                dataGridView4.Columns.Add(or[w].ToString(), or[w].ToString());
            }

            foreach (Estado est in automata)
            {
                dataGridView4.Rows.Add();

            }
            for (int i = 0; i < automata.Count; i++)
            {
                dataGridView4.Rows[i].HeaderCell.Value = automata[i].nombre.ToString();
            }

            foreach(Conjunto conj in automata)
            {
                foreach(String ele in conj.elementos)
                {
                    for(int i = 0; i<automata.terminales.Count; i++)
                    {
                        if (ele.Contains("." + automata.terminales[i]))
                        {
                            dataGridView4.Rows[conj.nombre].Cells[i].Value = "d"+ir_A(conj, automata.terminales[i]).ToString();
                        }
                        
                    }
                    if (ele[ele.Length-1].Equals('.'))
                    {
                        
                        if (ele.Substring(0, 2).Equals(automata.gramatica[0].encabezado))
                        {
                            dataGridView4.Rows[conj.nombre].Cells[automata.terminales.Count].Value = "ac";
                        }
                        else
                        {
                            foreach(String str in encuentraEncabezado(ele, automata.gramatica).siguientes)
                            {
                                for(int j = 0; j<automata.terminales.Count; j++)
                                {
                                    if (automata.terminales[j].Equals(str))
                                        dataGridView4.Rows[conj.nombre].Cells[j].Value = "r" + encuentraPro(ele, automata.gramatica).ToString();
                                }
                                if(str == "$")
                                    dataGridView4.Rows[conj.nombre].Cells[automata.terminales.Count].Value = "r" + encuentraPro(ele, automata.gramatica).ToString();
                            }
                        }
                    }
                }
            }

            foreach(Conjunto con in automata)
            {
                foreach(Transicion tran in con.transiciones)
                {
                    for(int k = 0; k<automata.Noterminales.Count; k++)
                    {
                        if (tran.nombre.Equals(automata.Noterminales[k]))
                        {
                            dataGridView4.Rows[con.nombre].Cells[automata.terminales.Count + k].Value = tran.destino.nombre;
                        }
                    }
                }
            }
        }
        private int ir_A(Conjunto nodo, String x)
        {
            int res = -1;
            foreach(Transicion trans in nodo.transiciones)
            {
                if (trans.nombre.Equals(x))
                {
                    res = trans.destino.nombre;
                    break;
                }
            }
            return res;
        }

        private int encuentraPro(String prod, List<Produccion> grama)
        {
            int ind = 0;
            String cad = prod.Replace(".", "");
            String aux;
            bool prim = false;
            foreach(Produccion pro in grama)
            {
                if (prim)
                {
                    for (int i = 0; i < pro.cuerpo.Count; i++)
                    {
                        aux = pro.encabezado + "→" + pro.cuerpo[i].Replace(" ", "");
                        if (aux.Equals(cad))
                        {
                            ind = pro.num + i;
                            return ind;
                        }
                    }
                }
                prim = true;
            }
            return ind;
        }

        private String cambiaOriginal(String sim)
        {
            switch (sim)
            {
                case "P":
                    return("Programa");
                    break;
                case "S":
                    return("secuencia-sent");
                    break;
                case "T":
                    return("sentencia");
                    break;
                case "I":
                    return("sent-if");
                    break;
                case "R":
                    return ("sent-repeat");
                    break;
                case "A":
                    return ("sent-assign");
                    break;
                case "D":
                    return ("sent-read");
                    break;
                case "W":
                    return ("sent-write");
                    break;
                case "X":
                    return ("exp");
                    break;
                case "O":
                    return ("op-comp");
                    break;
                case "M":
                    return ("exp-simple");
                    break;
                case "U":
                    return ("opsuma");
                    break;
                case "E":
                    return ("term");
                    break;
                case "L":
                    return ("opmult");
                    break;
                case "F":
                    return ("factor");
                    break;
                case "i":
                    return ("if");
                    break;
                case "t":
                    return ("then");
                    break;
                case "e":
                    return ("end");
                    break;
                case "l":
                    return ("else");
                    break;
                case "r":
                    return ("repeat");
                    break;
                case "u":
                    return ("until");
                    break;
                case "d":
                    return ("identificador");
                    break;
                case "a":
                    return ("read");
                    break;
                case "w":
                    return ("write");
                    break;
                case "n":
                    return ("numero");
                    break;
                case ",":
                    return (":=");
                    break;
                default:
                    return (sim);
                    break;
            }
        }

        private Produccion encuentraEncabezado(String prod, List<Produccion> grama)
        {
            Produccion res = null;
            String cad = prod.Replace(".", "");
            String aux;
            foreach (Produccion pro in grama)
            {

                for (int i = 0; i < pro.cuerpo.Count; i++)
                {
                    aux = pro.encabezado + "→" + pro.cuerpo[i].Replace(" ", "");
                    if (aux.Equals(cad))
                    {
                        res = pro;
                        return pro;
                    }
                }
            }
            return res;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            
        }

        private void button6_Click(object sender, EventArgs e)
        {

            treeView1.Nodes.Clear();
           
            listBoxErrores.Items.Clear(); //limpiar lista de errores
            String[] evaluado = cadenaEvaluacion();
            if (evaluado.Contains("Error"))
            {
               // MessageBox.Show("se tiene un error lexico no se puede continuar");
            }
            else
            {
                Arbol estructura;
                LR0 lr0 = new LR0();

                obtieneTablaAFDCanonica(lr0.automata, lr0.original);
                foreach (Conjunto act in lr0.automata)
                {
                    listBox2.Items.Add("Estado " + act.nombre.ToString());
                    foreach (String ele in act.elementos)
                    {
                        String convert = "";
                        foreach (char c in ele)
                        {
                            convert += cambiaOriginal(c.ToString()) + " ";
                        }
                        listBox2.Items.Add(convert);
                    }
                }
                generaTablaAnalisis(lr0.automata, lr0.original);
                estructura = analizaCadena(evaluado, lr0);
                if (estructura != null)
                {
                    TreeNode nodito = treeView1.Nodes.Add(estructura.raiz.nombre);


                    recorreArbol(estructura.raiz, nodito);
                }
            }
            treeView1.ExpandAll(); //expandir arbol
        }

        private void recorreArbol(Nodo iterador, TreeNode nodoView)
        {
          
            foreach(Arista ar in iterador.hijas) 
            {
                TreeNode nodito = nodoView.Nodes.Add(ar.hijo.nombre);
                recorreArbol(ar.hijo,nodito);
                
            }
        }

        private Arbol analizaCadena(String[] cadena, LR0 lr)
        {
            int insertado;
            int pos = 0;
            List<Nodo> acomodo = new List<Nodo>();
            List<Nodo> tope = new List<Nodo>();
            int beta;
            Arbol arbol = new Arbol();
            int auxiliar = -1;
            List<int> pila = new List<int>();
            pila.Add(0);
            String a = cadena[0];
            int s;
            String encabezado="";
            String []cuerpo = new string[0];
            int num;
            int t;
            for(int i = 0; i<cadena.Length-1; i++)
            {
                Nodo nuevo = new Nodo(cadena[i]);
                nuevo.superpapa.padre = nuevo;
                nuevo.superhijas.Add(nuevo);
                arbol.Add(nuevo);
                tope.Add(nuevo);
            }

            while (true)
            {
                s = pila[pila.Count - 1];
                int columna = ubColum(a);
                if (dataGridView4.Rows[s].Cells[ubColum(a)].Value != null)
                {
                    String aux = dataGridView4.Rows[s].Cells[ubColum(a)].Value.ToString();
                    if (aux.Contains("d"))
                    {
                        insertado = int.Parse(aux.Substring(1));
                        pila.Add(insertado);
                        auxiliar++;
                        pos++;
                        a = cadena[pos];
                    }
                    else
                    {
                        if (aux.Contains("r"))
                        {
                            bool band = false;
                            insertado = int.Parse(aux.Substring(1));
                            bool prim = false;
                            foreach (Produccion gram in lr.gramatica)
                            {
                                if (prim)
                                {
                                    num = gram.num;
                                    for (int i = 0; i < gram.cuerpo.Count; i++)
                                    {
                                        if (i + num == insertado)
                                        {
                                            encabezado = gram.encabezado;
                                            cuerpo = gram.cuerpo[i].Split(' ');

                                            band = true;
                                            break;
                                        }

                                    }
                                    if (band)
                                        break;
                                }
                                prim = true;
                            }

                            for (int i = 0; i < cuerpo.Length; i++)
                            {
                                pila.RemoveAt(pila.Count - 1);
                            }
                            t = pila[pila.Count - 1];
                            int irA = int.Parse(dataGridView4.Rows[t].Cells[ubColum(cambiaOriginal(encabezado))].Value.ToString());
                            pila.Add(irA);
                            Nodo papa = new Nodo(cambiaOriginal(encabezado));
                            int reducciones = 0;
                            
                            while (reducciones<cuerpo.Length)
                            {

                                if(arbol[auxiliar-reducciones].ancestro.padre == null)
                                {

                                    
                                    arbol[auxiliar - reducciones].ancestro.padre = papa;
                                    Arista nueva = new Arista();
                                    nueva.padre = papa;
                                    nueva.hijo = arbol[auxiliar - reducciones];
                                    papa.hijas.Add(nueva);
                                    papa.superhijas.Add(nueva.hijo);
                                    arbol[auxiliar - reducciones].superpapa.padre = papa;
                                    reducciones++;
                                }
                                else
                                {
                                    acomodo.Clear();
                                    for (int i = 0; i < cadena.Length - 1; i++)
                                    {

                                        if (acomodo.IndexOf(arbol[i].superpapa.padre)== -1)
                                        {
                                            acomodo.Add(arbol[i].superpapa.padre);

                                        }
                                    }
                                    Arista nueva = new Arista();
                                    nueva.padre = papa;
                                    int actual = acomodo.FindIndex(x => x.Equals(arbol[auxiliar].superpapa.padre));
                                    nueva.hijo = acomodo[actual - reducciones];
                                    papa.hijas.Add(nueva);
                                    nueva.hijo.ancestro.padre = papa;
                                    
                                    reducciones++;
                                }
                            }
                            foreach (Arista hijo in papa.hijas)
                            {
                                foreach(Nodo nodito in hijo.hijo.superhijas)
                                {
                                    if (papa.superhijas.Equals(hijo) == false)
                                    {
                                        papa.superhijas.Add(nodito);
                                        nodito.superpapa.padre = papa;
                                    }
                                }
                            }
                            arbol.Add(papa);
                            arbol.raiz = papa;
                            acomodo.Clear();
                        }
                        else
                        {
                            if (aux.Contains("ac"))
                            {
                                //MessageBox.Show("arbol creado con exito");
                                
                                break;
                            }
                           
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Error sintáctico");
                    arbol = null;
                    break;
                }
            }
            return arbol;
        }

        private Nodo encuebtraPadre(Nodo recorredor)
        {
            if(recorredor.ancestro.padre == null)
            {
                //MessageBox.Show(recorredor.nombre);
                return recorredor;
            }
            else
            {
                //MessageBox.Show("Soy el nodo " + recorredor.nombre+" y mi padre es "+recorredor.ancestro.padre.nombre);
                return encuebtraPadre(recorredor.ancestro.padre);
            }
        }
        private int ubColum(String str)
        {
            for(int i = 0; i<dataGridView4.Columns.Count; i++)
            {
                //MessageBox.Show(dataGridView4.Columns[i].HeaderText);
                if (dataGridView4.Columns[i].HeaderText.Equals(str))
                    return i;
            }
            return -1;
        }
    }
}
