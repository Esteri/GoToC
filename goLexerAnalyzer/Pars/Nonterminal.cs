using System;
using System.Collections.Generic;

namespace goLexerAnalyzer
{
    public class Nonterminal : Symbol
    {
        private static Dictionary<TokenType, Nonterminal> _nonterminals = new Dictionary<TokenType, Nonterminal>();
        private TokenType _name;

        public override string Name
        {
            get
            {
                return _name.ToString();
            }
        }

        public override TokenType Type
        {
            get { return _name; }
        }

        public Nonterminal(TokenType name)
        {
            _name = name;
        }

        public static Nonterminal Of(TokenType v)
        {
            Nonterminal nonterminal;
            if (!_nonterminals.TryGetValue(v, out nonterminal))
            {
                nonterminal = new Nonterminal(v);
                _nonterminals[v] = nonterminal;
            }
            return nonterminal;
        }

        public override bool IsNonterminal
        {
            get { return true; }
        }
    }
}