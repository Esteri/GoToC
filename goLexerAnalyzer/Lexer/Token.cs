using System;
using System.Collections.Generic;
using System.Text;

namespace goLexerAnalyzer
{
    public class Token
    {
        private TokenType type;
        private string lexem;

        public Token(TokenType tt, string lex)
        {
            lexem = lex;
            this.type = tt;
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
