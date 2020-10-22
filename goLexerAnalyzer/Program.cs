using System;

namespace goLexerAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Lexer lex = new Lexer();
            lex.Parse("filepath");
        }
    }
}
