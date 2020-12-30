using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goLexerAnalyzer
{
    public class Funcs
    {
        public List<string> names = new List<string>();
        public List<string> types = new List<string>();
    }

    public class IDs
    {
        public List<string> names = new List<string>();
        public List<string> types = new List<string>();
    }

    public class FuncIDs
    {
        public List<string> names = new List<string>();
        public List<string> types = new List<string>();
    }

    public class SemAnalizer
    {
        public List<Token> tokens;
        public List<int> rulesList;
        public ParseTree ParsTr;
        public IDs ids = new IDs();
        public Funcs funcs = new Funcs();
        public FuncIDs funcIDs = new FuncIDs();
        public SemAnalizer(List<Token> tokens, List<int> rulesForSemer) 
        {
            foreach(Token token in tokens)
            {
                switch (token.Lexem)
                {
                    case "for": token.type = TokenType.IdentifierFor; break;
                    case "switch": token.type = TokenType.IdentifierSwitch; break;
                    case "case": token.type = TokenType.IdentifierCase; break;
                    case "default": token.type = TokenType.IdentifierDefault; break;
                    case "else": token.type = TokenType.IdentifierElse; break;
                    case "if": token.type = TokenType.IdentifierIf; break;
                    case "func": token.type = TokenType.IdentifierFunc; break;
                    case "return": token.type = TokenType.IdentifierReturn; break;
                    case "int": token.type = TokenType.IdentifierType; break;
                    case "bool": token.type = TokenType.IdentifierType; break;
                    case "string": token.type = TokenType.IdentifierType; break;
                    case "float": token.type = TokenType.IdentifierType; break;
                    case "var": token.type = TokenType.IdentifierVar; break;
                    case "const": token.type = TokenType.IdentifierConst; break;
                    case "false": token.type = TokenType.BoolLiteral; break;
                    case "true": token.type = TokenType.BoolLiteral; break;                    
                }
            }
            foreach(Token token in tokens)
            {
                Console.Out.WriteLine(token);
            }
            this.tokens = tokens;
            this.rulesList = rulesForSemer;
            this.ParsTr = new ParseTree(rulesForSemer);
            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].Lexem == "var" || tokens[i].Lexem == "const")
                {
                    if (ids.names != null)
                    {
                        if (!ids.names.Contains(tokens[i + 1].Lexem))
                        {
                            ids.names.Add(tokens[i + 1].Lexem);
                            ids.types.Add(tokens[i + 2].Lexem);
                        }
                    }
                    else
                    {
                        ids.names.Add(tokens[i + 1].Lexem);
                        ids.types.Add(tokens[i + 2].Lexem);
                    }
                }
                if (tokens[i].Lexem == "func" && tokens[i+1].Lexem != "main")
                {
                    funcs.names.Add(tokens[i + 1].Lexem);
                    int k = 1;
                    while (tokens[i + k].Type != TokenType.ClosingRoundBracket)
                    {
                        k++;
                    }
                    if (tokens[i + k].Lexem == "bool" || tokens[i + k].Lexem == "int" || tokens[i + k].Lexem == "float" || tokens[i + k].Lexem == "string")
                    {
                        funcs.names.Add(tokens[i + k].Lexem);
                    }
                    if (tokens[i + k].Type == TokenType.OpenningCurlyBracket)
                    {
                        funcs.types.Add("void");
                    }
                }
                {
                    if (tokens[i].Lexem == "func")
                    {
                        int k = 2;
                        while (tokens[i + k].Type != TokenType.ClosingRoundBracket)
                        {
                            if (tokens[i + k].Type == TokenType.Identifier)
                            {
                                funcIDs.names.Add(tokens[i + k].Lexem);
                                funcIDs.types.Add(tokens[i + k + 1].Lexem);
                            }
                            k++;
                        }
                    }
                }
            }
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

            //Проверка на переприсваивание значения константе:
            List<string> consts = new List<string>();
            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].Lexem == "const")
                {
                    if (!consts.Contains(tokens[i + 1].Lexem))
                        consts.Add(tokens[i + 1].Lexem);
                }
            }
            foreach(string c in consts)
            {
                int counter = 0;
                for(int i = 0; i<tokens.Count; i++)
                {
                    if(c == tokens[i].Lexem && tokens[i+1].Lexem == "=")
                    {
                        counter++;
                    }
                    if (counter > 1)
                    {
                        Console.Out.WriteLine("const " + c + " assigned a value twice");
                    }
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

        public void getExpr()
        {
            List<List<Token>> expressions = new List<List<Token>>();
            List<List<Token>> memory = new List<List<Token>>();
            List<Token> expression = new List<Token>();

            for(int i = 0; i<tokens.Count; i++)
            {
                if (funcs.names.Contains(tokens[i].Lexem))
                {
                    int k = 0;
                    while (tokens[i + k].Type != TokenType.ClosingRoundBracket)
                    {
                        expression.Add(tokens[i + k]);
                        k++;
                    }
                    i = i + k;
                }
                if (tokens[i].Type == TokenType.Addition || tokens[i].Type == TokenType.Multiplication || tokens[i].Type == TokenType.Comparison)                     
                {
                    expression.Add(tokens[i]);
                }
                if (ids.names.Contains(tokens[i].Lexem))
                {
                    expression.Add(tokens[i]);
                }
                if (funcIDs.names.Contains(tokens[i].Lexem))
                {
                    expression.Add(tokens[i]);
                }
                if (tokens[i].Type == TokenType.OpenningRoundBracket|| tokens[i].Type == TokenType.ClosingRoundBracket)
                {
                    expression.Add(tokens[i]);
                }
                if (tokens[i].Type == TokenType.BoolLiteral || tokens[i].Type == TokenType.IntLiteral || tokens[i].Type == TokenType.StringLiteral || tokens[i].Type == TokenType.FloatLiteral)
                {
                    expression.Add(tokens[i]);
                }
                if ((tokens[i].Type!= TokenType.Addition) && (tokens[i].Type != TokenType.Multiplication) && (tokens[i].Type !=  TokenType.Comparison) && (tokens[i].Type != TokenType.ClosingRoundBracket) && (tokens[i].Type != TokenType.OpenningRoundBracket) && (!funcs.names.Contains(tokens[i].Lexem)) && (!funcIDs.names.Contains(tokens[i].Lexem)) && (!ids.names.Contains(tokens[i].Lexem)) && (tokens[i].Type != TokenType.BoolLiteral) && (tokens[i].Type != TokenType.IntLiteral) && (tokens[i].Type != TokenType.StringLiteral) && (tokens[i].Type != TokenType.FloatLiteral))
                {
                    bool flag = false;
                    if (expression.Count == 1) expression.Clear();
                    else
                    {                        
                        for (int j = 0; j<expression.Count; j++)
                        {
                            if (expression[j].Type == TokenType.Addition || expression[j].Type == TokenType.Multiplication || expression[j].Type == TokenType.Comparison)
                                flag = true;   
                        }
                        if(flag)
                        {
                            expressions.Add(expression.GetRange(0, expression.Count));
                            memory.Add(expression.GetRange(0, expression.Count));
                            expression.Clear();
                        }
                        else
                        {
                            expression.Clear();
                        }
                    }
                }
            }

            //свёртка выражений

            /*foreach (List<Token> expr in expressions)
            {
                int i = 0;
                while (expr[i].Type != TokenType.ClosingRoundBracket)
                    i++;
                while (expr[i].Type != TokenType.OpenningRoundBracket)
                {
                    if(expr[i].Type == TokenType.Identifier)
                    {
                        if (funcs.names.Contains(expr[i].Lexem)){
                            int j = funcs.names.IndexOf(expr[i].Lexem);
                            if (funcs.types[j] == "int")
                                expr[i] = new Token(TokenType.IntLiteral, "");
                            if (funcs.types[j] == "float")
                                expr[i] = new Token(TokenType.FloatLiteral, "");
                            if (funcs.types[j] == "bool")
                                expr[i] = new Token(TokenType.BoolLiteral, "");
                            if (funcs.types[j] == "string")
                                expr[i] = new Token(TokenType.StringLiteral, "");
                        }

                        if (ids.names.Contains(expr[i].Lexem))
                        {
                            int j = ids.names.IndexOf(expr[i].Lexem);
                            if (ids.types[j] == "int")
                                expr[i] = new Token(TokenType.IntLiteral, "");
                            if (ids.types[j] == "float")
                                expr[i] = new Token(TokenType.FloatLiteral, "");
                            if (ids.types[j] == "bool")
                                expr[i] = new Token(TokenType.BoolLiteral, "");
                            if (ids.types[j] == "string")
                                expr[i] = new Token(TokenType.StringLiteral, "");
                        }

                        if (funcIDs.names.Contains(expr[i].Lexem))
                        {
                            int j = ids.names.IndexOf(expr[i].Lexem);
                            if (funcIDs.types[j] == "int")
                                expr[i] = new Token(TokenType.IntLiteral, "");
                            if (funcIDs.types[j] == "float")
                                expr[i] = new Token(TokenType.FloatLiteral, "");
                            if (funcIDs.types[j] == "bool")
                                expr[i] = new Token(TokenType.BoolLiteral, "");
                            if (funcIDs.types[j] == "string")
                                expr[i] = new Token(TokenType.StringLiteral, "");
                        }

                        if(tokens[i+1].Type == TokenType.OpenningRoundBracket)
                        {
                            while (tokens[i + 1].Type != TokenType.ClosingRoundBracket)
                                tokens.RemoveAt(i + 1);
                            tokens.RemoveAt(i + 1);
                        }
                    }
                    i--;
                }
            }*/
        }
    }
}
