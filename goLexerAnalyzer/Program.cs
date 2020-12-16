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

            int i = 0;
            foreach ( Token tkn in tokens)
            {
                Console.WriteLine(i++ + "\t" + tkn.ToString());
            }

            Grammar g = new Grammar();
            //g.Print();
            /*Console.Out.WriteLine("Rules for <Declarations>");
            List<Production> rules = g.GetRulesForNt(Nonterminal.Of(TokenType.Declarations));
            foreach (Production rule in rules) {
                Console.Out.WriteLine(rule.ToString());
            }
            Console.Out.WriteLine();

            Console.Out.WriteLine("Rules for <Operand6lvl>");
            rules = g.GetRulesForNt(Nonterminal.Of(TokenType.Operand6lvl));
            foreach(Production rule in rules) {
                Console.Out.WriteLine(rule.ToString());
            }*/

            EarleyParser synt = new EarleyParser(g);
            synt.Parse(tokens);
        }
    }
}
