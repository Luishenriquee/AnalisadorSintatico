using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AnalisadorSintatico
{
    public class AnalisadorLexico
    {
        static Dictionary<string, string> PalavraReservada = new Dictionary<string, string>()
        {
            { "for", "1" },
            { "in", "2" },
            { "range", "3" }
        };

        static Dictionary<char, string[]> CaractereEspecial = new Dictionary<char, string[]>()
        {
            { '(', new string[] { "4", "ABRE PARENT." } },
            { ')', new string[] { "5", "FECHA PARENT." } }
        };

        public void ValidarToken(List<string> tokens)
        {
            try
            {
                foreach (string token in tokens)
                {
                    //TODO: Validar Tokens
                    //TODO: Verificar se é palavra reservada, caractere especial ou Identificador
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("ERRO: " + ex.Message);
            }
        }

        public static bool VerificarIdentificador(string palavra)
        {
            string identificadorRegex = @"^[a-zA-Z_]\w*$";

            return Regex.IsMatch(palavra, identificadorRegex);
        }
    }
}