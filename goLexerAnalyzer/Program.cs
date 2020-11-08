using System;
using System.IO;
using System.Collections.Generic;

namespace goLexerAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Too few arguments, usage: Programm.exe <filepath>");
                Console.WriteLine("other arguments are ignored.");
                return;
            } 
            else if (!File.Exists(args[0]))
            {
                Console.WriteLine("File doesn't exist.");
                return;
            }

            //Lexer lex = new Lexer(args[0]);
            LexicalAnalyzer lex = new LexicalAnalyzer();
            List<Token> tokens = lex.Parse(args[0]);

            foreach ( Token tkn in tokens)
            {
                Console.WriteLine(tkn.ToString());
            }

            lex = null;
        }
    }
}
