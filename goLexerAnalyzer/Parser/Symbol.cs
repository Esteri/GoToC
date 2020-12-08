
using System.IO;

namespace goLexerAnalyzer
{
    public abstract class Symbol
    {
        public Symbol() { }

        public abstract string Name
        {
            get;
        }

        public abstract TokenType Type
        {
            get;
        }

        public abstract bool IsNonterminal
        {
            get;
        }

        public bool IsTerminal
        {
            get { return !IsNonterminal; }
        }
    }
}