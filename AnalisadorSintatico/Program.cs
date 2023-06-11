using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AnalisadorSintatico
{
    class Program
    {
        static void Main()
        {
            string nomeArquivo = "Arquivo.txt";
            string caminhoArquivo = @"C:\Users\solda\OneDrive\Área de Trabalho\Analisador";

            string expressao = ConsultarArquivo(nomeArquivo, caminhoArquivo);

            if (expressao is null)
                Console.WriteLine("Arquivo não encontrado.");
            else
            {
                List<string> palavras = SepararExpressaoEmPalavra(expressao);
                AnalisadorLexico analisadorLexico = new AnalisadorLexico();
                analisadorLexico.ValidarToken(palavras);
            }
        }

        static string ConsultarArquivo(string nomeArquivo, string caminhoArquivo)
        {
            string caminhoCompleto = Path.Combine(caminhoArquivo, nomeArquivo);

            if (File.Exists(caminhoCompleto))
                return File.ReadAllText(caminhoCompleto);
            else
                return null;
        }

        static List<string> SepararExpressaoEmPalavra(string expressao)
        {
            List<string> novasPalavras = new List<string>();
            List<string> palavras = new List<string>();

            string novaExpressao = expressao.Replace("(", " ( ");
            novaExpressao = novaExpressao.Replace(",", " , ");
            novaExpressao = novaExpressao.Replace(")", " ) ");

            palavras = novaExpressao.Split(' ').ToList();

            foreach (string palavra in palavras)
            {
                if (!string.IsNullOrEmpty(palavra))
                    novasPalavras.Add(palavra);
            }

            return novasPalavras;
        }
    }
}