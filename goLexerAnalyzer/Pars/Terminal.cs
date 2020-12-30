using System;
using System.Collections.Generic;

namespace goLexerAnalyzer
{
    internal class Terminal : Symbol
	{
		public static readonly Terminal Eof = new Terminal(TokenType.EndOfFile);

		private static Dictionary<TokenType, Terminal> _terminals = new Dictionary<TokenType, Terminal>();
    
		private TokenType _name;

		public Terminal(TokenType v)
		{
			_name = v;
		}

		public static Terminal Of(TokenType v)
		{
			Terminal terminal;
			if (!_terminals.TryGetValue(v, out terminal))
			{
				terminal = new Terminal(v);
				_terminals[v] = terminal;
			}
			return terminal;
		}

		public override string Name
		{
			get { return _name.ToString(); }
		}

		public override TokenType Type
		{
			get { return _name; }
		}

		public override bool IsNonterminal
		{
			get { return false; }
		}
	}
}