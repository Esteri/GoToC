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
            
            foreach (Token tkn in tokens)
            {
                Console.WriteLine(tkn.ToString());
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
            string rules = synt.Parse(tokens);
            Console.WriteLine(rules);

     // Cеманитческий анализатор
            List<int> rulesForSemer = new List<int>();
            foreach (string rule in rules.Split())
            {
                if (rule!="")
                    rulesForSemer.Add(Convert.ToInt32(rule));
            }
            rulesForSemer.Reverse();
            SemAnalizer semer = new SemAnalizer(tokens, rulesForSemer);
            semer.getCountOfCurlyBrackets();

            Console.WriteLine("----------------Semer----------------");
            semer.getCountOfCurlyBrackets();
            semer.getFuncCalls();
            semer.adChecks();
            //semer.GetFinalTokens();
            semer.chekTypes();

            List<int> sRules = new List<int>();
            if (sRules != null)
            {
                string[] subsRules = rules.Split(' ');
                foreach (string s in subsRules)
                {
                    if (!s.Equals(""))
                    {
                        sRules.Add(Int32.Parse(s));
                        //Console.Out.WriteLine(s);
                    }
                }
            }
            else
            {
                Console.Out.WriteLine("rules == null");
                return;
            }

            Transform t = new Transform();
            List<int> normalized_rules = t.transform(sRules);

            foreach (int e in normalized_rules)
            {
                Console.Out.WriteLine(e);
            }

            Generator gen = new Generator();
            string cppSrc = gen.GenCppSource(tokens, normalized_rules);

            Console.Out.WriteLine("\n //--------- CPP SRC FOLLOWS ---------------\n");
            Console.Out.WriteLine(cppSrc);


            //lex = null;
            Console.ReadKey();
        }
    }
}
