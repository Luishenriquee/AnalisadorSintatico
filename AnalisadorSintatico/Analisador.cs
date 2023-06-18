﻿using System;
using System.Collections.Generic;
using System.Linq;
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
                foreach (string token in tokens)
                {
                    RotularToken(token);
                }

                Mostrar();

                bool validacao = Validacao(tokens);
                
                if(validacao)
                    Console.WriteLine("Finalizada Análise sem Erros.");
                else
                    Console.WriteLine("Ocorreu um erro na Análise.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRO: " + ex.Message);
            }
        }

        //MOSTRAR NA TELA TIPO DO TOKEN E SUA DESCRIÇÃO
        static void Mostrar()
        {
            foreach (var token in tokens.Select((value, index) => new { Value = value, Index = index }))
                Console.WriteLine("Token: " + token.Value.Item1 + " | Descrição: " + token.Value.Item2);
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
            bool verificar = false;

            if (Regex.IsMatch(palavra, identificadorRegex) == true && !VerificarPalavraReservada(palavra) == true)
                verificar = Regex.IsMatch(palavra, identificadorRegex);

            return verificar;
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

        public static bool Validacao(List<string> tokens) 
        {
            foreach (var token in tokens.Select((value, index) => new { Value = value, Index = index }))
            {
                bool valid = AnalisadorSintatico();

                if (!valid)
                    return false;                
            }

            return true;
        }

        public static bool AnalisadorSintatico()
        {
            if (
                    tokens[0].Item1 == "for" && 
                    tokens[2].Item1 == "in" &&
                    tokens[3].Item1 == "range" &&
                    tokens[4].Item1 == "(" &&
                    tokens[5].Item1 == "1" &&
                    tokens[6].Item1 == "," &&
                    tokens[7].Item1 == "2" &&
                    tokens[8].Item1 == "," &&
                    tokens[9].Item1 == "3" &&
                    tokens[10].Item1 == ")" 
                )
                return true;

            return false;
        }
    }
}