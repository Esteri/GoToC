#define TOKEN_DEBUG

using System;
using System.Collections.Generic;
using System.Text;

namespace goLexerAnalyzer
{
    public class Token
    {
        public TokenType type;
        public string lexem;

        public Token(TokenType tt, string lex)
        {
            lexem = lex;
            this.type = tt;
#if TOKEN_DEBUG
            Console.WriteLine("dbg: Token Created " + this.ToString());
#endif
        }

        public override string ToString()
        {
            return "<" + type.ToString() + ", \"" + lexem + "\">"; 
        }

        public TokenType Type
        {
            get {
                return type;
            }
        }

        public string Lexem
        {
            get {
                return lexem;
            }
        }
    }
}
