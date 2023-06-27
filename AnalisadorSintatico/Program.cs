using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AnalisadorSintatico
{
    class Program
    {
        //BUSCAR O ARQUIVO PELO DIRETÓRIO E NOME E CHAMAR O MÉTODO PARA VALIDAR TOKENS
        static void Main()
        {
            string nomeArquivo = "Arquivo.txt";
            string caminhoArquivo = @"C:\Users\solda\OneDrive\Área de Trabalho\Analisador";

            string expressao = ConsultarArquivo(nomeArquivo, caminhoArquivo);

            if (expressao is null)
                Console.WriteLine("Arquivo não encontrado.");
            else
            {
                List<string> tokens = SepararExpressaoEmTokens(expressao);
                Analisador analisadorLexico = new Analisador();
                analisadorLexico.ValidarToken(tokens);
            }
        }

        //VERIFICAR SE O ARQUIVO EXISTE NO DIRETÓRIO INFORMADO
        static string ConsultarArquivo(string nomeArquivo, string caminhoArquivo)
        {
            string caminhoCompleto = Path.Combine(caminhoArquivo, nomeArquivo);

            if (File.Exists(caminhoCompleto))
                return File.ReadAllText(caminhoCompleto);
            else
                return null;
        }

        //SEPARA O EXPRESSÃO EM PALAVRAS(TOKENS) E MONTAR UMA LISTA DE PALAVRAS
        static List<string> SepararExpressaoEmTokens(string expressao)
        {
            List<string> novosTokens = new List<string>();

            string novaExpressao = expressao.Replace("(", " ( ");
            novaExpressao = novaExpressao.Replace(",", " , ");
            novaExpressao = novaExpressao.Replace(")", " ) ");
            novaExpressao = novaExpressao.Replace("+", " + ");
            novaExpressao = novaExpressao.Replace("-", " - ");
            novaExpressao = novaExpressao.Replace("*", " * ");
            novaExpressao = novaExpressao.Replace("/", " / ");

            List<string> tokens = novaExpressao.Split(' ').ToList();

            foreach (string token in tokens)
            {
                if (!string.IsNullOrEmpty(token))
                    novosTokens.Add(token);
            }

            return novosTokens;
        }
    }
}