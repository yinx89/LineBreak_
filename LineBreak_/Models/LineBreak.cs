using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LineBreak_.Models
{
    public class LineBreak
    {
        public string Texto { get; set; }
        public string Numero { get; set; }
        public List<string> Resultado { get; set; }

        public LineBreak()
        {
            Texto = String.Empty;
            Numero = String.Empty;
            Resultado = new List<string> { };
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("Bienvenido a LineBreak, inserte su nombre y pulse ENTER.\n");
            string nombre = Console.ReadLine();
            Console.WriteLine("\nHola {0}, el funcionamiento del programa es el siguiente: \nXXXXXXX \nheee \nheeeee \nXXXXXXXXXX\n", nombre);
            Console.WriteLine();

            Console.WriteLine("Por favor, inserte el Texto que desea utilizar:");
            string texto = Console.ReadLine();
            while (texto == "" || texto == null)
            {
                Console.WriteLine("Por favor, inserte un texto.");
                texto = Console.ReadLine();
            };

            int numero_entero = 0;
            Console.WriteLine("\nPor favor, ahora inserte el número que desea utilizar:");
            string numero = Console.ReadLine();
            while (!int.TryParse(numero, out numero_entero) || (numero_entero * numero_entero) - numero_entero < texto.Length)
            {
                Console.WriteLine("Se permiten {0} caracteres", (numero_entero * numero_entero) - numero_entero);
                Console.WriteLine("Se tienen {0} caracteres", texto.Length);
                Console.WriteLine("No está permitido este número, inserte un entero.");
                numero = Console.ReadLine();
            };

            Console.WriteLine("\nPerfecto.");

            char[] delimitador = { ' ', '\t' };
            string[] palabras = texto.Split(delimitador);
            System.Console.WriteLine("Hay {0} palabras en el texto,", palabras.Length);
            System.Console.WriteLine("se permiten hasta como máximo {0} caracteres,", (numero_entero * numero_entero) - numero_entero);
            System.Console.WriteLine("y el texto tiene {0} caracteres totales.", texto.Length);
            System.Console.WriteLine("\nEstas son las palabras que componen su texto:", texto.Length);
            foreach (string palabra in palabras)
            {
                Console.WriteLine(palabra);
            }


            int windex = 0; // inicializa el puntero que irá pasando por cada palabra
            string[] final = new string[numero_entero]; // guarda las lineas finales en las que se ha separado el texto
            for (int i = 0; i < numero_entero; i++) // itera y va aumentando la linea entre 0 y numero_entero
            {
                int linelength = 0; // longitud acumulada de la linea en ese momento, ahora acaba de comenzar y está a 0
                for (int j = windex; j < palabras.Length; j++) //itera en cada una de las palabras en cada linea hasta que la longitud supera linelenght, entonces break
                {
                    if ((numero_entero - linelength) < palabras[j].Length) // comprueba si hay espacio para la palabra restando el numero_entero y el acumulador
                    {
                        if (palabras[j].Length > 3)
                        {
                            char[] delimitador2 = { ' ' };
                            string palabra_silabas = Silabas(palabras[j]);
                            string[] palabras2 = palabra_silabas.Split(delimitador2);
                            List<string> syllables2 = new List<string>();
                            foreach (string silaba in palabras2)
                            {
                                syllables2.Add(silaba);
                            }
                            //List<string> syllables2 = palabras2;
                            string palabra = palabras[j];
                            palabras[j] = "";
                            Boolean primera = false;
                            Boolean nocabe = false;
                            for (int k = 0; k < syllables2.Count; k++)
                            {
                                if ((syllables2[k].Length + 1 <= (numero_entero - linelength)) && nocabe == false)
                                {
                                    if (primera == false)
                                    {
                                        if ((linelength + syllables2[k].Length + 1) <= (numero_entero - linelength))
                                        {
                                            final[i] = string.Concat(final[i], syllables2[k]);
                                            linelength = linelength + syllables2[k].Length;
                                            primera = true;
                                        }
                                        else
                                        {
                                            final[i] = string.Concat(final[i], ' ', syllables2[k]);
                                            linelength = linelength + syllables2[k].Length + 1;
                                            primera = true;
                                        }

                                    }
                                    else
                                    {
                                        final[i] = string.Concat(final[i], syllables2[k]);
                                        linelength = linelength + syllables2[k].Length;
                                    }

                                }
                                else
                                {
                                    nocabe = true;
                                    palabras[j] = string.Concat(palabras[j], syllables2[k]);
                                }
                                if ((syllables2.Count - 1 == k) && primera == true)
                                {
                                    final[i] = string.Concat(final[i], '-');
                                }
                            }
                            windex = j;
                            break;
                        }
                        else
                        {
                            windex = j;
                            break;
                        }
                    }
                    else
                    {


                        if (linelength == 0)
                        {
                            final[i] = string.Concat(final[i], palabras[j]);
                            linelength = linelength + palabras[j].Length;
                        }
                        else
                        {
                            final[i] = string.Concat(final[i], ' ', palabras[j]);
                            linelength = linelength + palabras[j].Length + 1;
                        }
                        windex = j + 1;
                    }
                }
                Console.WriteLine(final[i]);
            }
            Console.ReadLine();
        }
        public static List<string> Separa(string texto, string numero)
        {
            List<string> final = new List<string> { }; // guarda las lineas finales en las que se ha separado el texto
            if (texto == "" || texto == null || numero=="" || numero==null)
            {
                if(texto == "" || texto == null)
                {
                    if (numero=="" || numero==null)
                    {
                        final.Add("No hay texto ni número");
                    }
                    else
                    {
                        final.Add("No hay texto");
                    }
                }
                else
                {
                    final.Add("No hay número");
                }
                return final;
            }

            int numero_entero = 0;
            if (!int.TryParse(numero, out numero_entero) || (numero_entero * numero_entero) - numero_entero < texto.Length)
            {
                string permiten = ((numero_entero * numero_entero) - numero_entero).ToString();
                final.Add("No está permitido el número.");
                final.Add(String.Concat("Se permiten ", permiten, " caracteres y se tienen ", texto.Length.ToString()));
                return final;
            }
            char[] delimitador = { ' ', '\t' };
            string[] palabras = texto.Split(delimitador);
            String fila;

            int windex = 0; // inicializa el puntero que irá pasando por cada palabra
            
            for (int i = 0; i < numero_entero; i++) // itera y va aumentando la linea entre 0 y numero_entero
            {
                fila = "";
                int linelength = 0; // longitud acumulada de la linea en ese momento, ahora acaba de comenzar y está a 0
                for (int j = windex; j < palabras.Length; j++) //itera en cada una de las palabras en cada linea hasta que la longitud supera linelenght, entonces break
                {
                    if ((numero_entero - linelength) < palabras[j].Length) // comprueba si hay espacio para la palabra restando el numero_entero y el acumulador
                    {
                        if (palabras[j].Length > 3)
                        {
                            char[] delimitador2 = { ' ' };
                            string palabra_silabas = Silabas(palabras[j]);
                            string[] palabras2 = palabra_silabas.Split(delimitador2);
                            List<string> syllables2 = new List<string>();
                            foreach (string silaba in palabras2)
                            {
                                syllables2.Add(silaba);
                            }
                            //List<string> syllables2 = palabras2;
                            string palabra = palabras[j];
                            palabras[j] = "";
                            Boolean primera = false;
                            Boolean nocabe = false;
                            for (int k = 0; k < syllables2.Count; k++)
                            {
                                if ((syllables2[k].Length + 1 <= (numero_entero - linelength)) && nocabe == false)
                                {
                                    if (primera == false)
                                    {
                                        if ((linelength + syllables2[k].Length + 1) <= (numero_entero - linelength))
                                        {
                                            fila=(string.Concat(fila, syllables2[k]));
                                            linelength = linelength + syllables2[k].Length;
                                            primera = true;
                                        }
                                        else
                                        {
                                            fila=(string.Concat(fila, ' ', syllables2[k]));
                                            linelength = linelength + syllables2[k].Length + 1;
                                            primera = true;
                                        }

                                    }
                                    else
                                    {
                                        fila=(string.Concat(fila, syllables2[k]));
                                        linelength = linelength + syllables2[k].Length;
                                    }

                                }
                                else
                                {
                                    nocabe = true;
                                    palabras[j] = string.Concat(palabras[j], syllables2[k]);
                                }
                                if ((syllables2.Count - 1 == k) && primera == true)
                                {
                                    fila=(string.Concat(fila, '-'));
                                }
                            }
                            windex = j;
                            break;
                        }
                        else
                        {
                            windex = j;
                            break;
                        }
                    }
                    else
                    {


                        if (linelength == 0)
                        {
                            fila=string.Concat(fila, palabras[j]);
                            linelength = linelength + palabras[j].Length;
                        }
                        else
                        {
                            fila=(string.Concat(fila, ' ', palabras[j]));
                            linelength = linelength + palabras[j].Length + 1;
                        }
                        windex = j + 1;
                    }
                }
                final.Add(fila);
            }

                return final;
        }

        public static String Silabas(string palabra)
        {
            int I;
            String sil;
            switch (palabra)
            {
                case "limpiáis":
                    sil = "lim piáis";
                    break;
                case "limpiéis":
                    sil = "lim piéis";
                    break;
                default:
                    switch (palabra.Length)
                    {
                        case 1:
                            sil = palabra;
                            break;
                        case 2:
                            sil = palabra;
                            break;
                        case 3:
                            if (Consonante(LeftRightMid.Mid(palabra, 0, 1)))
                            {
                                if (Consonante(LeftRightMid.Mid(palabra, 1, 1)))
                                {
                                    sil = palabra;
                                }
                                else if (Diptongo(LeftRightMid.Mid(palabra, 1)))
                                {
                                    sil = palabra;
                                }
                                else if (Vocal(LeftRightMid.Mid(palabra, 2)))
                                {
                                    sil = LeftRightMid.Mid(palabra, 0, 2) + " " + LeftRightMid.Mid(palabra, 2);
                                }
                                else
                                {
                                    sil = palabra;
                                }
                            }
                            else
                            {
                                if (Diptongo(LeftRightMid.Mid(palabra, 0, 2)))
                                {
                                    sil = palabra;
                                }
                                else
                                {
                                    sil = LeftRightMid.Mid(palabra, 0, 1) + " " + LeftRightMid.Mid(palabra, 1);
                                }
                            }
                            break;

                        case 4:

                            if (Consonante(LeftRightMid.Mid(palabra, 0, 1)))
                            {
                                if (GrupoC(LeftRightMid.Mid(palabra, 0, 2)))
                                {
                                    if (Diptongo(LeftRightMid.Mid(palabra, 2, 2)))
                                    {
                                        sil = palabra;
                                    }
                                    else
                                    {
                                        if (Vocal(LeftRightMid.Mid(palabra, 3)))
                                        {
                                            sil = LeftRightMid.Mid(palabra, 0, 3) + " " + LeftRightMid.Mid(palabra, 3);
                                        }
                                        else
                                        {
                                            sil = palabra;
                                        }
                                    }
                                }
                                else
                                {
                                    if (Diptongo(LeftRightMid.Mid(palabra, 1, 2)))
                                    {
                                        sil = palabra;
                                    }
                                    else
                                    {
                                        if (palabra.Contains("sub"))
                                        {
                                            sil = LeftRightMid.Mid(palabra, 0, 3) + " " + LeftRightMid.Mid(palabra, 3);
                                        }
                                        else
                                        {
                                            sil = LeftRightMid.Mid(palabra, 0, 2) + " " + LeftRightMid.Mid(palabra, 2);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (Diptongo(LeftRightMid.Mid(palabra, 0, 2)))
                                {
                                    sil = LeftRightMid.Mid(palabra, 0, 2) + " " + LeftRightMid.Mid(palabra, 2);
                                }
                                else
                                {
                                    if (Vocal(LeftRightMid.Mid(palabra, 1, 1)))
                                    {
                                        sil = LeftRightMid.Mid(palabra, 0, 1) + " " + Silabas(LeftRightMid.Mid(palabra, 1));
                                    }
                                    else
                                    {
                                        if (GrupoC(LeftRightMid.Mid(palabra, 1, 2)))
                                        {
                                            sil = LeftRightMid.Mid(palabra, 0, 1) + " " + LeftRightMid.Mid(palabra, 1);
                                        }
                                        else
                                        {
                                            if (Consonante(LeftRightMid.Mid(palabra, 2, 1)))
                                            {
                                                sil = LeftRightMid.Mid(palabra, 0, 2) + " " + LeftRightMid.Mid(palabra, 2);
                                            }
                                            else
                                            {
                                                sil = LeftRightMid.Mid(palabra, 0, 1) + " " + Silabas(LeftRightMid.Mid(palabra, 1));
                                            }
                                        }
                                    }
                                }
                            }
                            break;

                        case 5:

                            if (Consonante(LeftRightMid.Mid(palabra, 0, 1)))
                            {
                                if (GrupoC(LeftRightMid.Mid(palabra, 0, 2)))
                                {
                                    if (Diptongo(LeftRightMid.Mid(palabra, 2, 2)))
                                    {
                                        sil = palabra;
                                    }
                                    else
                                    {
                                        sil = LeftRightMid.Mid(palabra, 0, 3) + " " + LeftRightMid.Mid(palabra, 3);
                                    }
                                }
                                else
                                {
                                    if (Diptongo(LeftRightMid.Mid(palabra, 1, 2)))
                                    {
                                        sil = LeftRightMid.Mid(palabra, 0, 3) + " " + LeftRightMid.Mid(palabra, 3);
                                    }
                                    else
                                    {
                                        switch (Cantidad(palabra, 2))
                                        {
                                            case 1:
                                                sil = LeftRightMid.Mid(palabra, 0, 2) + " " + Silabas(LeftRightMid.Mid(palabra, 2));
                                                break;
                                            case 2:
                                                if (!GrupoC(LeftRightMid.Mid(palabra, 2, 2)))
                                                {
                                                    sil = LeftRightMid.Mid(palabra, 0, 3) + " " + Silabas(LeftRightMid.Mid(palabra, 3));
                                                }
                                                else
                                                {
                                                    if (palabra.Contains("sub"))
                                                    {
                                                        sil = LeftRightMid.Mid(palabra, 0, 3) + " " + Silabas(LeftRightMid.Mid(palabra, 3));
                                                    }
                                                    else
                                                    {
                                                        sil = LeftRightMid.Mid(palabra, 0, 2) + " " + Silabas(LeftRightMid.Mid(palabra, 2));
                                                    }
                                                }
                                                break;
                                            default:
                                                sil = palabra;
                                                break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (Diptongo(LeftRightMid.Mid(palabra, 0, 2)))
                                {
                                    sil = LeftRightMid.Mid(palabra, 0, 1) + Silabas(LeftRightMid.Mid(palabra, 1));
                                }
                                else
                                {
                                    if (Vocal(LeftRightMid.Mid(palabra, 1, 1)))
                                    {
                                        sil = LeftRightMid.Mid(palabra, 0, 1) + " " + Silabas(LeftRightMid.Mid(palabra, 1));
                                    }
                                    else
                                    {
                                        if (GrupoC(LeftRightMid.Mid(palabra, 1, 2)))
                                        {
                                            if (palabra.Contains("ubr"))
                                            {
                                                sil = LeftRightMid.Mid(palabra, 0, 2) + " " + Silabas(LeftRightMid.Mid(palabra, 2));
                                            }
                                            else
                                            {
                                                sil = LeftRightMid.Mid(palabra, 0, 1) + " " + Silabas(LeftRightMid.Mid(palabra, 1));
                                            }
                                        }
                                        else
                                        {
                                            switch (Cantidad(palabra, 1))
                                            {
                                                case 1:
                                                    sil = LeftRightMid.Mid(palabra, 0, 1) + " " + Silabas(LeftRightMid.Mid(palabra, 1));
                                                    break;
                                                case 2:
                                                    sil = LeftRightMid.Mid(palabra, 0, 2) + " " + Silabas(LeftRightMid.Mid(palabra, 2));
                                                    break;
                                                case 3:
                                                    sil = LeftRightMid.Mid(palabra, 0, 3) + " " + Silabas(LeftRightMid.Mid(palabra, 3));
                                                    break;
                                                default:
                                                    sil = palabra;
                                                    break;
                                            }
                                        }
                                    }
                                }


                            }
                            break;

                        default:
                            if (Consonante(LeftRightMid.Mid(palabra, 0, 1)))
                            {
                                if (LeftRightMid.Mid(palabra, 0, 3) == "sub")
                                {
                                    sil = LeftRightMid.Mid(palabra, 0, 3) + " " + Silabas(LeftRightMid.Mid(palabra, 3));
                                }
                                else
                                {
                                    if (GrupoC(LeftRightMid.Mid(palabra, 0, 2)))
                                    {
                                        sil = LeftRightMid.Mid(palabra, 0, 2) + Silabas(LeftRightMid.Mid(palabra, 2));
                                    }
                                    else
                                    {
                                        sil = LeftRightMid.Mid(palabra, 0, 1) + Silabas(LeftRightMid.Mid(palabra, 1));
                                    }
                                }
                            }
                            else
                            {
                                if (Diptongo(LeftRightMid.Mid(palabra, 0, 2)))
                                {
                                    switch (Cantidad(palabra, 2))
                                    {
                                        case 1:
                                            sil = LeftRightMid.Mid(palabra, 0, 2) + " " + Silabas(LeftRightMid.Mid(palabra, 2));
                                            break;
                                        case 2:
                                            if (!GrupoC(LeftRightMid.Mid(palabra, 2, 2)))
                                            {
                                                sil = LeftRightMid.Mid(palabra, 0, 3) + " " + Silabas(LeftRightMid.Mid(palabra, 3));
                                            }
                                            else
                                            {
                                                sil = LeftRightMid.Mid(palabra, 0, 2) + " " + Silabas(LeftRightMid.Mid(palabra, 2));
                                            }
                                            break;
                                        case 3:
                                            if (GrupoC(LeftRightMid.Mid(palabra, 3, 2)))
                                            {
                                                sil = LeftRightMid.Mid(palabra, 0, 3) + " " + Silabas(LeftRightMid.Mid(palabra, 3));
                                            }
                                            else
                                            {
                                                sil = LeftRightMid.Mid(palabra, 0, 4) + " " + Silabas(LeftRightMid.Mid(palabra, 4));
                                            }
                                            break;
                                        case 4:
                                            sil = LeftRightMid.Mid(palabra, 0, 4) + " " + Silabas(LeftRightMid.Mid(palabra, 4));
                                            break;
                                        default:
                                            sil = palabra;
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (Cantidad(palabra, 1))
                                    {
                                        case 1:
                                            sil = LeftRightMid.Mid(palabra, 0, 1) + " " + Silabas(LeftRightMid.Mid(palabra, 1));
                                            break;
                                        case 2:
                                            if (!GrupoC(LeftRightMid.Mid(palabra, 1, 2)))
                                            {
                                                sil = LeftRightMid.Mid(palabra, 0, 2) + " " + Silabas(LeftRightMid.Mid(palabra, 2));
                                            }
                                            else
                                            {
                                                sil = LeftRightMid.Mid(palabra, 0, 1) + " " + Silabas(LeftRightMid.Mid(palabra, 1));
                                            }
                                            break;
                                        case 3:
                                            if (GrupoC(LeftRightMid.Mid(palabra, 2, 2)))
                                            {
                                                sil = LeftRightMid.Mid(palabra, 0, 2) + " " + Silabas(LeftRightMid.Mid(palabra, 2));
                                            }
                                            else
                                            {
                                                sil = LeftRightMid.Mid(palabra, 0, 3) + " " + Silabas(LeftRightMid.Mid(palabra, 3));
                                            }
                                            break;
                                        case 4:
                                            sil = LeftRightMid.Mid(palabra, 0, 3) + " " + Silabas(LeftRightMid.Mid(palabra, 3));
                                            break;
                                        default:
                                            sil = palabra;
                                            break;
                                    }
                                }
                            }
                            break;
                    }
                    break;
            }
            return sil;
        }

        public static Boolean Consonante(string letra)
        {
            Boolean Ctrl;
            String letras;
            Ctrl = false;
            letra = letra.Trim().ToLower();
            letras = "bcdfghjklmnñpqrstvwxyz";
            if (letras.Contains(letra))
            {
                Ctrl = true;
            }
            return Ctrl;
        }

        public static int Cantidad(string palabra, int p)
        {
            int I;
            for (I = p; I < palabra.Length; I++)
            {
                if (Vocal(LeftRightMid.Mid(palabra, I, 1)))
                {
                    break;
                }
            }
            if (I - p == 0)
            {
                I = 1;
            }
            else
            {
                I = I - p;
            }
            return I;
        }

        public static Boolean Diptongo(string Dip)
        {
            Boolean Ctrl;
            int I;
            String[] letras;
            String acentuadas, normal;
            acentuadas = "ü";
            normal = "u";
            if (acentuadas == LeftRightMid.Mid(Dip, 0, 1))
            {
                Dip.Replace(acentuadas, normal);
            }
            if (acentuadas == LeftRightMid.Mid(Dip, 1, 1))
            {
                Dip.Replace(acentuadas, normal);
            }
            Ctrl = false;
            Dip = Dip.Trim().ToLower();
            letras = new string[15] { "ai", "au", "ei", "eu", "oi", "ou", "ia", "ie", "io", "ua", "ue", "uo", "ui", "iu", "ou" };
            foreach (string letra in letras)
            {
                if (letra == Dip)
                {
                    Ctrl = true;
                }
            }
            return Ctrl;
        }

        public static Boolean Vocal(string letra)
        {
            Boolean Ctrl;
            String[] letras;
            String acentuadas, normal;
            acentuadas = "áéíóúü";
            normal = "aeiouu";


            for (int i = 0; i < acentuadas.Length; i++)
            {
                if (letra == LeftRightMid.Mid(acentuadas, i, 1))
                {
                    letra = LeftRightMid.Mid(normal, i, 1);
                }
            }
            Ctrl = false;
            letra = letra.Trim().ToLower();
            letras = new string[5] { "a", "e", "i", "o", "u" };
            if (letras.Contains(letra))
            {
                Ctrl = true;
            }
            return Ctrl;
        }

        public static Boolean GrupoC(string grupo)
        {
            Boolean Ctrl;
            int I;
            String[] letras;
            Ctrl = false;
            letras = new string[17] { "br", "bl", "ch", "cl", "cr", "dr", "fr", "fl", "gr", "gl", "ll", "pr", "rr", "tr", "tl", "pr", "pl" };
            foreach (string letra in letras)
            {
                if (letra == grupo)
                {
                    Ctrl = true;
                }
            }
            return Ctrl;
        }
    }

    public class LeftRightMid
    {
        public static string Left(string param, int length)
        {

            string result = param.Substring(0, length);
            return result;
        }

        public static string Right(string param, int length)
        {

            int value = param.Length - length;
            string result = param.Substring(value, length);
            return result;
        }

        public static string Mid(string param, int startIndex, int length)
        {
            string result = param.Substring(startIndex, length);
            return result;
        }

        public static string Mid(string param, int startIndex)
        {
            string result = param.Substring(startIndex);
            return result;
        }
    }
}