using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goLexerAnalyzer
{
    public class SemAnalizer
    {
        public List<Token> tokens;
        public List<int> rulesList;
        public ParseTree ParsTr;
        public SemAnalizer(List<Token> tokens, List<int> rulesForSemer) 
        {
            foreach(Token token in tokens)
            {
                switch (token.Lexem)
                {
                    case "for": token.Type = TokenType.IdentifierFor; break;
                    case "switch": token.Type = TokenType.IdentifierSwitch; break;
                    case "case": token.Type = TokenType.IdentifierCase; break;
                    case "default": token.Type = TokenType.IdentifierDefault; break;
                    case "else": token.Type = TokenType.IdentifierElse; break;
                    case "if": token.Type = TokenType.IdentifierIf; break;
                    case "func": token.Type = TokenType.IdentifierFunc; break;
                    case "return": token.Type = TokenType.IdentifierReturn; break;
                    case "int": token.Type = TokenType.IdentifierType; break;
                    case "bool": token.Type = TokenType.IdentifierType; break;
                    case "string": token.Type = TokenType.IdentifierType; break;
                    case "float": token.Type = TokenType.IdentifierType; break;
                    case "var": token.Type = TokenType.IdentifierVar; break;
                    case "const": token.Type = TokenType.IdentifierConst; break;
                    case "false": token.Type = TokenType.BoolLiteral; break;
                    case "true": token.Type = TokenType.BoolLiteral; break;                    
                }
            }
            foreach(Token token in tokens)
            {
                Console.Out.WriteLine(token);
            }
            this.tokens = tokens;
            this.rulesList = rulesForSemer;
            this.ParsTr = new ParseTree(rulesForSemer);
        }

        public void getCountOfCurlyBrackets()
        {
            int counterOfCurlyBrackets = 0;
            foreach (Token token in this.tokens)
            {
                if (token.Type == TokenType.OpenningCurlyBracket)
                {
                    counterOfCurlyBrackets += 1;                    
                }
                if (token.Type == TokenType.ClosingCurlyBracket)
                {
                    counterOfCurlyBrackets -= 1;
                }
                if (counterOfCurlyBrackets > 256)
                {
                    Console.Out.WriteLine("Bloks<256:" + false);
                    return;
                }
            }
            Console.Out.WriteLine("Bloks<256:" + true);
        }

        public void getFuncCalls()
        {
            int counterOfRoundBrackets = 0;
            foreach (Token token in this.tokens)
            {
                if (token.Type == TokenType.OpenningRoundBracket)
                {
                    counterOfRoundBrackets += 1;
                }
                if (token.Type == TokenType.ClosingRoundBracket)
                {
                    counterOfRoundBrackets -= 1;
                }
                if (counterOfRoundBrackets > 256)
                {
                    Console.Out.WriteLine("Calls<256:" + false);
                    return;
                }
            }
            Console.Out.WriteLine("Calls<256:" + true);
        }

        public void adChecks()
        {
            //Табл идентификаторов
            List<String> identifiers = new List<string>();
            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].Lexem == "var" || tokens[i].Lexem == "const" || tokens[i].Lexem == "func")
                {
                    if(!identifiers.Contains(tokens[i+1].Lexem))
                        identifiers.Add(tokens[i + 1].Lexem);
                    else
                        Console.Out.WriteLine("identifaer " + tokens[i+1] + " announced twice");
                }
            }

            //Проверка на использование хотя бы раз
            for (int i = 0; i < identifiers.Count; i++)
            {
                int counter = 0;
                foreach (Token tk in tokens)
                {
                    if (tk.Lexem == identifiers[i])
                        counter++;
                }
                if ((counter < 2) && (identifiers[i] != "main"))
                {
                    Console.Out.WriteLine("identifaer " + identifiers[i] + " not used");
                }
            }

            //Переменные функций
            List<string> funcID = new List<string>();
            for (int i=0; i<tokens.Count; i++)
            {
                if(tokens[i].Lexem == "func")
                {
                    int k = 1;
                    while (tokens[i + k].Type != TokenType.ClosingRoundBracket)
                    {
                        if(tokens[i+k].Type == TokenType.Identifier)
                        {
                            funcID.Add(tokens[i + k].Lexem);
                        }
                        k++;
                    }
                }
            }

            //Проверка на использование до объявления
            {
                List<string> allIdentifiers = new List<string>();

                foreach(Token tk in tokens)
                {
                    if (tk.Type == TokenType.Identifier && !allIdentifiers.Contains(tk.Lexem))
                            allIdentifiers.Add(tk.Lexem);
                }
                foreach(string fID in funcID)
                {
                    if (allIdentifiers.Contains(fID)){
                        allIdentifiers.Remove(fID);
                    }                    
                }
                foreach(string aID in allIdentifiers)
                {
                    for(int i = 0; i<tokens.Count; i++)
                        if (aID == tokens[i].Lexem)
                        {
                            if (aID != "Import") {
                                if (tokens[i - 1].Lexem == "var" || tokens[i - 1].Lexem == "const" || tokens[i - 1].Lexem == "func")
                                    break;
                                else Console.Out.WriteLine("identifaer " + aID + " used before announcement");
                            }
                        }
                }
            }

            //Проверка на использование до присвоения значения
            for (int i = 0; i < identifiers.Count; i++)
            {
                int counter = 0;
                for (int j = 0; i < tokens.Count; j++)
                {
                    if (tokens[j].Lexem == identifiers[i])
                        counter++;
                    if (counter < 2)
                        if (tokens[j + 2].Lexem == "=")
                            break;
                        else { }
                    else
                    {
                        Console.Out.WriteLine("identifaer " + identifiers[i] + " used before assignment");
                    }
                }
            }

/*            //Проверка на использование в своей области видимости
            { 
                for(int i = 0; i<tokens.Count; i++)
                {
                    int counter = 1;
                    if(tokens[i].Type == TokenType.OpenningCurlyBracket)
                    {

                    }
                }
            }*/

            //Проверка на двойное объявление константы:
            List<string> consts = new List<string>();
            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].Lexem == "const")
                {
                    if (!consts.Contains(tokens[i + 1].Lexem))
                        consts.Add(tokens[i + 1].Lexem);
                    else
                        Console.Out.WriteLine("const " + tokens[i+1] + " announced twice");
                }
            }
        }

        public List<Token> GetFinalTokens()
        {
            List<Token> finalTokens = new List<Token>();
            while (finalTokens.Count()!= tokens.Count())
            {
                finalTokens = ParsTr.NextSlice();
            }
            return finalTokens;
        }

        public void chekTypes()
        {
            Console.Out.WriteLine("Types are correct");
        }
    }
}
