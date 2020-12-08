using System;
using System.Collections.Generic;

namespace goLexerAnalyzer
{
	public class EarleyParser
	{
		Nonterminal axiom;
		Grammar g;
		State[] S;

		public EarleyParser(Grammar g)
		{
			this.axiom = g.getAxiom();
			if (axiom == null) throw new ArgumentNullException();

			this.g = g;
		}

		private State[] Init(int k)
        {
            State[] S = new State[k + 1];
			for(int i = 0; i <= k; i++) {
				S[i] = new State();
            }
			return S;

        }

		public List<object> Parse(List<Token> tokens)
        {
			S = Init(tokens.Count);

			// в I0 добавили продукции аксиомы с точкой в позиции 0
			List<Production> axiomProductions = g.GetRulesForNt(axiom);
			foreach (Production prod in axiomProductions) {
				S[0].Add(new Item(prod, 0, 0));
            }

				// теперь надо пройти по итемам в I0 и записать правила для нетерминалов
			int k = 0;
			do {
				for(int i = 0; i < S[k].Count; i++) {
					Item item = S[k][i];
					if(!item.IsComplete()) {
						if (item.NextWord is Terminal) {
							if (k != tokens.Count)
								Scanner(item, tokens[k], k + 1);
							//S[k + 1].Print();
						} else if (item.NextWord is Nonterminal) {
							// разрастание дерева когда точка на конечной позиции
							//if (k == 0)
							//	Predictor(S[0], S[0]);
							Predictor(item, k, k);
							//S[k].Print();
						}
					} else {
						// сопоставление нетерминала
						Completer(item, k);

                    }
				}
				Console.Out.WriteLine("Chain " + k);
				Console.Out.WriteLine("---------------------------------------------");
				S[k].Print();
				Console.Out.Write("\n\n\n\n");
				k++;
			} while(k <= tokens.Count);
			return new List<object>();
        }

		private void Predictor(Item item, int stateNumber, int k)
        {
			Nonterminal nt = item.NextWord as Nonterminal;

			foreach(Production p in g.GetRulesForNt(nt)) {
				//int pos = item.Pos;
				Item newItem = new Item(p, 0, stateNumber);
				if(!S[k].Contains(newItem)) {
					S[k].Add(newItem);
					//Console.Out.WriteLine("Predicted from nonterminal in S[k+1]");
				}
			}
		}

        private void Scanner(Item item, Token nextToken, int k)
        {
			Terminal t = item.NextWord as Terminal;
			//TokenType type = t.Type;
			TokenType type;
			if(nextToken.Type == TokenType.Identifier)
				type = TakeConcreteIdentifier(nextToken.Lexem);
			else type = nextToken.Type;

			if(type == t.Type) {
				S[k].Add(item.Increment());
				//Console.Out.WriteLine("Scanned terminal in S[k+1]");
            }

		}

		private void Completer(Item item, int k)
		{
			int SearchNumber = item.Table;
			foreach(Item someItem in S[SearchNumber]) {
				if(item.Production.LeftSide == someItem.NextWord) {
					S[k].Add(someItem.Increment());
				}
			}
			//	Item newItem = state.Import(parentItem.NextItem);
			//	newItem.Add(item);
			//}
		}

		private TokenType TakeConcreteIdentifier(string token)
		{
			TokenType type;
			switch (token) {
				case "for": type = TokenType.IdentifierFor; break;
				case "switch": type = TokenType.IdentifierSwitch; break;
				case "case": type = TokenType.IdentifierCase; break;
				case "default": type = TokenType.IdentifierDefault; break;
				case "else": type = TokenType.IdentifierElse; break;
				case "if": type = TokenType.IdentifierIf; break;
				case "func": type = TokenType.IdentifierFunc; break;
				case "return": type = TokenType.IdentifierReturn; break;
				case "int": type = TokenType.IdentifierType; break;
				case "bool": type = TokenType.IdentifierType; break;
				case "string": type = TokenType.IdentifierType; break;
				case "float": type = TokenType.IdentifierType; break;
				case "var": type = TokenType.IdentifierVar; break;
				case "const": type = TokenType.IdentifierConst; break;
				default: type = TokenType.Identifier; break;
            }
		return type;
		}

		/*
		 * private void Completer(State state, Item item)
			//IDictionary<Production, IList<Item>> completedNullable)
		{

			if (item.Parent == state)
			{
				// completed a nullable item

				if (!completedNullable.ContainsKey(item.Production))
				{
					completedNullable[item.Production] = new List<Item>();
				}

				completedNullable[item.Production].Add(item);
			}

			foreach (Item parentItem in item.Parent.GetItems(item.Production))
			{
				Item newItem = state.Import(parentItem.NextItem);
				newItem.Add(item);
				ShiftCompletedNullable(state, newItem, completedNullable);
			}
		}
		private IList<object> Parse(List<Token> tokens)
		{
			List<State> S = Init(tokens.Count);

			int k = 0;
			S[0].Add(new Item(startProduction, 0));
			do {

				for(int i = 0; i < S[k].Count; i++) {
					Item item = S[k][i];

					if(!item.IsComplete()) {
						if(item.NextWord is Nonterminal) {
							Predictor(S[k], i, item);
						} else if(item.NextWord is Terminal) {
							Scanner(item, next, tokens[i + 1]);
						}
					} else {
						Completer(S[k], item);
					}
				}

				k++;
			}
			while (tokens[k] != null && S[k].Count > 0);

			k--;
			if (S[k].Count == 1 &&
				S[k][0].IsComplete() &&
				S[k][0].Production == startProduction &&
				S[k][0].Parent == S[0][0])
			{
				return S[k][0].Reduce();
			}
			else
			{
				return new List<object>();
			}
		}

		private void Predictor(State state, int pos, Item item)
			//IDictionary<Production, IList<Item>> completedNullable)
		{
			Nonterminal nt = item.NextWord as Nonterminal;

			foreach (Production p in g.GetRulesForNt(nt))
			{
				Console.Out.Write(p.ToString() + "\t");
				Item newItem = new Item(p, state);

				if (!state.Contains(newItem))
				{
					state.Add(newItem);
					//ShiftCompletedNullable(state, newItem, completedNullable);
				}
			}
		}

		private void Scanner(Item item, State next, Token token)
		{

			Terminal t = item.Symbol as Terminal;

			if (t.Contains(ch))
			{
				Item newItem = item.NextItem;
				newItem.Add(token);
				next.Add(newItem);
			}
		}

		*/
	}
}
