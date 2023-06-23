using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AnalisadorSintatico
{
    public class Analisador
    {
        //REPRESENTA A PALAVRAS RESERVADAS DA EXPRESSÃO 
        static Dictionary<string, string> palavraReservada = new Dictionary<string, string>()
        {
            { "for", "1" },
            { "in", "2" },
            { "range", "3" }
        };

        //REPRESENTA OS CARACTERES ESPECIAIS DA EXPRESSÃO
        static Dictionary<char, string[]> caractereEspecial = new Dictionary<char, string[]>()
        {
            { '(', new string[] { "4", "ABRE PARENT." } },
            { ')', new string[] { "5", "FECHA PARENT." } },
            { ',', new string[] { "6", "VIRGULA." } }
        };

        //VAI SER SALVO OS TOKENS DA EXPRESSÃO
        static List<Tuple<string, string>> tokens = new List<Tuple<string, string>>();

        public void ValidarToken(List<string> tokens)
        {
            try
            {
                Console.WriteLine(" == ANALISE LEXICA == \n");

                foreach (string token in tokens)
                    RotularToken(token);

                Mostrar();
                Console.WriteLine(" \n == FIM DA ANALISE LEXICA == \n");

                AnalisadorSintatico();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRO: " + ex.Message);
            }
        }

        //MOSTRAR NA TELA TIPO DO TOKEN E SUA DESCRIÇÃO
        static void Mostrar()
        {
            foreach (var token in tokens)
                Console.WriteLine("Token: " + token.Item1 + " | Descrição: " + token.Item2);
        }

        //MÉTODO PRINCIPAL QUE VAI VERIFICAR SE UM TOKEN É IDENTIFICADOR, CARACTERE ESPECIAL, PALAVRA RESERVADA,
        //SE É DO TIPO NÚMERIOCO
        public static void RotularToken(string token)
        {
            if (VerificarIdentificador(token))
            {
                tokens.Add(new Tuple<string, string>(token, "IDENTIFICADOR"));
            }
            else if (VerificarCaractereEspecial(token))
            {
                tokens.Add(new Tuple<string, string>(token, "CARACTERES ESPECIAIS"));
            }
            else if (VerificarPalavraReservada(token))
            {
                tokens.Add(new Tuple<string, string>(token, "PALAVRA RESERVADA"));
            }
            else if (VerificarNumero(token))
            {
                tokens.Add(new Tuple<string, string>(token, "TIPO NUMERICO"));
            }
            else if (VerificarOperador(token))
            {
                tokens.Add(new Tuple<string, string>(token, "TIPO OPERADOR"));
            }
            else
            {
                tokens.Add(new Tuple<string, string>(token, "NÃO RECONHECIDO"));
            }
        }

        //VERIFICAR SE UM TOKEN É UM IDENTIFICADOR
        public static bool VerificarIdentificador(string palavra)
        {
            string identificadorRegex = @"^[a-zA-Z_]\w*$";
            return Regex.IsMatch(palavra, identificadorRegex) && !VerificarPalavraReservada(palavra);
        }

        //VERIFICAR SE UM TOKEN É UMA PALOAVRA RESERVADA
        public static bool VerificarPalavraReservada(string palavra)
        {
            return palavraReservada.ContainsKey(palavra);
        }

        //VERIFICAR SE UM TOKEN É UM CARACTERE ESPECIAL
        public static bool VerificarCaractereEspecial(string palavra)
        {
            char[] caracteres = palavra.ToCharArray();
            bool vereficar = false;

            if (caracteres.Length == 1)
                vereficar = caractereEspecial.ContainsKey(caracteres[0]);

            return vereficar;
        }

        //VERIFICAR SE O TOKEN É UM NÚMERO
        public static bool VerificarNumero(string palavra)
        {
            string identificadorRegex = @"^-?\d+(\.\d+)?$";
            return Regex.IsMatch(palavra, identificadorRegex);
        }

        //VERIFICAR SE O TOKEN É UM OPERADOR
        public static bool VerificarOperador(string palavra)
        {
            string identificadorRegex = @"[+\-/*]";

            return Regex.IsMatch(palavra, identificadorRegex);
        }

        public static void AnalisadorSintatico()
        {
            Console.WriteLine("\n == ANALISE SINTATICA == \n");

            For();
        }

        public static void For()
        {
            Console.WriteLine("Entrou no <for>");

            if (tokens[0].Item1 == "for")
                Identificador();
            else
                Error();
        }

        public static void Identificador()
        {
            Console.WriteLine("Entrou no <Identificador>");

            if (VerificarIdentificador(tokens[1].Item1))
                In();
            else
                Error();
        }

        public static void In()
        {
            Console.WriteLine("Entrou no <IN>");

            if (tokens[2].Item1 == "in")
                Range();
            else
                Error();
        }

        public static void Range()
        {
            Console.WriteLine("Entrou no <RANGE>");

            if (tokens[3].Item1 == "range")
                ParemEsq();
            else
                Error();
        }

        public static void ParemEsq()
        {
            Console.WriteLine("Entrou no <PAREM_ESQ>");

            if (tokens[4].Item1 == "(")
                ValidarNumero();
        }

        public static void ValidarNumero()
        {
            Console.WriteLine("Entrou no <NUMERAL>");

            int cont = QtdTokens();

            string item5 = tokens[5].Item1;
            string item7 = tokens[7].Item1;
            string aux = tokens[cont - 2].Item1;

            if ((item5 == "1" && aux == "1") ||
                (item5 == "2" && aux == "2") ||
                (item5 == "3" && aux == "3"))
            {
                ParemDir();
            }
            else if ((item5 == "1" && item7 == "2" && aux == "2") ||
                     (item5 == "1" && item7 == "3" && aux == "3") ||
                     (item5 == "2" && item7 == "3" && aux == "3"))
            {
                ParemDir();
            }
            else if (item5 == "1" && item7 == "2" && tokens[9].Item1 == "3" && aux == "3")
            {
                ParemDir();
            }
            else if ((item5 == "1" || item5 == "2") && aux == ",")
            {
                Virgula();
            }
            else if (item5 == "1" && item7 == "2" && aux == ",")
            {
                Virgula();
            }
            else
            {
                Error();
            }
        }

        public static void Virgula()
        {
            Console.WriteLine("Entrou no <VIRGULA>");

            int cont = QtdTokens();

            if (tokens[6].Item1 == "," && tokens[cont - 2].Item1 == "2")
                ValidarNumero();
            else
                Error();
        }

        public static void ParemDir()
        {
            Console.WriteLine("Entrou no <PAREM_DIR>");

            int cont = QtdTokens();

            if (tokens[cont - 1].Item1 == ")")
                Console.WriteLine("Fim da analisie sintatica");
        }

        public static void Error()
        {
            Console.WriteLine("Um erro foi detectado\n");
            Environment.Exit(0);
        }

        public static int QtdTokens()
        {
            return tokens.Count;
        }
    }
}